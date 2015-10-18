/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-19 11:35:13
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
    public partial class T_Payment_TypeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Payment_TypeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String Payment_Type_Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Payment_Type_Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Payment_Type_Code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PaymentCompany { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PaymentItemType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LogoURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PaymentDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

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
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsJSPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsNativePay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsCSPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}