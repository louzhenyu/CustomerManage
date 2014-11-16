/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:19:14
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
    /// ʵ�壺  
    /// </summary>
    public partial class EEnterpriseCustomersEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EEnterpriseCustomersEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String EnterpriseCustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IndustryId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ScaleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CityId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Tel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ECSourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

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
		public String EnterpriseCustomerName { get; set; }


        #endregion

    }
}