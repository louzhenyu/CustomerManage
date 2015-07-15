/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-3 10:30:22
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
    public partial class T_SalesReturnHistoryEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SalesReturnHistoryEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? HistoryID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SalesReturnID { get; set; }

		/// <summary>
		/// 1.����   2.���ͨ��   3.��˲�ͨ��   4.ȷ���ջ�   5.�ܾ��ջ�   6.�˿�
		/// </summary>
		public Int32? OperationType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OperationDesc { get; set; }

		/// <summary>
		/// 1.����ȡ�����ɹ�ԭ��   2.�����޸�ȡ��ʱ�� ����ĳʱ�����ĳʱ�䡱
		/// </summary>
		public String HisRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OperatorID { get; set; }

		/// <summary>
		/// 0=��Ա   1=��Ա
		/// </summary>
		public Int32? OperatorType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OperatorName { get; set; }

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


        #endregion

    }
}