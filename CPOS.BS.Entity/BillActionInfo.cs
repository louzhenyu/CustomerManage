using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 单据当前的动作
    /// </summary>
    public class BillActionInfo
    {
        /// <summary>
        /// 表单标识[保存必须]
        /// </summary>
        public string bill_id { get; set; }
        /// <summary>
        /// 创建按钮标识（1=显示，0=隐藏）[保存必须]
        /// </summary>
        public int create_flag { get; set; }
        /// <summary>
        /// 修改按钮标识（1=显示，0=隐藏）[保存必须]
        /// </summary>
        public int modify_flag { get; set; }
        /// <summary>
        /// 删按钮标识（1=显示，0=隐藏）[保存必须]
        /// </summary>
        public int delete_flag { get; set; }
        /// <summary>
        /// 审批按钮标识（1=显示，0=隐藏）[保存必须]
        /// </summary>
        public int approve_flag { get; set; }
        /// <summary>
        /// 回退标识（1=显示，0=隐藏）[保存必须]
        /// </summary>
        public int reject_flag { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int display_index { get; set; }
    }
}
