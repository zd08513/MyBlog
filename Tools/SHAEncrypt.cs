using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// 哈希加密类
    /// </summary>
    public class SHAEncrypt
    {
        /// <summary>
        /// 16字节,128位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] byteArr = MD5.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        /// <summary>
        /// 20字节,160位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA128(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] byteArr = SHA1.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        /// <summary>
        /// 32字节,256位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA256(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider();
            byte[] byteArr = SHA256.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }


        /// <summary>
        /// 48字节,384位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA384(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA384CryptoServiceProvider SHA384 = new SHA384CryptoServiceProvider();
            byte[] byteArr = SHA384.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }

        /// <summary>
        /// 64字节,512位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA512(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            SHA512CryptoServiceProvider SHA512 = new SHA512CryptoServiceProvider();
            byte[] byteArr = SHA512.ComputeHash(buffer);
            return BitConverter.ToString(byteArr);
        }
    }
}
