/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    public partial class SpecialDateEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SpecialDateEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? SpecialId { get; set; }

		/// <summary>
		/// �����ͱ�ʶ
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// ���ձ�ʶ
		/// </summary>
		public String HolidayID { get; set; }

		/// <summary>
		/// �����û���
		/// </summary>
		public Int32? NoAvailablePoints { get; set; }

		/// <summary>
		/// �������ۿ�
		/// </summary>
		public Int32? NoAvailableDiscount { get; set; }

		/// <summary>
		/// ���ɻ�������
		/// </summary>
		public Int32? NoRewardPoints { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Desciption { get; set; }

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
		public String CustomerID { get; set; }


        #endregion

    }
}