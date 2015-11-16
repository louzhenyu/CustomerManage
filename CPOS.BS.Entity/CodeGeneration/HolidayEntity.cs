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
    public partial class HolidayEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public HolidayEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? HolidayId { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String HolidayName { get; set; }

		/// <summary>
		/// ��ʼ����
		/// </summary>
		public DateTime? BeginDate { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// ģ��
		/// </summary>
		public String Model { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String Workdays { get; set; }

		/// <summary>
		/// ѡ��(1��2��3)
		/// </summary>
		public String Options { get; set; }

		/// <summary>
		/// ��
		/// </summary>
		public String Months { get; set; }

		/// <summary>
		/// ��
		/// </summary>
		public String Weeks { get; set; }

		/// <summary>
		/// ��
		/// </summary>
		public String Days { get; set; }

		/// <summary>
		/// �ۿ�
		/// </summary>
		public Decimal? Discount { get; set; }

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