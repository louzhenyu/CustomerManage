using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response
{
   public class TemplateDetailRD : IAPIResponseData
    {
       public List<CTW_LEventThemeEntity> TemplateThemeList { get; set; }
       /// <summary>
       /// 推广设置
       /// </summary>
       public List<CTW_SpreadSettingEntity> TemplateSpreadSettingList { get; set; }
       public CustomerCTWEventInfo CustomerCTWEventInfo { get; set; }
       public string TemplateId { get; set; }
       public string TemplateName { get; set; }
       public string ActivityGroupId { get; set; }
       public string CTWEventId { get; set; }
       public string ImageURL { get; set; }




    }
    /// <summary>
    /// 风格信息
    /// </summary>
    public class ThemeInfo
   {
       public string TemplateId { get; set; }
       public string ThemeId { get; set; }
       public string ThemeName { get; set; }
       public string ImageId { get; set; }
       public string H5Url { get; set; }
       public string H5TemplateId { get; set; }

       public List<EventInteraction> EventInteractionList { get; set; }
   }
    /// <summary>
    /// 互动(游戏，吸粉)
    /// </summary>
    public class EventInteraction
    {
        public string ThemeId { get; set; }
        /// <summary>
        /// 互动类型  1.吸粉(游戏)2.促销
        /// </summary>
        public int InteractionType { get; set; }
        /// <summary>
        /// 游戏或促销方式
        /// </summary>
        public string DrawMethodId { get; set; }
        public string LeventId { get; set; }
    }

    public class CustomerCTWEventInfo
    {
        public string TemplateId { get; set; }
        public string CTWEventId { get; set; }
        public string name { get; set; }
        public string ActivityGroupCode { get; set; }
        public string InteractionType { get; set; }
        public string ImageURL { get; set; }
        public string OnlineQRCodeId { get; set; }
        public string OfflineQRCodeId { get; set; }
        /// <summary>
        /// 线上活动二维码(包括h5)
        /// </summary>
        public string QRCodeImageUrlForOnline { get; set; }
        /// <summary>
        /// 门店应用二维码(仅游戏活动或者促销活动)
        /// </summary>
        public string QRCodeImageUrlForUnit { get; set; }
        public string Status { get; set; }
        public string ThemeId { get; set; }
        /// <summary>
        /// 微秀场作品id
        /// </summary>
        public string worksId { get; set; }
        public string OriginalThemeId { get; set; }
        public string H5Url { get; set; }
        public string H5TemplateId { get; set; }
        public string DrawMethodCode { get; set; }
        public string LeventId { get; set; }
        public string MappingId { get; set; }
        /// <summary>
        /// 游戏活动列表
        /// </summary>
        public LEventsInfo EventInfo { get; set; }  
        /// <summary>
        /// 促销活动列表
        /// </summary>
        public T_CTW_PanicbuyingEventKVEntity PanicbuyingEventInfo { get; set; }
        
        /// <summary>
        /// 图文信息
        /// </summary>
        public WMaterialTextEntity MaterialText { get; set; }

        public List<T_CTW_SpreadSettingEntity> SpreadSettingList { get; set; }
        public List<ContactEventInfo> ContactEventList { get; set; }
    }
    public class LEventsInfo
    {
        public string EventID { get; set; }
        public int DrawMethodId { get; set; }
        public string DrawMethodName { get; set; }
        public string DrawMethodCode { get; set; }

        public string EventName { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public int EventStatus { get; set; }
        public string statusDESC { get; set; }

        public string EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        /// <summary>
        /// 参与活动频率
        /// </summary>
        public int PersonCount { get; set; }
        /// <summary>
        /// 可参与次数
        /// </summary>
        public int LotteryNum { get; set; }
        public List<Prize> PrizeList { get; set; }
        public List<ObjectImagesEntity> ImageList { get; set; }
    }
    /// <summary>
    /// 奖品
    /// </summary>
    public class Prize
    {
        public string EventId { get; set; }
        public string PrizesID { get; set; }
        public string PrizeName { get; set; }
        public int PrizeLevel { get; set; }
        public string PrizeTypeId { get; set; }
        public string PrizeLevelName { get; set; }
        public string CouponTypeName { get; set; }
        public string CouponTypeID { get; set; }
        public int PrizeCount { get; set; }
        public int IssuedQty { get; set; }
        public decimal Probability { get; set; }
        public string ImageUrl { get; set; }
        //剩余数量
        public int RemainCount { get; set; }

        public int Point { get; set; }
    }
    public  class ContactEventInfo
    {
        public string ContactTypeCode { get; set; }
        public List<Prize> ContactPrizeList { get; set; }

    }

}
