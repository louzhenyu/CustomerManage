/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/28 11:20:43
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
    public partial class VipCardEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// 卡类型标识
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// 卡等级标识
		/// </summary>
		public Int32? VipCardGradeID { get; set; }

		/// <summary>
		/// 卡号
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// 卡内码
		/// </summary>
		public String VipCardISN { get; set; }

		/// <summary>
		/// 卡名称
		/// </summary>
		public String VipCardName { get; set; }

		/// <summary>
		/// 制卡批次
		/// </summary>
		public String BatchNo { get; set; }

		/// <summary>
		/// 卡状态(0未激活，1正常，2冻结，3失效，4挂失，5休眠)
		/// </summary>
		public Int32? VipCardStatusId { get; set; }

		/// <summary>
		/// 入会时间
		/// </summary>
		public DateTime? MembershipTime { get; set; }

		/// <summary>
		/// 入会门店
		/// </summary>
		public String MembershipUnit { get; set; }

		/// <summary>
		/// 卡开始时间
		/// </summary>
		public String BeginDate { get; set; }

		/// <summary>
		/// 卡结束时间
		/// </summary>
		public String EndDate { get; set; }

		/// <summary>
		/// 卡内总金额
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 卡内余额
		/// </summary>
		public Decimal? BalanceAmount { get; set; }

		/// <summary>
		/// 积分余额
		/// </summary>
		public Decimal? BalancePoints { get; set; }

		/// <summary>
		/// 额外奖励余额
		/// </summary>
		public Decimal? BalanceBonus { get; set; }

		/// <summary>
		/// 累积额外奖励
		/// </summary>
		public Decimal? CumulativeBonus { get; set; }

		/// <summary>
		/// 累计购买金额
		/// </summary>
		public Decimal? PurchaseTotalAmount { get; set; }

		/// <summary>
		/// 累计下单数
		/// </summary>
		public Int32? PurchaseTotalCount { get; set; }

		/// <summary>
		/// 校验串
		/// </summary>
		public String CheckCode { get; set; }

		/// <summary>
		/// 单笔交易限额
		/// </summary>
		public Decimal? SingleTransLimit { get; set; }

		/// <summary>
		/// 是否超限验证
		/// </summary>
		public Int32? IsOverrunValid { get; set; }

		/// <summary>
		/// 累积充值
		/// </summary>
		public Decimal? RechargeTotalAmount { get; set; }

		/// <summary>
		/// 最近消费时间
		/// </summary>
		public DateTime? LastSalesTime { get; set; }

		/// <summary>
		/// 是否赠送
		/// </summary>
		public Int32? IsGift { get; set; }

		/// <summary>
		/// 售卡金额
		/// </summary>
		public String SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesUserId { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesUserName { get; set; }


        #endregion

    }
}