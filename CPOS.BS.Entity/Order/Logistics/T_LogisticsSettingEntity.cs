/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-6 16:59:24
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
    /// ʵ�壺  
    /// </summary>
    public partial class T_LogisticsSettingEntity : BaseEntity 
    {
        #region ���Լ�

        public String LogisticsName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LogisticsShortName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LogisticsLogo { get; set; }

        #endregion
    }
}