/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/23 23:09:59
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
    /// 实体： 操作历史记录表：购买 支付 赎回（三种操作 都需要记录在此表中）:一个订单购买/赎回/支付 只会有一条记录，以后每次只需更新状态即可 
    /// </summary>
    public partial class WXHouseTransactionRecordEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseTransactionRecordEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? RecordID { get; set; }

		/// <summary>
		/// 订单表标识
		/// </summary>
		public Guid? PrePaymentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TraderNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDate { get; set; }

		/// <summary>
		/// 买家信息=客户协议号
		/// </summary>
		public String Assignbuyer { get; set; }

		/// <summary>
		/// 商户日期格式：年月日（20140618）
		/// </summary>
		public String SeqNO { get; set; }

		/// <summary>
		/// 世联交易日期
		/// </summary>
		public String TraderDate { get; set; }

		/// <summary>
		///  未知 Unknown = 0,   成功  Success = 1,    失败 Error = 2,
		/// </summary>
		public Int32? FundState { get; set; }

		/// <summary>
		/// 申购 ReservationPurchase = 1,    赎回 ReservationRedeem = 2,    支付 ReservationPay = 3
		/// </summary>
		public Int32? Fundtype { get; set; }

		/// <summary>
		/// 本次交易流水金额
		/// </summary>
		public Decimal? TraderAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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


        #endregion

    }
}