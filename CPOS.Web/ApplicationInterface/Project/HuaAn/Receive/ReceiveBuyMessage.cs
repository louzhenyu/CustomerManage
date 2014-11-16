using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive
{
    /// <summary>
    /// 接受基金购买（买号）消息类定义。
    /// </summary>
    [XmlRoot("xml")]
    public class ReceiveBuyMessage
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
        /// 
        [XmlElement("Totalamt")]
        public decimal Totalamt { get; set; }

        /// <summary>
        /// 交易附加信息
        /// </summary>
        /// 
        [XmlElement("Tradeappendinfo")]
        public string Tradeappendinfo { get; set; }

        /// <summary>
        /// 客户协议号
        /// </summary>
        /// 
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
        /// 手续费(此处填0)
        /// </summary>
        [XmlElement("Fee")]
        public string Fee { get; set; }

        /// <summary>
        /// 公共回传字段
        /// </summary>
        [XmlElement("Commonreturn")]
        public string Commonreturn { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        [XmlElement("RetURL")]
        public string RetURL { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        [XmlElement("PageURL")]
        public string PageURL { get; set; }

        /// <summary>
        /// 定单描述
        /// </summary>
        [XmlElement("Memo")]
        public string Memo { get; set; }
    }
}