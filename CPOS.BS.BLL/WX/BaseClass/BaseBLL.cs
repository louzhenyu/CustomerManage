using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using JIT.CPOS.Common;
using System;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL.WX
{
    /// <summary>
    /// 微信基类
    /// </summary>
    public class BaseBLL
    {
        #region 成员变量

        protected CommonBLL commonService = null;
        protected HttpContext httpContext = null;
        protected RequestParams requestParams = null;

        #endregion

        #region 构造函数

        public BaseBLL(HttpContext httpContext, RequestParams requestParams)
        {
            commonService = new CommonBLL();
            this.httpContext = httpContext;
            this.requestParams = requestParams;
        }

        #endregion

        #region 响应微信平台推送的消息

        /// <summary>
        /// 响应微信平台推送的消息
        /// </summary>
        public void ResponseMsg()
        {
            //获取会员信息
            VipBLL vipService = new VipBLL(requestParams.LoggingSessionInfo);
            var vipId = string.Empty;
            var vipEntity = vipService.QueryByEntity(new VipEntity
            {
                WeiXinUserId = requestParams.OpenId,
                WeiXin = requestParams.WeixinId
            }, null);
            VipEntity vip = null;
            if (vipEntity != null && vipEntity.Length > 0)
            {
                vipId = vipEntity.FirstOrDefault().VIPID;
                vip = vipEntity.FirstOrDefault();
            }

            //根据不同的消息类型，进行不同的处理操作
            switch (requestParams.MsgType)
            {
                case MsgType.TEXT:    //文本消息
                    BaseService.WriteLogWeixin("消息类型：---------------texttext文本消息！");

                    var content = requestParams.XmlNode.SelectSingleNode("//Content").InnerText.Trim();   //文本消息内容
                    
                    //HandlerText();
                    //GetIsMoreCS(content); //多客服  
                    //StoreRebate(content, vipId);//门店返现推送消息
                    #region
                    var vipDcodeBll = new VipDCodeBLL(requestParams.LoggingSessionInfo);
                    var vipDcodeEntity = vipDcodeBll.GetByID(content.Replace(" ", ""));
                    if (vipDcodeEntity == null)
                    {
                        HandlerText();
                        GetIsMoreCS(content); //多客服                       
                    }
                    else
                    {
                        content = content.Replace(" ", "");//去空格
                        if (vipDcodeEntity.IsReturn == 1)
                        {
                            if (DateTime.Now > (vipDcodeEntity.LastUpdateTime ?? DateTime.Now).AddSeconds(3))
                            {
                                StoreRebateRepeaterMessage(vipEntity[0], content);
                            }

                        }
                        else if (DateTime.Now > (vipDcodeEntity.CreateTime ?? DateTime.Now).AddDays(1))
                        {
                            StoreRebateByTimeOut(vipEntity[0]);
                        }
                        else
                        {
                            StoreRebate(content, vipId);//门店返现推送消息
                        }

                    }
                    #endregion

                    new WUserMessageDAO(requestParams.LoggingSessionInfo).Create(new WUserMessageEntity
                    {
                        MessageId = Utils.NewGuid(),
                        VipId = vipId,
                        MaterialTypeId = "1",
                        Text = content,
                        ImageUrl = string.Empty,
                        OpenId = requestParams.OpenId,
                        WeiXinId = requestParams.WeixinId,
                        DataFrom = 1
                    });

                    break;
                case MsgType.IMAGE:   //图片消息

                    BaseService.WriteLogWeixin("消息类型：---------------image图片消息！");

                    var picUrl = requestParams.XmlNode.SelectSingleNode("//PicUrl").InnerText.Trim();   //图片链接
                    GetIsMoreCS(picUrl); //多客服
                    new WUserMessageDAO(requestParams.LoggingSessionInfo).Create(new WUserMessageEntity
                    {
                        MessageId = Utils.NewGuid(),
                        VipId = vipId,
                        MaterialTypeId = "2",
                        Text = string.Empty,
                        ImageUrl = picUrl,
                        OpenId = requestParams.OpenId,
                        WeiXinId = requestParams.WeixinId,
                        DataFrom = 1
                    });

                    HandlerImage();
                    
                    break;
                case MsgType.LOCATION:    //地理位置
                    BaseService.WriteLogWeixin("消息类型：---------------location地理位置！");
                    HandlerLocation();
                    break;
                case MsgType.EVENT:   //事件
                    BaseService.WriteLogWeixin("消息类型：---------------event事件！");
                    HandlerEvent(vip);
                    break;
            }
        }

        public void StoreRebateRepeaterMessage(VipEntity vipEntity, string content)
        {
            BaseService.WriteLogWeixin(content);
            //发送消息

            JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage("对不起，该返利码已经被领取", "1", requestParams.LoggingSessionInfo, vipEntity);

        }
        public void StoreRebateByTimeOut(VipEntity vipEntity)
        {
            //发送消息
            JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage("对不起，您的返利码已经过期，请在收到返利码后的24小时内使用", "1", requestParams.LoggingSessionInfo, vipEntity);

        }

        /// <summary>
        /// 根据用户发送的二维码去二维码表中VipDCode匹配
        /// </summary>
        /// <param name="content"></param>
        /// <param name="vipID"></param>
        public void StoreRebate(string content, string vipID)
        {
            content = content.Trim();
            BaseService.WriteLogWeixin("返利信息：" + content);

            VipDCodeBLL bll = new VipDCodeBLL(requestParams.LoggingSessionInfo);
            WXSalesPolicyRateBLL SalesPolicybll = new WXSalesPolicyRateBLL(requestParams.LoggingSessionInfo);
            //var tran = bll.GetTran();
            try
            {
                //判断当前发送二维码的微信号是否是 二维码表中当前二维码Code的会员 VipId=vipID;
                //var temp = bll.QueryByEntity(new VipDCodeEntity { IsDelete = 0, DCodeId = content}, null);
                var temp = bll.GetByID(content);
                decimal? ReturnAmount = 0;
                string PushInfo = string.Empty;
                //using (tran.Connection)
                //{
                if (temp != null)   //如果可以匹配，则更新二维码表中的会员ID，OpenId
                {

                    #region 1.更新返现金额。更新返现状态
                    VipDCodeEntity entity = new VipDCodeEntity();
                    entity = temp;
                    DataSet ds = SalesPolicybll.getReturnAmount(Convert.ToDecimal(entity.SalesAmount), entity.CustomerId);
                    if (ds != null && ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
                    {
                        BaseService.WriteLogWeixin("该客户没有配置策略信息");

                        throw new Exception("该客户没有配置策略信息");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //返现金额
                        ReturnAmount = entity.ReturnAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ReturnAmount"].ToString());
                        //返现消息内容
                        PushInfo = ds.Tables[0].Rows[0]["PushInfo"].ToString();

                        BaseService.WriteLogWeixin("PushInfo1：" + PushInfo);

                    }
                    else
                    {
                        //返现金额
                        ReturnAmount = entity.ReturnAmount = Convert.ToDecimal(ds.Tables[1].Rows[0]["ReturnAmount"].ToString());
                        //返现消息内容
                        PushInfo = ds.Tables[1].Rows[0]["PushInfo"].ToString();

                        BaseService.WriteLogWeixin("PushInfo2：" + PushInfo);
                    }

                    entity.OpenId = requestParams.OpenId;
                    entity.VipId = vipID;
                    entity.ReturnAmount = ReturnAmount;
                    VipAmountBLL Amountbll = new VipAmountBLL(requestParams.LoggingSessionInfo);

                    var vipBll = new VipBLL(requestParams.LoggingSessionInfo);

                    var vipEntity = vipBll.GetByID(vipID);


                    string strErrormessage = "";
                    if (entity.IsReturn != 1)  //当未返现的时候金额变更
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "begin update VipDcode："
                        });

                        if (Amountbll.SetVipAmountChange(entity.CustomerId, 2, vipID, ReturnAmount ?? 0, entity.ObjectId, "门店返现", "IN", out strErrormessage))
                        {
                            entity.IsReturn = 1;
                            entity.DCodeId = content;
                            bll.Update(entity); //更新返现金额

                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = "update VipDcode success"
                            });
                        }
                    }

                    #endregion



                    var vipamountBll = new VipAmountBLL(requestParams.LoggingSessionInfo);
                    var vipAmountEntity = vipamountBll.GetByID(vipID);
                    decimal endAmount = 0;
                    if (vipAmountEntity != null)
                    {
                        endAmount = vipAmountEntity.EndAmount ?? 0;
                    }
                    var message = PushInfo.Replace("#SalesAmount#", entity.SalesAmount.ToString()).Replace("#ReturnAmount#", Convert.ToDecimal(ReturnAmount).ToString("0.00")).Replace("#EndAmount#", endAmount.ToString("0.00")).Replace("#VipName#", vipEntity.VipName);

                    #region 插入门店返现推送消息日志表
                    WXSalesPushLogBLL PushLogbll = new WXSalesPushLogBLL(requestParams.LoggingSessionInfo);
                    WXSalesPushLogEntity pushLog = new WXSalesPushLogEntity();
                    pushLog.LogId = Guid.NewGuid();
                    pushLog.WinXin = requestParams.WeixinId;
                    pushLog.OpenId = requestParams.OpenId;
                    pushLog.VipId = vipID;
                    pushLog.PushInfo = message;
                    pushLog.DCodeId = content;
                    pushLog.RateId = Guid.NewGuid();
                    PushLogbll.Create(pushLog);
                    #endregion
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = message
                    });
                    string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, "1", requestParams.LoggingSessionInfo, vipEntity);

                    Loggers.Debug(new DebugLogInfo() { Message = "消息推送完成，code=" + code + ", message=" + message });

                    #region 增加抽奖信息
                    var rateList = SalesPolicybll.QueryByEntity(new WXSalesPolicyRateEntity { CustomerId = temp.CustomerId }, null);
                    if (rateList == null || rateList.Length == 0)
                    {
                        //rateList = SalesPolicybll.QueryByEntity(new WXSalesPolicyRateEntity{CustomerId =null},null);
                        rateList = SalesPolicybll.GetWxSalesPolicyRateList().ToArray();
                    }

                    if (rateList != null && rateList.Length > 0)
                    {

                        var wxSalespolicyRateMapBll = new WXSalesPolicyRateObjectMappingBLL(requestParams.LoggingSessionInfo);

                        var rateMappingEntity =
                            wxSalespolicyRateMapBll.QueryByEntity(new WXSalesPolicyRateObjectMappingEntity { RateId = rateList[0].RateId }, null);
                        if (rateMappingEntity != null && rateMappingEntity.Length > 0)
                        {
                            if (Convert.ToDecimal(temp.SalesAmount) >= rateMappingEntity[0].CoefficientAmount)
                            {
                                if (rateMappingEntity[0].PushInfo != null)
                                {
                                    var eventMessage = rateMappingEntity[0].PushInfo.Replace("#CustomerId#", temp.CustomerId).Replace("#EventId#", rateMappingEntity[0].ObjectId).Replace("#VipId#", vipID).Replace("#OpenId#", vipEntity.WeiXinUserId);


                                    BaseService.WriteLogWeixin("微信推送的抽奖活动URL：" + eventMessage);

                                    WXSalesPushLogEntity qrLog = new WXSalesPushLogEntity();
                                    qrLog.LogId = Guid.NewGuid();
                                    qrLog.WinXin = requestParams.WeixinId;
                                    qrLog.OpenId = requestParams.OpenId;
                                    qrLog.VipId = vipID;
                                    qrLog.PushInfo = eventMessage;
                                    qrLog.DCodeId = content;
                                    qrLog.RateId = Guid.NewGuid();
                                    PushLogbll.Create(qrLog);

                                    #region 增加抽奖机会

                                    LEventsVipObjectBLL eventbll = new LEventsVipObjectBLL(requestParams.LoggingSessionInfo);
                                    LEventsVipObjectEntity evententity = new LEventsVipObjectEntity();
                                    evententity.MappingId = Guid.NewGuid().ToString();
                                    evententity.EventId = rateMappingEntity[0].ObjectId;
                                    evententity.VipId = vipID;
                                    evententity.ObjectId = "";
                                    evententity.IsCheck = 0;
                                    evententity.LotteryCode = "0";
                                    evententity.IsLottery = 0;
                                    eventbll.Create(evententity);
                                    #endregion

                                    JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(eventMessage, "1", requestParams.LoggingSessionInfo, vipEntity);
                                }
                            }
                        }


                    }
                    #endregion
                }
                // }
            }
            catch (Exception)
            {
                // tran.Rollback();
                throw;
            }

        }

        #endregion

        #region WApplicationInterface 表中新增字段IsMoreCS=1 支持。=0不支持判断是否支持多客服
        public void GetIsMoreCS(string content)
        {
            try
            {
                WApplicationInterfaceBLL bll = new WApplicationInterfaceBLL(requestParams.LoggingSessionInfo);
                int isMoreCs = 0;
                DataSet wentity = bll.GetIsMoreCS(requestParams.LoggingSessionInfo.ClientID, requestParams.WeixinId);
                if (wentity != null && wentity.Tables[0].Rows.Count > 0)
                {
                    isMoreCs = Convert.ToInt32(wentity.Tables[0].Rows[0]["IsMoreCS"].ToString());
                    if (isMoreCs == 1)  //是否支持多客服
                        ResponseTextMessage(requestParams.WeixinId, requestParams.OpenId, content, httpContext);
                }
            }
            catch (Exception ex)
            {
                BaseService.WriteLogWeixin("异常信息:  " + ex.ToString());
            }
        }
        #endregion

        #region 回复文本消息

        /// <summary>
        /// 回复文本消息 多客服【<![CDATA[transfer_customer_service]]>】 Add by changjian.tian 2014-06-12
        /// </summary>
        /// <param name="weixinID">开发者微信号</param>
        /// <param name="openID">接收方帐号（收到的OpenID）</param>
        /// <param name="content">文本消息内容</param>
        public void ResponseTextMessage(string weixinID, string openID, string content, HttpContext httpContext)
        {
            var response = "<xml>";
            response += "<ToUserName><![CDATA[" + openID + "]]></ToUserName>";
            response += "<FromUserName><![CDATA[" + weixinID + "]]></FromUserName>";
            response += "<CreateTime>" + new BaseService().ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            response += "<MsgType><![CDATA[transfer_customer_service]]></MsgType>";   //多客户
            response += "<Content><![CDATA[" + content + "]]></Content> ";
            response += "<FuncFlag>0</FuncFlag>";
            response += "</xml>";

            BaseService.WriteLogWeixin("公众平台返回给用户的文本消息:  " + response);
            BaseService.WriteLogWeixin("回复文本消息结束--------测试已经调用多客服方法。-----------------------------------\n");

            httpContext.Response.Write(response);
        }

        #endregion


        #region 处理文本消息

        //处理文本消息
        public void HandlerTextOld()
        {
            var content = requestParams.XmlNode.SelectSingleNode("//Content").InnerText.Trim();   //文本消息内容

            var keywordDAO = new WKeywordReplyDAO(requestParams.LoggingSessionInfo);
            var ds = keywordDAO.GetMaterialByKeyword(content);

            #region 复星临时代码  以后需要删除  qianzhi  2014-01-16

            //复星临时代码  以后需要删除  qianzhi  2014-01-16
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ds = keywordDAO.GetMaterialByKeyword("任意回复");
            }

            #endregion

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                string materialId = ds.Tables[0].Rows[0]["MaterialId"].ToString();  //素材ID

                BaseService.WriteLogWeixin("typeId：" + typeId);
                BaseService.WriteLogWeixin("materialId：" + materialId);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        ReplyText(materialId);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        ReplyNews(materialId);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //推送客服消息  qianzhi  2014-03-04
                VipBLL vipService = new VipBLL(requestParams.LoggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity
                {
                    WeiXinUserId = requestParams.OpenId,
                    WeiXin = requestParams.WeixinId
                }, null);

                if (vipList != null && vipList.Length > 0)
                {
                    var vipEntity = vipList.FirstOrDefault();
                    CSInvokeMessageBLL msg = new CSInvokeMessageBLL(requestParams.LoggingSessionInfo);
                    msg.SendMessage(1, vipEntity.VIPID, 0, null, content, null, null, null, 1);
                }
            }
        }

        /// <summary>
        /// 处理文本消息 Jermyn20140512
        /// </summary>
        public virtual void HandlerText()
        {
            var content = requestParams.XmlNode.SelectSingleNode("//Content").InnerText.Trim();   //文本消息内容
            var weixinId = requestParams.WeixinId;
            var keywordDAO = new WKeywordReplyDAO(requestParams.LoggingSessionInfo);
            //var ds = keywordDAO.GetMaterialByKeyword(content);
            var ds = keywordDAO.GetMaterialByKeywordJermyn(content, weixinId, 1);
            int keywordType = 1;

            #region 如果没有关键字回复，给予自动回复内容

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ds = keywordDAO.GetMaterialByKeywordJermyn(content, weixinId, 3);
                keywordType = 3;
            }

            #endregion

            #region 如果回复，调用老的接口

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                HandlerTextOld();
                return;
            }

            #endregion

            //#region 复星临时代码  以后需要删除  qianzhi  2014-01-16

            ////复星临时代码  以后需要删除  qianzhi  2014-01-16
            //if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            //{
            //    ds = keywordDAO.GetMaterialByKeyword("任意回复");
            //}

            //#endregion

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string Text = ds.Tables[0].Rows[0]["Text"].ToString();  //素材类型
                string ReplyId = ds.Tables[0].Rows[0]["ReplyId"].ToString();  //素材ID
                string typeId = ds.Tables[0].Rows[0]["ReplyType"].ToString();  //素材ID

                BaseService.WriteLogWeixin("ReplyId：" + ReplyId);
                BaseService.WriteLogWeixin("typeId：" + typeId);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        //ReplyText(materialId);
                        ReplyTextJermyn(Text);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        //ReplyNews(materialId);
                        ReplyNewsJermyn(ReplyId, keywordType, 1);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //推送客服消息  qianzhi  2014-03-04
                VipBLL vipService = new VipBLL(requestParams.LoggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity
                {
                    WeiXinUserId = requestParams.OpenId,
                    WeiXin = requestParams.WeixinId
                }, null);

                if (vipList != null && vipList.Length > 0)
                {
                    var vipEntity = vipList.FirstOrDefault();
                    CSInvokeMessageBLL msg = new CSInvokeMessageBLL(requestParams.LoggingSessionInfo);
                    msg.SendMessage(1, vipEntity.VIPID, 0, null, content, null, null, null, 1);
                }
            }
        }
        #endregion

        #region 处理图片消息

        //处理图片消息
        public virtual void HandlerImage()
        {
            string picUrl = requestParams.XmlNode.SelectSingleNode("//PicUrl").InnerText;  //图片链接
            string msgId = requestParams.XmlNode.SelectSingleNode("//MsgId").InnerText;   //消息id，64位整型
            BaseService.WriteLogWeixin("图片链接PicUrl：---------------" + picUrl);
            BaseService.WriteLogWeixin("消息id，64位整型MsgId：---------------" + msgId);

            //推送客服消息  qianzhi  2014-03-04
            VipBLL vipService = new VipBLL(requestParams.LoggingSessionInfo);
            var vipList = vipService.QueryByEntity(new VipEntity
            {
                WeiXinUserId = requestParams.OpenId,
                WeiXin = requestParams.WeixinId
            }, null);

            if (vipList != null && vipList.Length > 0)
            {
                var vipEntity = vipList.FirstOrDefault();
                CSInvokeMessageBLL msg = new CSInvokeMessageBLL(requestParams.LoggingSessionInfo);
                msg.SendMessage(1, vipEntity.VIPID, 0, null, picUrl, null, null, null, 2);
            }
        }

        #endregion

        #region 处理位置消息

        public virtual void HandlerLocation()
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
        }

        #endregion

        #region 处理事件

        //处理事件  事件类型(subscribe, unsubscribe, click)
        protected void HandlerEvent(VipEntity vip)
        {
            string eventStr = requestParams.XmlNode.SelectSingleNode("//Event").InnerText.Trim().ToLower();
            var eventKey = string.Empty;

            switch (eventStr)
            {
                //关注
                case EventType.SUBSCRIBE:
                    BaseService.WriteLogWeixin("用户加关注！");
                    #region test sending template message to new vip
                    //if (null != vip)
                    //{
                    //    var vipBll = new VipBLL(requestParams.LoggingSessionInfo);
                    //    vipBll.SendNotification2NewVip(vip);
                    //}
                    #endregion
                    UserSubscribe();
                    break;
                //取消关注
                case EventType.UNSUBSCRIBE:
                    BaseService.WriteLogWeixin("用户取消关注！");
                    UserUnSubscribe();
                    break;
                //点击
                case EventType.CLICK:
                    BaseService.WriteLogWeixin("click事件");

                    eventKey = requestParams.XmlNode.SelectSingleNode("//EventKey").InnerText;
                    BaseService.WriteLogWeixin("EventKey：" + eventKey);

                    HandlerClickJermyn(eventKey); //新版本 Jermyn20140512
                    HandlerClick(eventKey);         //旧版本，需要兼容
                    break;
                //扫描带参数二维码事件
                case EventType.SCAN:
                    BaseService.WriteLogWeixin("扫描带参数二维码事件，用户已关注时的事件推送");

                    eventKey = requestParams.XmlNode.SelectSingleNode("//EventKey").InnerText;
                    BaseService.WriteLogWeixin("EventKey：" + eventKey);
                    HandlerScan(eventKey);

                    break;
                case EventType.TEMPLATESENDJOBFINISH:
                    BaseService.WriteLogWeixin("发送模板消息完成推送");
                    var status = requestParams.XmlNode.SelectSingleNode("//Status").InnerText;
                    HandleSendTemplateMessage(status);
                    break;
            }
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">是否成功</param>
        private void HandleSendTemplateMessage(string status)
        {
            BaseService.WriteLogWeixin("发送模板消息完成状态:" + status);
            this.httpContext.Response.Write("");
        }
        #region 用户关注微信号

        //public virtual void UserSubscribe()
        //{
        //    //设置关注信息
        //    var modelDAO = new WModelDAO(requestParams.LoggingSessionInfo);
        //    var ds = modelDAO.GetMaterialByWeixinIdJermyn(requestParams.WeixinId, 2);

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        string typeId = ds.Tables[0].Rows[0]["ReplyType"].ToString();  //素材类型 1=文字2=图片3=图文4=语音5=视频6=其他
        //        string ReplyId = ds.Tables[0].Rows[0]["ReplyId"].ToString();  //素材ID
        //        string Text = ds.Tables[0].Rows[0]["text"].ToString();  //素材ID

        //        BaseService.WriteLogWeixin("typeId：" + typeId);
        //        BaseService.WriteLogWeixin("ReplyId：" + ReplyId);

        //        switch (typeId)
        //        {
        //            case MaterialType.TEXT:         //回复文字消息 
        //                //ReplyText(materialId);
        //                ReplyTextJermyn(Text);
        //                break;
        //            case MaterialType.IMAGE_TEXT:   //回复图文消息 
        //                //ReplyNews(materialId);
        //                ReplyNewsJermyn(ReplyId, 2, 1);
        //                break;
        //            case MaterialType.OTHER:    //后台处理
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //}

        //用户关注微信号
        public virtual void UserSubscribe()
        {
            //设置关注信息
            var modelDAO = new WModelDAO(requestParams.LoggingSessionInfo);
            var ds = modelDAO.GetMaterialByWeixinId(requestParams.WeixinId);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                string materialId = ds.Tables[0].Rows[0]["MaterialId"].ToString();  //素材ID

                BaseService.WriteLogWeixin("typeId：" + typeId);
                BaseService.WriteLogWeixin("materialId：" + materialId);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        ReplyText(materialId);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        ReplyNews(materialId);
                        break;
                    case MaterialType.OTHER:    //后台处理
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region 用户取消关注微信号

        //用户取消关注微信号
        public virtual void UserUnSubscribe()
        {

        }

        #endregion

        #region 处理点击事件

        //处理点击事件
        public virtual void HandlerClick(string eventKey)
        {
            //事件KEY值，与自定义菜单接口中KEY值对应 
            BaseService.WriteLogWeixin("EventKey：" + eventKey);

            #region 动态处理事件KEY值

            var menuDAO = new WMenuDAO(requestParams.LoggingSessionInfo);
            var ds = menuDAO.GetMenusByKey(requestParams.WeixinId, eventKey);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                string materialId = ds.Tables[0].Rows[0]["MaterialId"].ToString();  //素材ID
                string modelId = ds.Tables[0].Rows[0]["ModelId"].ToString();        //模型ID

                BaseService.WriteLogWeixin("typeId：" + typeId);
                BaseService.WriteLogWeixin("materialId：" + materialId);
                BaseService.WriteLogWeixin("modelId：" + modelId);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        ReplyText(materialId);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        ReplyNews(materialId);
                        break;
                    default:
                        break;
                }
            }

            #endregion
        }

        /// <summary>
        /// 处理点击事件 Jermyn20140512
        /// </summary>
        /// <param name="eventKey"></param>
        public virtual void HandlerClickJermyn(string eventKey)
        {
            //事件KEY值，与自定义菜单接口中KEY值对应 
            BaseService.WriteLogWeixin("EventKey：" + eventKey);

            #region 动态处理事件KEY值

            var menuDAO = new WMenuDAO(requestParams.LoggingSessionInfo);
            var ds = menuDAO.GetMenusByKeyJermyn(requestParams.WeixinId, eventKey);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string typeId = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();  //素材类型
                string Text = ds.Tables[0].Rows[0]["Text"].ToString();  //素材ID
                string menuId = ds.Tables[0].Rows[0]["Id"].ToString();        //模型ID

                BaseService.WriteLogWeixin("typeId：" + typeId);
                BaseService.WriteLogWeixin("menuId：" + menuId);
                BaseService.WriteLogWeixin("Text：" + Text);

                switch (typeId)
                {
                    case MaterialType.TEXT:         //回复文字消息 
                        //ReplyText(materialId);
                        ReplyTextJermyn(Text);
                        break;
                    case MaterialType.IMAGE_TEXT:   //回复图文消息 
                        //ReplyNews(materialId);
                        ReplyNewsJermyn(menuId, 2, 2);
                        break;
                    default:
                        break;
                }
            }

            #endregion
        }

        #endregion

        #region 处理扫描带参数二维码事件

        //处理扫描带参数二维码事件
        public virtual void HandlerScan(string eventKey)
        {
            var application = new WApplicationInterfaceDAO(requestParams.LoggingSessionInfo);
            var appEntitys = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = requestParams.WeixinId }, null);

            if (appEntitys != null && appEntitys.Length > 0)
            {
                var entity = appEntitys.FirstOrDefault();

                BaseService.WriteLogWeixin("AppID:  " + entity.AppID);
                BaseService.WriteLogWeixin("AppSecret:  " + entity.AppSecret);
                BaseService.WriteLogWeixin("qrcodeId:  " + eventKey);

                //保存用户信息
                commonService.SaveUserInfo(requestParams.OpenId, requestParams.WeixinId, "1", entity.AppID, entity.AppSecret, eventKey, requestParams.LoggingSessionInfo);

                #region　微信扫描二维码 回复消息 update by wzq 20140731
                var eventsBll = new LEventsBLL(requestParams.LoggingSessionInfo);

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "二维码信息：" + eventKey
                });

                string nodeMsg = string.Empty;
                foreach ( System.Xml.XmlNode item in requestParams.XmlNode.ChildNodes)
	            {
                    nodeMsg += " |  " + item.Name +"-"+ item.Value;
	            }  

                //加log记录信息看下 2014-11-24 15:57:30
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "二维码全部信息："
                    + "~" + requestParams.OpenId
                    + "~" + requestParams.WeixinId
                    + "~" + requestParams.MsgType
                    + "~" + requestParams.XmlNode.ToString()
                    + nodeMsg
                });

                BaseService.WriteLogWeixin("开始推送消息Wzq");

                eventsBll.SendQrCodeWxMessage(requestParams.LoggingSessionInfo, requestParams.LoggingSessionInfo.CurrentLoggingManager.Customer_Id, requestParams.WeixinId, eventKey,
                    requestParams.OpenId, this.httpContext);

                BaseService.WriteLogWeixin("推送消息成功Wzq");
                #endregion

            }
        }

        #endregion

        #region 根据ID回复文字消息

        /// <summary>
        /// 根据ID回复文字消息
        /// </summary>
        /// <param name="materialId">文本消息ID</param>
        public void ReplyText(string materialId)
        {
            var dsMaterialWriting = new WMaterialWritingDAO(requestParams.LoggingSessionInfo).GetWMaterialWritingByID(materialId);

            if (dsMaterialWriting != null && dsMaterialWriting.Tables.Count > 0 && dsMaterialWriting.Tables[0].Rows.Count > 0)
            {
                var content = dsMaterialWriting.Tables[0].Rows[0]["Content"].ToString();

                commonService.ResponseTextMessage(requestParams.WeixinId, requestParams.OpenId, content, httpContext);
            }
        }

        /// <summary>
        /// 发送文字 Jermyn20140512
        /// </summary>
        /// <param name="Text"></param>
        public void ReplyTextJermyn(string Text)
        {
            //var dsMaterialWriting = new WMaterialWritingDAO(requestParams.LoggingSessionInfo).GetWMaterialWritingByID(materialId);

            //if (dsMaterialWriting != null && dsMaterialWriting.Tables.Count > 0 && dsMaterialWriting.Tables[0].Rows.Count > 0)
            //{
            //    var content = dsMaterialWriting.Tables[0].Rows[0]["Content"].ToString();

            commonService.ResponseTextMessage(requestParams.WeixinId, requestParams.OpenId, Text, httpContext);
            //}
        }

        #endregion

        #region 根据ID回复图文消息

        /// <summary>
        /// 根据ID回复图文消息
        /// </summary>
        /// <param name="materialId">图文消息ID</param>
        public void ReplyNews(string materialId)
        {
            var dsMaterialText = new WMaterialTextDAO(requestParams.LoggingSessionInfo).GetMaterialTextByID(materialId);

            if (dsMaterialText != null && dsMaterialText.Tables.Count > 0 && dsMaterialText.Tables[0].Rows.Count > 0)
            {
                var newsList = new List<WMaterialTextEntity>();

                foreach (DataRow dr in dsMaterialText.Tables[0].Rows)
                {
                    var url = dr["OriginalUrl"].ToString();

                    #region 分享业务 链接后面加上openId和userId

                    if (url.IndexOf("ParAll=") != -1)
                    {
                        var vipId = string.Empty;

                        VipBLL vipService = new VipBLL(requestParams.LoggingSessionInfo);
                        var vipEntity = vipService.QueryByEntity(new VipEntity { WeiXinUserId = requestParams.OpenId, WeiXin = requestParams.WeixinId }, null);
                        if (vipEntity != null && vipEntity.Length > 0)
                        {
                            vipId = vipEntity.FirstOrDefault().VIPID;
                        }

                        url += "&openId=" + requestParams.OpenId + "&userId=" + vipId;
                    }

                    #endregion

                    newsList.Add(new WMaterialTextEntity()
                    {
                        Title = dr["Title"].ToString(),
                        Text = dr["Author"].ToString(),
                        CoverImageUrl = dr["CoverImageUrl"].ToString(),
                        OriginalUrl = url
                    });
                }

                commonService.ResponseNewsMessage(requestParams.WeixinId, requestParams.OpenId, newsList, httpContext);
            }
        }

        /// <summary>
        /// 根据Id回复图文消息 Jermyn20140512
        /// </summary>
        /// <param name="objectId">对象标识</param>
        /// <param name="KeywordType">1=关键字回复 2=关注回复 3=自动回复</param>
        /// <param name="ObjectDataFrom">1=关键字 2=菜单</param>
        public void ReplyNewsJermyn(string objectId, int KeywordType, int ObjectDataFrom)
        {
            var dsMaterialText = new WMaterialTextDAO(requestParams.LoggingSessionInfo).GetMaterialTextByIDJermyn(objectId, ObjectDataFrom);

            if (dsMaterialText != null && dsMaterialText.Tables.Count > 0 && dsMaterialText.Tables[0].Rows.Count > 0)
            {
                var newsList = new List<WMaterialTextEntity>();

                foreach (DataRow dr in dsMaterialText.Tables[0].Rows)
                {
                    var url = dr["OriginalUrl"].ToString();

                    #region 分享业务 链接后面加上openId和userId

                    if (url.IndexOf("ParAll=") != -1)
                    {
                        var vipId = string.Empty;

                        VipBLL vipService = new VipBLL(requestParams.LoggingSessionInfo);
                        var vipEntity = vipService.QueryByEntity(new VipEntity { WeiXinUserId = requestParams.OpenId, WeiXin = requestParams.WeixinId }, null);
                        if (vipEntity != null && vipEntity.Length > 0)
                        {
                            vipId = vipEntity.FirstOrDefault().VIPID;
                        }

                        url += "&openId=" + requestParams.OpenId + "&userId=" + vipId;
                    }

                    #endregion

                    newsList.Add(new WMaterialTextEntity()
                    {
                        Title = dr["Title"].ToString(),
                        Text = dr["Author"].ToString(),
                        CoverImageUrl = dr["CoverImageUrl"].ToString(),
                        OriginalUrl = url
                    });
                }

                commonService.ResponseNewsMessage(requestParams.WeixinId, requestParams.OpenId, newsList, httpContext);
            }
        }
        #endregion
    }
}
