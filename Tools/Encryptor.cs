using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    #region 加密、解密字符串
    /// <summary>
    /// 加密字符串。可以解密
    /// </summary>
    public class Encryptor
    {
        #region 加密，固定密钥
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceData">原文</param>
        /// <returns></returns>
        public static string Encrypt(string sourceData)
        {
            //set key and initialization vector values
            //Byte[] key = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            //Byte[] iv = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            Byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            try
            {
                //convert data to byte array
                Byte[] sourceDataBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(sourceData);
                //get target memory stream
                MemoryStream tempStream = new MemoryStream();
                //get encryptor and encryption stream
                DESCryptoServiceProvider encryptor = new DESCryptoServiceProvider();
                CryptoStream encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                //encrypt data
                encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
                encryptionStream.FlushFinalBlock();

                //put data into byte array
                Byte[] encryptedDataBytes = tempStream.GetBuffer();
                //convert encrypted data into string
                return System.Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 加密，可以设置密钥
        /// <summary>
        /// 可以设置密钥的加密方式
        /// </summary>
        /// <param name="sourceData">原文</param>
        /// <param name="key">密钥，8位数字，字符串方式</param>
        /// <returns></returns>
        public static string Encrypt(string sourceData, string key)
        {
            //set key and initialization vector values
            //Byte[] key = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            //Byte[] iv = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};

            #region 检查密钥是否符合规定
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            #endregion

            char[] tmp = key.ToCharArray();
            Byte[] keys = new byte[8];
            //Byte[] keys = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            //设置密钥
            for (int i = 0; i < 8; i++)
            {
                if (tmp.Length > i)
                {
                    keys =Functions.ToConvert<byte[]>(tmp);
                }
                else
                {
                    keys = Functions.ToConvert<byte[]>(i);
                }
            }

            try
            {
                //convert data to byte array
                Byte[] sourceDataBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(sourceData);
                //get target memory stream
                MemoryStream tempStream = new MemoryStream();
                //get encryptor and encryption stream
                DESCryptoServiceProvider encryptor = new DESCryptoServiceProvider();
                CryptoStream encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(keys, iv), CryptoStreamMode.Write);

                //encrypt data
                encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
                encryptionStream.FlushFinalBlock();

                //put data into byte array
                Byte[] encryptedDataBytes = tempStream.GetBuffer();
                //convert encrypted data into string
                return System.Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 解密，固定密钥
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sourceData">密文</param>
        /// <returns></returns>
        public static string Decrypt(string sourceData)
        {
            //set key and initialization vector values
            //Byte[] key = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            //Byte[] iv = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            Byte[] keys = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            try
            {
                //convert data to byte array
                Byte[] encryptedDataBytes = System.Convert.FromBase64String(sourceData);
                //get source memory stream and fill it 
                MemoryStream tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);
                //get decryptor and decryption stream 
                DESCryptoServiceProvider decryptor = new DESCryptoServiceProvider();
                CryptoStream decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(keys, iv), CryptoStreamMode.Read);

                //decrypt data 
                StreamReader allDataReader = new StreamReader(decryptionStream);

                return allDataReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 解密，固定密钥
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sourceData">密文</param>
        /// <param name="key">密钥，8位数字，字符串方式</param>
        /// <returns></returns>
        public static string Decrypt(string sourceData, string key)
        {
            //set key and initialization vector values
            //Byte[] key = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            //Byte[] iv = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};

            #region 检查密钥是否符合规定
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            #endregion

            char[] tmp = key.ToCharArray();
            Byte[] keys = new byte[8];
            //Byte[] keys = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            //设置密钥
            for (int i = 0; i < 8; i++)
            {
                if (tmp.Length > i)
                {
                    keys = Functions.ToConvert<byte[]>(tmp);
                }
                else
                {
                    keys = Functions.ToConvert<byte[]>(i);
                }
            }


            try
            {
                //convert data to byte array
                Byte[] encryptedDataBytes = System.Convert.FromBase64String(sourceData);
                //get source memory stream and fill it 
                MemoryStream tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);
                //get decryptor and decryption stream 
                DESCryptoServiceProvider decryptor = new DESCryptoServiceProvider();
                CryptoStream decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(keys, iv), CryptoStreamMode.Read);

                //decrypt data 
                StreamReader allDataReader = new StreamReader(decryptionStream);

                return allDataReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        /// <summary>
        /// 对需要加密的字串进行MD5加密(针对密码加密)
        /// </summary>
        /// <param name="ConvertString">需要加密的字串</param>
        /// <returns>加密后的字串</returns>
        /// <remarks>
        /// Modifier: Xiaoliang Ge
        /// Modified Date: 2009-12-26
        /// </remarks>
        public static string MD5EncryptStr(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 8);
            return t2;
        }
    }

    #endregion
}
