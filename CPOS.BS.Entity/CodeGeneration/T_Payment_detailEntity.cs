/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-19 17:56:45
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
    public partial class T_Payment_detailEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_Payment_detailEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String Payment_Id { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Inout_Id { get; set; }

		/// <summary>
		/// �ŵ���
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// ֧������
		/// </summary>
		public String Payment_Type_Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Payment_Type_Code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Payment_Type_Name { get; set; }

		/// <summary>
		/// �۸�
		/// </summary>
		public Decimal? Price { get; set; }

		/// <summary>
		/// �ܶ�
		/// </summary>
		public Decimal? Total_Amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? If_Flag { get; set; }

		/// <summary>
		/// ֧������
		/// </summary>
		public Decimal? Pay_Points { get; set; }

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
		public String CustomerId { get; set; }


        #endregion

    }
}