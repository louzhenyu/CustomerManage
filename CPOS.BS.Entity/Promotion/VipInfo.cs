using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Promotion
{
    /// <summary>
    /// 会员信息
    /// </summary>
    [Serializable]
    public class VipInfo : VipBaseInfo
    {
        public VipInfo()
            : base()
        {
            this.ActivateUnit = new UnitInfo();
            this.Type = new VipTypeInfo();
        }
       
        /// <summary>
        /// 办卡门店
        /// </summary>
        [XmlIgnore()]
        public UnitInfo ActivateUnit
        { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlIgnore()]
        public string StatusDescription
        { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [XmlIgnore()]
        public VipTypeInfo Type
        { get; set; }
    }
}
