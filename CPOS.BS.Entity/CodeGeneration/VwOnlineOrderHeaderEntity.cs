/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/24 11:37:23
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
    /// 实体：  
    /// </summary>
    public partial class VwOnlineOrderHeaderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VwOnlineOrderHeaderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StoreId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DisCountRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CarrierId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CarrierName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Mobile { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String STATUS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StatusDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PaymentTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeliveryName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ClinchTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ReceiptTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IsDelete { get; set; }


        #endregion

    }
}