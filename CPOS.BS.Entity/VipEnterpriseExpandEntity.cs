/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/30 14:42:33
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
    public partial class VipEnterpriseExpandEntity : BaseEntity 
    {
        #region 属性集
        public VipEntity Vip { get; set; }

        public string VipName { get; set; }
        public Int32? Gender { get; set; }
        public string GenderName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PDRoleName { get; set; }
        public string EnterpriseCustomerName { get; set; }
        public string StatusName { get; set; }
        #endregion
    }
}