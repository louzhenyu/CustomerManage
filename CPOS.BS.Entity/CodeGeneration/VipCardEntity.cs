/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/28 11:20:43
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
    public partial class VipCardEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// �����ͱ�ʶ
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// ���ȼ���ʶ
		/// </summary>
		public Int32? VipCardGradeID { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String VipCardISN { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String VipCardName { get; set; }

		/// <summary>
		/// �ƿ�����
		/// </summary>
		public String BatchNo { get; set; }

		/// <summary>
		/// ��״̬(0δ���1������2���ᣬ3ʧЧ��4��ʧ��5����)
		/// </summary>
		public Int32? VipCardStatusId { get; set; }

		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime? MembershipTime { get; set; }

		/// <summary>
		/// ����ŵ�
		/// </summary>
		public String MembershipUnit { get; set; }

		/// <summary>
		/// ����ʼʱ��
		/// </summary>
		public String BeginDate { get; set; }

		/// <summary>
		/// ������ʱ��
		/// </summary>
		public String EndDate { get; set; }

		/// <summary>
		/// �����ܽ��
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public Decimal? BalanceAmount { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public Decimal? BalancePoints { get; set; }

		/// <summary>
		/// ���⽱�����
		/// </summary>
		public Decimal? BalanceBonus { get; set; }

		/// <summary>
		/// �ۻ����⽱��
		/// </summary>
		public Decimal? CumulativeBonus { get; set; }

		/// <summary>
		/// �ۼƹ�����
		/// </summary>
		public Decimal? PurchaseTotalAmount { get; set; }

		/// <summary>
		/// �ۼ��µ���
		/// </summary>
		public Int32? PurchaseTotalCount { get; set; }

		/// <summary>
		/// У�鴮
		/// </summary>
		public String CheckCode { get; set; }

		/// <summary>
		/// ���ʽ����޶�
		/// </summary>
		public Decimal? SingleTransLimit { get; set; }

		/// <summary>
		/// �Ƿ�����֤
		/// </summary>
		public Int32? IsOverrunValid { get; set; }

		/// <summary>
		/// �ۻ���ֵ
		/// </summary>
		public Decimal? RechargeTotalAmount { get; set; }

		/// <summary>
		/// �������ʱ��
		/// </summary>
		public DateTime? LastSalesTime { get; set; }

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public Int32? IsGift { get; set; }

		/// <summary>
		/// �ۿ����
		/// </summary>
		public String SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesUserId { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String SalesUserName { get; set; }


        #endregion

    }
}