using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Response
{
    public class GetOrderDetailRD : IAPIResponseData
    {
        public OrderListInfo OrderListInfo { get; set; } 
    }
    public class OrderListInfo
    {
        #region 订单信息

        public decimal discount_rate { get; set; }
        public string OrderID { get; set; }
        public string OrderCode { get; set; }
        public string OrderDate { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remark { get; set; }

        public decimal ActualDecimal { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }
        public string StatusDesc { get; set; }

        public string ClinchTime { get; set; }
        /// <summary>
        /// 成交时间
        /// </summary>
        public string CarrierID { get; set; }
        public string CarrierName { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string CourierNumber { get; set; }
        /// <summary>
        /// 发票信息
        /// </summary>
        public string Invoice { get; set; }
        /// <summary>
        /// 收货时间
        /// </summary>
        public string ReceiptTime { get; set; }
      //  public string CreateTime { get; set; }
        public OrderDetailEntity[] OrderDetailInfo { get; set; }
        public string StoreID { get; set; }
       // public string Timestamp { get; set; }
        /// <summary>
        /// 订单积分
        /// </summary>
        public decimal? ReceivePoints { get; set; }
        /// <summary>
        /// 使用积分
        /// </summary>
        public decimal OrderIntegral { get; set; }
        /// <summary>
        /// 积分抵扣金额
        /// </summary>
        public decimal UseIntegralToAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public decimal VipEndAmount { get; set; }
        /// <summary>
        /// 使用的返现金额
        /// </summary>
        public decimal ReturnAmount { get; set; }
      
        //public string Consignee { get; set; }
        /// <summary>
        /// 是否支付
        /// </summary>
        public string IsPayment { get; set; }

       /// <summary>
       /// 收件人
       /// </summary>
        public string ReceiverName { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Postcode { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string PaymentTime { get; set; }
        public string Mobile { get; set; }
        #endregion
        #region 配送信息
        /// <summary>
        /// 配送方式ID
        /// </summary>
        public string DeliveryID { get; set; }
        /// <summary>
        /// 配送备注
        /// </summary>
        public string DeliveryRemark { get; set; }
        /// <summary>
        /// 配送地址
        /// </summary>
        public string DeliveryAddress { get; set; }
        /// <summary>
        /// 配送时间
        /// </summary>
        public string DeliveryTime { get; set; }
       /// <summary>
       /// 配送方式
       /// </summary>
        public string DeliveryName { get; set; }

        #endregion

        #region 门店信息
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreTel { get; set; }
        #endregion

      

        #region 会员信息
        /// <summary>
        /// 会员等级
        /// </summary>
        public int VipLevel { get; set; }

        public string VipCode { get; set; }
        /// <summary>
        /// 会员等级描述
        /// </summary>
        public string VipLevelDesc { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string OpenID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// 会员真实名字
        /// </summary>
        public string VipRealName { get; set; }
        public int VipIntegral { get; set; }
        #endregion
        /// <summary>
        /// 优惠劵提醒语
        /// </summary>
        public string CouponsPrompt { get; set; }

        
        /// <summary>
        /// 支付方式,例如 GetToPay
        /// </summary>
        public string PaymentTypeCode { get; set; }
        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string PaymentTypeName { get; set; }
        /// <summary>
        /// 配送费
        /// </summary>
        public decimal DeliveryAmount { get; set; }
        /// <summary>
        /// 订单是否有效
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 使用阿拉币个数
        /// </summary>
        public decimal ALDAmount { get; set; }
        /// <summary>
        /// 使用阿拉币抵扣金额
        /// </summary>
        public decimal ALDAmountMoney { get; set; }
        /// <summary>
        /// 是否是活动 1=团购商品；0=普通商品
        /// </summary>
        public int IsEvent { get; set; }
        /// <summary>
        /// 是否已评论 0=未评；2=已评
        /// </summary>
        public int IsEvaluation { get; set; }
    }
    public class OrderDetailEntity
    {
        public string SkuID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
       public GuiGeInfo GG { get; set; }

        public decimal SalesPrice { get; set; }
        public decimal StdPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal Qty { get; set; }
        public OrderDetailImage[] ImageInfo { get; set; }
        public string ItemCategoryName { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int DayCount { get; set; }
        /// <summary>
        /// 退换货状态 0=可申请退换货；0<不可申请退换货
        /// </summary>
        public int SalesReturnFlag { get; set; }

        public int IfService { get; set; }
    }
    public class OrderDetailImage
    {
        public string ImageID { get; set; }
        public string ImageUrl { get; set; }
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
