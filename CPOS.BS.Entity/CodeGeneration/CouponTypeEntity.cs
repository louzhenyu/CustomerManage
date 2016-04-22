/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/22 11:41:32
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
    public partial class CouponTypeEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CouponTypeEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? CouponTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponTypeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponCategory { get; set; }

		/// <summary>
		/// ��ֵ
		/// </summary>
		public Decimal? ParValue { get; set; }

		/// <summary>
		/// �ۿۣ�����0С��1��С��
		/// </summary>
		public Decimal? Discount { get; set; }

		/// <summary>
		/// ����ֵ�������ܶ���������ֵ����ʹ��
		/// </summary>
		public Decimal? ConditionValue { get; set; }

		/// <summary>
		/// �Ƿ���Ե���ʹ��
		/// </summary>
		public Int32? IsRepeatable { get; set; }

		/// <summary>
		/// �Ƿ�����������Ż�ȯ���ʹ��
		/// </summary>
		public Int32? IsMixable { get; set; }

		/// <summary>
		/// �Ż�ȯ��Դ
		/// </summary>
		public String CouponSourceID { get; set; }

		/// <summary>
		/// �Ż�ȯ��Ч�ڣ���λ�����ӣ�Ϊ0ʱ������Ч
		/// </summary>
		public Int32? ValidPeriod { get; set; }

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
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IssuedQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsVoucher { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UsableRange { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ServiceLife { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SuitableForStore { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponTypeDesc { get; set; }


        #endregion

    }
}