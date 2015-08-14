/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 11:23:51
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
    public partial class AgentCustomerEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AgentCustomerEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String AgentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AgentName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AgentPhone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AgentMail { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AgentPost { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CompanyName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CompanyScale { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? StoreNumber { get; set; }

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
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TryOrAgent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FromSource { get; set; }


        #endregion

    }
}