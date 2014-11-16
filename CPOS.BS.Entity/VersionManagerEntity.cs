/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/5 9:24:38
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
    public partial class VersionManagerEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 是否有新版本可用  0、1
        /// </summary>
        public string IsNewVersionAvailable { get; set; }

        /// <summary>
        /// 该版本是否可被忽略不强制下载。1：可忽略，0：不可忽略
        /// </summary>
        public string CanSkip { get; set; }

        /// <summary>
        /// 渠道描述  0：市场版  1：企业版
        /// </summary>
        public string ChannelDesc { get; set; }

        /// <summary>
        /// 更新用户范围描述  0：全部用户  1：正式用户  2：测试用户
        /// </summary>
        public string UserScopeDesc { get; set; }
        #endregion
    }
}