/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/21 11:38:21
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
    public partial class MarketEventEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MarketEventEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ����ʶ
		/// </summary>
		public String MarketEventID { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public String EventCode { get; set; }

		/// <summary>
		/// Ʒ�Ʊ�ʶ
		/// </summary>
		public String BrandID { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public String EventType { get; set; }

		/// <summary>
		/// ���ʽ
		/// </summary>
		public String EventMode { get; set; }

		/// <summary>
		/// �״̬
		/// </summary>
		public Int32? EventStatus { get; set; }

		/// <summary>
		/// Ԥ���ܽ��
		/// </summary>
		public Decimal? BudgetTotal { get; set; }

		/// <summary>
		/// �˾��������
		/// </summary>
		public Decimal? PerCapita { get; set; }

		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public String BeginTime { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public String EndTime { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public String EventDesc { get; set; }

		/// <summary>
		/// �Ƿ��в���
		/// </summary>
		public Int32? IsWaveBand { get; set; }

		/// <summary>
		/// �ŵ�����
		/// </summary>
		public Int32? StoreCount { get; set; }

		/// <summary>
		/// ��Ⱥ����
		/// </summary>
		public Int32? PersonCount { get; set; }

		/// <summary>
		/// ģ���ʶ
		/// </summary>
		public String TemplateID { get; set; }

		/// <summary>
		/// ͳ�Ʊ�ʶ
		/// </summary>
		public String StatisticsID { get; set; }

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
		/// ģ������
		/// </summary>
		public String TemplateContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TemplateContentSMS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SendTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TemplateContentAPP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}