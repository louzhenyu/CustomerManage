using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;
using System.Configuration;
using JIT.CPOS.Common;
using System.Data;
using System.Drawing;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingActivity
{
    public class SetCTWEventAH : BaseActionHandler<SetCTWEventRP, SetCTWEventRD>
    {
        LoggingSessionInfo loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
        T_CTW_LEventEntity entityCustomerEvent = new T_CTW_LEventEntity();
        T_CTW_LEventThemeEntity entityTheme = new T_CTW_LEventThemeEntity();
        T_CTW_LEventInteractionEntity entityInteraction = new T_CTW_LEventInteractionEntity();
        T_CTW_PanicbuyingEventKVEntity entityPanicbuyingEventKV = new T_CTW_PanicbuyingEventKVEntity();
        T_CTW_SpreadSettingEntity entitySpreadSetting = new T_CTW_SpreadSettingEntity();
        ObjectImagesEntity imageEntity = new ObjectImagesEntity();

        string strCTWEventId = string.Empty;


        protected override SetCTWEventRD ProcessRequest(APIRequest<SetCTWEventRP> pRequest)
        {

            //图文信息
            //微信 公共平台
            var wapentity = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity
            {
                CustomerId = loggingSessionInfo.ClientID,
                IsDelete = 0
            }, null).FirstOrDefault();//取默认的第一个微信

            if (wapentity == null)
            {
                throw new APIException("微信公众号未授权");
            }
            var rd = new SetCTWEventRD();

            var para = pRequest.Parameters;

            strCTWEventId = para.CTWEventId;
            string strThemeId = string.Empty;
            string strStartDate = string.Empty;
            string strEndDate = string.Empty;
            string strGameEventGuid = string.Empty;
            string strPageParamJson = string.Empty;

            T_CTW_LEventBLL bllCustomerEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            T_CTW_LEventThemeBLL bllCustomerTheme = new T_CTW_LEventThemeBLL(loggingSessionInfo);
            T_CTW_LEventInteractionBLL bllCustomerInteraction = new T_CTW_LEventInteractionBLL(loggingSessionInfo);
            T_CTW_PanicbuyingEventKVBLL bllPanicbuyingEventKV = new T_CTW_PanicbuyingEventKVBLL(loggingSessionInfo);
            T_CTW_SpreadSettingBLL bllSpreadSetting = new T_CTW_SpreadSettingBLL(loggingSessionInfo);
            LPrizesBLL bllPrize = new LPrizesBLL(loggingSessionInfo);
            ObjectImagesBLL imageBll = new ObjectImagesBLL(loggingSessionInfo);


            entityCustomerEvent = bllCustomerEvent.GetByID(strCTWEventId);
            if (entityCustomerEvent!=null)
            {
                ///风格
                entityTheme = bllCustomerTheme.QueryByEntity(new T_CTW_LEventThemeEntity() { CTWEventId = new Guid(strCTWEventId), OriginalThemeId = new Guid(para.OriginalThemeId) }, null).FirstOrDefault();
                //互动方式
                entityInteraction = bllCustomerInteraction.QueryByEntity(new T_CTW_LEventInteractionEntity { CTWEventId = new Guid(strCTWEventId), OriginalLeventId = new Guid(para.OriginalLeventId) }, null).FirstOrDefault();



            }
            //保存风格
            SaveAndUpdateTheme(para,entityTheme,out strThemeId);
            //互动类型--游戏
            if (para.InteractionType == 1 && para.GameEventInfo != null)
            {
                SaveGameEvent(para, bllPrize, bllCustomerInteraction, strThemeId, out strStartDate, out strEndDate, out strGameEventGuid);
                para.MaterialText.PageParamJson = "[{\"key\":\"eventId\",\"value\":\"" + strCTWEventId + "\"}]";
                strPageParamJson = "[{\"key\":\"eventId\",\"value\":\"" + strCTWEventId + "\"}]";

            }
            //互动类型--促销
            if (para.InteractionType == 2 && para.PanicbuyingEventInfo != null)
            {
                SavePanicbuyingEvent(para, bllPanicbuyingEventKV, imageBll, bllCustomerInteraction, strThemeId, out strStartDate, out strEndDate);
                para.MaterialText.PageParamJson = "[{\"key\":\"CTWEventId\",\"value\":\"" + strCTWEventId + "\"}]";
                strPageParamJson = "[{\"key\":\"CTWEventId\",\"value\":\"" + strCTWEventId + "\"}]";


                //SaveAndUpdatePanicbuyingEvent(para,bllPrize,bllPanicbuyingEventKV,imageBll,bllCustomerInteraction, strThemeId, out strStartDate, out strEndDate);

            }
           
            //分享，推广
            if (para.SpreadSettingList.Count > 0)
            {
                SaveSpreadSetting(para, bllSpreadSetting, imageBll);

            }
            ///推广关注的奖励设置入 触点活动数据表
            if (para.ContactPrizeList!=null && para.ContactPrizeList.Count > 0)
            {
                SaveContactPrize(para, bllPrize, strStartDate, strEndDate);

            }
            string strOnlineQRCodeId=string.Empty;
            string strOfflineQRCodeId = string.Empty;
            string strOnlineQRCodeUrl = string.Empty;
            string strOnfflineQRCodeUrl = string.Empty;

            var WQRCodeManagerbll = new WQRCodeManagerBLL(loggingSessionInfo);
            if(!string.IsNullOrEmpty(para.OfflineQRCodeId))
            {
                WQRCodeManagerbll.Delete(new WQRCodeManagerEntity() { QRCodeId = new Guid(para.OfflineQRCodeId) });
            }
            if (!string.IsNullOrEmpty(para.OnlineQRCodeId))
            {
                WQRCodeManagerbll.Delete(new WQRCodeManagerEntity() { QRCodeId = new Guid(para.OnlineQRCodeId) });
            }
            CreateQRCode(para, wapentity, strPageParamJson, out strOfflineQRCodeId, out strOnfflineQRCodeUrl);
            CreateH5QRCode(para, wapentity, out strOnlineQRCodeId, out strOnlineQRCodeUrl);

            rd.OfflineQRCodeUrl = strOnfflineQRCodeUrl;
            rd.OnlineQRCodeUrl=strOnlineQRCodeUrl;

            if (entityCustomerEvent != null)
            {   //活动主表
                entityCustomerEvent.Name = para.TemplateName;
                entityCustomerEvent.Desc = para.TemplateDesc;
                entityCustomerEvent.InteractionType = para.InteractionType;
                entityCustomerEvent.ActivityGroupId = new Guid(para.ActivityGroupId);
                entityCustomerEvent.ImageURL = para.TemplateImageURL;
                entityCustomerEvent.StartDate = Convert.ToDateTime(strStartDate);
                entityCustomerEvent.EndDate = Convert.ToDateTime(strEndDate);
                entityCustomerEvent.OnlineQRCodeId = strOnlineQRCodeId;
                entityCustomerEvent.OfflineQRCodeId = strOfflineQRCodeId;
                bllCustomerEvent.Update(entityCustomerEvent);

            }
            else
            {
                entityCustomerEvent = new T_CTW_LEventEntity()
                {
                    CTWEventId = new Guid(para.CTWEventId),
                    TemplateId = new Guid(para.TemplateId),
                    Name = para.TemplateName,
                    Desc = para.TemplateDesc,
                    ActivityGroupId = new Guid(para.ActivityGroupId),
                    InteractionType = para.InteractionType,
                    ImageURL = para.TemplateImageURL,
                    Status = 10,
                    CustomerId = loggingSessionInfo.ClientID,
                    StartDate = Convert.ToDateTime(strStartDate),
                    EndDate = Convert.ToDateTime(strEndDate),
                    OfflineQRCodeId=strOfflineQRCodeId,
                    OnlineQRCodeId=strOnlineQRCodeId

                };
                bllCustomerEvent.Create(entityCustomerEvent);
            }
            return rd;

        }
        /// <summary>
        /// 保存更新主题
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public void SaveAndUpdateTheme(SetCTWEventRP para, T_CTW_LEventThemeEntity entityTheme, out string strThemeId)
        {
            T_CTW_LEventThemeBLL bllCustomerTheme = new T_CTW_LEventThemeBLL(loggingSessionInfo);

            if (entityTheme.ThemeId == null)
            {

                ///风格表
                if (para.EventThemeInfo != null)
                {
                    if (!string.IsNullOrEmpty(para.CTWEventId))
                    {
                        bllCustomerTheme.DeleteByCTWEventID(para.CTWEventId);
                    }
                    entityTheme = new T_CTW_LEventThemeEntity()
                    {
                        CTWEventId = new Guid(strCTWEventId),
                        ThemeName = para.EventThemeInfo.ThemeName,
                        H5Url = para.EventThemeInfo.H5Url,
                        H5TemplateId = para.EventThemeInfo.H5TemplateId,
                        OriginalThemeId = new Guid(para.OriginalThemeId),
                        CustomerId = loggingSessionInfo.ClientID,
                        WorksId=para.EventThemeInfo.WorksId
                    };

                    bllCustomerTheme.Create(entityTheme);
                    
                }
            }
            else
            {
                if (para.EventThemeInfo != null)
                {
                    entityTheme = new T_CTW_LEventThemeEntity()
                    {
                        CTWEventId = new Guid(strCTWEventId),
                        ThemeName = para.EventThemeInfo.ThemeName,
                        H5Url = para.EventThemeInfo.H5Url,
                        H5TemplateId = para.EventThemeInfo.H5TemplateId,
                        OriginalThemeId = new Guid(para.OriginalThemeId),
                        ThemeId = para.EventThemeInfo.ThemeId,
                        WorksId = para.EventThemeInfo.WorksId

                    };

                    bllCustomerTheme.Update(entityTheme);
                }
            }
            strThemeId = entityTheme.ThemeId.ToString();
          
        }
        /// <summary>
        /// 保存更新游戏信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public void SaveGameEvent(SetCTWEventRP para, LPrizesBLL bllPrize, T_CTW_LEventInteractionBLL bllCustomerInteraction, string strThemeId, out string strStartDate, out string strEndDate,out string strGameEventGuid)
        {
                strGameEventGuid = string.Empty;
           
                var imageBll = new ObjectImagesBLL(loggingSessionInfo);

                LEventsBLL bllGameEvent = new LEventsBLL(loggingSessionInfo);

                var eventEntity = new LEventsEntity();
                strStartDate = para.GameEventInfo.BeginTime;
                strEndDate = para.GameEventInfo.EndTime;
                if (string.IsNullOrEmpty(para.GameEventInfo.LeventId))
                {

                    strGameEventGuid = Guid.NewGuid().ToString();
                    eventEntity.EventID = strGameEventGuid;
                    eventEntity.Title = para.GameEventInfo.Title;
                    eventEntity.BeginTime = para.GameEventInfo.BeginTime;
                    eventEntity.EndTime = para.GameEventInfo.EndTime;
                    eventEntity.DrawMethodId = GetDrawMethodIdByDrawMethodCode(para.DrawMethodCode);
                    eventEntity.PersonCount = para.GameEventInfo.PersonCount;
                    eventEntity.PointsLottery = para.GameEventInfo.PointsLottery;
                    eventEntity.LotteryNum = para.GameEventInfo.LotteryNum;
                    eventEntity.EventStatus = 10;
                    eventEntity.IsCTW = 1;
                    eventEntity.CustomerId = loggingSessionInfo.ClientID;
                    bllGameEvent.Create(eventEntity);
                }
                else
                {
                    strGameEventGuid = para.GameEventInfo.LeventId;
                    eventEntity = bllGameEvent.GetByID(strGameEventGuid);
                    eventEntity.Title = para.GameEventInfo.Title;
                    eventEntity.BeginTime = para.GameEventInfo.BeginTime;
                    eventEntity.EndTime = para.GameEventInfo.EndTime;
                    eventEntity.DrawMethodId = GetDrawMethodIdByDrawMethodCode(para.DrawMethodCode);
                    eventEntity.PersonCount = para.GameEventInfo.PersonCount;
                    eventEntity.PointsLottery = para.GameEventInfo.PointsLottery;
                    eventEntity.LotteryNum = para.GameEventInfo.LotteryNum;

                    bllGameEvent.Update(eventEntity);
                }

                ///图片
                if (para.GameEventInfo.ImageList.Count > 0)
                {
                    imageBll.DeleteByObjectID(strGameEventGuid);

                    foreach (var i in para.GameEventInfo.ImageList)
                    {
                        imageEntity = new ObjectImagesEntity();
                        imageEntity.ImageURL = i.ImageURL;
                        imageEntity.ObjectId = strGameEventGuid;
                        imageEntity.CreateBy = loggingSessionInfo.UserID;
                        imageEntity.ImageId = Guid.NewGuid().ToString();
                        imageEntity.BatId = i.BatId;
                        imageEntity.RuleId = para.GameEventInfo.RuleId ?? 1;
                        imageEntity.RuleContent = para.GameEventInfo.RuleContent;
                        imageEntity.IsDelete = 0;
                        imageEntity.CustomerId = loggingSessionInfo.ClientID;
                        imageBll.Create(imageEntity);

                    }

                }
                //奖品信息
                var entityPrize = new LPrizesEntity();
                if (!string.IsNullOrEmpty(para.GameEventInfo.LeventId))
                {
                    bllPrize.Delete(new LPrizesEntity() { EventId = para.GameEventInfo.LeventId });
                }
                if (para.GameEventInfo.PrizeList.Count > 0)
                {
                    foreach (var i in para.GameEventInfo.PrizeList)
                    {
                        entityPrize.EventId = strGameEventGuid;
                        entityPrize.PrizeTypeId = i.PrizeTypeId;
                        entityPrize.Point = i.Point;
                        entityPrize.CouponTypeID = i.CouponTypeID;
                        entityPrize.PrizeName = i.PrizeName;
                        entityPrize.CountTotal = i.PrizeCount;
                        entityPrize.CreateBy = loggingSessionInfo.UserID;
                        entityPrize.PrizesID = Guid.NewGuid().ToString();

                        bllPrize.SavePrize(entityPrize);
                    }
                }
                if (!string.IsNullOrEmpty(para.CTWEventId))
                {
                    bllCustomerInteraction.DeleteByCTWEventID(para.CTWEventId);
                }
                ///互动类型与（游戏或促销）关联
                entityInteraction = new T_CTW_LEventInteractionEntity()
                {
                    CTWEventId = new Guid(strCTWEventId),
                    ThemeId = new Guid(strThemeId),
                    InteractionType = para.InteractionType,
                    DrawMethodCode = para.DrawMethodCode,
                    LeventId = strGameEventGuid,
                    OriginalLeventId = new Guid(para.OriginalLeventId),
                    CustomerId = loggingSessionInfo.ClientID
                };
                bllCustomerInteraction.Create(entityInteraction);

            
        }
        /// <summary>
        /// 保存更新促销活动信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public void SavePanicbuyingEvent(SetCTWEventRP para, T_CTW_PanicbuyingEventKVBLL bllPanicbuyingEventKV, ObjectImagesBLL imageBll, T_CTW_LEventInteractionBLL bllCustomerInteraction, string strThemeId, out string strStartDate, out string strEndDate)
        {
            strStartDate = string.Empty;
            strEndDate = string.Empty;

            var PanicbuyingEvent = para.PanicbuyingEventInfo;
            imageEntity = new ObjectImagesEntity();
            imageEntity.ImageURL = PanicbuyingEvent.ImageUrl;
            imageEntity.ObjectId = "";
            imageEntity.CreateBy = loggingSessionInfo.UserID;
            imageEntity.IsDelete = 0;
            imageEntity.CustomerId = loggingSessionInfo.ClientID;
            imageEntity.ImageId = Guid.NewGuid().ToString();
            imageBll.Create(imageEntity);
            if (!string.IsNullOrEmpty(para.CTWEventId))
            {
                bllPanicbuyingEventKV.DeleteByCTWEventID(para.CTWEventId);
            }
            entityPanicbuyingEventKV = new T_CTW_PanicbuyingEventKVEntity()
            {
                CTWEventId = new Guid(strCTWEventId),
                EventName = PanicbuyingEvent.EventName,
                ImageId = imageEntity.ImageId

            };
            bllPanicbuyingEventKV.Create(entityPanicbuyingEventKV);

            if (!string.IsNullOrEmpty(para.CTWEventId))
            {
                bllCustomerInteraction.DeleteByCTWEventID(para.CTWEventId);
            }
            foreach (var eventid in PanicbuyingEvent.PanicbuyingEventId)
            {
                ///互动类型与（游戏或促销）关联
                entityInteraction = new T_CTW_LEventInteractionEntity()
                {
                    CTWEventId = new Guid(strCTWEventId),
                    ThemeId = new Guid(strThemeId),
                    InteractionType = para.InteractionType,
                    DrawMethodCode = para.DrawMethodCode,
                    LeventId = eventid.ToString(),
                    CustomerId = loggingSessionInfo.ClientID
                };
                bllCustomerInteraction.Create(entityInteraction);
            }

            DataSet ds = bllCustomerInteraction.GetPanicbuyingEventDate(strCTWEventId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                strStartDate = ds.Tables[0].Rows[0]["BeginTime"].ToString();
                strEndDate = ds.Tables[0].Rows[0]["EndTime"].ToString();
            }
        }
        /// <summary>
        /// 推广分享设置
        /// </summary>
        /// <param name="para"></param>
        /// <param name="bllSpreadSetting"></param>
        /// <param name="imageBll"></param>
        public void SaveSpreadSetting(SetCTWEventRP para, T_CTW_SpreadSettingBLL bllSpreadSetting, ObjectImagesBLL imageBll)
        {
            if (!string.IsNullOrEmpty(para.CTWEventId))
            {
                bllSpreadSetting.DeleteByCTWEventID(para.CTWEventId);
            }
            foreach (var sItem in para.SpreadSettingList)
            {
                var bgimageEntity = new ObjectImagesEntity();
                bgimageEntity.ImageURL = sItem.BGImageUrl;
                bgimageEntity.ObjectId = "";
                bgimageEntity.CreateBy = loggingSessionInfo.UserID;
                bgimageEntity.IsDelete = 0;
                bgimageEntity.CustomerId = loggingSessionInfo.ClientID;
                bgimageEntity.ImageId = Guid.NewGuid().ToString();
                imageBll.Create(bgimageEntity);

                if (!string.IsNullOrEmpty(sItem.LeadPageQRCodeImageUrl))
                {
                    imageEntity = new ObjectImagesEntity();
                    imageEntity.ImageURL = sItem.LeadPageQRCodeImageUrl;
                    imageEntity.ObjectId = "";
                    imageEntity.CreateBy = loggingSessionInfo.UserID;
                    imageEntity.IsDelete = 0;
                    imageEntity.CustomerId = loggingSessionInfo.ClientID;
                    imageEntity.ImageId = Guid.NewGuid().ToString();
                    imageBll.Create(imageEntity);
                }

                entitySpreadSetting = new T_CTW_SpreadSettingEntity()
                {
                    SpreadType = sItem.SpreadType,
                    Title = sItem.Title,
                    ImageId = bgimageEntity.ImageId,
                    Summary = sItem.Summary,
                    PromptText = sItem.PromptText,
                    LeadPageQRCodeImageId = string.IsNullOrEmpty(sItem.LeadPageQRCodeImageUrl) == true ? "" : imageEntity.ImageId,
                    LeadPageSharePromptText = sItem.LeadPageSharePromptText,
                    LeadPageFocusPromptText = sItem.LeadPageFocusPromptText,
                    LeadPageRegPromptText = sItem.LeadPageRegPromptText,
                    CustomerId = loggingSessionInfo.ClientID,
                    CTWEventId = new Guid(strCTWEventId)
                };
                bllSpreadSetting.Create(entitySpreadSetting);
            }
        }
        /// <summary>
        /// 保存触点奖励
        /// </summary>
        /// <param name="para"></param>
        /// <param name="bllPrize"></param>
        /// <param name="strStartDate"></param>
        /// <param name="strEndDate"></param>
        public void SaveContactPrize(SetCTWEventRP para, LPrizesBLL bllPrize, string strStartDate, string strEndDate)
        {
            var bllContactEvent = new ContactEventBLL(loggingSessionInfo);



            if (!string.IsNullOrEmpty(para.CTWEventId))
            {
                bllContactEvent.DeleteContact(para.CTWEventId);
            }
            if (para.ContactPrizeList.Count>0)
            {
                foreach (var cItem in para.ContactPrizeList)
                {
                    ContactEventEntity contactEvent = new ContactEventEntity();
                    int PrizeCount = 0;
                    if (cItem.PrizeList != null && cItem.PrizeList.Count > 0)
                    {
                        foreach (var ItemPrize in cItem.PrizeList)
                        {
                            PrizeCount = PrizeCount + ItemPrize.PrizeCount;
                        }
                    }
                    contactEvent.PrizeCount = PrizeCount;
                    contactEvent.ContactTypeCode = cItem.ContactTypeCode;
                    contactEvent.ContactEventName = cItem.ContactTypeCode;
                    contactEvent.BeginDate = Convert.ToDateTime(strStartDate);
                    contactEvent.EndDate = Convert.ToDateTime(strEndDate);
                    contactEvent.PrizeType = "";
                    contactEvent.CustomerID = loggingSessionInfo.ClientID;
                    contactEvent.RewardNumber = "OnlyOne";
                    contactEvent.EventId = strCTWEventId;
                    contactEvent.Status = 1;
                    contactEvent.IsCTW = 1;
                    bllContactEvent.Create(contactEvent);
                    ///保存奖品 生成奖品池
                    var entityPrize = new LPrizesEntity();
                    entityPrize.EventId = contactEvent.ContactEventId.ToString();
                    entityPrize.PrizeName = cItem.ContactTypeCode;
                    entityPrize.PrizeTypeId = cItem.PrizeType;
                    entityPrize.Point = cItem.Point;
                    entityPrize.CountTotal = PrizeCount;
                    entityPrize.CreateBy = loggingSessionInfo.UserID;
                    entityPrize.PrizesID = Guid.NewGuid().ToString();
                    bllPrize.Create(entityPrize);

                    if (cItem.PrizeType == "Coupon")
                    {
                        if (cItem.PrizeList != null && cItem.PrizeList.Count > 0)
                        {
                            foreach (var ItemPrize in cItem.PrizeList)
                            {
                                entityPrize = new LPrizesEntity();
                                entityPrize.EventId = contactEvent.ContactEventId.ToString();
                                entityPrize.PrizeName = ItemPrize.PrizeName;
                                entityPrize.PrizeTypeId = ItemPrize.PrizeTypeId;
                                entityPrize.PrizesID = entityPrize.PrizesID;
                                entityPrize.CouponTypeID = ItemPrize.CouponTypeID;
                                entityPrize.CountTotal = ItemPrize.PrizeCount;
                                entityPrize.CreateBy = loggingSessionInfo.UserID;
                                bllContactEvent.AddContactEventPrizeForCTW(entityPrize);
                            }
                        }
                    }
                }
            };
        }
        public int GetDrawMethodIdByDrawMethodCode(string strCode)
        {
            switch (strCode)
            {
                case "HB":
                    return 4;
                case "DZP":
                    return 2;
                case "QN":
                    return 5;
                default:
                    return 0;
            }

        }
        public void CreateQRCode(SetCTWEventRP para, WApplicationInterfaceEntity wapentity,string strPageParamJson,out string strQRCode,out string QRCodeUrl)
        {
 


            #region 获取Page信息
            var pageBll = new SysPageBLL(loggingSessionInfo);
  
            //根据活动的抽奖方式获取：“HB” “BigDial“
            string strPageKey = string.Empty;
            switch (para.DrawMethodCode)
            {
                case "HB":
                    strPageKey = "RedPacket";
                    break;
                case "DZP":
                    strPageKey = "BigDial";
                    break;
                case "QN":
                    strPageKey = "Questionnaire";
                    break;
                default:
                    strPageKey = "RedPacket";
                    break;
            }
            #endregion
            #region 生成URL
            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");
            var Domain1 = ConfigurationManager.AppSettings["interfacehost1"].Replace("http://", "");
            string URL = string.Empty;

            #region 系统模块
            //var page = pageBll.QueryByEntity(new SysPageEntity() { CustomerID = loggingSessionInfo.ClientID,PageKey=strPageKey },null).SingleOrDefault();
            var page = pageBll.GetPageByCustomerIdAndPageKey(loggingSessionInfo.ClientID, strPageKey).SingleOrDefault();
            if (page==null)
                throw new APIException("未找到Page信息" ) { ErrorCode = 341 };
            string path = string.Empty;//要替换的路径
            string urlTemplate = page.URLTemplate;//模板URL
            string json = page.JsonValue;// JSON体
            var jsonDic = json.DeserializeJSONTo<Dictionary<string, object>>();//转换后的字典
            var htmls = jsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//htmls是一个数组，里面有他的很多属性
            Dictionary<string, object> html = null;//选择的html信息
            var defaultHtmlId = jsonDic["defaultHtml"].ToString();
            html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);//默认的htmlid*****
            if (html != null)
                path = html["path"].ToString();
            //判断高级oauth认证
            var scope = "snsapi_userinfo";
            //if (jsonDic.ContainsKey("scope"))//必须要判断key里是否包含scope
            //{
            //    scope = (jsonDic["scope"] == null || jsonDic["scope"].ToString() == "") ? "snsapi_base" : "snsapi_userinfo";
            //}

            //判断是否有定制,没有则取JSON体中的默认
            //找出订制内容
            if (page!=null)
            {
                //看是否有htmls的定制(Node值=2)
                if (page.Node=="2")
                {
                    var nodeValue = page.NodeValue;
                    //在Json解析后的集合中找到path
                    html = htmls.Find(t => t["id"].ToString() == nodeValue);
                    if (html != null)
                    {
                        path = html["path"].ToString();
                    }
                }
 
            }

            //读取配置信息域名,检查不用http://开头,如果有则去除
            var IsAuth = false;
            //TODO:判断页面是否需要Auth认证,如果页面需要证则再判断这个客户有没有Auth认证,Type=3
            if (page.IsAuth == 1)
            {
                //判断这个客户是否是认证客户,type=3
                var applicationBll = new WApplicationInterfaceBLL(loggingSessionInfo);
                var application = applicationBll.GetByID(wapentity.ApplicationId);//取默认的第一个
                if (application.WeiXinTypeId == "3")
                {
                    IsAuth = true;
                }
            }

            //替换URL模板
            #region 替换URL模板
            urlTemplate = urlTemplate.Replace("_pageName_", path);//用path替换掉_pageName_***（可以查看红包或者客服的path信息即可以知道）
            var paraDic = strPageParamJson.DeserializeJSONTo<Dictionary<string, object>[]>();
            foreach (var item in paraDic)   //这里key和value对于活动来说，其实就是活动的eventId，和eventId的值
            {
                if (item.ContainsKey("key") && item.ContainsKey("value"))
                    urlTemplate = urlTemplate.Replace("{" + item["key"] + "}", item["value"].ToString());
            }
            #endregion

            //根据规则组织URL
            #region 组织URL
            //读取配置文件中的域名

            if (string.IsNullOrEmpty(Domain))
                throw new APIException("微信管理:未配置域名,请在web.config中添加<add key='host' value=''/>") { ErrorCode = 342 };
            if (IsAuth)
            {
                //需要认证（传参数时，需要传递applicationId，对于一个商户有多个微信服务号的，不能取默认的第一个，而是精确地取固定的微信服务号）
                //通过urlTemplate（用path替换了_pageName_），生成了goUrl
                URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&applicationId={2}&goUrl={3}&scope={4}", Domain.Trim('/'), loggingSessionInfo.ClientID, wapentity.ApplicationId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate)), scope);
            }
            else
            {
                //不需要认证
                URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), loggingSessionInfo.ClientID, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/'))));
            }

            #endregion
            #endregion

            #endregion
            string sourcePath = HttpContext.Current.Server.MapPath("/QRCodeImage/qrcode.jpg");
            string targetPath =  HttpContext.Current.Server.MapPath("/QRCodeImage/");
            string currentDomain ="http://"+HttpContext.Current.Request.Url.Host;//当前项目域名

            QRCodeUrl=Utils.GenerateQRCode(URL, currentDomain, sourcePath, targetPath);
            ////二维码中奖加图片
            //string strQRCodeFilePath = targetPath + QRCodeUrl.Substring(QRCodeUrl.LastIndexOf("/") + 1);
            //Image img = Image.FromFile(strQRCodeFilePath);
            //System.IO.MemoryStream MStream1 = new System.IO.MemoryStream();
            //Utils.CombinImage(img, HttpContext.Current.Server.MapPath("~/QRCodeImage/33.jpg")).Save(MStream1, System.Drawing.Imaging.ImageFormat.Png);
            //Image ii = Image.FromStream(MStream1);
            //img.Dispose();
            //ii.Save(strQRCodeFilePath, System.Drawing.Imaging.ImageFormat.Png);
            //MStream1.Dispose();  
            ObjectImagesBLL bllObjectImages = new ObjectImagesBLL(loggingSessionInfo);
            ObjectImagesEntity entityObjectImages = new ObjectImagesEntity();

            entityObjectImages = new ObjectImagesEntity()
            {
                ImageId = Utils.NewGuid(),
                CustomerId = loggingSessionInfo.ClientID,
                ImageURL = QRCodeUrl,
                ObjectId = strCTWEventId,
                Title = "",
                Description = "创意仓库活动二维码"
            };
            //把下载下来的图片的地址存到ObjectImages
            bllObjectImages.Create(entityObjectImages);
            strQRCode = entityObjectImages.ImageId.ToString(); 
        }
        public void CreateH5QRCode(SetCTWEventRP para, WApplicationInterfaceEntity wapentity,out string strQRCode,out string QRCodeUrl)
        {
            #region 图文，二维码
            #region 生成图文素材

            #region 获取Page信息
            var pageBll = new SysPageBLL(loggingSessionInfo);
            var textBll = new WMaterialTextBLL(loggingSessionInfo);
                      //组织图文实体
            var entity = new WMaterialTextEntity()
            {
                ApplicationId = wapentity.ApplicationId,//用自己取出来的
                CoverImageUrl = para.MaterialText.ImageUrl,//图片地址
                PageId =Guid.NewGuid(),  //页面模块的标识
                PageParamJson = para.MaterialText.PageParamJson,//这个比较重要
                Text = para.MaterialText.Text,
                TextId = "",//为空时在后面保存时生成
                Title = para.MaterialText.Title,
                TypeId = para.MaterialText.TypeId
            };
            #endregion
            #region 生成URL
            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");
            var Domain1 = ConfigurationManager.AppSettings["interfacehost1"].Replace("http://", "");
            string URL = string.Empty;
            bool IsAuth=false;

            URL = ConfigurationManager.AppSettings["LinKinUrl"]+ "id=" + para.EventThemeInfo.WorksId;

            entity.IsAuth = Convert.ToInt32(IsAuth);
            entity.PageParamJson = para.MaterialText.PageParamJson;
            #endregion
            #endregion

            entity.OriginalUrl = URL;//图文素材要跳转到的页面
            #endregion

            #region 保存
            var unionMappingBll = new WModelTextMappingBLL(loggingSessionInfo);

            entity.TextId = Guid.NewGuid().ToString("N");
            textBll.Create(entity);//创建图文素材


            #endregion

            string strQRCodeObjectId = Guid.NewGuid().ToString();
            //活动的二维码自己查找QRCodeId
            var wqrCodeManagerEntity = new WQRCodeManagerBLL(loggingSessionInfo).QueryByEntity(new WQRCodeManagerEntity() { ObjectId = strCTWEventId }, null).FirstOrDefault();
            Guid QRCodeId;
            if (wqrCodeManagerEntity == null)
            {
                #region 生成二维码

                var wqrentity = new WQRCodeTypeBLL(loggingSessionInfo).QueryByEntity(

                    new WQRCodeTypeEntity { TypeCode = "CreativeCode" }

                    , null).FirstOrDefault();
                var wxCode = CretaeWxCode();

                var WQRCodeManagerbll = new WQRCodeManagerBLL(loggingSessionInfo);

                QRCodeId = Guid.NewGuid();

                if (!string.IsNullOrEmpty(wxCode.ImageUrl))
                {
                    wqrCodeManagerEntity = new WQRCodeManagerEntity()
                    {
                        QRCodeId = QRCodeId,
                        QRCode = wxCode.MaxWQRCod.ToString(),
                        QRCodeTypeId = wqrentity.QRCodeTypeId,
                        IsUse = 1,
                        ObjectId = strCTWEventId,
                        CreateBy = loggingSessionInfo.UserID,
                        ApplicationId = wapentity.ApplicationId,
                        IsDelete = 0,
                        ImageUrl = wxCode.ImageUrl,
                        CustomerId = loggingSessionInfo.ClientID
                    };
                    WQRCodeManagerbll.Create(wqrCodeManagerEntity);
                }
                else
                {
                    throw new APIException(wxCode.msg) { ErrorCode = 342 };

                }
                #endregion

                //throw new APIException("活动没有生成二维码！") { ErrorCode = 342 };
            }
            QRCodeId = (Guid)wqrCodeManagerEntity.QRCodeId;//活动二维码的标识
            ////根据二维码标识查找是否有他的关键字回复
            var WKeywordReplyentity = new WKeywordReplyBLL(loggingSessionInfo).QueryByEntity(new WKeywordReplyEntity()
            {
                Keyword = QRCodeId.ToString()  //二维码的标识

            }, null).FirstOrDefault();
            var ReplyBLL = new WKeywordReplyBLL(loggingSessionInfo);
            var ReplyId = Guid.NewGuid().ToString();//创建临时
            if (WKeywordReplyentity == null)
            {
                ReplyBLL.Create(new WKeywordReplyEntity()
                {
                    ReplyId = ReplyId,
                    Keyword = QRCodeId.ToString(),
                    ReplyType = 3,  //用图文素材
                    KeywordType = 4,//标识
                    IsDelete = 0,
                    CreateBy = loggingSessionInfo.UserID,
                    ApplicationId = wapentity.ApplicationId,
                });

            }
            else
            {
                ReplyId = WKeywordReplyentity.ReplyId; //用取出来的数据查看           
                WKeywordReplyentity.Text = "";
                WKeywordReplyentity.ReplyType = 3;//图文素材
                ReplyBLL.Update(WKeywordReplyentity);
            }
            #region 添加图文消息

            WMenuMTextMappingBLL MenuMTextMappingServer = new WMenuMTextMappingBLL(loggingSessionInfo);
  

                WMenuMTextMappingEntity MappingEntity = new WMenuMTextMappingEntity();
                MappingEntity.MenuId = ReplyId;
                MappingEntity.TextId = entity.TextId;   // 用图文素材标识******
                MappingEntity.DisplayIndex = 1;//排列顺序
                MappingEntity.CustomerId = loggingSessionInfo.ClientID;
  
                    MappingEntity.MappingId = Guid.NewGuid();
                    MenuMTextMappingServer.Create(MappingEntity);
          

            strQRCode = QRCodeId.ToString();
            QRCodeUrl = wqrCodeManagerEntity.ImageUrl;

            #endregion
        }
        #region new 生成活动二维码
        public WxCode CretaeWxCode()
        {
            var responseData = new WxCode();
            responseData.success = false;
            responseData.msg = "二维码生成失败!";
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            try
            {
                //微信 公共平台
                var wapentity = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity
                {

                    CustomerId = loggingSessionInfo.ClientID,
                    IsDelete = 0

                }, null).FirstOrDefault();

                //获取当前二维码 最大值
                var MaxWQRCod = new WQRCodeManagerBLL(loggingSessionInfo).GetMaxWQRCod() + 1;

                if (wapentity == null)
                {
                    responseData.success = false;
                    responseData.msg = "无设置微信公众平台!";
                    return responseData;
                }

                string imageUrl = string.Empty;

                #region 生成二维码
                JIT.CPOS.BS.BLL.WX.CommonBLL commonServer = new JIT.CPOS.BS.BLL.WX.CommonBLL();
                try
                {
                    imageUrl = commonServer.GetQrcodeUrl(wapentity.AppID.ToString().Trim()
                                                              , wapentity.AppSecret.Trim()
                                                              , "1", MaxWQRCod
                                                              , loggingSessionInfo);
                }
                catch (Exception e)
                {
                    responseData.success = false;
                    responseData.msg = e.Message;// "二维码生成失败!";
                    return responseData;
                }

                if (!string.IsNullOrEmpty(imageUrl))
                {

                    string host = ConfigurationManager.AppSettings["DownloadImageUrl"];
                    CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                    imageUrl = downloadServer.DownloadFile(imageUrl, host);
                }
                #endregion
                responseData.success = true;
                responseData.msg = "";
                responseData.ImageUrl = imageUrl;
                responseData.MaxWQRCod = MaxWQRCod;


                return responseData;
            }
            catch (Exception ex)
            {
                //throw new APIException(ex.Message);
                return responseData;
            }

        }
        public void CreateQRCode2(SetCTWEventRP para, WApplicationInterfaceEntity wapentity, out string strQRCode, out string QRCodeUrl)
        {
            #region 图文，二维码
            #region 生成图文素材

            #region 获取Page信息
            var pageBll = new SysPageBLL(loggingSessionInfo);
            var textBll = new WMaterialTextBLL(loggingSessionInfo);
            string strPageId = string.Empty;
            var list = pageBll.GetPagesByCustomerID(loggingSessionInfo.ClientID);  //增加根据customer_id查询
            var temp = list.GroupBy(t => new { t.ModuleName, t.PageCode, t.PageID, t.URLTemplate, t.PageKey }).Select(t => new ModulePageInfo()
            {
                ModuleName = t.Key.ModuleName,
                PageCode = t.Key.PageCode,
                PageID = t.Key.PageID.Value.ToString("N"),
                URLTemplate = t.Key.URLTemplate,
                PageKey = t.Key.PageKey
            }).Distinct().ToArray();
            var SysModuleList = temp;

            //获取相应页面的信息
            //根据活动的抽奖方式获取：“HB” “BigDial“
            switch (para.DrawMethodCode)
            {
                case "HB":
                    strPageId = SysModuleList.Where(p => p.PageKey == "RedPacket").SingleOrDefault().PageID;//红包-新
                    break;
                case "DZP":
                    strPageId = SysModuleList.Where(p => p.PageKey == "BigDial").SingleOrDefault().PageID;//大转盘
                    break;
                case "QN":
                    strPageId = SysModuleList.Where(p => p.PageKey == "Questionnaire").SingleOrDefault().PageID;//问卷
                    break;
                default:
                    strPageId = SysModuleList.Where(p => p.PageKey == "RedPacket").SingleOrDefault().PageID;
                    break;
            }


            //para.MaterialText.PageParamJson = "[{\"key\":\"eventId\",\"value\":\"" + strGameEventGuid + "\",\"pageModule\":\"{\\\"PageID\\\":\\\"" + strPageId + "””\\\",\\\"ModuleName\\\":\\\"大转盘\\\",\\\"PageCode\\\":\\\"EventDetail\\\",\\\"URLTemplate\\\":\\\"/HtmlAppsml/_pageName_?eventId={eventId}\\\",\\\"PageKey\\\":\\\"BigDial\\\"}\",\"pageDetail\":\"{\\\"EventId\\\":\\\"b30eb04c-0fb7-4321-9fe8-60030538e0a7\\\",\\\"EventName\\\":\\\"大转盘盘\\\",\\\"EventTypeId\\\":\\\"081AEC92-CC16-4041-9496-B4F6BC3B11FC\\\",\\\"EventTypeName\\\":\\\"\\\",\\\"BegTime\\\":\\\"2016/4/1 0:00:00\\\",\\\"EndTime\\\":\\\"2016/4/8 0:00:00\\\",\\\"EventStatus\\\":20,\\\"CityName\\\":\\\"\\\",\\\"EventStatusName\\\":\\\"运行中\\\",\\\"DrawMethod\\\":\\\"\\\"}\",\"UnionTypeId\":3}]";
            //组织图文实体
            var entity = new WMaterialTextEntity()
            {
                ApplicationId = wapentity.ApplicationId,//用自己取出来的
                CoverImageUrl = para.MaterialText.ImageUrl,//图片地址
                PageId = new Guid(strPageId),  //页面模块的标识
                PageParamJson = para.MaterialText.PageParamJson,//这个比较重要
                Text = para.MaterialText.Text,
                TextId = "",//为空时在后面保存时生成
                Title = para.MaterialText.Title,
                TypeId = para.MaterialText.TypeId
            };
            #endregion
            #region 生成URL
            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");
            var Domain1 = ConfigurationManager.AppSettings["interfacehost1"].Replace("http://", "");
            string URL = string.Empty;

            #region 系统模块
            var pages = pageBll.GetPageByID(new Guid(strPageId));//通过pageid查找syspage信息***
            if (pages.Length == 0)
                throw new APIException("未找到Page:" + strPageId) { ErrorCode = 341 };
            SysPageEntity CurrentPage;
            string path = string.Empty;//要替换的路径
            string urlTemplate = pages[0].URLTemplate;//模板URL
            string json = pages[0].JsonValue;// JSON体
            var jsonDic = json.DeserializeJSONTo<Dictionary<string, object>>();//转换后的字典
            var htmls = jsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//htmls是一个数组，里面有他的很多属性
            Dictionary<string, object> html = null;//选择的html信息
            var defaultHtmlId = jsonDic["defaultHtml"].ToString();
            html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);//默认的htmlid*****
            if (html != null)
                path = html["path"].ToString();
            //判断高级oauth认证
            var scope = "snsapi_base";
            if (jsonDic.ContainsKey("scope"))//必须要判断key里是否包含scope
            {
                scope = (jsonDic["scope"] == null || jsonDic["scope"].ToString() == "") ? "snsapi_base" : "snsapi_userinfo";
            }

            //判断是否有定制,没有则取JSON体中的默认
            //找出订制内容
            var customerPages = pages.ToList().FindAll(t => t.CustomerID == loggingSessionInfo.ClientID);
            if (customerPages.Count > 0)
            {
                //看是否有htmls的定制(Node值=2)
                CurrentPage = customerPages.Find(t => t.Node == "2");
                if (CurrentPage != null)
                {
                    var nodeValue = CurrentPage.NodeValue;
                    //在Json解析后的集合中找到path
                    html = htmls.Find(t => t["id"].ToString() == nodeValue);
                    if (html != null)
                    {
                        path = html["path"].ToString();
                    }
                }
                else
                {
                    CurrentPage = pages[0];
                }
            }
            else
            {
                CurrentPage = pages[0];
            }
            //读取配置信息域名,检查不用http://开头,如果有则去除
            var IsAuth = false;
            //TODO:判断页面是否需要Auth认证,如果页面需要证则再判断这个客户有没有Auth认证,Type=3
            if (CurrentPage.IsAuth == 1)
            {
                //判断这个客户是否是认证客户,type=3
                var applicationBll = new WApplicationInterfaceBLL(loggingSessionInfo);
                var application = applicationBll.GetByID(wapentity.ApplicationId);//取默认的第一个
                if (application.WeiXinTypeId == "3")
                {
                    IsAuth = true;
                }
            }

            //替换URL模板
            #region 替换URL模板
            urlTemplate = urlTemplate.Replace("_pageName_", path);//用path替换掉_pageName_***（可以查看红包或者客服的path信息即可以知道）
            var paraDic = para.MaterialText.PageParamJson.DeserializeJSONTo<Dictionary<string, object>[]>();
            foreach (var item in paraDic)   //这里key和value对于活动来说，其实就是活动的eventId，和eventId的值
            {
                if (item.ContainsKey("key") && item.ContainsKey("value"))
                    urlTemplate = urlTemplate.Replace("{" + item["key"] + "}", item["value"].ToString());
            }
            #endregion

            //根据规则组织URL
            #region 组织URL
            //读取配置文件中的域名

            if (string.IsNullOrEmpty(Domain))
                throw new APIException("微信管理:未配置域名,请在web.config中添加<add key='host' value=''/>") { ErrorCode = 342 };
            if (IsAuth)
            {
                //需要认证（传参数时，需要传递applicationId，对于一个商户有多个微信服务号的，不能取默认的第一个，而是精确地取固定的微信服务号）
                //通过urlTemplate（用path替换了_pageName_），生成了goUrl
                URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&applicationId={2}&goUrl={3}&scope={4}", Domain.Trim('/'), loggingSessionInfo.ClientID, wapentity.ApplicationId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate)), scope);
            }
            else
            {
                //不需要认证
                URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), loggingSessionInfo.ClientID, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/'))));
            }
            entity.IsAuth = Convert.ToInt32(IsAuth);
            entity.PageParamJson = para.MaterialText.PageParamJson;
            #endregion
            #endregion

            entity.OriginalUrl = URL;//图文素材要跳转到的页面
            #endregion

            #region 保存
            var unionMappingBll = new WModelTextMappingBLL(loggingSessionInfo);

            entity.TextId = Guid.NewGuid().ToString("N");
            #region 图文详情要对占位符#TextId#和#customerId#进行替换
            entity.OriginalUrl = entity.OriginalUrl.Replace("#TextId#", entity.TextId).Replace("#cutomerId#", loggingSessionInfo.ClientID);
            #endregion
            textBll.Create(entity);//创建图文素材


            #endregion
            #endregion

            //活动的二维码自己查找QRCodeId
            var wqrCodeManagerEntity = new WQRCodeManagerBLL(loggingSessionInfo).QueryByEntity(new WQRCodeManagerEntity() { ObjectId = para.CTWEventId }, null).FirstOrDefault();
            Guid QRCodeId;
            if (wqrCodeManagerEntity == null)
            {
                #region 生成二维码

                var wqrentity = new WQRCodeTypeBLL(loggingSessionInfo).QueryByEntity(

                    new WQRCodeTypeEntity { TypeCode = "CreativeCode" }

                    , null).FirstOrDefault();
                var wxCode = CretaeWxCode();

                var WQRCodeManagerbll = new WQRCodeManagerBLL(loggingSessionInfo);

                QRCodeId = Guid.NewGuid();

                if (!string.IsNullOrEmpty(wxCode.ImageUrl))
                {
                    wqrCodeManagerEntity = new WQRCodeManagerEntity()
                    {
                        QRCodeId = QRCodeId,
                        QRCode = wxCode.MaxWQRCod.ToString(),
                        QRCodeTypeId = wqrentity.QRCodeTypeId,
                        IsUse = 1,
                        ObjectId = para.CTWEventId,
                        CreateBy = loggingSessionInfo.UserID,
                        ApplicationId = wapentity.ApplicationId,
                        IsDelete = 0,
                        ImageUrl = wxCode.ImageUrl,
                        CustomerId = loggingSessionInfo.ClientID
                    };
                    WQRCodeManagerbll.Create(wqrCodeManagerEntity);
                }
                else
                {
                    throw new APIException(wxCode.msg) { ErrorCode = 342 };

                }
                #endregion

                //throw new APIException("活动没有生成二维码！") { ErrorCode = 342 };
            }
            QRCodeId = (Guid)wqrCodeManagerEntity.QRCodeId;//活动二维码的标识
            ////根据二维码标识查找是否有他的关键字回复
            var WKeywordReplyentity = new WKeywordReplyBLL(loggingSessionInfo).QueryByEntity(new WKeywordReplyEntity()
            {
                Keyword = QRCodeId.ToString()  //二维码的标识

            }, null).FirstOrDefault();
            var ReplyBLL = new WKeywordReplyBLL(loggingSessionInfo);
            var ReplyId = Guid.NewGuid().ToString();//创建临时
            if (WKeywordReplyentity == null)
            {
                ReplyBLL.Create(new WKeywordReplyEntity()
                {
                    ReplyId = ReplyId,
                    Keyword = QRCodeId.ToString(),
                    ReplyType = 3,  //用图文素材
                    KeywordType = 4,//标识
                    IsDelete = 0,
                    CreateBy = loggingSessionInfo.UserID,
                    ApplicationId = wapentity.ApplicationId,
                });

            }
            else
            {
                ReplyId = WKeywordReplyentity.ReplyId; //用取出来的数据查看           
                WKeywordReplyentity.Text = "";
                WKeywordReplyentity.ReplyType = 3;//图文素材
                ReplyBLL.Update(WKeywordReplyentity);
            }
            #region 添加图文消息

            WMenuMTextMappingBLL MenuMTextMappingServer = new WMenuMTextMappingBLL(loggingSessionInfo);

            WMenuMTextMappingEntity MappingEntity = new WMenuMTextMappingEntity();
            MappingEntity.MenuId = ReplyId;
            MappingEntity.TextId = entity.TextId;   // 用图文素材标识******
            MappingEntity.DisplayIndex = 1;//排列顺序
            MappingEntity.CustomerId = loggingSessionInfo.ClientID;
            MappingEntity.MappingId = Guid.NewGuid();
            MenuMTextMappingServer.Create(MappingEntity);


            strQRCode = QRCodeId.ToString();
            QRCodeUrl = wqrCodeManagerEntity.ImageUrl;

            #endregion
            #endregion
        }

        public class WxCode
        {
            public bool success { get; set; }
            public string msg { get; set; }
            public string ImageUrl { get; set; }
            public int MaxWQRCod { get; set; }
        }
        #endregion
        public class ModulePageInfo
        {
            public string PageID { get; set; }//String	模板Page标识
            public string ModuleName { get; set; }//String	Code
            public string PageCode { get; set; }//String	页面类别码,根据类别来确定前端页面显示如活动、系统功能等
            public string URLTemplate { get; set; }//URL模板
            public string PageKey { get; set; }//URL模板

        }
    }
}