using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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
    }
}
