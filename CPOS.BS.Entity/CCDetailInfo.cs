using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 盘点单明细类
    /// </summary>
    public class CCDetailInfo
    {
        /// <summary>
        /// 盘点单明细标识【必须输入 长度不大于50】 guid
        /// </summary>
        public string order_detail_id { get; set; }
        /// <summary>
        /// 盘点单标识 【必须输入 长度不大于50】
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 原单据明细标识
        /// </summary>
        public string ref_order_detail_id { get; set; }
        /// <summary>
        /// 订单号 【必须输入 长度不大于50】
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 库存标识
        /// </summary>
        public string stock_balance_id { get; set; }
        /// <summary>
        /// sku标识【必须输入 长度不大于50】
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// 仓库标识 【长度不大于50】
        /// </summary>
        public string warehouse_id { get; set; }
        /// <summary>
        /// 库存数 【decimal（18，4）】
        /// </summary>
        public decimal end_qty { get; set; }
        /// <summary>
        /// 盘点单数量【decimal（18，4）】
        /// </summary>
        public decimal order_qty { get; set; }
        /// <summary>
        /// 差异数
        /// </summary>
        public decimal difference_qty { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 次序 【int】
        /// </summary>
        public int display_index { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建人标识
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 修改人标识
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 是否下载
        /// </summary>
        public int if_flag { get; set; }
        /// <summary>
        /// 商品号码【显示】
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        /// 商品名称【显示】
        /// </summary>
        public string item_name { get; set; }
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
        /// 总记录数
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 获取盘点单明细集合
        /// </summary>
        public IList<CCDetailInfo> CCDetailInfoList { get; set; }
        /// <summary>
        /// 前端显示名称（包含条码等信息）
        /// </summary>
        public string display_name { get; set; }
    }
}
