using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JIT.CPOS.BS.BLL.CS;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 客服处理业务层
    /// </summary>
    public class CSInvokeMessageBLL
    {

        private LoggingSessionInfo loggingSessionInfo;
        public CSInvokeMessageBLL(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="csPipelineId">****消息通道ID1:微信2:短信3:IOS4:Android</param>
        /// <param name="userId">发送者ID</param>
        /// <param name="isCS">是否是客服1：是0：否</param>
        /// <param name="messageId">要回复的消息ID，如果为首次请求，请传NULL</param>
        /// <param name="messageContent">消息内容</param>
        /// <param name="serviceTypeId">服务类型，用于特殊类型的服务，如订单咨询</param>
        /// <param name="objectId">服务对象ID，请求对的对象ID，如订单ID</param>
        /// <param name="messageTypeId">消息类型，默认为NULL，如果是特殊类型的消息，则传特殊类型定义ID，现在暂定=5的ID为 微信优先、短信其次、App再次发送</param>
        /// <param name="contentTypeId">消息内容类型默认NULL或 1文本 2图片 3语音 4 视频 </param>
        /// <param name="sign">短信签名</param>
        /// <param name="mobileNo"></param>
        /// 会员从微信发的信息和员工从app上发的信息都要往这里面写
        public void SendMessage(int csPipelineId, string userId, int isCS, string messageId, string messageContent, int? serviceTypeId, string objectId, string messageTypeId, int? contentTypeId, string sign = null, string mobileNo = null)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("调用SendMessage方法：csPipelineId:[{0}];userId:[{1}];isCS:[{2}];messageId:[{3}];messageContent:[{4}];serviceTypeId:[{5}];objectId:[{6}];messageTypeId:[{7}];contentTypeId:[{8}];sign:[{9}];mobileNo:[{10}]"
                , csPipelineId
                , userId
                , isCS
                , messageId
                , messageContent
                , serviceTypeId
                , objectId
                , messageTypeId
                , contentTypeId
                , sign
                , mobileNo
            ) });
            var person = new VipBLL(loggingSessionInfo).GetByID(userId);//取会员的信息，如果是员工发的，就取到是空
            var user = new cUserService(loggingSessionInfo).GetUserById(loggingSessionInfo, userId);//如果是会员这里获取到的就是空
            //保存回话信息
            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);
            Guid conversationID = Guid.NewGuid();
            Guid newMessageID = Guid.NewGuid();
            CSMessageEntity messageEntity;

            ISQLHelper sqlHelper = new DefaultSQLHelper(loggingSessionInfo.Conn);
            DataSet ds = sqlHelper.ExecuteDataset("select dbo.DateToTimestamp('" + DateTime.Now + "')");

            CSConversationEntity conversationEntity = new CSConversationEntity();
            conversationEntity.Content = messageContent + (!string.IsNullOrEmpty(sign) ? "[|" + sign : "") + (!string.IsNullOrEmpty(mobileNo) ? "," + mobileNo : "");
            conversationEntity.PersonID = userId;
            conversationEntity.Person = isCS == 0 ? person.VipName : user.User_Name;
            conversationEntity.HeadImageUrl = isCS == 0 ? person.HeadImgUrl : null;
            conversationEntity.TimeStamp = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
            conversationEntity.OpenId = isCS == 0 ? person.WeiXinUserId : null;
            conversationEntity.ContentTypeID = contentTypeId.HasValue ? contentTypeId.Value : 1;//TODO：以后根据
            conversationEntity.IsCS = isCS;
            conversationEntity.CreateTime = DateTime.Now;
            conversationEntity.IsDelete = 0;
            if (!string.IsNullOrEmpty(messageId) && messageTypeId == "5")
            {
                new PushMessageType5(loggingSessionInfo).PushMessage(conversationEntity);
                return;
            }
            //如果是用户客服请求，并且是通过微信发送来的
            //TODO:取出最后回话的MessageID，如果最后回话超过60分钟则认为是新的回话
            if (isCS == 0 && csPipelineId == 1)
            {
                var conversations = conversationBll.PagedQueryByEntity(new CSConversationEntity//从会话里查，而不是从message表里查
                    {
                        PersonID = userId,
                        IsCS = 0//查询非客服的信息
                    }, new[] { new OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }, 1, 1);//1，1代表只取第一条，按照创建时间倒叙排列
                if (conversations.Entities.Length > 0)
                {
                    TimeSpan ts = DateTime.Now - conversations.Entities[0].CreateTime.Value;
                    if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes < 60)
                    {
                        messageId = conversations.Entities[0].CSMessageID.ToString();//60分钟内的那句话的信息（超过60分钟就不取了）
                    }
                }
            }
            //下面这段主要对员工起作用的
            if (!string.IsNullOrEmpty(messageId))//取到相关1小时内的信息()
            {
                conversationEntity.CSMessageID = Guid.Parse(messageId);//用1小时内会话的messageid

                messageEntity = messageBll.GetByID(messageId);//获取关联的message主信息标识
            }
            else//没有取到相关1小时内的信息
            {
                messageId = newMessageID.ToString();//新建的guid
                messageEntity = new CSMessageEntity();
                messageEntity.CSMessageID = newMessageID;//新建的guid
                messageEntity.CSPipelineID = csPipelineId;
                messageEntity.Content = messageContent;
                messageEntity.CSObjectID = objectId;
                messageEntity.CSServiceTypeID = serviceTypeId;
                messageEntity.MemberID = userId;
                messageEntity.MemberName = person != null ? person.VipName : "";
                messageEntity.ClientID = loggingSessionInfo.ClientID;
                messageEntity.CreateTime = DateTime.Now;
                messageBll.Create(messageEntity);


            }
            //别的客服回复了该信息（ConnectionTime记录最新的客服的回复时间******）
            if (conversationEntity.IsCS.Value == 1 && !string.IsNullOrEmpty(messageEntity.CurrentCSID) && messageEntity.ConnectionTime != null && messageEntity.CurrentCSID.ToLower() != conversationEntity.PersonID.ToLower())
            {
                TimeSpan ts = DateTime.Now - messageEntity.ConnectionTime.Value;//取得是messageEntity的连接时间
                if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes < 60)
                {
                    throw new Exception("已经有人回复该消息了");
                }
                //移除队列
                CSQueueBLL queueBll = new CSQueueBLL(loggingSessionInfo);//把已经有人回复的消息从队伍里移除（虚拟删除）
                CSQueueEntity[] queueEntities = queueBll.QueryByEntity(new CSQueueEntity
                    {
                        CSMessageID = messageEntity.CSMessageID
                    }, null);
                foreach (var csQueueEntity in queueEntities)
                {
                    queueBll.Delete(csQueueEntity);
                }
            }

            conversationEntity.CSConversationID = conversationID;//新的guid*****
            conversationEntity.CreateTime = DateTime.Now;
            conversationBll.Create(conversationEntity);//创建了conversation

            //会员请求（客服最后回话时间）
            if (conversationEntity.IsCS.Value == 0)
            {
                conversationEntity.CSMessageID = Guid.Parse(messageId);
                conversationBll.Update(conversationEntity);
                //判断是否是第一次请求
                if (!string.IsNullOrEmpty(messageEntity.CurrentCSID))//已经有客服回复了
                {
                    //判断链接时间
                    if (messageEntity.ConnectionTime != null)
                    {
                        TimeSpan ts = DateTime.Now - messageEntity.ConnectionTime.Value;//判断链接时间
                        if (ts.Days == 0 && ts.Hours >= 1)
                        {
                            //TODO:如果客服回复时间超过1小时，则重新加入消息队列
                            AddToMessageQueue(messageEntity); //****重新加入队伍，并给所有客服都推送信息“您有新的客服请求”，因为上面已经有了CSConversationID了
                        }
                        else
                        {
                            var currentCSPipelineIDs = GetCurrentCSPipeline(messageEntity.CurrentCSID);//当前的客服信息
                            //消息推送给客服（您有新的客服请求）
                            foreach (var currentCSPipelineID in currentCSPipelineIDs)
                            {
                                PushNotificationMessage(messageEntity.CurrentCSID, currentCSPipelineID,//您有新的客服请求
                                                        conversationEntity);
                            }
                        }
                    }
                }
                else
                {
                    //第一次请求，加入消息队列
                    AddToMessageQueue(messageEntity);//并且给所有的客服都推送信息
                }
            }
            //客服回复
            else
            {

                if (!conversationEntity.CSMessageID.HasValue)
                {
                    conversationEntity.CSMessageID = newMessageID;
                }
                //更新最后客服信息
                messageEntity = messageBll.GetByID(conversationEntity.CSMessageID);//客服信息
                bool b = false;
                if (messageEntity.ConnectionTime.HasValue)
                {
                    TimeSpan ts = DateTime.Now - messageEntity.ConnectionTime.Value;
                    b = ts.Days == 0 && ts.Hours > 1;//今天并且时间大于1
                }

                if (messageEntity.CurrentCSID == null || b)//客服为空或者最近一次客服回复的时间大于1小时******
                {
                    string currentUserName = messageEntity.MemberName;
                    //第一次回复，推送给其他客服消息
                    IList<UserInfo> userInfos =
                        new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
                    string currentCSName = "";
                    cUserService userService = new cUserService(loggingSessionInfo);
                    currentCSName =
                        userService.GetUserById(loggingSessionInfo, conversationEntity.PersonID).User_Name;
                    foreach (var userInfo in userInfos)
                    {
                        if (userInfo.User_Id.ToLower() == conversationEntity.PersonID.ToLower())
                        {
                            continue;
                        }
                        var csPipelineIDs = GetCurrentCSPipeline(userInfo.User_Id);
                        foreach (var csPipelineID in csPipelineIDs)
                        {
                            PushNotificationMessage(userInfo.User_Id, csPipelineID,
                                                    new CSConversationEntity
                                                        {
                                                            Content = "该客户【" + currentUserName + "】已经由【" + currentCSName + "】提供客户服务"
                                                        });
                        }
                    }
                }
                messageEntity.CurrentCSID = conversationEntity.PersonID;//切换最新的客服
                messageEntity.LastUpdateTime = DateTime.Now;
                messageEntity.ConnectionTime = DateTime.Now;//每次都更新连接时间*******
                messageBll.Update(messageEntity);

                //消息推送给用户
                if (messageEntity.CSPipelineID != null)
                {
                    PushNotificationMessage(messageEntity.MemberID, messageEntity.CSPipelineID.Value,  //这里传的上次message信息的通道id，而不是传过来的员工回复的管道id，所以即使员工传过来的是3，这里取的也是会员的管道id：微信1
                                            conversationEntity);
                }

            }
        }

        /// <summary>
        /// TODO:获取指定客户的通道
        /// </summary>
        /// <param name="currentCsid"></param>
        /// <returns></returns>
        private int[] GetCurrentCSPipeline(string currentCsid)
        {
            List<int> rst = new List<int>();
            //查下用户是否在使用Android
            PushAndroidBasicBLL pushBll = new PushAndroidBasicBLL(loggingSessionInfo);
            var android = pushBll.GetByID(currentCsid);
            if (android != null)
                rst.Add(4);
            //查看用户是否在使用IOS
            PushUserBasicBLL pushUserBasicBll = new PushUserBasicBLL(loggingSessionInfo);
            var ios = pushUserBasicBll.GetByID(currentCsid);
            if (ios != null)
                rst.Add(3);
            return rst.ToArray();
        }


        /// <summary>
        /// 消息推送给用户
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="pipelineID"></param>
        /// <param name="conversationEntity"></param>
        private void PushNotificationMessage(string memberID, int pipelineID, CSConversationEntity conversationEntity)
        {
            try
            {
                Loggers.DEFAULT.Debug(new DebugLogInfo
                {
                    Message = "消息推送：" + memberID + "/" + pipelineID + "/" + conversationEntity.ToJSON()
                });
                switch (pipelineID)
                {
                    //微信
                    case 1:
                        IPushMessage pushWeixinMessage = new PushWeiXinMessage(loggingSessionInfo);
                        pushWeixinMessage.PushMessage(memberID, conversationEntity.Content);
                        break;
                    //sms
                    case 2:
                        IPushMessage pushSMSMessage = new PushSMSMessage(loggingSessionInfo);
                        pushSMSMessage.PushMessage(memberID, conversationEntity.Content);
                        break;
                    //ios
                    case 3:
                        IPushMessage pushIOSMessage = new PushIOSMessage(loggingSessionInfo);
                        string msg = "";
                        if (conversationEntity.CSConversationID == null)
                        {
                            msg = conversationEntity.Content;
                        }
                        else
                        {
                            msg = conversationEntity.CSMessageID.HasValue ? "您有新的客服消息" : "您有新的客服请求";
                        }
                        pushIOSMessage.PushMessage(memberID, msg);
                        break;
                    //android
                    case 4:
                        IPushMessage pushAndroidMessage = new PushAndroidMessage(loggingSessionInfo);
                        string msg1 = "";
                        if (conversationEntity.CSConversationID == null)//会话信息为空时
                        {
                            msg1 = conversationEntity.Content;
                        }
                        else
                        {
                            msg1 = conversationEntity.CSMessageID.HasValue ? "您有新的客服消息" : "您有新的客服请求";
                        }
                        pushAndroidMessage.PushMessage(memberID, msg1);
                        break;
                }
                //更新已经推送
                if (conversationEntity.CSConversationID.HasValue)
                {
                    CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
                    conversationEntity.IsPush = 1;
                    conversationBll.Update(conversationEntity);
                }

            }
            catch (Exception ex)
            {
                Loggers.DEFAULT.Debug(new DebugLogInfo
                {
                    Message = "消息推送错误：" + ex.Message + "|" + memberID + "/" + pipelineID + "/" + conversationEntity.ToJSON()
                });
            }
        }

        /// <summary>
        /// 加入消息队列
        /// </summary>
        /// <param name="messageEntity"></param>
        private void AddToMessageQueue(CSMessageEntity messageEntity)
        {
            IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
            foreach (var userInfo in userInfos)
            {
                var csPipelineIDs = GetCurrentCSPipeline(userInfo.User_Id);
                foreach (var csPipelineID in csPipelineIDs)
                {
                    PushNotificationMessage(userInfo.User_Id, csPipelineID, new CSConversationEntity { Content = "您有新的客服请求" });
                }
            }

            CSQueueBLL queueBll = new CSQueueBLL(loggingSessionInfo);
            CSQueueEntity queueEntity = new CSQueueEntity
                {
                    ClientID = loggingSessionInfo.ClientID,
                    CreateBy = messageEntity.CreateBy,
                    CreateTime = messageEntity.CreateTime,
                    CSPipelineID = messageEntity.CSPipelineID,
                    CSServiceTypeID = messageEntity.CSServiceTypeID,
                    CSMessageID = messageEntity.CSMessageID,
                    CSQueueID = Guid.NewGuid()
                };
            queueBll.Create(queueEntity);
        }

        /// <summary>
        /// 接收消息列表
        /// </summary>
        /// <param name="isCS">是否是客服1：是0：否</param>
        /// <param name="personID">当前用户ID</param>
        /// <param name="messageId">当前消息ID，如果要获取所有消息，则消息ID为NULL</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>消息列表</returns>
        public IList<CSConversationEntity> ReceiveMessage(string personID, int isCS, string messageId, int pageSize, int pageIndex,string customerId, out int recordCount)
        {
            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);

            //返回用户消息列表
            string conditionExpression;
            if (!string.IsNullOrEmpty(messageId))
            {
                conditionExpression = string.Format("CSMessageID='{0}'", messageId);//主要用于员工在app上使用
            }
            else
            {
                IWhereCondition[] conditions;
                if (isCS == 0)//会员的情况（非会员）
                {
                    conditions = new IWhereCondition[]
                        {
                           // new DirectCondition("PersonID='" + personID + "'")//查找会员的回话
                           new DirectCondition("MemberID='" + personID + "'")//查找会员的回话(查找CSMessage)
                        };
                }
                else
                {
                    conditions = new IWhereCondition[]
                        {
                            //这个条件是查找当前客服可以查看的message的，客服是当前员工的，或者客服人员是空的，或者客服人员不是当前人员（并且最后链接时间大于60分钟的会员）
                            new DirectCondition("((CurrentCSID='" + personID + "') or (CurrentCSID IS NULL ) OR (CurrentCSID<>'" + personID + "' and datediff(minute,ConnectionTime,getdate())>60)) and ClientID = '"+customerId+"'")
                        };
                }
                var messageEntities = messageBll.Query(conditions, new[]{
                            new OrderBy
                                {
                                    FieldName = "CreateTime",
                                    Direction = OrderByDirections.Desc//按照创建时间
                                }
                        });

                if (messageEntities.Length > 0)
                {
                    string messageIds = messageEntities.Aggregate(string.Empty,
                                                                  (current, messageEntity) =>
                                                                  string.Format("{0}'{1}'" + ",", current,
                                                                                messageEntity.CSMessageID));//代表总的初始信息，CSMessageID代表遍历的每个对象
                    conditionExpression = string.Format("CSMessageID in ({0})", messageIds.TrimEnd(','));
                }
                else
                {
                    recordCount = 0;
                    return null;
                }
            }
            Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = string.Format("conditionExpression={0}", conditionExpression) });
            //得到回话列表
            var conversations = conversationBll.PagedQuery(new IWhereCondition[]
                        {
                            new DirectCondition(conditionExpression)
                        }, new[]
                            {
                                new OrderBy
                                    {
                                        FieldName = "CreateTime",
                                        Direction = OrderByDirections.Desc
                                    }
                            },
                                                           pageSize,
                                                           pageIndex);
            recordCount = conversations.RowCount;
            return conversations.Entities;

        }

        /// <summary>
        /// 从消息队列里获取消息【废弃、改为主动推送消息】
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        private IList<CSConversationEntity> GetFromQueue(string personID)
        {
            IList<CSConversationEntity> conversationEntities = new List<CSConversationEntity>();
            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);

            //从队列里取得微信发送的客服信息
            CSQueueBLL queueBll = new CSQueueBLL(loggingSessionInfo);
            CSQueueEntity[] queueEntities = queueBll.QueryByEntity(new CSQueueEntity
                {
                    CSPipelineID = 1
                }, new[]
                        {
                            new OrderBy { FieldName = "CSPipelineID", Direction = OrderByDirections.Asc }
                        });
            if (queueEntities.Length > 0)
            {
                CSQueueEntity queueEntity = queueEntities[0];

                CSMessageEntity messageEntity = messageBll.GetByID(queueEntity.CSMessageID);
                conversationEntities = conversationBll.QueryByEntity(new CSConversationEntity
                                                                {
                                                                    CSMessageID = queueEntity.CSMessageID
                                                                }, new[]
                                                                        {
                                                                            new OrderBy
                                                                                {
                                                                                    FieldName = "CreateTime", 
                                                                                    Direction = OrderByDirections.Desc
                                                                                }
                                                                        });

                //更新消息
                messageEntity.ConnectionTime = DateTime.Now;
                messageEntity.CurrentCSID = personID;
                messageBll.Update(messageEntity);
                //移除队列
                queueEntity.IsDelete = 1;
                queueBll.Delete(queueEntity);
                //更新护花ID的队列ID
                foreach (var conversationEntity in conversationEntities)
                {
                    conversationEntity.CSQueueID = queueEntity.CSQueueID;
                    conversationBll.Update(conversationEntity);
                }
            }
            return conversationEntities;
        }

    }

}

