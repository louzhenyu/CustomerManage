/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:40
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
    public partial class EnterpriseMemberStructureEntity : BaseEntity ,IExtensionable
    {
        #region 属性集
        /// <summary>
        /// 企业会员名称
        /// </summary>
        public string MemberTitle { get; set; }
        /// <summary>
        /// 上级部门
        /// </summary>
        public string ParentTitle { get; set; }
        #endregion
    }
}