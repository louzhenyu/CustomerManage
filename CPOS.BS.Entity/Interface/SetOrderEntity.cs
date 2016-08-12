using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Interface
{
    /// <summary>
    /// 电子商城提交订单
    /// </summary>
    public class SetOrderEntity
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrderCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrderDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String StoreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SkuId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? TotalQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal SalesPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal StdPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal DiscountRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal TotalAmount { get; set; }
        /// <summary>
        /// 实际支付金额 Jermyn20131215
        /// </summary>
        public Decimal ActualAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Mobile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String StatusDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PaymentTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal PaymentAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PaymentTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DeliveryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DeliveryAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DeliveryTime { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户微信唯一码
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName { get; set; }

        public string deliveryName { get; set; }

        public string username { get; set; }
        /// <summary>
        /// 与订单号对应的外部交易号
        /// </summary>
        public string OutTradeNo { get; set; }

        public string itemId { get; set; }
        /// <summary>
        /// 订单详细信息
        /// </summary>
        public IList<InoutDetailInfo> OrderDetailInfoList { get; set; }
        /// <summary>
        /// 发票
        /// </summary>
        public string Invoice { get; set; }
        /// <summary>
        /// 桌号对应Field20
        /// </summary>
        public string tableNumber { get; set; }

        public string PurchaseUnitId { get; set; }
        /// <summary>
        /// 优惠券提示语（Jermyn20131213--Field16）
        /// </summary>
        public string CouponsPrompt { get; set; }
        /// <summary>
        /// 优惠券集合
        /// </summary>
        public IList<TOrderCouponMappingEntity> CouponList { get; set; }


        public string linkMan { get; set; }//收货人
        public string linkTel { get; set; }//收货电话
        public string address { get; set; }//收货地址

        public int JoinNo { get; set; } //Jermyn20140312

        public string IsALD { get; set; } //Jermyn20140314 是否同步到阿拉丁

        public string IsGroupBuy { get; set; }  //是否团购订单（Jermyn20140318—Field15）

        public string CarrierID { get; set; }//门店自提门店ID

        public int DataFromId { get; set; } //Updated By Willie Yan on 2014-05-06   订单来源

        public string SalesUser { get; set; } //店员ID add by donal 2014-9-25 18:08:16

        public string ChannelId { get; set; } //渠道ID add by donal 2014-9-28 14:29:00

        public decimal ReturnCash { get; set; } //佣金  add by donal 2014-12-9 10:37:10

        public string VipCardCode { get; set; } //卡号，券号

        public decimal Integral { get; set; } //使用积分

        public decimal IntegralAmount { get; set; } //积分转金额

        public string Field17 { get; set; } //会员卡号

        public string Field18 { get; set; } //会员卡类型
        
        #endregion
    }
}
