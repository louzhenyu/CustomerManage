using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Xml;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send
{
    /// <summary>
    ///  回复最新万份收益和年华收益率消息类定义。 
    /// 查询最新每万份收益、年化收益率（5002）
    /// </summary>
    [XmlRoot("xml")]
    public class SendProfitSelectMessage
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        [XmlElement("MerchantID", Type = typeof(XmlCDataSection))]
        public string MerchantID { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [XmlElement("Allpageno", Type = typeof(XmlCDataSection))]
        public string Allpageno { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [XmlElement("Curpageno", Type = typeof(XmlCDataSection))]
        public string Curpageno { get; set; }

        /// <summary>
        /// 货币基金代码
        /// </summary>
        [XmlElement("Fundno", Type = typeof(XmlCDataSection))]
        public string Fundno { get; set; }

        /// <summary>
        /// 货币基金名称
        /// </summary>
        [XmlElement("Fundnm", Type = typeof(XmlCDataSection))]
        public string Fundnm { get; set; }

        /// <summary>
        /// 净值当前日期
        /// </summary>
        [XmlElement("Bonuscurrdate", Type = typeof(XmlCDataSection))]
        public string Bonuscurrdate { get; set; }

        /// <summary>
        /// 净值当前每万份收益
        /// </summary>
        [XmlElement("Bonuscuramt", Type = typeof(XmlCDataSection))]
        public string Bonuscuramt { get; set; }

        /// <summary>
        /// 净值当前七日年化收益率
        /// </summary>
        [XmlElement("Bonuscurratio", Type = typeof(XmlCDataSection))]
        public string Bonuscurratio { get; set; }

        /// <summary>
        /// 净值上一日期
        /// </summary>
        [XmlElement("Bonusbefdate", Type = typeof(XmlCDataSection))]
        public string Bonusbefdate { get; set; }

        /// <summary>
        /// 净值上一日期每万份收益
        /// </summary>
        [XmlElement("Bonusbefamt", Type = typeof(XmlCDataSection))]
        public string Bonusbefamt { get; set; }

        /// <summary>
        /// 净值上一日期七日年化收益率率
        /// </summary>
        [XmlElement("Bonusbefratio", Type = typeof(XmlCDataSection))]
        public string Bonusbefratio { get; set; }

        /// <summary>
        /// 查询结果
        /// </summary>
        [XmlElement("Retcode", Type = typeof(XmlCDataSection))]
        public string Retcode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("Retmsg", Type = typeof(XmlCDataSection))]
        public string Retmsg { get; set; }

    }
}