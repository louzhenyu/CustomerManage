/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/23 23:09:59
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
    /// ʵ�壺 ������ʷ��¼������ ֧�� ��أ����ֲ��� ����Ҫ��¼�ڴ˱��У�:һ����������/���/֧�� ֻ����һ����¼���Ժ�ÿ��ֻ�����״̬���� 
    /// </summary>
    public partial class WXHouseTransactionRecordEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseTransactionRecordEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? RecordID { get; set; }

		/// <summary>
		/// �������ʶ
		/// </summary>
		public Guid? PrePaymentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TraderNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDate { get; set; }

		/// <summary>
		/// �����Ϣ=�ͻ�Э���
		/// </summary>
		public String Assignbuyer { get; set; }

		/// <summary>
		/// �̻����ڸ�ʽ�������գ�20140618��
		/// </summary>
		public String SeqNO { get; set; }

		/// <summary>
		/// ������������
		/// </summary>
		public String TraderDate { get; set; }

		/// <summary>
		///  δ֪ Unknown = 0,   �ɹ�  Success = 1,    ʧ�� Error = 2,
		/// </summary>
		public Int32? FundState { get; set; }

		/// <summary>
		/// �깺 ReservationPurchase = 1,    ��� ReservationRedeem = 2,    ֧�� ReservationPay = 3
		/// </summary>
		public Int32? Fundtype { get; set; }

		/// <summary>
		/// ���ν�����ˮ���
		/// </summary>
		public Decimal? TraderAmount { get; set; }

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