using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Advertise
{
    /// <summary>
    /// 广告播放订单与广告关系
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class AdvertiseOrderAdvertiseInfo
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 订单标识
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 广告标识
        /// </summary>
        public string advertise_id { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 广告播放订单与广告关系集合
        /// </summary>
        [XmlIgnore()]
        public IList<AdvertiseOrderAdvertiseInfo> advertiseOrderAdvertiseInfoList { get; set; }
    }
}
