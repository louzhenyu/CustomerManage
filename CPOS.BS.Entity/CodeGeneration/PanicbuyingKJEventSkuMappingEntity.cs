/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/27 13:54:52
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
    public partial class PanicbuyingKJEventSkuMappingEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PanicbuyingKJEventSkuMappingEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? EventSKUMappingId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventItemMappingID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SkuID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddedTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Qty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? KeepQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SoldQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SinglePurchaseQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Price { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BasePrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BargainStartPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BargainEndPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 1=显示   0=不显示
		/// </summary>
		public Int32? IsFirst { get; set; }

		/// <summary>
		/// -1 = 不上架   1 = 上架   
		/// </summary>
		public Int32? Status { get; set; }

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
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}