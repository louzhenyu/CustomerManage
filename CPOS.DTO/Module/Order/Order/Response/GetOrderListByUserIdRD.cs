using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Response
{
    public class GetOrderListByUserIdRD : IAPIResponseData
    {
        /// <summary>
        /// 当前页的订单列表。
        /// </summary>
        public OrdersInfo[] OrderList { get; set; }
    }

    public class OrdersInfo
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
        public string DeliveryTypeID { get; set; }
        /// <summary>
        /// 下单时间。注意：订单表的Order_date时间没有时分秒。因此此字段取的是Create_time的值
        /// </summary>
        public string OrderDate { get; set; }

        public string VipName { get; set; }
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
        public decimal TotalQty { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 订单商品项
        /// </summary>
        //public OrderDetailInfo[] OrderDetails { get; set; }
    }
}
