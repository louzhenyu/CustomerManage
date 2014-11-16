/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-05-31 20:42
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
    public partial class AlipayWapTradeResponseEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AlipayWapTradeResponseEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String ResponseID { get; set; }

		/// <summary>
		/// ���׶����š�
		/// </summary>
		public String OrderID { get; set; }

		/// <summary>
		/// ��Ӧ�̻���վ�Ķ���ϵͳ�е�Ψһ�����ţ���֧�������׺š��豣֤���̻���վ�е�Ψһ�ԡ�������ʱ��Ӧ�Ĳ�����ԭ�����ء�
		/// </summary>
		public String OutTradeNo { get; set; }

		/// <summary>
		/// ��Ʒ�ı���/���ױ���/��������/�����ؼ��ֵȡ�����֧�����Ľ�����ϸ�����ڵ�һ�У����ڲ��������Ϊ��Ҫ��������ʱ��Ӧ�Ĳ�����ԭ��֪ͨ������
		/// </summary>
		public String Subject { get; set; }

		/// <summary>
		/// �ñʶ������ܽ�����ʱ��Ӧ�Ĳ�����ԭ��֪ͨ������
		/// </summary>
		public String TotalFee { get; set; }

		/// <summary>
		/// �û���֧����ʽ��1����Ʒ����4��������
		/// </summary>
		public String PaymentType { get; set; }

		/// <summary>
		/// �ý�����֧����ϵͳ�еĽ�����ˮ�š����16λ���64λ��
		/// </summary>
		public String TradeNo { get; set; }

		/// <summary>
		/// ���֧�����˺ţ�������email���ֻ����롣
		/// </summary>
		public String BuyerEmail { get; set; }

		/// <summary>
		/// �ñʽ��״�����ʱ�䡣��ʽΪyyyy-MM-dd HH:mm:ss��
		/// </summary>
		public String GmtCreate { get; set; }

		/// <summary>
		/// ֪ͨ�����͡��̶�ֵ��trade_status_sync
		/// </summary>
		public String NotifyType { get; set; }

		/// <summary>
		/// ������Ʒ��������
		/// </summary>
		public String Quantity { get; set; }

		/// <summary>
		/// ֪ͨ�ķ���ʱ�䡣��ʽΪyyyy-MM-dd HH:mm:ss��
		/// </summary>
		public String NotifyTime { get; set; }

		/// <summary>
		/// ����֧�����˺Ŷ�Ӧ��֧����Ψһ�û��š���2088��ͷ�Ĵ�16λ���֡�
		/// </summary>
		public String SellerID { get; set; }

		/// <summary>
		/// ���׵�״̬��WAIT_BUYER_PAY�����״������ȴ���Ҹ��TRADE_CLOSED����ָ��ʱ�����δ֧��ʱ�رյĽ��ף��ڽ������ȫ���˿�ɹ�ʱ�رյĽ��ס�TRADE_SUCCESS�����׳ɹ����ҿɶԸý������������磺�༶�����˿�ȡ�TRADE_PENDING���ȴ������տ��Ҹ������������˺ű����ᣩ��TRADE_FINISHED�����׳ɹ��ҽ����������������κβ�����
		/// </summary>
		public String TradeStatus { get; set; }

		/// <summary>
		/// �ý����Ƿ�������۸񡣱��ӿڴ����Ľ��ײ��ᱻ�޸��ܼۣ��̶�ֵΪN��
		/// </summary>
		public String IsTotalFeeAdjust { get; set; }

		/// <summary>
		/// �ñʽ��׵���Ҹ���ʱ�䡣��ʽΪyyyy-MM-dd HH:mm:ss���������δ����򲻷��ظò�����
		/// </summary>
		public String GmtPayment { get; set; }

		/// <summary>
		/// ����֧�����˺ţ�������email���ֻ����롣
		/// </summary>
		public String SellerEmail { get; set; }

		/// <summary>
		/// ���׹ر�ʱ�䡣��ʽΪyyyy-MM-dd HH:mm:ss��
		/// </summary>
		public String GmtClose { get; set; }

		/// <summary>
		/// Ŀǰ��total_feeֵ��ͬ����λ��Ԫ����Ӧ����0.01Ԫ��
		/// </summary>
		public String Price { get; set; }

		/// <summary>
		/// ���֧�����˺Ŷ�Ӧ��֧����Ψһ�û��š���2088��ͷ�Ĵ�16λ���֡�
		/// </summary>
		public String BuyerID { get; set; }

		/// <summary>
		/// ֪ͨУ��ID��Ψһʶ��֪ͨ���ݡ��ط���ͬ���ݵ�֪ͨʱ����ֵ���䡣
		/// </summary>
		public String NotifyID { get; set; }

		/// <summary>
		/// �Ƿ��ڽ��׹�����ʹ���˺����
		/// </summary>
		public String UseCoupon { get; set; }

		/// <summary>
        /// �û�������;�˳�����URL
		/// </summary>
		public String MerchantUrl { get; set; }

		/// <summary>
        /// �û�����ɹ�ͬ������URL
		/// </summary>
		public String CallBackUrl { get; set; }

		/// <summary>
		/// 1: �ȴ��Է�����  2�� ֧���ɹ�
		/// </summary>
		public String Status { get; set; }

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