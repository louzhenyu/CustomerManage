/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/6 13:39:18
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
    public partial class MarketEventResponseEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MarketEventResponseEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String ReponseID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MarketEventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

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
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// �͵���
		/// </summary>
		public Decimal? CustomerPrice { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public Decimal? UnitPrice { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public Int32? PurchaseNumber { get; set; }

		/// <summary>
		/// ���ѻ���
		/// </summary>
		public Int32? SalesIntegral { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public Decimal? PurchaseAmount { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public Int32? PurchaseCount { get; set; }

		/// <summary>
		/// ��Ա΢��Ψһ��
		/// </summary>
		public String OpenID { get; set; }

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public String ProductName { get; set; }

		/// <summary>
		/// �Ƿ���
		/// </summary>
		public Int32? IsSales { get; set; }


        #endregion

    }
}