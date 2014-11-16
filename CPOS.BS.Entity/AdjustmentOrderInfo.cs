using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 商品调价单
    /// </summary>
    public class AdjustmentOrderInfo
    {
        /// <summary>
        /// 商品调价单主标识[保存必须]
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单号码[保存必须]
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 订单日期[保存必须]
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 起始日期[保存必须]
        /// </summary>
        public string begin_date { get; set; }
        /// <summary>
        /// 终止日期[保存必须]
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        /// 商品价格类型标识[保存必须]
        /// </summary>
        public string item_price_type_id { get; set; }
        /// <summary>
        /// 商品价格类型名称[显示]
        /// </summary>
        public string item_price_type_name { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string status_desc { get; set; }
        /// <summary>
        /// 创建人标识
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 修改人标识
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
       
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string create_user_name { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string modify_user_name { get; set; }

        /// <summary>
        /// 行号[显示]
        /// </summary>
        public int Row_No { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 调价单集合
        /// </summary>
        public IList<AdjustmentOrderInfo> AdjustmentOrderInfoList { get; set; }

        /// <summary>
        /// 商品明细集合
        /// </summary>
        public IList<AdjustmentOrderDetailItemInfo> AdjustmentOrderDetailItemList { get; set; }

        /// <summary>
        /// Sku明细集合
        /// </summary>
        public IList<AdjustmentOrderDetailSkuInfo> AdjustmentOrderDetailSkuList { get; set; }

        /// <summary>
        /// 组织明细集合
        /// </summary>
        public IList<AdjustmentOrderDetailUnitInfo> AdjustmentOrderDetailUnitList { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int no { get; set; }

        public string customer_id { get; set; }
    }
}
