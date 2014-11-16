/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-05-31 20:42
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class AlipayWapTradeResponseEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AlipayWapTradeResponseEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ResponseID { get; set; }

		/// <summary>
		/// 交易订单号。
		/// </summary>
		public String OrderID { get; set; }

		/// <summary>
		/// 对应商户网站的订单系统中的唯一订单号，非支付宝交易号。需保证在商户网站中的唯一性。是请求时对应的参数，原样返回。
		/// </summary>
		public String OutTradeNo { get; set; }

		/// <summary>
		/// 商品的标题/交易标题/订单标题/订单关键字等。它在支付宝的交易明细中排在第一列，对于财务对账尤为重要。是请求时对应的参数，原样通知回来。
		/// </summary>
		public String Subject { get; set; }

		/// <summary>
		/// 该笔订单的总金额。请求时对应的参数，原样通知回来。
		/// </summary>
		public String TotalFee { get; set; }

		/// <summary>
		/// 用户的支付方式：1：商品购买；4：捐赠。
		/// </summary>
		public String PaymentType { get; set; }

		/// <summary>
		/// 该交易在支付宝系统中的交易流水号。最短16位，最长64位。
		/// </summary>
		public String TradeNo { get; set; }

		/// <summary>
		/// 买家支付宝账号，可以是email或手机号码。
		/// </summary>
		public String BuyerEmail { get; set; }

		/// <summary>
		/// 该笔交易创建的时间。格式为yyyy-MM-dd HH:mm:ss。
		/// </summary>
		public String GmtCreate { get; set; }

		/// <summary>
		/// 通知的类型。固定值。trade_status_sync
		/// </summary>
		public String NotifyType { get; set; }

		/// <summary>
		/// 购买商品的数量。
		/// </summary>
		public String Quantity { get; set; }

		/// <summary>
		/// 通知的发送时间。格式为yyyy-MM-dd HH:mm:ss。
		/// </summary>
		public String NotifyTime { get; set; }

		/// <summary>
		/// 卖家支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字。
		/// </summary>
		public String SellerID { get; set; }

		/// <summary>
		/// 交易的状态。WAIT_BUYER_PAY：交易创建，等待买家付款。TRADE_CLOSED：在指定时间段内未支付时关闭的交易；在交易完成全额退款成功时关闭的交易。TRADE_SUCCESS：交易成功，且可对该交易做操作，如：多级分润、退款等。TRADE_PENDING：等待卖家收款（买家付款后，如果卖家账号被冻结）。TRADE_FINISHED：交易成功且结束，即不可再做任何操作。
		/// </summary>
		public String TradeStatus { get; set; }

		/// <summary>
		/// 该交易是否调整过价格。本接口创建的交易不会被修改总价，固定值为N。
		/// </summary>
		public String IsTotalFeeAdjust { get; set; }

		/// <summary>
		/// 该笔交易的买家付款时间。格式为yyyy-MM-dd HH:mm:ss。如果交易未付款，则不返回该参数。
		/// </summary>
		public String GmtPayment { get; set; }

		/// <summary>
		/// 卖家支付宝账号，可以是email和手机号码。
		/// </summary>
		public String SellerEmail { get; set; }

		/// <summary>
		/// 交易关闭时间。格式为yyyy-MM-dd HH:mm:ss。
		/// </summary>
		public String GmtClose { get; set; }

		/// <summary>
		/// 目前和total_fee值相同。单位：元。不应低于0.01元。
		/// </summary>
		public String Price { get; set; }

		/// <summary>
		/// 买家支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字。
		/// </summary>
		public String BuyerID { get; set; }

		/// <summary>
		/// 通知校验ID。唯一识别通知内容。重发相同内容的通知时，该值不变。
		/// </summary>
		public String NotifyID { get; set; }

		/// <summary>
		/// 是否在交易过程中使用了红包。
		/// </summary>
		public String UseCoupon { get; set; }

		/// <summary>
        /// 用户付款中途退出返回URL
		/// </summary>
		public String MerchantUrl { get; set; }

		/// <summary>
        /// 用户付款成功同步返回URL
		/// </summary>
		public String CallBackUrl { get; set; }

		/// <summary>
		/// 1: 等待对方付款  2： 支付成功
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}