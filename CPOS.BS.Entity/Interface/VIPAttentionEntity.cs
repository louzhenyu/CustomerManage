using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Interface
{
    /// <summary>
    /// 获取各项近期关注的人数
    /// </summary>
    public class VIPAttentionEntity
    {
        #region
        /// <summary>
        /// 标识  
        /// </summary>
        public string vipSourceId { get; set; }
        /// <summary>
        /// 来源名称
        /// </summary>
        public string vipSourceName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int iCount { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long newTimestamp { get; set; }

        public IList<VIPAttentionEntity> VipAttentionInfoList { get; set; }
        #endregion
    }
}
