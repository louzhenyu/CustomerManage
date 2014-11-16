using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Report
{
    /// <summary>
    /// 商品销售报表模板
    /// </summary>
    public class ItemSalesReportInfo
    {
        /// <summary>
        /// 商品标识
        /// </summary>
        public string item_id { get; set; }
        /// <summary>
        /// 商品号码
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// 标准售价
        /// </summary>
        public decimal std_price { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public decimal enter_qty { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal enter_amount { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 总记录
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 商品销售报表记录集
        /// </summary>
        public IList<ItemSalesReportInfo> ItemSalesReportList { get; set; }
    }
}
