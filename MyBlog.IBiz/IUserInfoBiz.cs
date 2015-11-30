using MyBlog.Entity;
using MyBlog.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IBiz
{
    /// <summary>
    /// 用户业务接口
    /// </summary>
    public interface IUserInfoBiz
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserInfo Login(string username, string password);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        bool Add(UserInfo userinfo);
    }
}