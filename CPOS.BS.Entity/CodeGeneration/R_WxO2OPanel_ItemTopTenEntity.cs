/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 18:30:11
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
    public partial class R_WxO2OPanel_ItemTopTenEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_WxO2OPanel_ItemTopTenEntity()
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
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ItemType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SortIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ItemSoldCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ItemSoldAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ItemUV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ItemPV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LogIDs { get; set; }


        #endregion

    }
}