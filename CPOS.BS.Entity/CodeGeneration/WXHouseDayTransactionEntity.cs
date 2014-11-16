/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 14:42:41
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
    public partial class WXHouseDayTransactionEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseDayTransactionEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? TransactID { get; set; }

		/// <summary>
		/// ������  8λ
		/// </summary>
		public String OrderDate { get; set; }

		/// <summary>
		/// �����Ϣ
		/// </summary>
		public String Assignbuyer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SeqNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HatradedDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ThirdOrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FundType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? EntrustPay { get; set; }

		/// <summary>
		/// �����Ƕ�Ӧ�ñʽ����ڻ����Ƿ��Ѿ���������״̬��0Ϊ??���㣬1Ϊ�����㡣
		/// </summary>
		public Int32? ClearType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FundState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RetMsg { get; set; }

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