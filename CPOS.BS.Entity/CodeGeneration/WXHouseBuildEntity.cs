/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/6 13:13:26
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
    public partial class WXHouseBuildEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseBuildEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? HouseID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HouseCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HouseName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HouseImgURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Coordinate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Hotline { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HouseAddr { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LowestPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SaleHoseNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? HouseOpenDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DeliverDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? HouseState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public Int32? IsDelete { get; set; }


        #endregion

    }
}