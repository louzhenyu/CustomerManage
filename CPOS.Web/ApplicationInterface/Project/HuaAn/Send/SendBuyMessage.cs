using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send
{
    /// <summary>
    /// /回复购买基金消息类定义。
    /// </summary>
    [XmlRoot("xml")]
    public class SendBuyMessage
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
        /// 申购金额（单位：圆）
        /// </summary>
        [XmlElement("Orgtotalamt")]
        public string Orgtotalamt { get; set; }

        /// <summary>
        /// 买家信息(客户协议号)	
        /// </summary>
        [XmlElement("Assignbuyer")]
        public string Assignbuyer { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        [XmlElement("Assbuyername")]
        public string Assbuyername { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        [XmlElement("Assbuyermobile")]
        public string Assbuyermobile { get; set; }

        /// <summary>
        /// 客户证件类型(0为身份证)
        /// </summary>
        [XmlElement("Assbuyeridtp")]
        public string Assbuyeridtp { get; set; }

        /// <summary>
        /// 交易号(对应资产)
        /// </summary>
        [XmlElement("Logisticsinfo")]
        public string Logisticsinfo { get; set; }

        /// <summary>
        /// 手续费(此处为0)
        /// </summary>
        [XmlElement("Fee")]
        public string Fee { get; set; }

        /// <summary>
        /// 交易类型（1：申购；2：赎回）
        /// </summary>
        [XmlElement("Fundtype")]
        public string Fundtype { get; set; }

        /// <summary>
        /// 返回码(0000为成功)
        /// </summary>
        [XmlElement("Retcode")]
        public string Retcode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("Retmsg")]
        public string Retmsg { get; set; }

        /// <summary>
        /// 公共回传字段
        /// </summary>
        [XmlElement("Commonreturn")]
        public string Commonreturn { get; set; }


    }
}