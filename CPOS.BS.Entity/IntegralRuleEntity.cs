/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 9:45:28
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
    public partial class IntegralRuleEntity : BaseEntity 
    {
        #region 属性集
        public string IntegralSourceName { get; set; }
        public string BeginDateStr { get; set; }
        public string EndDateStr { get; set; }
        /// <summary>
        /// 类型描述
        /// </summary>
        public string TypeCodeDesc { get; set; }
        #endregion
    }
}