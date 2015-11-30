using MyBlog.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;

namespace MyBlog.Website.Extensions
{
    /// <summary>
    /// 用户扩展类
    /// </summary>
    public class UserInfoExtensions
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetUserInfo()
        {
            UserInfo userInfo = null;
            //获取加密用户ID
            string userId = CookieHelper.GetCookie("UserId");
            if (string.IsNullOrEmpty(userId)) return userInfo;

            //解密
            userId = DESEncrypt.Decode(userId);
            if (string.IsNullOrEmpty(userId)) return userInfo;

            //获取缓存中用户信息
            userInfo = CacheHelper.GetCache(userId) as UserInfo;

            return userInfo;
        }

        /// <summary>
        /// 设置用户缓存（默认20分钟）
        /// </summary>
        /// <param name="info">用户信息类</param>
        public static void SetUserInfo(UserInfo info)
        {
            //加密用户ID
            string key = DESEncrypt.Encode(info.UserId);

            //将加密数据保存到cookie中
            CookieHelper.SetCookie("UserId", key);

            //缓存用户信息20分钟
            CacheHelper.SetCache(info.UserId, info, TimeSpan.FromMinutes(20));
        }

        /// <summary>
        /// 设置用户缓存
        /// </summary>
        /// <param name="info">用户信息类</param>
        /// <param name="Timeout">缓存时间</param>
        public static void SetUserInfo(UserInfo info, TimeSpan Timeout)
        {
            //加密用户ID
            string key = DESEncrypt.Encode(info.UserId);

            //将加密数据保存到cookie中
            CookieHelper.SetCookie("UserId", key);

            //缓存用户信息20分钟
            CacheHelper.SetCache(info.UserId, info, Timeout);
        }

        /// <summary>
        /// 清除当前用户信息
        /// </summary>
        public static void ClearUserInfo()
        {
            //获取用户信息
            UserInfo model = GetUserInfo();
            //清除cookie
            CookieHelper.ClearCookie("UserId");
            //清除缓存
            CacheHelper.RemoveAllCache(model.UserId);
        }
    }
}