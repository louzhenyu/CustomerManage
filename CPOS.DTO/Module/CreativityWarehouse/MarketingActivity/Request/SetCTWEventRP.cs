using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
    public class SetCTWEventRP : IAPIRequestParameter
    {
       public void Validate()
       { }
       public string CTWEventId { get; set; }
       /// <summary>
       /// 模版标识
       /// </summary>
       public string TemplateId{get;set;}
       /// <summary>
       /// 模版图片
       /// </summary>
       public string TemplateImageURL{get;set;}
       /// <summary>
       /// 模版名称
       /// </summary>
       public string TemplateName{get;set;}
        /// <summary>
        /// 模版描述
        /// </summary>
       public string TemplateDesc{get;set;}
        /// <summary>
        /// 模版风格id
        /// </summary>
       public string OriginalThemeId { get; set; }
       /// <summary>
       /// 互动类型
       /// </summary>
       public int? InteractionType{get;set;}
        /// <summary>
       /// 模版游戏或促销活动Id
        /// </summary>
       public string OriginalLeventId { get; set; }
       /// <summary>
       /// 营销类型
       /// </summary>
       public string ActivityGroupId { get; set; }
       /// <summary>
       /// 游戏或促销方式
       /// </summary>
       public string DrawMethodCode{get;set;}
       public string BegDate { get; set; }
       public string EndDate { get; set; }
       public string MappingId { get; set; }
       public string OnlineQRCodeId { get; set; }
       public string OfflineQRCodeId { get; set; }
       /// <summary>
       /// 风格信息
       /// </summary>
       public T_CTW_LEventThemeEntity EventThemeInfo { get; set; }
       /// <summary>
       /// 团购，秒杀，抢购
       /// </summary>
       public PanicbuyingEventInfo PanicbuyingEventInfo { get; set; }
       /// <summary>
       /// 游戏活动信息
       /// </summary>
       public GameEventInfo GameEventInfo { get; set; }
       /// <summary>
       /// 推广设置(分享，关注)
       /// </summary>
       public List<T_CTW_SpreadSettingEntity> SpreadSettingList { get; set; }
        /// <summary>
        /// 分享，关注，注册奖励
        /// </summary>
       public List<ContactPrize> ContactPrizeList { get; set; }
       public MaterialTextInfo MaterialText { get; set; } 
       
    }
    /// <summary>
    /// 团购/秒杀
    /// </summary>
    public class PanicbuyingEventInfo
    {
        public string ImageUrl { get; set; }
        public string EventName { get; set; }
        public object[] PanicbuyingEventId { get; set; } 
    }
    /// <summary>
    /// 活动信息
    /// </summary>
    public class GameEventInfo
    {
        public string LeventId { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        /// <summary>
        /// 参与游戏周期限制（1.活动期间2.每天一次3.每周一次4.无限制）
        /// </summary>
        public int? PersonCount { get; set; }
        /// <summary>
        /// 参与游戏所需积分
        /// </summary>
        public int? PointsLottery { get; set; }
        /// <summary>
        /// 抽奖次数
        /// </summary>
        public int? LotteryNum { get; set; }
        public string Title { get; set; }
        public int? RuleId { get; set; }
        public string RuleContent { get; set; }

        public List<ObjectImage> ImageList { get; set; }
        public List<PrizeInfo> PrizeList { get; set; }

    }
    public class ObjectImage
    {
        public string ImageURL { get; set; }
        public string BatId { get; set; }

    }
    /// <summary>
    /// 奖品信息
    /// </summary>
    public class PrizeInfo
    {
        public string PrizeName { get; set; }
        public int PrizeCount { get; set; }
        /// <summary>
        /// 奖品类型：Point,Coupon
        /// </summary>
        public string PrizeTypeId { get; set; }
        public string CouponTypeID { get; set; }
        /// <summary>
        /// 积分(当奖品类型为Point 必须有值)
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 概率
        /// </summary>
        public decimal Probability { get; set; }
    }
    public class ContactPrize
    {
        public string ContactTypeCode { get; set; }
        public string PrizeType { get; set; }
        public int Point { get; set; }
        public List<PrizeInfo> PrizeList { get; set; }

    }
    /// <summary>
    /// 图文信息
    /// </summary>
    public class MaterialTextInfo
    {
        /// <summary>
        ///标识	为空时是增加,否则为修改
        /// </summary>  
        public string TextId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片URL	
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 原文连接
        /// </summary>
        public string OriginalUrl { get; set; }
        /// <summary>
        /// 文本内容	
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 作为请求是不作为参数
        /// </summary>
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 申请接口主标识
        /// </summary>
        // public string ApplicationId { get; set; }
        /// <summary>
        /// 图文类别	
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// 链接类别ID
        /// </summary>
        public string UnionTypeId { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public Guid? PageID { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 页面替换参数JSON	
        /// </summary>
        public string PageParamJson { get; set; }

    }
}
