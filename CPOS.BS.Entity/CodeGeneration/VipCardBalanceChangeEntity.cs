/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 19:35:01
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
    public partial class VipCardBalanceChangeEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardBalanceChangeEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String ChangeID { get; set; }

		/// <summary>
		/// ��Ա����
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// �䶯���
		/// </summary>
		public Decimal? ChangeAmount { get; set; }

		/// <summary>
		/// �䶯ǰ���
		/// </summary>
		public Decimal? ChangeBeforeBalance { get; set; }

		/// <summary>
		/// �䶯�����
		/// </summary>
		public Decimal? ChangeAfterBalance { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String RelatedOrderNo { get; set; }

		/// <summary>
		/// �ŵ��ʶ
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// �ŵ����
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// �䶯ʱ��
		/// </summary>
		public DateTime? ChangeTime { get; set; }

		/// <summary>
		/// �䶯ԭ��
		/// </summary>
		public String ChangeReason { get; set; }

		/// <summary>
		/// ����Ա
		/// </summary>
		public String OperUser { get; set; }

		/// <summary>
		/// ״̬��1=������4=�˿��
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// ��ע
		/// </summary>
		public String Remark { get; set; }

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