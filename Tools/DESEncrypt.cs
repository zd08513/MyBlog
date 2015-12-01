/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：[url=http://www.cckan.net/thread-655-1-1.html]http://www.cckan.net/thread-655-1-1.html[/url]
/// </summary>
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;namespace Tools{  /// <summary>  /// DES加密/解密类。  /// </summary>  public class DESEncrypt  {    public DESEncrypt()    {          }

    public static string _KEY = ConfigurationManager.AppSettings["DESKey"].ToString();  //密钥  
    public static string _IV = ConfigurationManager.AppSettings["DESIV"].ToString();   //向量  

    /// <summary>  
    /// 加密  
    /// </summary>  
    /// <param name="data"></param>  
    /// <returns></returns>  
    public static string Encode(string data)
    {

        byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
        byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);

        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        int i = cryptoProvider.KeySize;
        MemoryStream ms = new MemoryStream();
        CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

        StreamWriter sw = new StreamWriter(cst);
        sw.Write(data);
        sw.Flush();
        cst.FlushFinalBlock();
        sw.Flush();

        string strRet = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        return strRet;
    }

    /// <summary>  
    /// 解密  
    /// </summary>  
    /// <param name="data"></param>  
    /// <returns></returns>  
    public static string Decode(string data)
    {

        byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
        byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);

        byte[] byEnc;

        try
        {
            data.Replace("_%_", "/");
            data.Replace("-%-", "#");
            byEnc = Convert.FromBase64String(data);

        }
        catch
        {
            return null;
        }

        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        MemoryStream ms = new MemoryStream(byEnc);
        CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cst);
        return sr.ReadToEnd();
    }     #region ========加密========           /// <summary>        /// 加密        /// </summary>        /// <param name="Text"></param>        /// <returns></returns>    public static string Encrypt(string Text)     {      return Encrypt(Text,"MATICSOFT");    }    /// <summary>     /// 加密数据     /// </summary>     /// <param name="Text"></param>     /// <param name="sKey"></param>     /// <returns></returns>     public static string Encrypt(string Text,string sKey)     {       DESCryptoServiceProvider des = new DESCryptoServiceProvider();       byte[] inputByteArray;       inputByteArray=Encoding.Default.GetBytes(Text);       des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));       des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));       System.IO.MemoryStream ms=new System.IO.MemoryStream();       CryptoStream cs=new CryptoStream(ms,des.CreateEncryptor(),CryptoStreamMode.Write);       cs.Write(inputByteArray,0,inputByteArray.Length);       cs.FlushFinalBlock();       StringBuilder ret=new StringBuilder();       foreach( byte b in ms.ToArray())       {         ret.AppendFormat("{0:X2}",b);       }       return ret.ToString();     }      #endregion         #region ========解密========               /// <summary>        /// 解密        /// </summary>        /// <param name="Text"></param>        /// <returns></returns>    public static string Decrypt(string Text)     {      return Decrypt(Text,"MATICSOFT");    }    /// <summary>     /// 解密数据     /// </summary>     /// <param name="Text"></param>     /// <param name="sKey"></param>     /// <returns></returns>     public static string Decrypt(string Text,string sKey)     {       DESCryptoServiceProvider des = new DESCryptoServiceProvider();       int len;       len=Text.Length/2;       byte[] inputByteArray = new byte[len];       int x,i;       for(x=0;x<len;x++)       {         i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);         inputByteArray[x]=(byte)i;       }       des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));       des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));       System.IO.MemoryStream ms=new System.IO.MemoryStream();       CryptoStream cs=new CryptoStream(ms,des.CreateDecryptor(),CryptoStreamMode.Write);       cs.Write(inputByteArray,0,inputByteArray.Length);       cs.FlushFinalBlock();       return Encoding.Default.GetString(ms.ToArray());     }       #endregion     }}