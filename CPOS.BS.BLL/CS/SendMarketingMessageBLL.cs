using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.BS.BLL
{
    public class SendMarketingMessageBLL
    {
        private LoggingSessionInfo loggingSessionInfo;
        public SendMarketingMessageBLL(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        /// <summary>
        /// 外发主程序
        /// </summary>
        public void Send()
        {
            TimingPushMessageRuleBLL ruleBll = new TimingPushMessageRuleBLL(loggingSessionInfo);
            TimingPushMessageRuleEntity[] ruleEntities = ruleBll.GetAll();
            if (ruleEntities.Length > 0)
            {
                foreach (var timingPushMessageRuleEntity in ruleEntities)
                {
                    if (timingPushMessageRuleEntity != null)
                    {
                        bool isRun = false;
                        if (timingPushMessageRuleEntity.ActiveTime.HasValue)
                        {
                            TimeSpan ts = DateTime.Now - timingPushMessageRuleEntity.ActiveTime.Value;
                            if (ts.Hours == 0 && ts.Minutes > -10 && ts.Minutes < 10)
                            {
                                isRun = true;
                            }
                        }
                        else
                        {
                            isRun = true;
                        }
                        if (isRun)
                        {
                            switch (timingPushMessageRuleEntity.CSPipelineID)
                            {
                                //微信
                                case 1:
                                    SendWeixinMessage(timingPushMessageRuleEntity);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void SendWeixinMessage(TimingPushMessageRuleEntity timingPushMessageRuleEntity)
        {

            //得到可以发送的人员列表
            WUserMessageBLL wUserMessageBll = new WUserMessageBLL(loggingSessionInfo);

            WUserMessageEntity[] wUserMessageEntities = wUserMessageBll.GetActiveUserMessageList();
            if (wUserMessageEntities.Length == 0)
            {
                return;
            }
            WApplicationInterfaceEntity applicationInterfaceEntity = null;
            foreach (var vipEntity in wUserMessageEntities)
            {
                //TODO:默认为一个客户一个微信，为了性能增加如下代码。如果多个用多个微信，则要将null判断取消
                if (applicationInterfaceEntity == null)
                {
                    applicationInterfaceEntity =
                        new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity
                            {
                                WeiXinID = vipEntity.WeiXinId
                            }, null)[0];
                }

                string appID = applicationInterfaceEntity.AppID;
                string appSecret = applicationInterfaceEntity.AppSecret;

                CommonBLL commonService = new CommonBLL();
                //获取对应模型列表
                TimingPushMessageRuleModelMappingBLL ruleModelMappingBll = new TimingPushMessageRuleModelMappingBLL(loggingSessionInfo);
                TimingPushMessageRuleModelMappingEntity[] ruleModelMappingEntities =
                    ruleModelMappingBll.QueryByEntity(new TimingPushMessageRuleModelMappingEntity
                        {
                            TimingPushMessageRuleID = timingPushMessageRuleEntity.TimingPushMessageRuleID

                        }, null);
                foreach (var timingPushMessageRuleModelMappingEntity in ruleModelMappingEntities)
                {
                    WModelEntity modelEntity =
                        new WModelBLL(loggingSessionInfo).GetByID(timingPushMessageRuleModelMappingEntity.ModelID);
                    //得到当前用户当前模型已经发送的最后文章ID
                    TimingPushMessageVipLastRecordBLL lastRecordBll =
                        new TimingPushMessageVipLastRecordBLL(loggingSessionInfo);
                    TimingPushMessageVipLastRecordEntity[] lastRecordEntities = lastRecordBll.QueryByEntity(new TimingPushMessageVipLastRecordEntity
                        {
                            VipID = vipEntity.VipId,
                            LastModelID = modelEntity.ModelId
                        }, null);
                    TimingPushMessageVipLastRecordEntity lastRecordEntity = new TimingPushMessageVipLastRecordEntity();
                    if (lastRecordEntities.Length > 0)
                    {
                        lastRecordEntity = lastRecordEntities[0];
                    }


                    switch (modelEntity.MaterialTypeId)
                    {
                        case 1:
                            var dsMaterialWriting = new WMaterialWritingDAO(loggingSessionInfo).GetWMaterialWritingByID(modelEntity.MaterialId);

                            if (dsMaterialWriting != null && dsMaterialWriting.Tables.Count > 0 && dsMaterialWriting.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] content = GetCurrentArticle(dsMaterialWriting, lastRecordEntity, 1);
                                if (content == null)
                                {
                                    return;
                                }
                                //推送消息
                                if (content.Length > 0)
                                {
                                    SendMessageEntity sendInfo = new SendMessageEntity();
                                    sendInfo.msgtype = "text";
                                    sendInfo.touser = vipEntity.OpenId;
                                    sendInfo.content = content[0]["Content"].ToString();
                                    ResultEntity msgResultObj = commonService.SendMessage(sendInfo, appID, appSecret,
                                                                                          loggingSessionInfo);
                                    //保存发送记录
                                    TimingPushMessageBLL timingPushMessageBll = new TimingPushMessageBLL(loggingSessionInfo);
                                    TimingPushMessageEntity timingPushMessageEntity = new TimingPushMessageEntity();
                                    timingPushMessageEntity.ClientID = loggingSessionInfo.ClientID;
                                    timingPushMessageEntity.IsDelete = 0;
                                    timingPushMessageEntity.CreateBy = "System";
                                    timingPushMessageEntity.CreateTime = DateTime.Now;
                                    timingPushMessageEntity.TimingPushMessageID = Guid.NewGuid();
                                    timingPushMessageEntity.MemberID = vipEntity.VipId;
                                    timingPushMessageEntity.ObjectID = content[0]["WritingId"].ToString();
                                    timingPushMessageEntity.Status = 1;
                                    timingPushMessageBll.Create(timingPushMessageEntity);
                                    //保存最后一条记录
                                    if (lastRecordEntity != null && lastRecordEntity.LastContentID != null)
                                    {
                                        lastRecordBll.Update(new TimingPushMessageVipLastRecordEntity
                                        {
                                            TimingPushMessageVipLastRecordID = lastRecordEntity.TimingPushMessageVipLastRecordID,
                                            ClientID = loggingSessionInfo.ClientID,
                                            CreateBy = "System",
                                            CreateTime = DateTime.Now,
                                            CSPipelineID = timingPushMessageRuleEntity.CSPipelineID,
                                            TimingPushMessageRuleID = timingPushMessageRuleEntity.TimingPushMessageRuleID,
                                            VipID = vipEntity.VipId,
                                            IsDelete = 0,
                                            LastContentID = content[0]["WritingId"].ToString(),
                                            LastContentIndex = lastRecordEntity.LastContentIndex + 1,
                                            LastModelID = modelEntity.ModelId
                                        });

                                    }
                                    else
                                    {
                                        lastRecordBll.Create(new TimingPushMessageVipLastRecordEntity
                                        {
                                            TimingPushMessageVipLastRecordID = Guid.NewGuid(),
                                            ClientID = loggingSessionInfo.ClientID,
                                            CreateBy = "System",
                                            CreateTime = DateTime.Now,
                                            TimingPushMessageRuleID = timingPushMessageRuleEntity.TimingPushMessageRuleID,
                                            CSPipelineID = timingPushMessageRuleEntity.CSPipelineID,
                                            VipID = vipEntity.VipId,
                                            IsDelete = 0,
                                            LastContentID = content[0]["WritingId"].ToString(),
                                            LastContentIndex = 1,
                                            LastModelID = modelEntity.ModelId
                                        });
                                    }
                                }

                            }
                            break;
                        case 2:
                            break;
                        case 3:
                            var dsMaterialText = new WMaterialTextDAO(loggingSessionInfo).GetMaterialTextByID(modelEntity.MaterialId);
                            if (dsMaterialText != null && dsMaterialText.Tables.Count > 0 && dsMaterialText.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] content = GetCurrentArticle(dsMaterialText, lastRecordEntity, 3);
                                if (content == null)
                                {
                                    return;
                                }
                                var newsList = new List<NewsEntity>();
                                foreach (DataRow dr in content)
                                {
                                    var url = dr["OriginalUrl"].ToString();
                                    newsList.Add(new NewsEntity()
                                    {
                                        title = dr["Title"].ToString(),
                                        description = "",
                                        picurl = dr["CoverImageUrl"].ToString(),
                                        url = url
                                    });
                                }

                                //推送消息
                                SendMessageEntity sendInfo = new SendMessageEntity();
                                sendInfo.msgtype = "news";
                                sendInfo.touser = vipEntity.OpenId;
                                sendInfo.articles = newsList;

                                ResultEntity msgResultObj = commonService.SendMessage(sendInfo, appID, appSecret, loggingSessionInfo);

                                //保存发送记录
                                TimingPushMessageBLL timingPushMessageBll = new TimingPushMessageBLL(loggingSessionInfo);
                                TimingPushMessageEntity timingPushMessageEntity = new TimingPushMessageEntity();
                                timingPushMessageEntity.ClientID = loggingSessionInfo.ClientID;
                                timingPushMessageEntity.IsDelete = 0;
                                timingPushMessageEntity.CreateBy = "System";
                                timingPushMessageEntity.CreateTime = DateTime.Now;
                                timingPushMessageEntity.TimingPushMessageID = Guid.NewGuid();
                                timingPushMessageEntity.MemberID = vipEntity.VipId;
                                timingPushMessageEntity.ObjectID = content[0]["TextId"].ToString();
                                timingPushMessageEntity.Status = 1;
                                timingPushMessageBll.Create(timingPushMessageEntity);

                                //保存最后一条记录 
                                if (lastRecordEntity != null && lastRecordEntity.LastContentID != null)
                                {
                                    lastRecordBll.Update(new TimingPushMessageVipLastRecordEntity
                                    {
                                        TimingPushMessageVipLastRecordID = lastRecordEntity.TimingPushMessageVipLastRecordID,
                                        ClientID = loggingSessionInfo.ClientID,
                                        CreateBy = "System",
                                        CreateTime = DateTime.Now,
                                        TimingPushMessageRuleID = timingPushMessageRuleEntity.TimingPushMessageRuleID,
                                        CSPipelineID = timingPushMessageRuleEntity.CSPipelineID,
                                        IsDelete = 0,
                                        VipID = vipEntity.VipId,
                                        LastContentID = content[0]["TextId"].ToString(),
                                        LastContentIndex = lastRecordEntity.LastContentIndex + 1,
                                        LastModelID = modelEntity.ModelId
                                    });
                                }
                                else
                                {
                                    lastRecordBll.Create(new TimingPushMessageVipLastRecordEntity
                                    {
                                        TimingPushMessageVipLastRecordID = Guid.NewGuid(),
                                        ClientID = loggingSessionInfo.ClientID,
                                        CreateBy = "System",
                                        CreateTime = DateTime.Now,
                                        TimingPushMessageRuleID = timingPushMessageRuleEntity.TimingPushMessageRuleID,
                                        CSPipelineID = timingPushMessageRuleEntity.CSPipelineID,
                                        VipID = vipEntity.VipId,
                                        IsDelete = 0,
                                        LastContentID = content[0]["TextId"].ToString(),
                                        LastContentIndex = 1,
                                        LastModelID = modelEntity.ModelId
                                    });
                                }

                            }
                          
                            break;
                    }

                }
            }
        }
        /// <summary>
        /// 根据模板类型，最后发送记录，获得当前发送记录
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="lastRecordEntity"></param>
        /// <param name="materialTypeID"></param>
        /// <returns></returns>
        private DataRow[] GetCurrentArticle(DataSet dataSet, TimingPushMessageVipLastRecordEntity lastRecordEntity, int materialTypeID)
        {
            DataRow[] allRows = null;
            switch (materialTypeID)
            {
                case 1:
                    if (lastRecordEntity != null && lastRecordEntity.LastContentID != null)
                    {
                        allRows = dataSet.Tables[0].Select("WritingId<>'" + lastRecordEntity.LastContentID + "'",
                                                           "CreateTime");
                    }
                    else
                    {
                        allRows = dataSet.Tables[0].Select("", "CreateTime");
                    }
                    break;
                case 3:
                    if (lastRecordEntity != null && lastRecordEntity.LastContentID != null)
                    {
                        WMaterialTextEntity materialTextEntity =
                            new WMaterialTextBLL(loggingSessionInfo).QueryByEntity(new WMaterialTextEntity
                                {
                                    TextId = lastRecordEntity.LastContentID
                                }, null)[0];
                        allRows = dataSet.Tables[0].Select("DisplayIndex>'" + materialTextEntity.DisplayIndex + "'",
                                                           "DisplayIndex");
                    }
                    else
                    {
                        allRows = dataSet.Tables[0].Select("", "DisplayIndex");
                    }
                    break;
            }
            //返回一条记录，如果需要返回所有记录，则修改这块逻辑
            if (allRows != null)
            {
                var list = (from DataRow a in allRows
                            select a).Take(1).ToArray();
                return list;

            }
            return null;
        }
    }
}
