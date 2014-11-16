using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Promotion
{
    /// <summary>
    /// 会员信息（与终端交换时使用)
    /// </summary>
    [XmlRoot("data")]
    public class VipExchangeInfo : VipBaseInfo
    {
        public VipExchangeInfo()
            : base()
        { }

        /// <summary>
        /// 类型(编码)
        /// </summary>
        [XmlElement("vip_type")]
        public string Type
        { get; set; }

        /// <summary>
        /// 办卡门店(ID)
        /// </summary>
        [XmlElement("activate_unit_id")]
        public string ActivateUnitID
        { get; set; }
    }
}
