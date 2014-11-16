/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/2 13:33:22
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
    public partial class TagsEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName { get; set; }
        public string MappingId { get; set; }
        public string VipId { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }
        public int VipCount { get; set; }
        #endregion
    }
}