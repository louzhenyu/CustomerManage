/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:47
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
    public partial class PanicbuyingKJEventJoinDetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PanicbuyingKJEventJoinDetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? EventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SkuId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WXName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenId { get; set; }

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
		public Decimal? BargainPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? MomentSalesPrice { get; set; }

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
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? KJEventJoinId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? KJEventJoinDetailId { get; set; }


        #endregion

    }
}