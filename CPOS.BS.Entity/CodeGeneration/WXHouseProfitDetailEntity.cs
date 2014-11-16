/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 22:14:03
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
    /// 实体： 收益历史记录表 
    /// </summary>
    public partial class WXHouseProfitDetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseProfitDetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ProfitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AssetsDate { get; set; }

		/// <summary>
		/// 买家信息为：客户协议号
		/// </summary>
		public String Assignbuyer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ThirdOrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAssetsMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AvailableMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? GrandProfit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? NewProfit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OtherProfit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NoteInformation { get; set; }

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