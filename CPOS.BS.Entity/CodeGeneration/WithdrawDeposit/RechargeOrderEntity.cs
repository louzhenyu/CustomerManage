/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:14:28
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
    public partial class RechargeOrderEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public RechargeOrderEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? OrderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// ��Ա����
		/// </summary>
		public String VipCardNo { get; set; }

		/// <summary>
		/// �ŵ���
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// ���
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// ʵ�����
		/// </summary>
		public Decimal? ActuallyPaid { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public Decimal? ReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PayPoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReceivePoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public String UnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AmountFromPayPoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PayDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeId { get; set; }


        #endregion

    }
}