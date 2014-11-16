/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:38
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
    public partial class MarketEventEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDesc { get; set; }
        /// <summary>
        /// 邀约内容
        /// </summary>
        //public string TemplateContent { get; set; }
        /// <summary>
        /// 活动模板
        /// </summary>
        public MarketTemplateEntity MarketTemplageInfo { get; set; }
        /// <summary>
        /// 活动波段集合
        /// </summary>
        public IList<MarketWaveBandEntity> MarketWaveBandInfoList { get; set; }
        /// <summary>
        /// 活动门店集合
        /// </summary>
        public IList<MarketStoreEntity> MarketStoreInfoList { get; set; }
        /// <summary>
        /// 邀请人信息表
        /// </summary>
        public IList<MarketPersonEntity> MarketPersonInfoList { get; set; }
        /// <summary>
        /// 波段集合
        /// </summary>
        public IList<MarketWaveBandEntity> MarketWaveBandList { get; set; }

        public string EventModeDesc { get; set; }
        #endregion
    }
}