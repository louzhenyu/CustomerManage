/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/6 13:39:18
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
    public partial class MarketEventResponseEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MarketEventResponseEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ReponseID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MarketEventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

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
		/// 客单价
		/// </summary>
		public Decimal? CustomerPrice { get; set; }

		/// <summary>
		/// 件单价
		/// </summary>
		public Decimal? UnitPrice { get; set; }

		/// <summary>
		/// 购买件数
		/// </summary>
		public Int32? PurchaseNumber { get; set; }

		/// <summary>
		/// 消费积分
		/// </summary>
		public Int32? SalesIntegral { get; set; }

		/// <summary>
		/// 购买金额
		/// </summary>
		public Decimal? PurchaseAmount { get; set; }

		/// <summary>
		/// 购买次数
		/// </summary>
		public Int32? PurchaseCount { get; set; }

		/// <summary>
		/// 会员微信唯一码
		/// </summary>
		public String OpenID { get; set; }

		/// <summary>
		/// 商品名称
		/// </summary>
		public String ProductName { get; set; }

		/// <summary>
		/// 是否购买
		/// </summary>
		public Int32? IsSales { get; set; }


        #endregion

    }
}