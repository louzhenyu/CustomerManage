/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/9 16:52:55
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
    public partial class VwVipPosOrderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VwVipPosOrderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Phone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RewardAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FansAwards { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TransactionAwards { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RewardTotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesUnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PurchaseUnitId { get; set; }

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
		public String StatusId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StatusDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesUnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PurchaseUnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TotalQty { get; set; }


        #endregion

    }
}