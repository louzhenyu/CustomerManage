using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Response
{
    public class GetOrderListRD:IAPIResponseData
    {
        /// <summary>
        /// 当前页的订单列表。
        /// </summary>
        public OrderInfo[] OrderList { get; set; }
    }

    public class OrderInfo
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderNO { get; set; }
        /// <summary>
        /// 配送方式类别ID。值 1=送货到家;2=到店自提;
        /// </summary>
        public int DeliveryTypeID { get; set; }
        /// <summary>
        /// 下单时间。注意：订单表的Order_date时间没有时分秒。因此此字段取的是Create_time的值
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 订单状态描述
        /// </summary>
        public string OrderStatusDesc { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 商品购买数量
        /// </summary>
        public int TotalQty { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 订单商品项
        /// </summary>
        public OrderDetailInfo[] OrderDetails { get; set; }
    }
    public class OrderDetailInfo
    {
        /// <summary>
        /// 订单商品项ID
        /// </summary>
        public string OrderDetailID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ItemID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// SKU ID
        /// </summary>
        public string SKUID { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 规格描述
        /// </summary>
        public string SpecificationDesc { get; set; }
        /// <summary>
        /// 实际单价
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// SKU图片
        /// </summary>
        public string ImageUrl { get; set; }

        public GuiGeInfo GG { get; set; }
    }
}
