/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/6 14:23:25
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
    public partial class VwUnitPropertyEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VwUnitPropertyEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String UnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsWeixinPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSMSPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAPPPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAPP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FirstPageImage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LoginImage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ProductsBackgroundImage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FansAwards { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TransactionAwards { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinUnitCode { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? StockCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Distance { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ADDRESS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Tel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsCallSMSPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsCallEmailPush { get; set; }


        #endregion

    }
}