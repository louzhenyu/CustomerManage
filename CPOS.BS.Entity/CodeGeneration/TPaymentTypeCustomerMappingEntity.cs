/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/17 9:41:56
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
    public partial class TPaymentTypeCustomerMappingEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TPaymentTypeCustomerMappingEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? MappingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PaymentTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

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
        public String ChannelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String APPId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? PayDeplyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PayAccountNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PayAccounPublic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PayPrivate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EncryptionCertificate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EncryptionPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DecryptionCertificate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DecryptionPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AccountIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PublicKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TenPayIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TenPayKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PayEncryptedPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SalesTBAccess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ApplyMD5Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DefaultName { get; set; }


        #endregion

    }
}