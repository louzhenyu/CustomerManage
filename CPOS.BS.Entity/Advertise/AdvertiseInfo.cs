using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Advertise
{
    /// <summary>
    /// 广告模板
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class AdvertiseInfo
    {
        /// <summary>
        /// 广告标识
        /// </summary>
        public string advertise_id { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string advertise_name { get; set; }
        /// <summary>
        /// 广告号码
        /// </summary>
        public string advertise_code { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string file_size { get; set; }
        /// <summary>
        /// 文件格式
        /// </summary>
        public string file_format { get; set; }
        /// <summary>
        /// 显示方式1 = 播放，2 = 显示。。。。。
        /// </summary>
        public string display { get; set; }
        /// <summary>
        /// 播放时间长度
        /// </summary>
        public string playback_time { get; set; }
        /// <summary>
        /// 文件链接地址
        /// </summary>
        public string url_address { get; set; }
        /// <summary>
        /// 广告公司标识
        /// </summary>
        public string brand_customer_id { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string brand_id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 下载标志
        /// </summary>
        public string if_flag { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 广告信息集合
        /// </summary>
        [XmlIgnore()]
        public IList<AdvertiseInfo> advertiseInfoList { get; set; }
        /// <summary>
        /// 广告订单与广告关系标识
        /// </summary>
        public string advertise_order_advertise_id { get; set; }
        
    }
}
