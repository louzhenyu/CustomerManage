using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 监控日志
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class MonitorLogInfo
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string monitor_log_id { get; set;}
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 门店标识
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 终端标识
        /// </summary>
        public string pos_id { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public string  upload_time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 上传标志
        /// </summary>
        public int if_flag { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string bat_id { get; set; }
        /// <summary>
        /// 监控日志集合
        /// </summary>
        [XmlIgnore()]
        public IList<MonitorLogInfo> monitorLogInfoList { get; set; }
    }
}
