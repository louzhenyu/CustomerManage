using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send
{
    /// <summary>
    /// 回复支付消息类定义。
    /// </summary>
    [XmlRoot("xml")]
    public class SendPayMessage
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
        /// 定单号
        /// </summary>
        [XmlElement("orderNO")]
        public string orderNO { get; set; }

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
        [XmlElement("Assbuyeridtp")]/// 
        public string Assbuyeridtp { get; set; }

        /// <summary>
        /// 客户证件号码
        /// </summary>
        [XmlElement("Assbuyeridno")]
        public string Assbuyeridno { get; set; }

        /// <summary>
        /// 交易号(对应资产)
        /// </summary>
        [XmlElement("Logisticsinfo")]
        public string Logisticsinfo { get; set; }

        /// <summary>
        /// 支付金额（单位：圆）
        /// </summary>
        [XmlElement("Totalpay")]
        public string Totalpay { get; set; }

        /// <summary>
        /// 总折扣(此处为0)
        /// </summary>
        [XmlElement("Totaldiscount")]
        public string Totaldiscount { get; set; }

        /// <summary>
        /// 总抵扣(此处为0)
        /// </summary>
        [XmlElement("Totaldeduction")]
        public string Totaldeduction { get; set; }

        /// <summary>
        /// 应付金额(此处与支付金额一致)
        /// </summary>
        [XmlElement("Actualtotal")]
        public string Actualtotal { get; set; }

        /// <summary>
        /// 手续费类型(此处暂为0)
        /// </summary>
        [XmlElement("Feetype")]
        public string Feetype { get; set; }

        /// <summary>
        /// 手续费(此处暂为0)
        /// </summary>
        [XmlElement("Fee")]
        public string Fee { get; set; }

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
        /// 华安交易日期
        /// </summary>
        [XmlElement("Hatradedt")]
        public string Hatradedt { get; set; }

        /// <summary>
        /// 华安交易流水号
        /// </summary>
        [XmlElement("HaorderNO")]
        public string HaorderNO { get; set; }

        /// <summary>
        /// 公共回传字段
        /// </summary>
        [XmlElement("Commonreturn")]
        public string Commonreturn { get; set; }
    }
}