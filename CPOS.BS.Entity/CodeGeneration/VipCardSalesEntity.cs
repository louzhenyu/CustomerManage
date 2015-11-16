/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:28
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
    public partial class VipCardSalesEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardSalesEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String SalesID { get; set; }

		/// <summary>
		/// 会员卡标识
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// 消费金额
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// 消费前金额
		/// </summary>
		public Decimal? SalesBeforeAmount { get; set; }

		/// <summary>
		/// 消费后金额
		/// </summary>
		public Decimal? SalesAfterAmount { get; set; }

		/// <summary>
		/// 关联订单号
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 消费门店
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 消费时间
		/// </summary>
		public DateTime? SalesTime { get; set; }

		/// <summary>
		/// 操作人
		/// </summary>
		public String OperationUserID { get; set; }

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


        #endregion

    }
}