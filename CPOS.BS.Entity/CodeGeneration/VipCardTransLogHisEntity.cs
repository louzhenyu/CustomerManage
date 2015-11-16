/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:11
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
    public partial class VipCardTransLogHisEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardTransLogHisEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Int32? TransHisID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TransID { get; set; }

		/// <summary>
		/// ��Ա���
		/// </summary>
		public String VipCode { get; set; }

		/// <summary>
		/// ��Ա����
		/// </summary>
		public String VipName { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String VipCardTypeCode { get; set; }

		/// <summary>
		/// �ŵ����
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// �ŵ�����
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// ���ݺ�
		/// </summary>
		public String BillNo { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String TransType { get; set; }

		/// <summary>
		/// �����ն�����
		/// </summary>
		public String TransTerminalType { get; set; }

		/// <summary>
		/// �����ն˱���
		/// </summary>
		public String TransTerminalCode { get; set; }

		/// <summary>
		/// ���׵ص�
		/// </summary>
		public String TransAddress { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String TransContent { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? TransTime { get; set; }

		/// <summary>
		/// ���׽��
		/// </summary>
		public Decimal? TransAmount { get; set; }

		/// <summary>
		/// ���׺����
		/// </summary>
		public Decimal? TransBalance { get; set; }

		/// <summary>
		/// �������JSON
		/// </summary>
		public String RequestJSON { get; set; }

		/// <summary>
		/// У�鴮
		/// </summary>
		public String CheckCode { get; set; }

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