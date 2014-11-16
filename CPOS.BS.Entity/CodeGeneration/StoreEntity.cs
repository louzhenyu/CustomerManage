/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 17:59:13
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
    public partial class StoreEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public StoreEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String StoreID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StoreCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StoreName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BusinessDistrict { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Address { get; set; }

		/// <summary>
		/// 会员数
		/// </summary>
		public Int32? MembersCount { get; set; }

		/// <summary>
		/// 年销售额
		/// </summary>
		public Decimal? SalesYear { get; set; }

		/// <summary>
		/// 开业时间
		/// </summary>
		public String Opened { get; set; }

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
		/// 经度
		/// </summary>
		public String Longitude { get; set; }

		/// <summary>
		/// 维度
		/// </summary>
		public String Latitude { get; set; }


        #endregion

    }
}