/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/22 14:04:57
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
    public partial class WMenuEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// ParentName
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// ModelName
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// MaterialTypeName
        /// </summary>
        public string MaterialTypeName { get; set; }
        /// <summary>
        /// 图文消息WMaterialText -- Title
        /// </summary>
        public string WMaterialTextTitle { get; set; }
        /// <summary>
        /// 图片消息WMaterialImage -- Title
        /// </summary>
        public string WMaterialImageTitle { get; set; }
        /// <summary>
        /// 语音消息WMaterialVoice -- Title
        /// </summary>
        public string WMaterialVoiceTitle { get; set; }
        /// <summary>
        /// ApplicationId
        /// </summary>
        public string ApplicationId { get; set; }
        #endregion
    }
}