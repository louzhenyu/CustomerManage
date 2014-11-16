using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 订单明细model
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class OrderDetailInfo
    {
        /// <summary>
        /// 标识(必须)
        /// </summary>
        public string order_detail_id { get; set; }
        /// <summary>
        /// 主单标识(必须)
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 原单据明细标识
        /// </summary>
        public string ref_order_detail_id { get; set; }
        /// <summary>
        /// sku标识(必须)
        /// </summary>
        public string sku_id { get; set; }
        /// <summary>
        /// 组织标识(必须)
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 订单数量(出入库数)
        /// </summary>
        public decimal order_qty { get; set; }
        /// <summary>
        /// 输入数量（预订数）
        /// </summary>
        public decimal enter_qty { get; set; }
        /// <summary>
        /// 输入价格
        /// </summary>
        public decimal enter_price { get; set; }
        /// <summary>
        /// 输入金额（）
        /// </summary>
        public decimal enter_amount { get; set; }
        /// <summary>
        /// 标准价格（建议零售价）
        /// </summary>
        public decimal std_price { get; set; }
        /// <summary>
        /// 合同折扣
        /// </summary>
        public decimal order_discount_rate { get; set; }
        /// <summary>
        /// 折扣(折上折)
        /// </summary>
        public decimal discount_rate { get; set; }
        /// <summary>
        /// 实际价格
        /// </summary>
        public decimal retail_price { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal retail_amount { get; set; }
        
        /// <summary>
        /// 积分
        /// </summary>
        public decimal receive_points { get; set; }
        /// <summary>
        /// 支付积分
        /// </summary>
        public decimal pay_points { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        
        
        /// <summary>
        /// 排序
        /// </summary>
        public int display_index { get; set; }
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
        /// 原单据标识
        /// </summary>
        public string ref_order_id { get; set; }
        /// <summary>
        /// 是否下载
        /// </summary>
        public string if_flag { get; set; }
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
        /// 前端显示名称（包含条码等信息）
        /// </summary>
        public string display_name { get; set; }

        /// <summary>
        /// 主单据状态(前端)
        /// </summary>
        public string order_status { get; set; }
    }
}
