using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response
{
    public class ActivityListRD : IAPIResponseData
    {
        public List<MyActivity> MyActivityList { get; set; }
        public List<EventStatusCoount> EventStatusCoountList { get; set; }

    }   
    public class EventStatusCoount
    {
        public string Name { get; set; }
        public string ActivityGroupCode { get; set; }
        public int Status     { get; set; }
        public int Prepare { get; set; }
        public int Running { get; set; }
        public int Pause { get; set; }
        public int End { get; set; }

    }
    public class MyActivity
    {
        /// <summary>
        /// 活动标识
        /// </summary>
        public string CTWEventId { get; set; }
        /// <summary>
        /// 营销类型
        /// </summary>
        public string ActivityGroupCode { get; set; }
        public string ActivityGroupId { get; set; }
        public string TemplateId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动封面
        /// </summary>
        public string ImageURL { get; set; }
        /// <summary>
        /// 活动状态编码
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// 主题标识
        /// </summary>
        public string ThemeId { get; set; }
        /// <summary>
        /// 活动方式
        /// </summary>
        public string DrawMethodCode { get; set; }
        /// <summary>
        /// 活动标识
        /// </summary>
        public string LeventId { get; set; }

        public EventInfo EventInfo { get; set; }
        /// <summary>
        /// 线上活动二维码(包括h5)
        /// </summary>
        public string QRCodeImageUrlForOnline { get; set; }
        /// <summary>
        /// 门店应用二维码(仅游戏活动或者促销活动)
        /// </summary>
        public string QRCodeImageUrlForUnit { get; set; }
        public int InteractionType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    /// <summary>
    /// 活动信息
    /// </summary>
    public class EventInfo
    {
        /// <summary>
        /// 活动标识
        /// </summary>
        public string EventID { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
  
}
