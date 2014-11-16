using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Report
{
    /// <summary>
    /// 销售报表主信息
    /// </summary>
    public class SalesReportInfo
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 门店标识
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 销售笔数
        /// </summary>
        public decimal sales_qty { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal sales_amount { get; set; }
        /// <summary>
        /// 销售总笔数
        /// </summary>
        public decimal sales_total_qty { get; set; }
        /// <summary>
        /// 销售总金额
        /// </summary>
        public decimal sales_total_amount { get; set; }
        /// <summary>
        /// 销售报表主信息集合
        /// </summary>
        public IList<SalesReportInfo> SalesReportList { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int icount { get; set; }
    }
}
