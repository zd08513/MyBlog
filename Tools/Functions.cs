using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
 
namespace Tools
{
    public class Functions
    {
 
        #region 前面补零
        /// <summary>
        /// 不住位数的数字，前面补零
        /// </summary>
        /// <param name="value">要补足的数字</param>
        /// <param name="size">不齐的位数</param>
        /// <returns></returns>
        public static string Zerofill(string value, int size)
        {
            string tmp = "";
            for (int i = 0; i < size - value.Length; i++)
            {
                tmp += "0";
            }
 
            return tmp + value;
        }
        #endregion
 
        #region 过滤掉 html代码
        /// <summary>
        /// 过滤html标签
        /// </summary>
        /// <param name="strHtml">html的内容</param>
        /// <returns></returns>
        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={
                                  @"<script[^>]*?>.*?</script>",
 
                                  @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                                  @"([\r\n])[\s]+",
                                  @"&(quot|#34);",
                                  @"&(amp|#38);",
                                  @"&(lt|#60);",
                                  @"&(gt|#62);", 
                                  @"&(nbsp|#160);", 
                                  @"&(iexcl|#161);",
                                  @"&(cent|#162);",
                                  @"&(pound|#163);",
                                  @"&(copy|#169);",
                                  @"&#(\d+);",
                                  @"-->",
                                  @"<!--.*\n"
                              };
 
            string[] aryRep = {
                                   "",
                                   "",
                                   "",
                                   "\"",
                                   "&",
                                   "<",
                                   ">",
                                   " ",
                                   "\xa1",//chr(161),
                                   "\xa2",//chr(162),
                                   "\xa3",//chr(163),
                                   "\xa9",//chr(169),
                                   "",
                                   "\r\n",
                                   ""
                               };
 
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            //strOutput.Replace("<", "");
            //strOutput.Replace(">", "");
            strOutput = strOutput.Replace("\r\n", "");
            return strOutput;
        }
        #endregion
 
        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            /*for (int i = 0; i < c.Length; i++)
            {
                if (c == 32)
                {
                    c = (char)12288;
                    continue;
                }
                if (c < 127)
                    c = (char)(c + 65248);
            }*/
            return new string(c);
        }
 
 
        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            /*for (int i = 0; i < c.Length; i++)
            {
                if (c == 12288)
                {
                    c = (char)32;
                    continue;
                }
                if (c > 65280 && c < 65375)
                    c = (char)(c - 65248);
            }*/
            return new string(c);
        }
        #endregion
 
 
        #region 传入URL返回网页的html代码
 
        /// <summary>
        /// 传入URL返回网页的html代码
        /// </summary>
        /// <param name="Url">URL</param>
        /// <returns></returns>
        public static string GetUrltoHtml(string Url)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.
                System.Net.WebResponse wResp = wReq.GetResponse();
                // Read an HTTP-specific property
                //if (wResp.GetType() ==HttpWebResponse)
                //{
                //DateTime updated  =((System.Net.HttpWebResponse)wResp).LastModified;
                //}
                // Get the response stream.
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, System.Text.Encoding.GetEncoding("gb2312"));
                return reader.ReadToEnd();
 
            }
            catch (System.Exception ex)
            {
                //errorMsg = ex.Message;
            }
            return "";
        }
 
        #endregion
 
        #region MD5加密字符串。截取字符串
 
        /// <summary>
        /// 传入明文，返回用MD%加密后的字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>用MD5加密后的字符串</returns>
        public static string ToMD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
        }
        /// <summary>
        /// 截取字符串。
        /// </summary>
        /// <param name="str">要接取得字符串</param>
        /// <param name="number">保留的字节数。按半角计算</param>
        /// <returns>指定长度的字符串</returns>
        public static string StringCal(string str, int number)
        {
            Byte[] tempStr = System.Text.Encoding.Default.GetBytes(str);
            if (tempStr.Length > number)
            {
                return System.Text.Encoding.Default.GetString(tempStr, 0, number - 2) + "..";
            }
            else
                return str;
        }
 
 
        #endregion
 
        #region 删除文件
 
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath">文件的物理地址</param>
        /// <returns></returns>
        public static bool DeleteFile(string FilePath)
        {
            try
            {
                System.IO.File.Delete(FilePath);
                return true;
            }
            catch
            {
                //errorMsg = "删除不成功！";
                return false;
            }
        }
        #endregion
 
        #region 验证——数字部分
        /// <summary>
        /// 判断是否是实数，是返回true 否返回false。可以传入null。
        /// </summary>
        /// <param name="strVal">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(string strVal)
        {
            //System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("-?([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");  
            //return reg1.IsMatch(strVal);  
            //string tmp="";
            //判断是否为null 和空字符串
            if (strVal == null || strVal.Length == 0)
                return false;
            //判断是否只有.、-、 -.
            if (strVal == "." || strVal == "-" || strVal == "-.")
                return false;
 
            //记录是否有多个小数点
            bool isPoint = false;            //是否有小数点
 
            //去掉第一个负号，中间是不可以有负号的
            strVal = strVal.TrimStart('-');
 
            foreach (char c in strVal)
            {
                if (c == '.')
                    if (isPoint)
                        return false;
                    else
                        isPoint = true;
 
                if ((c < '0' || c > '9') && c != '.')
                    return false;
            }
            return true;
        }
 
        /// <summary>
        /// 判断是否为整数。是返回true 否返回false。可以传入null。
        /// </summary>
        /// <param name="strVal">要判断的字符</param>
        /// <returns></returns>
        public static bool IsInt(string strVal)
        {
            if (strVal == null || strVal.Length == 0)
                return false;
            //判断是否只有.、-、 -.
            if (strVal == "." || strVal == "-" || strVal == "-.")
                return false;
 
            //去掉第一个负号，中间是不可以有负号的
            if (strVal.Substring(0, 1) == "-")
                strVal = strVal.Remove(0, 1);
 
            foreach (char c in strVal)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
 
        /// <summary>
        /// 判断是否为ID串。是返回true 否返回false。可以传入null。
        /// </summary>
        /// <example >
        /// ,1,2,3,4,5,6,7,
        /// </example>
        /// <param name="strVal">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsIDString(string strVal)
        {
            bool flag = false;
            if (strVal == null)
                return false;
            if (strVal == "")
                return true;
            //判断是否只有 ,
            if (strVal == ",")
                return false;
 
            //判断第一位是否是,号
            if (strVal.Substring(0, 1) == ",")
                return false;
 
            //判断最后一位是否是,号
            if (strVal.Substring(strVal.Length - 1, 1) == ",")
                return false;
 
            foreach (char c in strVal)
            {
                if (c == ',')
                    if (flag) return false; else flag = true;
 
                else if ((c >= '0' && c <= '9'))
                    flag = false;
                else
                    return false;
            }
            return true;
        }
 
        /// <summary>
        /// 转换为整数。不是整数的话，返回“-1”
        /// </summary>
        /// <param name="str">要转换的字符</param>
        /// <returns></returns>
        public static int StringToInt(string str)
        {
            //判断是否是数字，是数字返回数字，不是数字返回-1
            if (IsInt(str))
                return Int32.Parse(str);
            else
                return -1;
        }
 
        /// <summary>
        /// 转换为实数。不是实数的话，返回“-1”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float StrTofloat(string str)
        {
            //判断是否是数字，是数字返回数字，不是数字返回-1
            if (IsNumeric(str))
                return float.Parse(str);
            else
                return -1;
        }
 
        /// <summary>
        /// 验证是否是GUID
        /// 6454bc76-5f98-de11-aa4c-00219bf56456
        /// </summary>
        /// <returns></returns>
        public static bool IsGUID(string strVal)
        {
            if (strVal == null)
                return false;
 
            if (strVal == "")
                return false;
 
            strVal = strVal.TrimStart('{');
            strVal = strVal.TrimEnd('}');
 
            //长度必须是36位
            if (strVal.Length != 36)
                return false;
 
            foreach (char c in strVal)
            {
                if (c == '-')
                    continue;
                else if (c >= 'a' && c <= 'f')
                    continue;
                else if (c >= 'A' && c <= 'F')
                    continue;
                else if ((c >= '0' && c <= '9'))
                    continue;
                else
                    return false;
            }
            return true;
        }
        #endregion
 
 
        #region 验证——处理字符串部分
 
        /// <summary>
        /// 去掉两边的空格，把“'”替换为“＇”SBC
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns></returns>
        public static string StringReplaceToSBC(string str)
        {
            //过滤不安全的字符
            string tstr;
            tstr = str.Trim();
            return tstr.Replace("'", "＇");
        }
 
        /// <summary>
        /// 去掉两边的空格，把“'”替换为“''”DBC
        /// </summary>
        /// <param name="str">要验证的字符串</param>
        /// <returns></returns>
        public static string StringReplaceToDBC(string str)
        {
            //过滤不安全的字符
            string tstr;
            tstr = str.Trim();
            return tstr.Replace("'", "''");
        }
        /// <summary>
        /// 去掉两边的空格，把“'”替换为“”
        /// </summary>
        /// <param name="str">要验证的字符串</param>
        /// <returns></returns>
        public static string StringReplaceToEmpty(string str)
        {
            //过滤不安全的字符
            string tstr;
            tstr = str.Trim();
            return tstr.Replace("'", "");
        }
 
        #endregion
 
 
        #region 验证——时间部分
 
        /// <summary>
        /// 转换时间。不正确的话，返回当前时间
        /// </summary>
        /// <param name="isdt">要转换的字符串</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string isdt)
        {
            //判断时间是否正确
            DateTime mydt;
            try
            {
                mydt = Convert.ToDateTime(isdt);
            }
            catch
            {
                //时间格式不正确
                return mydt = DateTime.Now;
            }
 
            return mydt;
        }
 
        /// <summary>
        /// 判断是否是正确的时间格式。正确返回“true”，不正确返回提示信息。
        /// 
        /// </summary>
        /// <param name="isdt">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(string isdt)
        {
            //判断时间是否正确
            DateTime mydt;
            try
            {
                mydt = Convert.ToDateTime(isdt);
                return true;
            }
            catch
            {
                //时间格式不正确
                //errorMsg = "您填的时间格式不正确，请按照2004-1-1的形式填写。";
                return false;
            }
 
 
        }
        #endregion
 
 
        #region 生成查询条件
        /// <summary>
        /// 组成查询字符串
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="keyword">查询条件</param>
        /// <param name="hasContent">是否已经有查询条件了，true：加and；false：不加and</param>
        /// <param name="colType">1：数字；2：字符串，精确查询；3：字符串，模糊查询，包括时间查询</param>
        /// <returns></returns>
        public static string GetSearchString(string columnName, string keyword, ref bool hasContent, int colType)
        {
            if (keyword == "" || keyword == "0")
            {
                return "";
            }
            else
            {
                System.Text.StringBuilder tmp = new System.Text.StringBuilder();
                switch (colType)
                {
                    case 1:
                        //数字
                        tmp.Append(columnName);
                        tmp.Append(" = ");
                        tmp.Append(keyword);
                        break;
                    case 2:
                        //字符串，精确查询
                        tmp.Append(columnName);
                        tmp.Append(" = '");
                        tmp.Append(keyword);
                        tmp.Append("' ");
                        break;
                    case 3:
                        //字符串，模糊查询，包括时间查询
                        tmp.Append(columnName);
                        tmp.Append(" like '% ");
                        tmp.Append(keyword);
                        tmp.Append("%' ");
 
                        break;
                }
                if (hasContent)
                    tmp.Insert(0, " and ");
 
                hasContent = true;
                return tmp.ToString();
 
            }
 
        }
        #endregion
 
        #region ========解密========
 
 
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "litianping");
        }
 
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
 
        #endregion

        #region 基本类型转换
        /// <summary>
        /// 基本类型转换
        /// </summary>
        /// <typeparam name="T">int,string,obj...</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static T ToConvert<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
        #endregion
    }
 
}