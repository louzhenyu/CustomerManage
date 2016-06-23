/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 10:56:31
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
    public partial class T_SuperRetailTraderConfigEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SuperRetailTraderConfigEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? MustBuyAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AgreementName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Agreement { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SkuCommission { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DistributionProfit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CustomerProfit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Cost { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? RefId { get; set; }

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
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}