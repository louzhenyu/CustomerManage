using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 一键报修
    /// </summary>
    [Serializable]
    public class RepairInfo
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string repair_id { get; set; }
        /// <summary>
        /// 类型标识
        /// </summary>
        public string repair_type_id { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 门店标识
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 终端标识
        /// </summary>
        public string pos_id { get; set; }
        /// <summary>
        /// 终端序列号
        /// </summary>
        public string pos_sn { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string status_desc { get; set; }
        /// <summary>
        /// 报修时间
        /// </summary>
        public string repair_time { get; set; }
        /// <summary>
        /// 报修人
        /// </summary>
        public string repair_user_id { get; set; }
        /// <summary>
        /// 响应时间
        /// </summary>
        public string response_time { get; set; }
        /// <summary>
        /// 响应人
        /// </summary>
        public string response_user_id { get; set; }
        /// <summary>
        /// 修复完成时间
        /// </summary>
        public string complete_time { get; set; }
        /// <summary>
        /// 修复完成人
        /// </summary>
        public string complete_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 批量一键修复信息
        /// </summary>
        public IList<RepairInfo> repairInfoList { get; set; }
        /// <summary>
        /// 门店名称【显示】
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// pos号码【显示】
        /// </summary>
        public string pos_code { get; set; }
        /// <summary>
        /// 修复类型名称【显示】
        /// </summary>
        public string repair_type_name { get; set; }
        /// <summary>
        /// 报修人名称【显示】
        /// </summary>
        public string repair_user_name { get; set; }
        /// <summary>
        /// 修复完成确认人名称【显示】
        /// </summary>
        public string complete_user_name { get; set; }
        /// <summary>
        /// 响应人名称【显示】
        /// </summary>
        public string response_user_name { get; set; }
        /// <summary>
        /// 总数量【显示】
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 行号【显示】
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 报修日期开始【查询】
        /// </summary>
        public string repair_date_begin { get; set; }
        /// <summary>
        /// 报修日期终止【查询】
        /// </summary>
        public string repair_date_end { get; set; }
    }
}
