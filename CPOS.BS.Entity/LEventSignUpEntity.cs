/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
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
    public partial class LEventSignUpEntity : BaseEntity
    {
        #region 属性集
        /// <summary>
        /// 总数量
        /// </summary>
        public int ICount { get; set; }

        /// <summary>
        /// 集合
        /// </summary>
        public IList<LEventSignUpEntity> EntityList { get; set; }


        public string VipVipId { get; set; }
        public string WeiXin { get; set; }
        public string WeiXinUserId { get; set; }
        public string HeadImgUrl { get; set; }
        public int Status { get; set; }
        public Int64 DisplayIndex { get; set; }
        public int IsSign { get; set; }
        #endregion
    }
}