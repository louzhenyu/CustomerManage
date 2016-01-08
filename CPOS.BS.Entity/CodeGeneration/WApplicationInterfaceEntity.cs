/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-12-31 12:20:35
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
    public partial class WApplicationInterfaceEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WApplicationInterfaceEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ApplicationId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String URL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Token { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppSecret { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ServerIP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FileAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsHeight { get; set; }

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
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LoginUser { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LoginPass { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AuthUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RequestToken { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ExpirationTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsMoreCS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PrevEncodingAESKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CurrentEncodingAESKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EncryptType { get; set; }

		/// <summary>
		/// 是否支持微信支付(0=不支持；1=支持）
		/// </summary>
		public Int32? IsPayments { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenOAuthAppid { get; set; }


        #endregion

    }
}