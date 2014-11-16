using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Advertise
{
    /// <summary>
    /// 广告播放订单
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class AdvertiseOrderInfo
    {
        /// <summary>
        /// 广告播放标识
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 号码
        /// </summary>
        public string order_code { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 播放开始日期
        /// </summary>
        public string date_start { get; set; }
        /// <summary>
        /// 播放结束日期
        /// </summary>
        public string date_end { get; set; }
        /// <summary>
        /// 播放开始时间
        /// </summary>
        public string time_start { get; set; }
        /// <summary>
        /// 播放结束时间
        /// </summary>
        public string time_end { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        public int playbace_no { get; set; }
        /// <summary>
        /// 物理地址
        /// </summary>
        public string url_address { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string status_desc { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 广告播放订单集合
        /// </summary>
         [XmlIgnore()]
        public IList<AdvertiseOrderInfo> advertiseOrderInfoList { get; set; }


         /// <summary>
         /// 广告播放订单与广告集合
         /// </summary>
         [XmlIgnore()]
         public IList<AdvertiseOrderAdvertiseInfo> advertiseOrderAdvertiseInfoList { get; set; }

         /// <summary>
         /// 广告播放订单与广告集合
         /// </summary>
         [XmlIgnore()]
         public IList<AdvertiseOrderUnitInfo> advertiseOrderUnitInfoList { get; set; }

         /// <summary>
         /// 客户标识
         /// </summary>
         public string customer_id { get; set; }
        /// <summary>
        /// 下载标识，1=已下载，0=未下载
        /// </summary>
         public string if_flag { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
         public string bat_no { get; set; }

         public string create_user_name { get; set; }

         public string modify_user_name { get; set; }
    }
}
