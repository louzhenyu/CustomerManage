/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/11 9:12:02
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
    public partial class IntegralRuleEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public IntegralRuleEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String IntegralRuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IntegralSourceID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Integral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IntegralDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

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


        #endregion

    }
}