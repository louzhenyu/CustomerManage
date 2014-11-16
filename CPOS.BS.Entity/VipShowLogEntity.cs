/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 15:34:04
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
    public partial class VipShowLogEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String VIPID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String VipCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String VipSourceId { get; set; }
        /// <summary>
        /// 是否老客户 1=是 0=否 Jermyn20130917 认为客户系统中存在的就是老客户
        /// </summary>
        public int IsOld { get; set; }
        /// <summary>
        /// 生日 20130923 Jermyn+
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        public int SerialNumber { get; set; }

        public decimal Integration { get; set; }

        #endregion
    }
}