using MyBlog.Entity.Entity;
using MyBlog.IDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MyBlog.DAL
{
    /// <summary>
    /// 实现用户数据接口
    /// </summary>
    public class UserInfoDAL : IUserInfoDAL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserInfo Login(string username, string password)
        {
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter { ParameterName = "@username", Value = username, DbType = DbType.String });
            arrParameter.Add(new SqlParameter { ParameterName = "@userpassword", Value = password, DbType = DbType.String });
            arrParameter.Add(new SqlParameter { ParameterName = "@result", DbType = DbType.Byte,Direction=ParameterDirection.Output });
            DataTable dtResult = SqlHelper.ExecuteDataSetProducts("myblogs_loginAndQry", arrParameter.ToArray()).Tables[0];

            //获取登录结果（1：成功，0：失败）
            int resultCount = Functions.ToConvert<int>(arrParameter.FirstOrDefault(p => p.ParameterName == "@result").SqlValue.ToString());
            if (resultCount == 1)
            {
                UserInfo info = new UserInfo
                {
                    UserId = Functions.ToConvert<string>(dtResult.Rows[0]["user_id"]),
                    UserName = Functions.ToConvert<string>(dtResult.Rows[0]["user_name"]),
                    UserPassword = Functions.ToConvert<string>(dtResult.Rows[0]["user_password"]),
                    CreateTime = Functions.ToConvert<DateTime>(dtResult.Rows[0]["create_time"]),
                    LoginTime = Functions.ToConvert<DateTime>(dtResult.Rows[0]["login_time"])
                };
                return info;
            }
            return null;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool Add(UserInfo userinfo)
        {
            IList<SqlParameter> arrParameter = new List<SqlParameter>();
            arrParameter.Add(new SqlParameter { ParameterName = "@userid", Value = userinfo.UserId, DbType = DbType.String });
            arrParameter.Add(new SqlParameter { ParameterName = "@username", Value = userinfo.UserName, DbType = DbType.String });
            arrParameter.Add(new SqlParameter { ParameterName = "@password", Value = userinfo.UserPassword, DbType = DbType.String });
            arrParameter.Add(new SqlParameter { ParameterName = "@createtime", Value = userinfo.CreateTime, DbType = DbType.DateTime });
            return SqlHelper.ExecteNonQueryText("INSERT INTO UserInfo (user_id,user_name,user_password,create_time) VALUES(@userid,@username,@password,@createtime)", arrParameter.ToArray()) > 0;
        }
    }
}
