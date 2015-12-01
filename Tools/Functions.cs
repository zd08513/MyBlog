using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
 
namespace Tools
{
    public class Functions
    {
 
        #region ǰ�油��
        /// <summary>
        /// ��סλ�������֣�ǰ�油��
        /// </summary>
        /// <param name="value">Ҫ���������</param>
        /// <param name="size">�����λ��</param>
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
 
        #region ���˵� html����
        /// <summary>
        /// ����html��ǩ
        /// </summary>
        /// <param name="strHtml">html������</param>
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
 
        #region ȫ�ǰ��ת��
        /// <summary>
        /// תȫ�ǵĺ���(SBC case)
        /// </summary>
        /// <param name="input">�����ַ���</param>
        /// <returns>ȫ���ַ���</returns>
        ///<remarks>
        ///ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
        ///�����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
        ///</remarks>
        public static string ToSBC(string input)
        {
            //���תȫ�ǣ�
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
 
 
        /// <summary> ת��ǵĺ���(DBC case) </summary>
        /// <param name="input">�����ַ���</param>
        /// <returns>����ַ���</returns>
        ///<remarks>
        ///ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
        ///�����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
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
 
 
        #region ����URL������ҳ��html����
 
        /// <summary>
        /// ����URL������ҳ��html����
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
 
        #region MD5�����ַ�������ȡ�ַ���
 
        /// <summary>
        /// �������ģ�������MD%���ܺ���ַ���
        /// </summary>
        /// <param name="str">Ҫ���ܵ��ַ���</param>
        /// <returns>��MD5���ܺ���ַ���</returns>
        public static string ToMD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
        }
        /// <summary>
        /// ��ȡ�ַ�����
        /// </summary>
        /// <param name="str">Ҫ��ȡ���ַ���</param>
        /// <param name="number">�������ֽ���������Ǽ���</param>
        /// <returns>ָ�����ȵ��ַ���</returns>
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
 
        #region ɾ���ļ�
 
        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="FilePath">�ļ��������ַ</param>
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
                //errorMsg = "ɾ�����ɹ���";
                return false;
            }
        }
        #endregion
 
        #region ��֤�������ֲ���
        /// <summary>
        /// �ж��Ƿ���ʵ�����Ƿ���true �񷵻�false�����Դ���null��
        /// </summary>
        /// <param name="strVal">Ҫ��֤���ַ���</param>
        /// <returns></returns>
        public static bool IsNumeric(string strVal)
        {
            //System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("-?([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");  
            //return reg1.IsMatch(strVal);  
            //string tmp="";
            //�ж��Ƿ�Ϊnull �Ϳ��ַ���
            if (strVal == null || strVal.Length == 0)
                return false;
            //�ж��Ƿ�ֻ��.��-�� -.
            if (strVal == "." || strVal == "-" || strVal == "-.")
                return false;
 
            //��¼�Ƿ��ж��С����
            bool isPoint = false;            //�Ƿ���С����
 
            //ȥ����һ�����ţ��м��ǲ������и��ŵ�
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
        /// �ж��Ƿ�Ϊ�������Ƿ���true �񷵻�false�����Դ���null��
        /// </summary>
        /// <param name="strVal">Ҫ�жϵ��ַ�</param>
        /// <returns></returns>
        public static bool IsInt(string strVal)
        {
            if (strVal == null || strVal.Length == 0)
                return false;
            //�ж��Ƿ�ֻ��.��-�� -.
            if (strVal == "." || strVal == "-" || strVal == "-.")
                return false;
 
            //ȥ����һ�����ţ��м��ǲ������и��ŵ�
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
        /// �ж��Ƿ�ΪID�����Ƿ���true �񷵻�false�����Դ���null��
        /// </summary>
        /// <example >
        /// ,1,2,3,4,5,6,7,
        /// </example>
        /// <param name="strVal">Ҫ�жϵ��ַ���</param>
        /// <returns></returns>
        public static bool IsIDString(string strVal)
        {
            bool flag = false;
            if (strVal == null)
                return false;
            if (strVal == "")
                return true;
            //�ж��Ƿ�ֻ�� ,
            if (strVal == ",")
                return false;
 
            //�жϵ�һλ�Ƿ���,��
            if (strVal.Substring(0, 1) == ",")
                return false;
 
            //�ж����һλ�Ƿ���,��
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
        /// ת��Ϊ���������������Ļ������ء�-1��
        /// </summary>
        /// <param name="str">Ҫת�����ַ�</param>
        /// <returns></returns>
        public static int StringToInt(string str)
        {
            //�ж��Ƿ������֣������ַ������֣��������ַ���-1
            if (IsInt(str))
                return Int32.Parse(str);
            else
                return -1;
        }
 
        /// <summary>
        /// ת��Ϊʵ��������ʵ���Ļ������ء�-1��
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float StrTofloat(string str)
        {
            //�ж��Ƿ������֣������ַ������֣��������ַ���-1
            if (IsNumeric(str))
                return float.Parse(str);
            else
                return -1;
        }
 
        /// <summary>
        /// ��֤�Ƿ���GUID
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
 
            //���ȱ�����36λ
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
 
 
        #region ��֤���������ַ�������
 
        /// <summary>
        /// ȥ�����ߵĿո񣬰ѡ�'���滻Ϊ������SBC
        /// </summary>
        /// <param name="str">Ҫ������ַ���</param>
        /// <returns></returns>
        public static string StringReplaceToSBC(string str)
        {
            //���˲���ȫ���ַ�
            string tstr;
            tstr = str.Trim();
            return tstr.Replace("'", "��");
        }
 
        /// <summary>
        /// ȥ�����ߵĿո񣬰ѡ�'���滻Ϊ��''��DBC
        /// </summary>
        /// <param name="str">Ҫ��֤���ַ���</param>
        /// <returns></returns>
        public static string StringReplaceToDBC(string str)
        {
            //���˲���ȫ���ַ�
            string tstr;
            tstr = str.Trim();
            return tstr.Replace("'", "''");
        }
        /// <summary>
        /// ȥ�����ߵĿո񣬰ѡ�'���滻Ϊ����
        /// </summary>
        /// <param name="str">Ҫ��֤���ַ���</param>
        /// <returns></returns>
        public static string StringReplaceToEmpty(string str)
        {
            //���˲���ȫ���ַ�
            string tstr;
            tstr = str.Trim();
            return tstr.Replace("'", "");
        }
 
        #endregion
 
 
        #region ��֤����ʱ�䲿��
 
        /// <summary>
        /// ת��ʱ�䡣����ȷ�Ļ������ص�ǰʱ��
        /// </summary>
        /// <param name="isdt">Ҫת�����ַ���</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string isdt)
        {
            //�ж�ʱ���Ƿ���ȷ
            DateTime mydt;
            try
            {
                mydt = Convert.ToDateTime(isdt);
            }
            catch
            {
                //ʱ���ʽ����ȷ
                return mydt = DateTime.Now;
            }
 
            return mydt;
        }
 
        /// <summary>
        /// �ж��Ƿ�����ȷ��ʱ���ʽ����ȷ���ء�true��������ȷ������ʾ��Ϣ��
        /// 
        /// </summary>
        /// <param name="isdt">Ҫ�жϵ��ַ���</param>
        /// <returns></returns>
        public static bool IsDateTime(string isdt)
        {
            //�ж�ʱ���Ƿ���ȷ
            DateTime mydt;
            try
            {
                mydt = Convert.ToDateTime(isdt);
                return true;
            }
            catch
            {
                //ʱ���ʽ����ȷ
                //errorMsg = "�����ʱ���ʽ����ȷ���밴��2004-1-1����ʽ��д��";
                return false;
            }
 
 
        }
        #endregion
 
 
        #region ���ɲ�ѯ����
        /// <summary>
        /// ��ɲ�ѯ�ַ���
        /// </summary>
        /// <param name="columnName">�ֶ���</param>
        /// <param name="keyword">��ѯ����</param>
        /// <param name="hasContent">�Ƿ��Ѿ��в�ѯ�����ˣ�true����and��false������and</param>
        /// <param name="colType">1�����֣�2���ַ�������ȷ��ѯ��3���ַ�����ģ����ѯ������ʱ���ѯ</param>
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
                        //����
                        tmp.Append(columnName);
                        tmp.Append(" = ");
                        tmp.Append(keyword);
                        break;
                    case 2:
                        //�ַ�������ȷ��ѯ
                        tmp.Append(columnName);
                        tmp.Append(" = '");
                        tmp.Append(keyword);
                        tmp.Append("' ");
                        break;
                    case 3:
                        //�ַ�����ģ����ѯ������ʱ���ѯ
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
 
        #region ========����========
 
 
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "litianping");
        }
 
        /// <summary> 
        /// �������� 
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

        #region ��������ת��
        /// <summary>
        /// ��������ת��
        /// </summary>
        /// <typeparam name="T">int,string,obj...</typeparam>
        /// <param name="obj">����</param>
        /// <returns></returns>
        public static T ToConvert<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
        #endregion
    }
 
}