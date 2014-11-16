/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:41:32
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
    public partial class VipCardGradeChangeLogEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// ChangeBeforeGradeName
        /// </summary>
        public string ChangeBeforeGradeName { get; set; }
        /// <summary>
        /// NowGradeName
        /// </summary>
        public string NowGradeName { get; set; }
        /// <summary>
        /// UnitName
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// OperationUserName
        /// </summary>
        public string OperationUserName { get; set; }
        /// <summary>
        /// OperationTypeName
        /// </summary>
        public string OperationTypeName { get; set; }
        #endregion
    }
}