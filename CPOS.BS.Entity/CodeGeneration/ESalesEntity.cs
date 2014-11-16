/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:20
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
    public partial class ESalesEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ESalesEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String SalesId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EnterpriseCustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesProductId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesVipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ECSourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Possibility { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ForecastAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

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


        #endregion

    }
}