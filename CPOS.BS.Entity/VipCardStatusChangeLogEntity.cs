/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:29
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
    public partial class VipCardStatusChangeLogEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// VipCardStatusName
        /// </summary>
        public string VipCardStatusName { get; set; }
        /// <summary>
        /// OldStatusName
        /// </summary>
        public string OldStatusName { get; set; }
        /// <summary>
        /// UnitName
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// OperationUserName
        /// </summary>
        public string OperationUserName { get; set; }
        #endregion
    }
}