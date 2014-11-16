/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/25 15:17:08
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
    public partial class WUserMessageEntity : BaseEntity 
    {
        #region 属性集
        public string UnitId { get; set; }
        public Int64 DisplayIndexLast { get; set; }
        public string timestamp { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public Int64 DisplayIndexByTime { get; set; }
        public string VipName { get; set; }
        public string HeadImgUrl { get; set; }
        public Int64 DisplayIndex { get; set; }
        #endregion
    }
}