/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/30 15:25:27
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
    public partial class VipIntegralDetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipIntegralDetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String VipIntegralDetailID { get; set; }

		/// <summary>
		/// 会员标识
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
		/// 会员卡号
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// 门店标识
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 门店名称
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 消费金额
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// 产生积分
		/// </summary>
		public Int32? Integral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UsedIntegral { get; set; }

		/// <summary>
		/// 原因
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// 积分来源标识
		/// </summary>
		public String IntegralSourceID { get; set; }

		/// <summary>
		/// 积分生效日期
		/// </summary>
		public DateTime? EffectiveDate { get; set; }

		/// <summary>
		/// 积分截止日期
		/// </summary>
		public DateTime? DeadlineDate { get; set; }

		/// <summary>
		/// 积分是否累计
		/// </summary>
		public Int32? IsAdd { get; set; }

		/// <summary>
		/// 来自会员标识
		/// </summary>
		public String FromVipID { get; set; }

		/// <summary>
		/// 对象标识（订单标识）
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public String Remark { get; set; }

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