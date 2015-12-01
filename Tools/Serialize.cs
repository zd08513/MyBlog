/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��[url=http://www.cckan.net/thread-655-1-1.html]http://www.cckan.net/thread-655-1-1.html[/url]
/// </summary>
using System;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
 
namespace Tools
{
 
    #region ���л�
    public class  Serialize  
    {   
        /// <summary>
        /// ���л�Ϊ����
        /// </summary>
        /// <param name="objname"></param>
        /// <param name="obj"></param>
        public static void BinarySerialize(string objname,object obj)
        {
            try
            {
                string filename = objname + ".Binary";
                if(System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    // �ö����Ƹ�ʽ���л�
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, obj);
                    fileStream.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
 
        /// <summary>
        /// �Ӷ������ļ��з����л�
        /// </summary>
        /// <param name="objname"></param>
        /// <returns></returns>
        public static object BinaryDeserialize(string objname)
        {
            System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
            //�����Ƹ�ʽ�����л�
            object obj;
            string filename = objname + ".Binary";
            if(!System.IO.File.Exists(filename))
                throw new Exception("�ڷ����л�֮ǰ,�������л�");
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                obj = formatter.Deserialize(stream);
                stream.Close();
            }
            //using (FileStream fs = new FileStream(filename, FileMode.Open))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    object obj = formatter.Deserialize(fs);
            //}
            return obj;
 
        }
 
        /// <summary>
        /// ���л�Ϊsoap ��xml
        /// </summary>
        /// <param name="objname"></param>
        /// <returns></returns>
        public static void SoapSerialize(string objname,object obj)
        {
            try
            {  
                string filename=objname+".Soap";
                if(System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    // ���л�ΪSoap
                    SoapFormatter formatter = new SoapFormatter();
                    formatter.Serialize(fileStream, obj);
                    fileStream.Close();
                }
 
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
 
 
        /// <summary>
        /// �����ж���
        /// </summary>
        /// <param name="objname"></param>
        public static object SoapDeserialize(string objname)
        {
            object obj;
            System.Runtime.Serialization.IFormatter formatter = new SoapFormatter();
            string filename=objname+".Soap";
            if (!System.IO.File.Exists(filename))
                throw new Exception("�Է����л�֮ǰ,�������л�");
            //Soap��ʽ�����л�
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                obj = formatter.Deserialize(stream);
                stream.Close();
            }
            return obj;
        }
 
        public static void XmlSerialize(string objname,object obj)
        {
              
            try
            {
                string filename=objname+".xml";
                if(System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    // ���л�Ϊxml
                    XmlSerializer formatter = new XmlSerializer(typeof(Car));
                    formatter.Serialize(fileStream, obj);
                    fileStream.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
 
        }
 
 
        /// <summary>
        /// ��xml�����з����л�
        /// </summary>
        /// <param name="objname"></param>
        /// <returns></returns>
        public static object XmlDeserailize(string objname)
        {
           // System.Runtime.Serialization.IFormatter formatter = new XmlSerializer(typeof(Car));
            string filename=objname+".xml";
            object obj;
            if (!System.IO.File.Exists(filename))
                throw new Exception("�Է����л�֮ǰ,�������л�");
            //Xml��ʽ�����л�
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Car));
                obj = (Car)formatter.Deserialize(stream);
                stream.Close();
            }
            return obj; 
        }
    }
    #endregion
 
    #region Ҫ���л�����
    [Serializable]
    public class Car
    {
        private string _Price;
        private string _Owner;
        private string m_filename;
 
        [XmlElement(ElementName = "Price")]
        public string Price
        {
            get { return this._Price; }
            set { this._Price = value; }
        }
 
        [XmlElement(ElementName = "Owner")]
        public string Owner
        {
            get { return this._Owner; }
            set { this._Owner = value; }
        }
 
        public string Filename
        {
            get
            {
                return m_filename;
            }
            set
            {
                m_filename = value;
            }
        }
 
        public Car(string o, string p)
        {
            this.Price = p;
            this.Owner = o;
        }
 
        public Car()
        {
 
        }
    }
    #endregion
 
    #region ����ʾ��
    public class Demo
    {
        public void DemoFunction()
        {
            //���л�
            Car car = new Car("chenlin", "120��");
            Serialize.BinarySerialize("Binary���л�", car);
            Serialize.SoapSerialize("Soap���л�", car);
            Serialize.XmlSerialize("XML���л�", car);
            //�����л�
            Car car2 = (Car)Serialize.BinaryDeserialize("Binary���л�");
            car2 = (Car)Serialize.SoapDeserialize("Soap���л�");
            car2 = (Car)Serialize.XmlDeserailize("XML���л�");
        }
    }
    #endregion
}