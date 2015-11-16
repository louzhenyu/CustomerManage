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
    public partial class VipIntegralDetailEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipIntegralDetailEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String VipIntegralDetailID { get; set; }

		/// <summary>
		/// ��Ա��ʶ
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
		/// ��Ա����
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// �ŵ��ʶ
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// �ŵ�����
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// ���ѽ��
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public Int32? Integral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UsedIntegral { get; set; }

		/// <summary>
		/// ԭ��
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// ������Դ��ʶ
		/// </summary>
		public String IntegralSourceID { get; set; }

		/// <summary>
		/// ������Ч����
		/// </summary>
		public DateTime? EffectiveDate { get; set; }

		/// <summary>
		/// ���ֽ�ֹ����
		/// </summary>
		public DateTime? DeadlineDate { get; set; }

		/// <summary>
		/// �����Ƿ��ۼ�
		/// </summary>
		public Int32? IsAdd { get; set; }

		/// <summary>
		/// ���Ի�Ա��ʶ
		/// </summary>
		public String FromVipID { get; set; }

		/// <summary>
		/// �����ʶ��������ʶ��
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// ��ע
		/// </summary>
		public String Remark { get; set; }

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