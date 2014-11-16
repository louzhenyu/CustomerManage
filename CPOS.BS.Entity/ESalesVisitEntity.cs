/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:21
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
    public partial class ESalesVisitEntity : BaseEntity 
    {
        #region 属性集
        public string StageId { get; set; }
        public int VipCount { get; set; }
        public int ObjectCount { get; set; }
        public IList<ObjectDownloadsEntity> ObjectDownloadsList { get; set; }

        public IList<ESalesVisitVipMappingEntity> ESalesVisitVipMappingList { get; set; }
        public IList<string> ESalesVisitVipMappingIds { get; set; }
        #endregion
    }
}