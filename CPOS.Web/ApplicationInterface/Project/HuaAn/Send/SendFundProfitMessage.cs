using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send
{
    /// <summary>
    /// 资产及收益。5000
    /// </summary>
      [XmlRoot("xml")]
    public class SendFundProfitMessage
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        [XmlElement("MerchantID")]
        public string MerchantID { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [XmlElement("Allpageno")]
        public string Allpageno { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [XmlElement("Curpageno")]
        public string Curpageno { get; set; }

        /// <summary>
        /// 总笔数
        /// </summary>
        [XmlElement("Allcount")]
        public string Allcount { get; set; }

        /// <summary>
        ///对账单内容，详细内容见备注说明
        /// </summary>
        [XmlElement("Content")]
        public string Content { get; set; }

        /// <summary>
        ///查询结果
        /// </summary>
        [XmlElement("Retcode")]
        public string Retcode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("Retmsg")]
        public string Retmsg { get; set; }

    }
}