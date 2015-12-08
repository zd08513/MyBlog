using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Tools;
using MyBlog.Entity;
using MyBlog.IDAL;

namespace MyBlog.DAL
{
    /// <summary>
    /// 应聘公司实现数据层接口
    /// </summary>
    public class CompanieDAL : ICompanieDAL
    {
        /// <summary>
        /// 查询应聘公司列表
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        public DataTable CompanieQry(CompanieSearchInfo searchInfo)
        {
            IList<SqlParameter> arrParameter = new List<SqlParameter>();

            //设置参数
            arrParameter.Add(new SqlParameter("@name", searchInfo.Name));
            arrParameter.Add(new SqlParameter("@user_to_state", searchInfo.UserToState));
            arrParameter.Add(new SqlParameter("@page_index", searchInfo.PageIndex));
            arrParameter.Add(new SqlParameter("@page_size", searchInfo.PageSize));
            arrParameter.Add(new SqlParameter { ParameterName = "@rows_count", Direction = ParameterDirection.Output, SqlDbType=SqlDbType.Int });

            DataTable dtCompanieQry = SqlHelper.GetTableProducts("companies_qry_sp", arrParameter.ToArray())[0];

            searchInfo.RowsCount = Convert.ToInt32(arrParameter.First(p => p.ParameterName == "@rows_count").SqlValue.ToString());

            return dtCompanieQry;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Company CompanieDetail(int Id)
        {
            IList<SqlParameter> arrParameter=new List<SqlParameter>();
            arrParameter.Add(new SqlParameter{ParameterName="Id",SqlValue=Id,SqlDbType=SqlDbType.Int});
            string sql = "SELECT * FROM Companies WHERE Id=@Id";
            DataTable dtCompanie = SqlHelper.GetTable(CommandType.Text, sql, arrParameter.ToArray())[0];
            Company company = new Company();
            foreach (DataRow dr in dtCompanie.Rows)
            {
                company.Id = Functions.ToConvert<int>(dr["Id"]);
                company.Name = Functions.ToConvert<string>(dr["Name"]);
                company.Address = Functions.ToConvert<string>(dr["Address"]);
                company.UserToState = (UserToState)Enum.Parse(typeof(UserToState), Functions.ToConvert<string>(dr["UserToState"]));
                company.WorkToDate = Functions.ToConvert<string>(dr["WorkToDate"]);
                company.WorkToDateString = Functions.ToConvert<string>(dr["WorkToDateString"]);
                company.CreateDate = Functions.ToConvert<DateTime>(dr["CreateDate"]);
            }
            return company;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="comany"></param>
        /// <returns></returns>
        public bool CompanieSave(Company company)
        {
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter{
                ParameterName="Name",
                SqlValue = company.Name
            });
            arrParameter.Add(new SqlParameter{
                ParameterName="Address",
                SqlValue = company.Address
            });
            arrParameter.Add(new SqlParameter{
                SqlValue = company.UserToState,
                ParameterName="UserToState"
            });
            arrParameter.Add(new SqlParameter{
                SqlValue = company.WorkToDate,
                ParameterName="WorkToDate"
            });
            arrParameter.Add(new SqlParameter{
                SqlValue = company.WorkToDateString,
                ParameterName="WorkToDateString"
            });
            arrParameter.Add(new SqlParameter{
                SqlValue = company.CreateDate,
                ParameterName="CreateDate"
            });

            string sql = "INSERT INTO Companies VALUES(@Name,@Address,@UserToState,@WorkToDate,@WorkToDateString,@CreateDate)";
            if (SqlHelper.ExecteNonQueryText(sql, arrParameter.ToArray()) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public bool CompanieUpdate(Company company)
        {
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter
            {
                ParameterName = "Id",
                SqlValue = company.Id
            });
            arrParameter.Add(new SqlParameter
            {
                ParameterName = "Name",
                SqlValue = company.Name
            });
            arrParameter.Add(new SqlParameter
            {
                ParameterName = "Address",
                SqlValue = company.Address
            });
            arrParameter.Add(new SqlParameter
            {
                SqlValue = company.UserToState,
                ParameterName = "UserToState"
            });
            arrParameter.Add(new SqlParameter
            {
                SqlValue = company.WorkToDate,
                ParameterName = "WorkToDate"
            });
            arrParameter.Add(new SqlParameter
            {
                SqlValue = company.WorkToDateString,
                ParameterName = "WorkToDateString"
            });

            string sql = "Update Companies SET Name=@Name,Address=@Address,UserToState=@UserToState,WorkToDate=@WorkToDate,WorkToDateString=@WorkToDateString WHERE Id=@Id";
            if (SqlHelper.ExecteNonQueryText(sql, arrParameter.ToArray()) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool CompanieDelete(int Id)
        {
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter
            {
                ParameterName = "Id",
                SqlValue = Id
            });
            string sql = "DELETE Companies WHERE Id=@Id";
            if (SqlHelper.ExecteNonQueryText(sql, arrParameter.ToArray()) > 0)
                return true;
            else
                return false;
        }
    }
}
