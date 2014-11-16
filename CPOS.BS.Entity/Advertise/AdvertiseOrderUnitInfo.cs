using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Advertise
{
    /// <summary>
    /// 广告订单与门店关系
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class AdvertiseOrderUnitInfo
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string order_unit_id { get; set; }
        /// <summary>
        /// 订单标识
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 门店标识
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 下载标识
        /// </summary>
        public string if_flag { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string bat_no { get; set; }
        /// <summary>
        /// 广告订单与门店关系集合
        /// </summary>
       [XmlIgnore()] 
        public IList<AdvertiseOrderUnitInfo> advertiseOrderUnitInfoList { get; set; }
    }
}
