/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/8 10:09:15
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
    public partial class VwMarketEventAnalysisEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VwMarketEventAnalysisEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String MarketEventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? StoreCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ResponseStoreCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ResponseStoreRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PersonCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PesponsePersonCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ResponsePersonRate { get; set; }

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