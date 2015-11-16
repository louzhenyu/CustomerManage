/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 20:05:31
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
    /// ʵ�壺 �����ƿ� 
    /// </summary>
    public partial class VipCardBatchEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardBatchEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? BatchID { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public Int32? BatchNo { get; set; }

		/// <summary>
		/// �����ʣ�IC/ID��
		/// </summary>
		public String CardMedium { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public String RegionNumber { get; set; }

		/// <summary>
		/// ��ǰ׺
		/// </summary>
		public String CardPrefix { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String VipCardTypeCode { get; set; }

		/// <summary>
		/// ��ʼ����
		/// </summary>
		public String StartCardNo { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String EndCardNo { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public Int32? Qty { get; set; }

		/// <summary>
		/// �쳣����
		/// </summary>
		public Int32? OutliersQty { get; set; }

		/// <summary>
		/// �ɱ���
		/// </summary>
		public Decimal? CostPrice { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public Int32? ExportCount { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ImportQty { get; set; }


        #endregion

    }
}