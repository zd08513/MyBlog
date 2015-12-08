using MyBlog.Entity;
using MyBlog.Entity.Entity;
using MyBlog.Factory;
using MyBlog.IBiz;
using MyBlog.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MyBlog.Biz
{
    /// <summary>
    /// 实现用户业务接口
    /// </summary>
    public class UserInfoBiz : IUserInfoBiz
    {
        private static IUserInfoDAL dal = DataBaseFactory.GetUserInfoFactory();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserInfo Login(string username, string password)
        {
            //加密
            password = SHAEncrypt.SHA128(password);
            return dal.Login(username, password);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool Add(UserInfo userinfo)
        {
            userinfo.UserId = Guid.NewGuid().ToString();
            //加密
            userinfo.UserPassword = SHAEncrypt.SHA128(userinfo.UserPassword);
            userinfo.CreateTime = DateTime.Now;
            return dal.Add(userinfo);
        }
    }
}
