/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/1/8 17:55:51
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
    /// 实体： 分销商包含门店和个人 
    /// </summary>
    public partial class RetailTraderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public RetailTraderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? RetailTraderCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderLogin { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderPass { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderMan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderPhone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetailTraderAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CooperateType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SellUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitID { get; set; }

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
		public String Status { get; set; }


        #endregion

    }
}