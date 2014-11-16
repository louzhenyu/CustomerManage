/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/14 11:26:51
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
    public partial class WeixinWallEntity : BaseEntity 
    {
        #region 属性集
        public int WallsCount { get; set; }

        public string UserName { get; set; }

        public string ImageUrl { get; set; }

        public Int64 DisplayIndex { get; set; }

        public IList<WeixinWallEntity> WallList { get; set; }
        #endregion
    }
}