/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/14 10:33:32
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
    public partial class LPrizesEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LPrizesEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String PrizesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PrizeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PrizeShortDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PrizeDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LogoURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContentText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContentUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Price { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CountTotal { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CountLeft { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventId { get; set; }

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
		public string   PrizeTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Point { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAutoPrizes { get; set; }
        /// <summary>
        /// 奖品等级
        /// </summary>
        public int PrizeLevel { get; set; }

        public decimal Probability { get; set; }
        

        #endregion

    }
}