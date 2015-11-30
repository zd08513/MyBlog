using MyBlog.Entity;
using MyBlog.Entity.SearchInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MyBlogs.DAL
{
    /// <summary>
    /// 工资数据类
    /// </summary>
    public class SalaryInfoDAL
    {
        public static bool SalaryInfoSave(SalaryInfo info)
        {
            string sql = "INSERT INTO SalaryInfo(id,money,send_date,createtime)VALUES(@id,@money,@send_date,@createtime)";
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter { ParameterName = "@id", SqlValue = info.Id });
            arrParameter.Add(new SqlParameter { ParameterName = "@money", SqlValue = info.Money });
            arrParameter.Add(new SqlParameter { ParameterName = "@send_date", SqlValue = info.SendDate });
            arrParameter.Add(new SqlParameter { ParameterName = "@createtime", SqlValue = info.CreateTime });
            return SqlHelper.ExecteNonQuery(CommandType.Text, sql, arrParameter.ToArray()) > 0;
        }

        public static bool SalaryInfoUpdate(SalaryInfo info)
        {
            string sql = "UPDATE SalaryInfo SET money='@money',send_date='@send_date' WHERE id='@id'";
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter { ParameterName = "@id", Value = info.Id });
            arrParameter.Add(new SqlParameter { ParameterName = "@money", Value = info.Money });
            arrParameter.Add(new SqlParameter { ParameterName = "@send_date", Value = info.SendDate });
            arrParameter.Add(new SqlParameter { ParameterName = "@createtime", Value = info.CreateTime });
            return SqlHelper.ExecteNonQuery(CommandType.Text, sql, arrParameter.ToArray()) > 0;
        }

        public static DataTable SalaryInfo(SalaryInfoSearchInfo searchInfo)
        {
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter { ParameterName = "@page_index", Value = searchInfo.PageIndex });
            arrParameter.Add(new SqlParameter { ParameterName = "@page_size", Value = searchInfo.PageSize });
            arrParameter.Add(new SqlParameter { ParameterName = "@rows_count", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int });
            arrParameter.Add(new SqlParameter { ParameterName = "@salary_sum", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Money });
            DataTable dtResult = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "SalaryInfo_qry_sp", arrParameter.ToArray()).Tables[0];
            searchInfo.RowsCount = Functions.ToConvert<int>(arrParameter.FirstOrDefault(p => p.ParameterName == "@rows_count").SqlValue.ToString());
            searchInfo.SalarySum = Functions.ToConvert<double>(arrParameter.FirstOrDefault(p => p.ParameterName == "@salary_sum").SqlValue.ToString());
            return dtResult;
        }


        public static DataTable Details(string id)
        {
            return SqlHelper.ExecuteDataSet(CommandType.Text, "SELECT * FROM SalaryInfo WHERE id=@id", new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar,Size=36 }).Tables[0];
        }
    }
}
