using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace JIT.CPOS.Common
{
    public static class XmlHelper
    {
        #region GetXmlNodeVal
        public static string GetXmlNodeVal(this XmlNode parentNode, string nodeName, bool empty)
        {
            nodeName = nodeName.Trim();
            string val = null;
            XmlNode targetAttr = parentNode.Attributes[nodeName];
            XmlNode targetNode = parentNode.SelectSingleNode(nodeName);

            if (targetAttr != null) val = targetAttr.Value;
            else if (targetNode != null) val = targetNode.InnerText;
            else if (nodeName == "value") val = parentNode.InnerText;

            if (val == null && targetNode != null && targetNode.ChildNodes.Count > 0)
            {
                XmlNode childNode = targetNode.ChildNodes[0];
                if (childNode is XmlCDataSection)
                {
                    val = ((XmlCDataSection)childNode).Value;
                }
            }

            if (!empty && (val == null || val == string.Empty))
                throw new Exception("节点值不能为空.");
            return val;
        }

        public static string GetXmlNodeVal(this XmlNode parentNode, string nodeName)
        {
            return GetXmlNodeVal(parentNode, nodeName, true);
        }

        public static int GetXmlNodeValToInt(this XmlNode parentNode, string nodeName, bool empty)
        {
            int val = 0;
            string valObj = GetXmlNodeVal(parentNode, nodeName, empty);
            if (valObj != null) val = int.Parse(valObj);
            return val;
        }

        public static int GetXmlNodeValToInt(this XmlNode parentNode, string nodeName)
        {
            return GetXmlNodeValToInt(parentNode, nodeName, true);
        }

        public static bool GetXmlNodeValToBool(this XmlNode parentNode, string nodeName, bool empty)
        {
            bool val = false;
            string valObj = GetXmlNodeVal(parentNode, nodeName, empty);
            if (valObj != null) val = bool.Parse(valObj);
            return val;
        }

        public static bool GetXmlNodeValToBool(this XmlNode parentNode, string nodeName)
        {
            return GetXmlNodeValToBool(parentNode, nodeName, true);
        }
        #endregion


        /// <summary>
        /// 序列化为XML字窜
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="OmitXmlDeclaration">是否忽略xml头</param>
        /// <returns></returns>
        public static string SerializeToXmlStr<T>(T obj, bool OmitXmlDeclaration = false)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            //去除xml声明
            settings.OmitXmlDeclaration = OmitXmlDeclaration;
            settings.Encoding = Encoding.Default;
            System.IO.MemoryStream mem = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(mem, settings))
            {
                //去除默认命名空间xmlns:xsd和xmlns:xsi
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer formatter = new XmlSerializer(obj.GetType());
                formatter.Serialize(writer, obj, ns);
            }
            return Encoding.Default.GetString(mem.ToArray());
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string xmlStr)
        {
            Type type = typeof(T);
            XElement xe = XElement.Parse(xmlStr);
            XmlSerializer xs = new XmlSerializer(type);

            return (T)xs.Deserialize(xe.CreateReader());
        }
    }
}
