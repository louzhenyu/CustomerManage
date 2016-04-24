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
using System.Data;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingActivity
{
    public class GetTemplateDetailAH : BaseActionHandler<TemplateDetailRP, TemplateDetailRD>
    {

        protected override TemplateDetailRD ProcessRequest(APIRequest<TemplateDetailRP> pRequest)
        {
            var rd = new TemplateDetailRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            CTW_LEventThemeBLL bllTheme = new CTW_LEventThemeBLL(loggingSessionInfo);
            CTW_LEventInteractionBLL bllInteraction = new CTW_LEventInteractionBLL(loggingSessionInfo);
            CTW_SpreadSettingBLL bllSpreadSetting = new CTW_SpreadSettingBLL(loggingSessionInfo);
            T_CTW_LEventsBLL bllEvents = new T_CTW_LEventsBLL(loggingSessionInfo);
            T_CTW_PanicbuyingEventBLL bllPanicbuying = new T_CTW_PanicbuyingEventBLL(loggingSessionInfo);
            ///获取模版信息
            DataSet dsInteraction = bllInteraction.GetEventInteractionByTemplateId(para.TemplateId);
            if (dsInteraction != null && dsInteraction.Tables[0].Rows.Count > 0)
            {
                var entityInteraction = DataTableToObject.ConvertToList<EventInteractionInfo>(dsInteraction.Tables[0]);

                var entityTheme =DataTableToObject.ConvertToList<CTW_LEventThemeEntity>(bllTheme.GetThemeInfo(para.TemplateId).Tables[0]);//.QueryByEntity(new CTW_LEventThemeEntity() { TemplateId = new Guid(para.TemplateId), IsDelete = 0 }, null);
                List<CTW_LEventThemeEntity> listTheme = new List<CTW_LEventThemeEntity>();
                foreach (var theme in entityTheme)
                {
                    List<EventInteractionInfo> listEventInteractionInfo = new List<EventInteractionInfo>();

                    ///互动信息
                    foreach (var itemAction in entityInteraction.Where(a => a.ThemeId == theme.ThemeId.ToString() && a.TemplateId==theme.TemplateId.ToString()))
                    {
                        listEventInteractionInfo.Add(itemAction);
                        if (itemAction.InteractionType == 1)
                        {
                            itemAction.GameEventImageList = DataTableToObject.ConvertToList<GameEventImageInfo>(bllEvents.GetImageList(itemAction.LeventId).Tables[0]);
                        }
                        if (itemAction.InteractionType == 2)//促销
                        {
                            itemAction.PanicbuyingEventImage = DataTableToObject.ConvertToObject<PanicbuyingEventImageInfo>(bllPanicbuying.GetPanicbuyingEventImage(itemAction.LeventId).Tables[0].Rows[0]);
                        }
                        theme.EventInteractionList = listEventInteractionInfo;
                    };
                    listTheme.Add(theme);
                    rd.TemplateThemeList = listTheme;
                }
                rd.ActivityGroupId = entityTheme.FirstOrDefault().ActivityGroupId;
                rd.TemplateId = entityTheme.FirstOrDefault().TemplateId.ToString();
                rd.TemplateName = entityTheme.FirstOrDefault().TemplateName;
                rd.ImageURL = dsInteraction.Tables[0].Rows[0]["ImageURL"].ToString();
                //推广设置
                DataSet dsSpresd = bllSpreadSetting.GetSpreadSettingInfoByTemplateId(para.TemplateId);
                if(dsSpresd!=null && dsSpresd.Tables[0].Rows.Count>0)
                {
                    rd.TemplateSpreadSettingList = DataTableToObject.ConvertToList<CTW_SpreadSettingEntity>(dsSpresd.Tables[0]);
                }
                
                ///获取商户所有信息
                if (!string.IsNullOrEmpty(para.CTWEventId))
                {
                    T_CTW_LEventBLL bllCTWEvent = new T_CTW_LEventBLL(loggingSessionInfo);
                    DataSet dsCTWEvent = bllCTWEvent.GetLeventInfoByCTWEventId(para.CTWEventId);
                    ObjectImagesBLL bllImage = new ObjectImagesBLL(loggingSessionInfo);
                    var bllPrizes = new LPrizesBLL(loggingSessionInfo);

                    T_CTW_SpreadSettingBLL bllCustomerSpreadSetting = new T_CTW_SpreadSettingBLL(loggingSessionInfo);
                    if (dsCTWEvent != null && dsCTWEvent.Tables.Count > 0 && dsCTWEvent.Tables[0].Rows.Count>0)
                    {
                        rd.CustomerCTWEventInfo = DataTableToObject.ConvertToObject<CustomerCTWEventInfo>(dsCTWEvent.Tables[0].Rows[0]);
                        //游戏活动信息
                        if (dsCTWEvent.Tables[0].Rows[0]["InteractionType"].ToString() == "1")
                        {
                            LEventsBLL bllLevents = new LEventsBLL(loggingSessionInfo);
                            string strEventId = dsCTWEvent.Tables[0].Rows[0]["LeventId"].ToString();
                            var ds = bllLevents.GetNewEventInfo(strEventId);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                rd.CustomerCTWEventInfo.EventInfo = DataTableToObject.ConvertToObject<LEventsInfo>(ds.Tables[0].Rows[0]);//直接根据所需要的字段反序列化
                            }
                            DataSet dsPrizes = bllPrizes.GetPirzeList(strEventId);
                            if (dsPrizes.Tables != null && dsPrizes.Tables.Count > 0 && dsPrizes.Tables[0] != null && dsPrizes.Tables[0].Rows.Count > 0)
                            {
                                rd.CustomerCTWEventInfo.EventInfo.PrizeList = DataTableToObject.ConvertToList<Prize>(dsPrizes.Tables[0]);
                            }


                            rd.CustomerCTWEventInfo.EventInfo.ImageList = bllImage.QueryByEntity(new ObjectImagesEntity() { ObjectId = strEventId, IsDelete = 0 }, null).ToList();
                            
                        }
                        //促销活动信息
                        if (dsCTWEvent.Tables[0].Rows[0]["InteractionType"].ToString() == "2")
                        {
                            T_CTW_PanicbuyingEventKVBLL bllPanicbuyingEventKV = new T_CTW_PanicbuyingEventKVBLL(loggingSessionInfo);
                            rd.CustomerCTWEventInfo.PanicbuyingEventInfo =DataTableToObject.ConvertToObject<T_CTW_PanicbuyingEventKVEntity>( bllPanicbuyingEventKV.GetPanicbuyingEventKV(dsCTWEvent.Tables[0].Rows[0]["LeventId"].ToString()).Tables[0].Rows[0]);
                        }
                        var ds2 = bllCTWEvent.GetMaterialTextInfo(dsCTWEvent.Tables[0].Rows[0]["OnlineQRCodeId"].ToString());//活动图文素材对应的keyword其实是这个活动的标识，也就是生成二维码的关键字
                        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                        {
                            rd.CustomerCTWEventInfo.MaterialText = DataTableToObject.ConvertToObject<WMaterialTextEntity>(ds2.Tables[0].Rows[0]);//直接根据所需要的字段反序列化
                            rd.CustomerCTWEventInfo.MappingId = ds2.Tables[0].Rows[0]["MappingId"].ToString();
                        }
                        //推广设置

                        DataSet dsCustomerSpread = bllCustomerSpreadSetting.GetSpreadSettingByCTWEventId(para.CTWEventId);
                        if (dsCustomerSpread != null && dsCustomerSpread.Tables[0].Rows.Count > 0)
                        {
                            rd.CustomerCTWEventInfo.SpreadSettingList = DataTableToObject.ConvertToList<T_CTW_SpreadSettingEntity>(dsCustomerSpread.Tables[0]);
                        }
                        //触点
                        ContactEventBLL bllContactEvent=new ContactEventBLL(loggingSessionInfo);
                        DataSet dsContact = bllContactEvent.GetContactEventByCTWEventId(para.CTWEventId);
                        if (dsContact != null && dsContact.Tables[0].Rows.Count > 0)
                        {
                            List<ContactEventInfo> ContactInfoList = new List<ContactEventInfo>();
                            ContactEventInfo ContactInfo = new ContactEventInfo();                   
                            foreach(DataRow dr in dsContact.Tables[0].Rows)
                            {
                                ContactInfo = new ContactEventInfo();    
                                ContactInfo.ContactTypeCode = dr["ContactTypeCode"].ToString();
                                DataSet dsPrizes = bllPrizes.GetPirzeListForCTW(dr["ContactEventId"].ToString());
                                if (dsPrizes.Tables != null && dsPrizes.Tables.Count > 0 && dsPrizes.Tables[0] != null && dsPrizes.Tables[0].Rows.Count > 0)
                                {
                                    ContactInfo.ContactPrizeList = DataTableToObject.ConvertToList<Prize>(dsPrizes.Tables[0]);
                                }
                                ContactInfoList.Add(ContactInfo);
                            }
                            rd.CustomerCTWEventInfo.ContactEventList = ContactInfoList;

                        }
                    }


                    rd.CTWEventId = para.CTWEventId;
                }
                else
                {
                    rd.CTWEventId = Guid.NewGuid().ToString();
                }
            }
            return rd;

        }
    }
}