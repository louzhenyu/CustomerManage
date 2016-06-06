/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 14:15:31
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
    public partial class IincentiveRuleEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public IincentiveRuleEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? IincentiveRuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SetoffEventID { get; set; }

		/// <summary>
		/// 1 ��Ա����   2 Ա������
		/// </summary>
		public Int32? SetoffType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 1:�ֽ� 2������
		/// </summary>
		public Int32? SetoffRegAwardType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SetoffRegPrize { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SetoffOrderPer { get; set; }

		/// <summary>
		/// �ջ���0��ʾ������Ч��>0��ʾ�������ƴ���
		/// </summary>
		public Int32? SetoffOrderTimers { get; set; }

		/// <summary>
		/// 10��ʹ����   90��ʧЧ
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

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
		/// 0 ȫ���ŵ�   1 ָ���ŵ� 
		/// </summary>
		public Int32? ApplyUnit { get; set; }


        #endregion

    }
}