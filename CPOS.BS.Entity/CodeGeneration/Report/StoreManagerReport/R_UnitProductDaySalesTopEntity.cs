/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:42
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
    public partial class R_UnitProductDaySalesTopEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_UnitProductDaySalesTopEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StoreType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TopIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SkuID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SkuName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}