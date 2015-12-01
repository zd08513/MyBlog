/// <summary>
/// ��˵�������������ݿ���ʷ�����
/// �������ڣ�2010-4-22
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  Email��[url=mailto:sufei.1013@163.com]sufei.1013@163.com[/url]  Blogs:[url=http://www.sufeinet.com]http://www.sufeinet.com[/url]
/// �޸����ڣ�2013-08-15
/// </summary>
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace Tools
{
    /// <summary>
    /// ���ݿ��ͨ�÷��ʴ��� �շ��޸�
    /// ����Ϊ�����࣬
    /// ������ʵ��������Ӧ��ʱֱ�ӵ��ü���
    /// </summary>
    public abstract class SqlHelper
    {
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MyBlogContext"].ToString();

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
 
        #region //ExecteNonQuery����
        /// <summary>
        ///ִ��һ������Ҫ����ֵ��SqlCommand���ͨ��ָ��ר�õ������ַ�����
        /// ʹ�ò���������ʽ�ṩ�����б� 
        /// </summary>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //ͨ��PrePareCommand����������������뵽SqlCommand�Ĳ���������
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //���SqlCommand�еĲ����б�
                cmd.Parameters.Clear();
                return val;
            }
        }
 
        /// <summary>
        ///ִ��һ������Ҫ����ֵ��SqlCommand���ͨ��ָ��ר�õ������ַ�����
        /// ʹ�ò���������ʽ�ṩ�����б� 
        /// </summary>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(connectionString, cmdType, cmdText, commandParameters);
        }
 
        /// <summary>
        ///�洢����ר��
        /// </summary>
        /// <param name="cmdText">�洢���̵�����</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQueryProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.StoredProcedure, cmdText, commandParameters);
        }
 
        /// <summary>
        ///Sql���ר��
        /// </summary>
        /// <param name="cmdText">T_Sql���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQueryText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
 
        #endregion
 
        #region//GetTable����
 
        /// <summary>
        /// ִ��һ�����ؽ������SqlCommand��ͨ��һ���Ѿ����ڵ����ݿ�����
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <param name="connecttionString">һ�����е����ݿ�����</param>
        /// <param name="cmdTye">SqlCommand��������</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTable(string connecttionString, CommandType cmdTye, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connecttionString))
            {
                PrepareCommand(cmd, conn, null, cmdTye, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            DataTableCollection table = ds.Tables;
            return table;
        }
 
        /// <summary>
        /// ִ��һ�����ؽ������SqlCommand��ͨ��һ���Ѿ����ڵ����ݿ�����
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <param name="cmdTye">SqlCommand��������</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTable(CommandType cmdTye, string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(SqlHelper.connectionString, cmdTye, cmdText, commandParameters);
        }
 
        /// <summary>
        /// �洢����ר��
        /// </summary>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTableProducts(string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(CommandType.StoredProcedure, cmdText, commandParameters);
        }
 
        /// <summary>
        /// Sql���ר��
        /// </summary>
        /// <param name="cmdText"> T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTableText(string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(CommandType.Text, cmdText, commandParameters);
        }
 
        #endregion
 
        /// <summary>
        /// Ϊִ������׼������
        /// </summary>
        /// <param name="cmd">SqlCommand ����</param>
        /// <param name="conn">�Ѿ����ڵ����ݿ�����</param>
        /// <param name="trans">���ݿ����ﴦ��</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">Command text��T-SQL��� ���� Select * from Products</param>
        /// <param name="cmdParms">���ش�����������</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            //�ж����ݿ�����״̬
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //�ж��Ƿ���Ҫ���ﴦ��
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
 
        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
 
        #region//ExecuteDataSet����
 
        /// <summary>
        /// return a dataset
        /// </summary>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }
            }
            catch
            {
                throw;
            }
        }
 
        /// <summary>
        /// ����һ��DataSet
        /// </summary>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, cmdType, cmdText, commandParameters);
        }
 
        /// <summary>
        /// ����һ��DataSet
        /// </summary>
        /// <param name="cmdText">�洢���̵�����</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }
 
        /// <summary>
        /// ����һ��DataSet
        /// </summary>
        /// <param name="cmdText">T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, cmdText, commandParameters);
        }
 
        public static DataView ExecuteDataSet(string connectionString, string sortExpression, string direction, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.Sort = sortExpression + " " + direction;
                    return dv;
                }
            }
            catch
            {
 
                throw;
            }
        }
        #endregion
 
        #region // ExecuteScalar����
 
        /// <summary>
        /// ���ص�һ�еĵ�һ��
        /// </summary>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, cmdType, cmdText, commandParameters);
        }
 
        /// <summary>
        /// ���ص�һ�еĵ�һ�д洢����ר��
        /// </summary>
        /// <param name="cmdText">�洢���̵�����</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������</returns>
        public static object ExecuteScalarProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }
 
        /// <summary>
        /// ���ص�һ�еĵ�һ��Sql���ר��
        /// </summary>
        /// <param name="cmdText">�� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������</returns>
        public static object ExecuteScalarText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, CommandType.Text, cmdText, commandParameters);
        }
 
        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
 
        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
 
        #endregion
 
        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }
 
        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms = (SqlParameter[])((ICloneable)cachedParms).Clone();
            return clonedParms;
        }
 
        /// <summary>
        /// ����Ƿ����
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <param name="cmdParms">����</param>
        /// <returns>bool���</returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, strSql, cmdParms));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// �����������ݵ����ݿ⣨demo��
        /// </summary>
        /// <param name="table">����Դ��</param>
        /// <param name="tableName">���ݿ����</param>
        /// <param name="connStr">���ݿ������ַ���</param>
        /// <param name="mappings">Դ�������ݿ�����ӳ���ϵ</param>
        public static void BulkCopyDataTable(DataTable table, string tableName, string connStr, params SqlBulkCopyColumnMapping[] mappings)
        {
            //ʹ��SqlBulkCopy�������������ݵ����ݿ�
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connStr))
            {
                //д�����ݿ��е�Ŀ�����
                bulkcopy.DestinationTableName = tableName;
                //��������Դ�����ݿ�֮���е�ӳ�䣬����Ҫ��SqlBulkCopy֪����Ҫ����һ�в嵽��һ��
                foreach (SqlBulkCopyColumnMapping mapping in mappings)
                {
                    //Ҳ����ͨ��bulkcopy.ColumnMappings.Add("����Դ����", "���ݱ�����");
                    bulkcopy.ColumnMappings.Add(mapping);
                }
                //�������ύ�����ݿ���
                bulkcopy.WriteToServer(table);
            }
        }

        /// <summary>
        /// ����ִ���б����ݲ�����demo��
        /// </summary>
        /// <param name="table">����Դ</param>
        public static void ExecuteAdapterDataTable(DataTable table)
        {
            //�������ݿ�����
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //�½�һ��SqlDataAdapter����Ϊֻ�����������£�������ʵ����ʱ�Ͳ�����дSelect��SQL���
                using (SqlDataAdapter adapter = new SqlDataAdapter("", conn))
                {
                    //д��������Ҫʹ�õ�SQL���
                    string sqlStr = "update TableName set NeedUpdate = @NewValue where Guid = @Guid";
                    //�½�һ��SqlCommand����adapter��UpdateCommand
                    adapter.UpdateCommand = new SqlCommand(sqlStr, conn);
                    //��UpdateCommand�����ӳ�䣬��������ֱ��ǣ�
                    //SQL�����������ֶ��������͡��ֶγ��ȣ���Ҳ��֪����ɶ��˼������charʲô��Ҫ�õ���������Դ������
                    adapter.UpdateCommand.Parameters.Add("@NewValue", SqlDbType.Bit, 1, "NewValue");
                    adapter.UpdateCommand.Parameters.Add("@Guid", SqlDbType.UniqueIdentifier, 1, "Guid");
                    //������UpdateBatchSizeΪ��1ʱ����Ҫ������ΪNone
                    adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    //����ÿ�ε������������������
                    adapter.UpdateBatchSize = 0;
                    //���и��²�����table������Դ������������ܷ������������ӳ���ϵ����
                    adapter.Update(table);
                }
            }
        }

        /// <summary>
        /// ���������demo��
        /// </summary>
        public static void ExecuteTransaction()
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.Open();
                //����һ������
                SqlTransaction myTran = con.BeginTransaction();
                //Ϊ���񴴽�һ�����ע������ִ��˫�������һ��ִ�е�Ȼ�ɹ���������ִ��һ�Σ�ʧ�ܡ�
                //���������Ǹ�����һ�������һ�����ģ���ʱ������ᱨ�������������ơ�
                SqlCommand myCom = new SqlCommand();
                myCom.Connection = con;
                myCom.Transaction = myTran;
                try
                {
                    myCom.CommandText = "insert into SqlAction values ('����2','111')";
                    myCom.ExecuteNonQuery();
                    myCom.CommandText = "insert into SqlAction values ('����3','111')";
                    myCom.ExecuteNonQuery();
                    myTran.Commit();

                }
                catch (Exception Ex)
                {
                    myTran.Rollback();
                }
            }
        }
    }
}