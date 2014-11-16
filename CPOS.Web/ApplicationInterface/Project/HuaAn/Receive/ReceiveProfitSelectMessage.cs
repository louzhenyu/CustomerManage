using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive
{
    /// <summary>
    ///  接受最新万份收益和年华收益率消息类定义。 
    /// 查询最新每万份收益、年化收益率（5002）
    /// </summary>
     [XmlRoot("xml")]
    public class ReceiveProfitSelectMessage
    {
         /// <summary>
        /// 商家ID
         /// </summary>
        [XmlElement("MerchantID")]
        public string MerchantID { get; set; }

        /// <summary>
        /// 商户日期
        /// </summary>
        [XmlElement("Merchantdate")]
        public string Merchantdate { get; set; }

        /// <summary>
        /// 当前请求页码（默认填1）
        /// </summary>
        [XmlElement("Pageno")]
        public string Pageno { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        [XmlElement("RetURL")]
        public string RetURL { get; set; }

        /// <summary>
        /// 定单描述
        /// </summary>
        [XmlElement("Memo")]
        public string Memo { get; set; }

    }
}