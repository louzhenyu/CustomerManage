/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-18 17:58
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
    public partial class LEventsAlbumEntity : BaseEntity 
    {
        #region 属性集

        /// <summary>
        /// 相册类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public string ModuleTypeName { get; set; }

        /// <summary>
        /// 相册照片数量 
        /// </summary>
        public int Count { get; set; }

        #endregion
    }
}