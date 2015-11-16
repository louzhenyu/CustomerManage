/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/30 15:25:27
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
    public partial class VipIntegralEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipIntegralEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// ��Ա����
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// ��ʼ����
		/// </summary>
		public Int32? BeginIntegral { get; set; }

		/// <summary>
		/// ���ӻ���
		/// </summary>
		public Int32? InIntegral { get; set; }

		/// <summary>
		/// ���ѻ���
		/// </summary>
		public Int32? OutIntegral { get; set; }

		/// <summary>
		/// ���ջ���
		/// </summary>
		public Int32? EndIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ImminentInvalidIntegral { get; set; }

		/// <summary>
		/// �ۼ�ʧЧ����
		/// </summary>
		public Int32? InvalidIntegral { get; set; }

		/// <summary>
		/// ��ǰ��Ч����
		/// </summary>
		public Int32? ValidIntegral { get; set; }

		/// <summary>
		/// �ۻ�����
		/// </summary>
		public Int32? CumulativeIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ValidNotIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

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


        #endregion

    }
}