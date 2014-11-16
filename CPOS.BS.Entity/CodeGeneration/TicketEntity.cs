/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/7 15:07:52
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
    public partial class TicketEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TicketEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// Ʊ��ID
		/// </summary>
		public Guid? TicketID { get; set; }

		/// <summary>
		/// Ʊ������
		/// </summary>
		public string TicketName { get; set; }

		/// <summary>
		/// Ʊ��ע
		/// </summary>
		public string TicketRemark { get; set; }

		/// <summary>
		/// Ʊ��۸�
		/// </summary>
		public decimal? TicketPrice { get; set; }

		/// <summary>
		/// Ʊ������
		/// </summary>
		public int? TicketNum { get; set; }

		/// <summary>
		/// Ʊ������
		/// </summary>
		public int? TicketSort { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public string EventID { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// ����޸���
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// ����޸�ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
		/// �ͻ���ʶ
		/// </summary>
		public string CustomerID { get; set; }


        #endregion

    }
}