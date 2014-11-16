using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// xml序列化与反序列化
    /// </summary>
    public class cXMLService
    {
        

        #region 序列化与反序列化
        /// <summary>
        /// 反序列化 从字符串
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="type">要生成的对象类型</param>
        /// <returns>反序列化后的对象</returns>
        public static object Deserialize(string xml, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            StringReader sr = new StringReader(xml);
            object obj = xs.Deserialize(sr);
            return obj;
        }

        /// <summary>
        /// 序列化成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialiaze(object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, System.Text.Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xs.Serialize(xtw, obj);
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms);
            string str = sr.ReadToEnd();
            xtw.Close();
            ms.Close();
            return str;
        }
        #endregion

        

    }
}
