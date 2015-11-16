/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 15:07:16
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
    public partial class C_PrizesEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public C_PrizesEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? PrizesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityID { get; set; }

		/// <summary>
		/// 1=������ȯ   2=�����ֽ����Ѷ���NԪ��   3=�ʴ�ֵ��ֵ����NԪ��   4=���ʴ�ֵ���Ѷ���NԪ��   5=�������Ѷ����NԪ��
		/// </summary>
		public Int32? PrizesType { get; set; }

		/// <summary>
		/// ȡ��һ��ȯ����
		/// </summary>
		public String PrizesName { get; set; }

		/// <summary>
		/// ����ȯ��������Ԥ����
		/// </summary>
		public Int32? Qty { get; set; }

		/// <summary>
		/// ����ȯ��ʣ�ࣨԤ����
		/// </summary>
		public Int32? RemainingQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AmountLimit { get; set; }

		/// <summary>
		/// 0=��1=��
		/// </summary>
		public Int32? IsAutoIncrease { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 0=��1=��
		/// </summary>
		public Int32? IsCirculation { get; set; }

		/// <summary>
		/// ��ǰN�죬ת����ʱ���ʽ����
		/// </summary>
		public DateTime? SendDate { get; set; }

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
		/// 0=����״̬��1=ɾ��״̬
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}