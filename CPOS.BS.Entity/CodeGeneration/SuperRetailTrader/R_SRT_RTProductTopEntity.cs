/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 14:12:49
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
    public partial class R_SRT_RTProductTopEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_SRT_RTProductTopEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 1：30天分享次数   2：30天订单数量
		/// </summary>
		public Int32? BusiType { get; set; }

		/// <summary>
		/// 1：正排名   2：倒排名
		/// </summary>
		public Int32? TopType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Idx { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ShareCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }


        #endregion

    }
}