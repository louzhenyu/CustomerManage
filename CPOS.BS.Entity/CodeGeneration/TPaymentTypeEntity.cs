/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/29 16:01:06
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
    public partial class TPaymentTypeEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TPaymentTypeEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String PaymentTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PaymentTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PaymentTypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PaymentCompany { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PaymentItemType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LogoURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PaymentDesc { get; set; }

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
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        public string ChannelId { set; get; }
        /// <summary>
        /// 1=客户配置支付;0=平台支付
        /// </summary>
        public int IsNativePay { get; set; }
        #endregion

    }
}