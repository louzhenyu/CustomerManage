/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/18 13:22:52
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
    public partial class LItemAuthEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LItemAuthEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String ItemAuthId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AuthCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CaptchaCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Norm { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Alcohol { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BaseWineYear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AgePitPits { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Barcode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAuthCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CategoryName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CategoryId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BrandName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DealerName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DealerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AuthCount { get; set; }

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
		/// 
		/// </summary>
		public String StoreCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }


        #endregion

    }
}