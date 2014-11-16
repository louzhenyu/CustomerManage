using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 班次模板
    /// </summary>
    public class ShiftInfo
    {
        /// <summary>
        /// 班次标识（保存必须）
        /// </summary>
        public string shift_id { get; set; }
        /// <summary>
        /// 收营员【保存必须】
        /// </summary>
        public string sales_user { get; set; }
        /// <summary>
        /// pos标识（保存必须）
        /// </summary>
        public string pos_id { get; set; }
        /// <summary>
        /// 上一个班次标识【保存必须】
        /// </summary>
        public string parent_shift_id { get; set; }
        /// <summary>
        /// 门店标识【保存必须】
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 准备金【保存必须】
        /// </summary>
        public decimal deposit_amount { get; set; }
        /// <summary>
        /// 销售金额【保存必须，空位0】
        /// </summary>
        public decimal sale_amount { get; set; }
        /// <summary>
        /// 退款金额【保存必须，空位0】
        /// </summary>
        public decimal return_amount { get; set; }
        /// <summary>
        /// 营业日期【保存必须,yyyy-MM-dd】
        /// </summary>
        public string pos_date { get; set; }
        /// <summary>
        /// 销售笔数【保存必须】
        /// </summary>
        public int sales_qty { get; set; }
        /// <summary>
        /// 销售总金额【保存必须：销售金额-退款金额】
        /// </summary>
        public decimal sales_total_amount { get; set; }
        /// <summary>
        /// 开班时间 【保存必须 yyyy-MM-dd hh:mm:dd】
        /// </summary>
        public string open_time { get; set; }
        /// <summary>
        /// 交班时间【保存必须 yyyy-MM-dd hh:mm:dd】
        /// </summary>
        public string close_time { get; set; }
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
        /// 门店【显示】
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 行号【显示】
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 总记录数【显示】
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 班次集合
        /// </summary>
        public IList<ShiftInfo> ShiftListInfo { get; set; }
        /// <summary>
        /// 总计笔数
        /// </summary>
        public int sales_total_qty { get; set; }
        /// <summary>
        /// 总计销售金额
        /// </summary>
        public decimal sales_total_total_amount { get; set; }
        /// <summary>
        /// 总的准备金
        /// </summary>
        public decimal total_deposit_amount { get; set; }
        /// <summary>
        /// 总的销售金额
        /// </summary>
        public decimal total_sale_amount { get; set; }
        /// <summary>
        /// 总的退款金额
        /// </summary>
        public decimal total_return_amount { get; set; }

    }
}
