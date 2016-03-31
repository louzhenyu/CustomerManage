using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Order.Response
{
    /// <summary>
    /// 4.4.4	获取会员个人中心订单列表响应内容
    /// </summary>
    public class GetOrdersRD : IAPIResponseData
    {
        /// <summary>
        /// 当前页码，将请求的页码数同时返回给客户端
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 订单分组的订单数量
        /// </summary>
        public GroupingOrderCount[] GroupingOrderCounts { get; set; }
        /// <summary>
        /// 当前页的订单列表。
        /// </summary>
        public OrderInfo[] Orders { get; set; }


    }

    public class GroupingOrderCount
    {
        /// <summary>
        /// 分组方式。1=待付款;2=待收货/提货;3=已完成
        /// </summary>
        public int GroupingType { get; set; }
        /// <summary>
        /// 分组内订单数量
        /// </summary>
        public int OrderCount { get; set; }

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
        /// 提货门店
        /// </summary>
        public string purchase_unit_id { get; set; }
        /// <summary>
        /// 下单时间。注意：订单表的Order_date时间没有时分秒。因此此字段取的是Create_time的值,由于有时间转化出现T的情况。改为string
        /// </summary>
        public string OrderDate { get; set; }
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
        /// 订单商品列表
        /// </summary>
        public OrderDetailInfo[] OrderDetails { get; set; }

        /// <summary>
        /// 支付方式号码,T_Payment_Type.Payment_Type_Code
        /// 如果是货到付款的支付方 PaymentTypeCode 的值为GetToPay
        /// 终端需要特殊处理,不显示支付的按钮
        /// </summary>
        public string PaymentTypeCode { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal ReturnCash { get; set; }
        /// <summary>
        /// 是否评论0=未评论；1=已评论
        /// </summary>
        public int IsEvaluation { get; set; }

        /// <summary>
        /// 是否包含非标准商品 0-非标准商品 1-标准商品
        /// </summary>
        public int IsIncludeGB { get; set; }

        /// <summary>
        /// 是否付款  0-未付款 1-付款
        /// </summary>
        public string IsPayed { get; set; }

        
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
        /// 商品编码
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// SKU ID
        /// </summary>
        public string SKUID { get; set; }
        public string SkuCode { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public decimal Qty { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DiscountAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal ReturnCash { get; set; }

        public GuiGeInfo GG { get; set; }

        /// <summary>
        /// 是否为标准商品 0-非标准商品 1-标准商品
        /// </summary>
        public int isGB { get; set; }



    }
    public class GuiGeInfo
    {
        public string PropName1 { get; set; }
        public string PropDetailName1 { get; set; }
        public string PropName2 { get; set; }
        public string PropDetailName2 { get; set; }
        public string PropName3 { get; set; }
        public string PropDetailName3 { get; set; }
        public string PropName4 { get; set; }
        public string PropDetailName4 { get; set; }
        public string PropName5 { get; set; }
        public string PropDetailName5 { get; set; }
    }
}
