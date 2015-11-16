/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/26 20:41:45
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
    public partial class ContactEventEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ContactEventEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? ContactEventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContactTypeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContactEventName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 1�����֣�2���Ż�ȯ��3���μӻ����
		/// </summary>
		public Int32? RewardType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Integral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ChanceCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

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


        #endregion

    }
}