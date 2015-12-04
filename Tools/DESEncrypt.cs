/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��[url=http://www.cckan.net/thread-655-1-1.html]http://www.cckan.net/thread-655-1-1.html[/url]
/// </summary>
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

    public static string _KEY = ConfigurationManager.AppSettings["DESKey"].ToString();  //��Կ  
    public static string _IV = ConfigurationManager.AppSettings["DESIV"].ToString();   //����  

    /// <summary>  
    /// ����  
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
    /// ����  
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
    }