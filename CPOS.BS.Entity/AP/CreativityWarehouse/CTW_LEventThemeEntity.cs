/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    public partial class CTW_LEventThemeEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 风格互动信息
        /// </summary>
        public List<EventInteractionInfo> EventInteractionList { get; set; }
        public string ImageURL { get; set; }
        public string ActivityGroupId { get; set; }
        public string TemplateName { get; set; }
        #endregion
    }
    public class EventInteractionInfo
    {
        public string TemplateId { get; set; }
        public string ThemeId { get; set; }
        public string ThemeName { get; set; }
        public string H5Url { get; set; }
        public string H5TemplateId { get; set; }
        public string LeventId { get; set; }
        public string InteractionTypeName { get; set; }
        public int? InteractionType { get; set; }

        public string DrawMethodCode { get; set; }

        //public string ActivityGroupId { get; set; }
        public List<GameEventImageInfo> GameEventImageList { get; set; }

        public PanicbuyingEventImageInfo PanicbuyingEventImage { get; set; }

    }
    /// <summary>
    /// 游戏活动图片信息
    /// </summary>
    public class GameEventImageInfo
    {
        public string ImageURL{get;set;}
        public int RuleId { get; set; }

        public string RuleContent { get; set; }

        public string BatId { get; set; }

    }
    /// <summary>
    /// 促销活动图片
    /// </summary>
    public class PanicbuyingEventImageInfo
    {
        public string PanicbuyingEventName { get; set; }

        public string ImageURL { get; set; }
    }

}