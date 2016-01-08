using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace JIT.CPOS.BS.BLL.WX.BaseClass
{
    /// <summary>
    /// 微信订阅号基类
    /// </summary>
    public class SubscriptionBLL : BaseBLL
    {
        #region 构造函数

        public SubscriptionBLL(HttpContext httpContext, RequestParams requestParams)
            : base(httpContext, requestParams)
        {

        }

        #endregion


        #region 处理位置消息

        public override void HandlerLocation()
        {
            string locationX = requestParams.XmlNode.SelectSingleNode("//Location_X").InnerText;   //地理位置维度
            string locationY = requestParams.XmlNode.SelectSingleNode("//Location_Y").InnerText;   //地理位置精度
            string scale = requestParams.XmlNode.SelectSingleNode("//Scale").InnerText;   //地图缩放大小
            string label = requestParams.XmlNode.SelectSingleNode("//Label").InnerText;   //地理位置信息
            string msgId = requestParams.XmlNode.SelectSingleNode("//MsgId").InnerText;   //消息id，64位整型
            BaseService.WriteLogWeixin("地理位置维度Location_X：---------------" + locationX);
            BaseService.WriteLogWeixin("地理位置精度Location_Y：---------------" + locationY);
            BaseService.WriteLogWeixin("地图缩放大小Scale：---------------" + scale);
            BaseService.WriteLogWeixin("地理位置信息Label：---------------" + label);
            BaseService.WriteLogWeixin("消息id，64位整型MsgId：---------------" + msgId);

            WXLocation(httpContext, commonService, requestParams, locationX, locationY);
        }

        public static void WXLocation(HttpContext httpContext, CommonBLL commonService, RequestParams requestParams, string locationX, string locationY)
        {
            WMenuDAO wMenuDAO = new WMenuDAO(requestParams.LoggingSessionInfo);
            var custId = wMenuDAO.GetCustomerIdByWx(requestParams.WeixinId);
            if (requestParams.LoggingSessionInfo.CurrentUser == null)
                requestParams.LoggingSessionInfo.CurrentUser = new Entity.User.UserInfo();
            requestParams.LoggingSessionInfo.CurrentUser.customer_id = custId;

            BaseService.WriteLogWeixin("customerId：---------------" + custId);
            BaseService.WriteLogWeixin("openID：---------------" + requestParams.OpenId);
            BaseService.WriteLogWeixin("weixinID：---------------" + requestParams.WeixinId);

            string HandlerLocation_xyds = ConfigurationManager.AppSettings["HandlerLocation_xyds"];
            BaseService.WriteLogWeixin("HandlerLocation_xyds：---------------" + HandlerLocation_xyds);
            if (HandlerLocation_xyds != null && HandlerLocation_xyds.Length > 0 && HandlerLocation_xyds.Contains(requestParams.WeixinId))
            {
                BaseService.WriteLogWeixin("xyds：---------------");
                VipBLL server = new VipBLL(requestParams.LoggingSessionInfo);
                var vipInfo = server.SearchVipInfoLocation(new VipSearchEntity()
                {
                    RoleCode = "CampusAmbassadors",
                    Longitude = locationY,
                    Latitude = locationX,
                    OrderBy = " a.Distance asc ",
                    Page = 1,
                    PageSize = 1,
                });

                if (vipInfo != null && vipInfo.vipInfoList != null && vipInfo.vipInfoList.Count > 0)
                {
                    var vip = vipInfo.vipInfoList[0];
                    if (vip != null)
                    {
                        var content = "";
                        content += "目前离你最近的校园产品专家是{0}，电话：{1}，{2}，距离{3}米。如有任何疑问也可直接回复消息与我们联系，我们将尽快答复。";
                        content = string.Format(
                            content,
                            vip.VipName,
                            vip.Phone,
                            vip.DeliveryAddress,
                            vip.Distance * 1000
                            );

                        BaseService.WriteLogWeixin("发送微信消息：---------------");
                        commonService.ResponseTextMessage(requestParams.WeixinId, requestParams.OpenId, content, httpContext,requestParams);
                        BaseService.WriteLogWeixin("发送微信消息完成：---------------");

                        MarketSendLogBLL marketSendLogBLL = new BLL.MarketSendLogBLL(requestParams.LoggingSessionInfo);
                        var logObj = new MarketSendLogEntity()
                        {
                            LogId = Common.Utils.NewGuid(),
                            VipId = vip.VIPID,
                            MarketEventId = "华硕校园大使推介活动",
                            TemplateContent = content,
                            SendTypeId = "1",
                            WeiXinUserId = vip.WeiXinUserId,
                            Phone = vip.Phone,
                            IsSuccess = 0
                        };
                        marketSendLogBLL.Create(logObj);

                        VipVipMappingBLL vipVipMappingBLL = new VipVipMappingBLL(requestParams.LoggingSessionInfo);
                        var vipMapObj = new VipVipMappingEntity()
                        {
                            MappingId = Guid.NewGuid(),
                            VipIdSrc  = vip.VIPID,
                            VipIdDst = requestParams.OpenId
                        };
                        vipVipMappingBLL.Create(vipMapObj);

                        logObj.IsSuccess = 1;
                        marketSendLogBLL.Update(logObj, false);

                    }
                }
            }
            else
            {
                string strError = "";
                StoreBrandMappingBLL server = new StoreBrandMappingBLL(requestParams.LoggingSessionInfo);
                var storeInfo = server.GetStoreListByItem(null
                                                            , 1
                                                            , 10
                                                            , locationY
                                                            , locationX
                                                            , out strError);

                if (storeInfo != null && storeInfo.StoreBrandList != null && storeInfo.StoreBrandList.Count > 0)
                {
                    var newsList = new List<WMaterialTextEntity>();
                    var original_url = ConfigurationManager.AppSettings["website_WWW"].ToString();
                    if (!original_url.EndsWith("/")) original_url += "/";
                    foreach (var store in storeInfo.StoreBrandList)
                    {
                        var url = original_url + "HtmlApps/Auth.html?pageName=Map&customerId=" + requestParams.LoggingSessionInfo.CurrentUser.customer_id;
                        url += "&lng=" + store.Longitude + "&lat=" + store.Latitude;
                        url += "&storeId=" + store.StoreId;
                        url += "&addr=" + HttpUtility.UrlEncode(store.Address);
                        url += "&store=" + HttpUtility.UrlEncode(store.StoreName);

                        #region 分享业务 链接后面加上openId和userId

                        //if (url.IndexOf("ParAll=") != -1)
                        //{
                        //    var vipId = string.Empty;

                        //    VipBLL vipService = new VipBLL(requestParams.LoggingSessionInfo);
                        //    var vipEntity = vipService.QueryByEntity(new VipEntity { WeiXinUserId = requestParams.OpenId, WeiXin = requestParams.WeixinId }, null);
                        //    if (vipEntity != null && vipEntity.Length > 0)
                        //    {
                        //        vipId = vipEntity.FirstOrDefault().VIPID;
                        //    }

                        //    url += "&openId=" + requestParams.OpenId + "&userId=" + vipId;
                        //}

                        #endregion

                        newsList.Add(new WMaterialTextEntity()
                        {
                            Title = store.StoreName + "(" + store.Distance.ToString("f1") + "公里)", // Title
                            Text = store.StoreName + "(" + store.Distance.ToString("f1") + "公里)", // Author
                            CoverImageUrl = store.ImageUrl, // CoverImageUrl
                            OriginalUrl = url
                        });
                    }

                    commonService.ResponseNewsMessage(requestParams.WeixinId, requestParams.OpenId, newsList, httpContext, requestParams);
                }
            }
        }
        #endregion

        #region 用户关注微信号

        public override void UserSubscribe()
        {
            BaseService.WriteLogWeixin("贱人贱人");
            //设置关注信息
            var modelDAO = new WModelDAO(requestParams.LoggingSessionInfo);
            var ds = modelDAO.GetMaterialByWeixinIdJermyn(requestParams.WeixinId, 2);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string typeId = ds.Tables[0].Rows[0]["ReplyType"].ToString();  //素材类型 1=文字2=图片3=图文4=语音5=视频6=其他
                string ReplyId = ds.Tables[0].Rows[0]["ReplyId"].ToString();  //素材ID
                string Text = ds.Tables[0].Rows[0]["text"].ToString();  //素材ID

                BaseService.WriteLogWeixin("typeId：" + typeId);
                BaseService.WriteLogWeixin("ReplyId：" + ReplyId);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        //ReplyText(materialId);
                        ReplyTextJermyn(Text);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        //ReplyNews(materialId);
                        ReplyNewsJermyn(ReplyId, 2, 1);
                        break;
                    case MaterialType.OTHER:    //后台处理
                        break;
                    default:
                        break;
                }
            }

            #region Jermyn20140728 订阅号添加获取用户信息
            var application = new WApplicationInterfaceDAO(requestParams.LoggingSessionInfo);
            var appEntitys = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = requestParams.WeixinId }, null);

            if (appEntitys != null && appEntitys.Length > 0)
            {
                var entity = appEntitys.FirstOrDefault();

                BaseService.WriteLogWeixin("AppID:  " + entity.AppID);
                BaseService.WriteLogWeixin("AppSecret:  " + entity.AppSecret);

                //扫描带参数二维码事件
                //var eventKey = requestParams.XmlNode.SelectSingleNode("//EventKey");
                //BaseService.WriteLogWeixin("eventKey:  " + eventKey.InnerText);

                var qrcodeId = string.Empty;
                //if (!string.IsNullOrEmpty(eventKey.InnerText))
                //{
                //    qrcodeId = eventKey.InnerText.Substring(8);
                //}

                //BaseService.WriteLogWeixin("qrcodeId:  " + qrcodeId);

                //保存用户信息
                commonService.SaveUserInfo(requestParams.OpenId, requestParams.WeixinId, "1", entity.AppID, entity.AppSecret, qrcodeId, requestParams.LoggingSessionInfo);
                BaseService.WriteLogWeixin("推送用户信息到业务系统成功.  " );
            }
            #endregion

        }

        
        #endregion

    }
}
