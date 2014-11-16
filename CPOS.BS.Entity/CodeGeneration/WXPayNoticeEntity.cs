/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/7 13:41:42
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
    public partial class WXPayNoticeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXPayNoticeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? PayNoticeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SignType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ServiceVersion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String InputCharset { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Sign { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SignKeyIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TradeMode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TradeState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Partner { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BankType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BankBillno { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TotalFee { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? FeeType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NotifyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TransactionId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OutTradeNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Attach { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TimeEnd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TransportFee { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ProductFee { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Discount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BuyerAlias { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TimeStamp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NonceStr { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppSignature { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSubscribe { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}