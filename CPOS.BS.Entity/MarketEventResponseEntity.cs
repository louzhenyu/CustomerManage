/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    public partial class MarketEventResponseEntity : BaseEntity 
    {
        #region 属性集
        //卡号
        public string VipCode { get; set; }
        //等级
        public string VipLevel { get; set; }
        //姓名
        public string VipName { get; set; }
        //统计时间
        public string StatisticsTime { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 响应人群集合
        /// </summary>
        public IList<MarketEventResponseEntity> MarketEventResponseInfoList { get; set; }
        #endregion
    }
}