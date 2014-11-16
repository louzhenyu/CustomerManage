/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 10:41:51
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
    public partial class WXHouseOrderEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseOrderEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? PrePaymentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? MappingID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? FeeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RealPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? AssignbuyerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ThirdOrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Assbuyeridtp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Assbuyeridno { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Assbuyername { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Assbuyermobile { get; set; }

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