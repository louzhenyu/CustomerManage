using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 库存类
    /// </summary>
    public class StockBalanceInfo
    {
        /// <summary>
        /// 库存标识【必须】
        /// </summary>
        public string stock_balance_id { get; set; }
        /// <summary>
        /// 单位标识【必须】
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 仓库标识【必须】
        /// </summary>
        public string warehouse_id { get; set; }
        /// <summary>
        /// sku标识【必须】
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// 开始数量
        /// </summary>
        public decimal begin_qty { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        public decimal in_qty { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public decimal out_qty { get; set; }
        /// <summary>
        /// 调整入库数量
        /// </summary>
        public decimal adjust_in_qty { get; set; }
        /// <summary>
        /// 调整出库数量
        /// </summary>
        public decimal adjust_out_qty { get; set; }
        /// <summary>
        /// 储备数量
        /// </summary>
        public decimal reserver_qty { get; set; }
        /// <summary>
        /// 在途数量
        /// </summary>
        public decimal on_way_qty { get; set; }
        /// <summary>
        /// 最终数量
        /// </summary>
        public decimal end_qty { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 商品类别标签【必须】
        /// </summary>
        public string item_label_type_id { get; set; }
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
        /// 品质号码【显示】
        /// </summary>
        public string item_label_type_code { get; set; }
        /// <summary>
        /// 品质名称【显示】
        /// </summary>
        public string item_label_type_name { get; set; }

        /// <summary>
        /// 商品号码【显示】
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        /// 商品名称【显示】
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string warehouse_name { get; set; }
        /// <summary>
        /// 属性1值【显示】
        /// </summary>
        public string prop_1_detail_name { get; set; }
        /// <summary>
        /// 属性2值【显示】
        /// </summary>
        public string prop_2_detail_name { get; set; }
        /// <summary>
        /// 属性3值【显示】
        /// </summary>
        public string prop_3_detail_name { get; set; }
        /// <summary>
        /// 属性4值【显示】
        /// </summary>
        public string prop_4_detail_name { get; set; }
        /// <summary>
        /// 属性5值【显示】
        /// </summary>
        public string prop_5_detail_name { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int row_no { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 库存集合
        /// </summary>
        public IList<StockBalanceInfo> StockBalanceInfoList { get; set; }
    }
}
