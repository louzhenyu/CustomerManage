﻿using System;
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
using System.Globalization;

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
        /// <param name="VipIDInit">员工(isCs=1)主动跟会员发起聊天时，会员的id</param>
        /// 会员从微信发的信息和员工从app上发的信息都要往这里面写
        public void SendMessage(int csPipelineId, string userId, int isCS, string messageId, string messageContent, int? serviceTypeId
            , string objectId, string messageTypeId, int? contentTypeId, string sign = null, string mobileNo = null, string VipIDInit = null)
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
            )
            });
            var person = new VipBLL(loggingSessionInfo).GetByID(userId);//取会员的信息，如果是员工发的，就取到是空
            var user = new cUserService(loggingSessionInfo).GetUserById(loggingSessionInfo, userId);//如果是会员这里获取到的就是空

            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);
            //员工主动发起聊天，在isCS=1的情况下，新加一个字段VipID，用于查找           
            //根据会员id查找csMessage信息，分为三种情况
            //1.有csMessage&connationtime没超过1小时&currentcsid不等于传过来的userid，报“有人正在跟该会员会话，不能发起会话”
            //2、（1）有csMessage，&connationtime没超过1小时并且&currentcsid等于传过来的userid，（2）*** 有csMessage， 超过1小时直接就用这个csMessage的id作为csmessageid继续下面的会话。
            //3 、没有csMessage，都需要创建新的csmessage（需要根据vipid去查会员信息，作为csMessage的memberid等信息），保存到数据库，把这个csmesage的csmessageid作为标识继续下面的操作。

            if (isCS == 1 && !string.IsNullOrEmpty(VipIDInit))//员工主动发起聊天
            {
                messageId = "";//设为空
                var conversationsInit = conversationBll.PagedQueryByEntity(new CSConversationEntity//从会话里查，而不是从message表里查，这主要从客户作为主动权查看来作为会话的主导者
                {
                    PersonID = VipIDInit,
                    IsCS = 0//查询非客服的信息
                }, new[] { new OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }, 1, 1);//1，1代表只取第一条，按照创建时间倒叙排列
                if (conversationsInit!=null && conversationsInit.Entities!=null &&  conversationsInit.Entities.Length > 0)
                {
                    TimeSpan ts = DateTime.Now - conversationsInit.Entities[0].CreateTime.Value;
                    //if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes < 60)//根据时间间隔获取时分秒
                    //{
                    messageId = conversationsInit.Entities[0].CSMessageID.ToString();//60分钟内的那句话的信息（超过60分钟就不取了）
                    //   }
                }

                CSMessageEntity messageEntityInit = null;
                if (!string.IsNullOrEmpty(messageId))
                {
                   messageEntityInit= messageBll.GetByID(messageId);//获取关联的message主信息标识
                }
                if (messageEntityInit == null)
                {
                    var personInit = new VipBLL(loggingSessionInfo).GetByID(VipIDInit);//取会员的信息，如果是员工发的，就取到是空

                    messageId = Guid.NewGuid().ToString();//新建的guid,以便传递给下面
                    messageEntityInit = new CSMessageEntity();
                    messageEntityInit.CSMessageID = new Guid(messageId);//新建的guid
                    messageEntityInit.CSPipelineID = 1;// csPipelineId;//模拟成微信里会员发的
                    messageEntityInit.Content = messageContent;
                    messageEntityInit.CSObjectID = objectId;
                    messageEntityInit.CSServiceTypeID = serviceTypeId;
                    messageEntityInit.MemberID = VipIDInit;  //员工主动向会员发的信息，如果之前没有会话，就要模拟一个会话请求
                    messageEntityInit.MemberName = personInit != null ? personInit.VipName : "";
                    messageEntityInit.ClientID = loggingSessionInfo.ClientID;
                    messageEntityInit.CreateTime = DateTime.Now;
                    messageBll.Create(messageEntityInit);
                }
                else {
                    if (isCS == 1 && !string.IsNullOrEmpty(messageEntityInit.CurrentCSID) && messageEntityInit.ConnectionTime != null && messageEntityInit.CurrentCSID.ToLower() != userId.ToLower())//换成userid
                    {
                        TimeSpan ts = DateTime.Now - messageEntityInit.ConnectionTime.Value;//取的是messageEntity的连接时间
                        if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes < 60)
                        {
                            throw new Exception("已经有人回复该消息了");
                        }
                        //移除队列
                        CSQueueBLL queueBll = new CSQueueBLL(loggingSessionInfo);//把已经有人回复的消息从队伍里移除（虚拟删除）
                        CSQueueEntity[] queueEntities = queueBll.QueryByEntity(new CSQueueEntity
                        {
                            CSMessageID = messageEntityInit.CSMessageID
                        }, null);
                        foreach (var csQueueEntity in queueEntities)
                        {
                            queueBll.Delete(csQueueEntity);
                        }
                    }//对于没有被别人处理的，不与处理
                }
            }

            //保存回话信息     
            Guid conversationID = Guid.NewGuid();
            Guid newMessageID = Guid.NewGuid();
            CSMessageEntity messageEntity;

            ISQLHelper sqlHelper = new DefaultSQLHelper(loggingSessionInfo.Conn);
            DataSet ds = sqlHelper.ExecuteDataset("select dbo.DateToTimestamp('" + DateTime.Now + "')");//时间转换为时间戳格式

            CSConversationEntity conversationEntity = new CSConversationEntity();
            conversationEntity.Content = messageContent + (!string.IsNullOrEmpty(sign) ? "[|" + sign : "") + (!string.IsNullOrEmpty(mobileNo) ? "," + mobileNo : "");
            conversationEntity.PersonID = userId;
            conversationEntity.Person = isCS == 0 ? person.VipName : user.User_Name;
            conversationEntity.HeadImageUrl = isCS == 0 ? person.HeadImgUrl : null;
            conversationEntity.TimeStamp = Convert.ToInt64(ds.Tables[0].Rows[0][0]);//取当前的时间戳
            conversationEntity.OpenId = isCS == 0 ? person.WeiXinUserId : null;
            conversationEntity.ContentTypeID = contentTypeId.HasValue ? contentTypeId.Value : 1;//TODO：以后根据
            conversationEntity.IsCS = isCS;
            conversationEntity.CreateTime = DateTime.Now;
            conversationEntity.IsDelete = 0;
            if (!string.IsNullOrEmpty(messageId) && messageTypeId == "5")//主动推送客服信息？
            {
                new PushMessageType5(loggingSessionInfo).PushMessage(conversationEntity);
                return;
            }
            //如果是用户客服请求，并且是通过微信发送来的
            //TODO:取出最后回话的MessageID，如果最后回话超过60分钟则认为是新的会话
            if (isCS == 0 && csPipelineId == 1)
            {
                var conversations = conversationBll.PagedQueryByEntity(new CSConversationEntity//从会话里查，而不是从message表里查，这主要从客户作为主动权查看来作为会话的主导者
                {
                    PersonID = userId,
                    IsCS = 0//查询非客服的信息
                }, new[] { new OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }, 1, 1);//1，1代表只取第一条，按照创建时间倒叙排列
                if (conversations.Entities.Length > 0)
                {
                    TimeSpan ts = DateTime.Now - conversations.Entities[0].CreateTime.Value;
                    if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes < 60)//根据时间间隔获取时分秒
                    {
                        messageId = conversations.Entities[0].CSMessageID.ToString();//60分钟内的那句话的信息（超过60分钟就不取了）
                    }
                }
            }
            //下面这段主要对员工起作用的(对会员也可以，会员是上面的数据（以会员聊天为最后一次一句话为时间标准）)
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
                messageEntity.MemberID = userId;  //如果是员工发起的，就存员工的值（员工发起会话都会有messageid的，所以不会存员工的值，只能是会员的值）
                messageEntity.MemberName = person != null ? person.VipName : "";
                messageEntity.ClientID = loggingSessionInfo.ClientID;
                messageEntity.CreateTime = DateTime.Now;
                messageBll.Create(messageEntity);
                //没有给CurrentCSID赋值

            }
            //别的客服回复了该信息（ConnectionTime记录最新的客服的回复时间******）
            if (conversationEntity.IsCS.Value == 1 && !string.IsNullOrEmpty(messageEntity.CurrentCSID) && messageEntity.ConnectionTime != null && messageEntity.CurrentCSID.ToLower() != conversationEntity.PersonID.ToLower())
            {
                TimeSpan ts = DateTime.Now - messageEntity.ConnectionTime.Value;//取的是messageEntity的连接时间
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
                    b = ts.Days == 0 && ts.Hours > 1;//今天并且时间大于1（以避免超过48小时的客服限制）
                }
                //新建的员工请求或者已有的会话超过一小时（而且是今天）
                if (messageEntity.CurrentCSID == null || b)//客服为空或者最近一次客服回复的时间大于1小时******
                {
                    string currentUserName = messageEntity.MemberName;
                    //第一次回复，推送给其他客服消息
                    //IList<UserInfo> userInfos =
                    //    new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
                    IList<UserInfo> userInfos =
                        new cUserService(loggingSessionInfo).GetUserListByMenuCode("CustomerService");
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
                    //如果超过了两三个小时会不会推送不出去？
                    PushNotificationMessage(messageEntity.MemberID, messageEntity.CSPipelineID.Value,  //这里传的上次message信息的通道id，而不是传过来的员工回复的管道id，所以即使员工传过来的是3，这里取的也是会员的管道id：微信1
                                            conversationEntity);
                }
                //要不要发一个主动发消息的接口
                //  https://api.weixin.qq.com/cgi-bin/message/send?access_token=   在CommonBLL里的SendMessage方法，试试

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
         //   IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
            IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByMenuCode("CustomerService");
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
        public IList<CSConversationEntity> ReceiveMessage(string personID, int isCS, string messageId, int pageSize
            , int pageIndex, string customerId, out int recordCount)
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
        /// 接收消息列表
        /// </summary>
        /// <param name="isCS">是否是客服1：是0：否</param>
        /// <param name="personID">当前用户ID</param>
        /// <param name="messageId">当前消息ID，如果要获取所有消息，则消息ID为NULL</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>消息列表</returns>
        public IList<CSConversationEntity> ReceiveMessageNew(string personID, int isCS, string messageId, int? pageSize, int? pageIndex, string customerId
            , int ReceiveType, DateTime? CurrentGetTime, DateTime? NextTime, out int recordCount, out DateTime? CurrentGetTimeNew
                      , out DateTime? NextTimeNew)
        {
            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);

            CurrentGetTimeNew = null;
            NextTimeNew = null;

            //返回用户消息列表
            string conditionExpression;
            if (!string.IsNullOrEmpty(messageId))
            {
                conditionExpression = string.Format("CSMessageID='{0}'", messageId);//主要用于员工在app上使用
            }
            else
            {
                IWhereCondition[] conditions;
                if (isCS == 0)//会员的情况（非）
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

                //根据条件查询****
                var messageEntities = messageBll.Query(conditions, new[]{//根据条件查出MessageEntity，然后获取CSMessageID
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


            recordCount = 0;
            var conversations = new PagedQueryResult<CSConversationEntity>();
            //第一次页面打开的查询方法
            if (ReceiveType == 1)
            {//普通分页查询方法
                //得到回话列表
                conversations = conversationBll.PagedQuery(new IWhereCondition[]
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
                                                                (int)pageSize,
                                                                 (int)pageIndex);
                recordCount = conversations.RowCount;
                if (conversations != null && conversations.Entities != null && conversations.Entities.Length != 0)
                {
                    //取出NextTime，如果没有数据怎么去
                    NextTimeNew = conversations.Entities[conversations.Entities.Length - 1].CreateTime;
                    //取出 CurrentGetTime
                    CurrentGetTimeNew = conversations.Entities[0].CreateTime;//日期最大的一条数据的时间,因为在取时间时，带了毫秒，而传进去，就没有了
                    //   CurrentGetTimeNew =DateTime.Now;

                }
                else
                { //因为只有第一次打开页面时调用这个页面，所以只有此会员没有一条客服信息时，才会出现下面的情况
                    CurrentGetTimeNew = DateTime.Now.AddHours(-1);//往后找一个小时
                    NextTimeNew = DateTime.Now;//时间
                }
                return conversations.Entities;//要到前端去倒叙查出来，因为app上都是这样处理的，不然，没法协调了***
            }


            //轮询查找的方法
            if (ReceiveType == 2)
            {
                // CreateTime>'2015-11-16 11:22:58.4670000'//这样不行，只能保留三位毫秒，保留七位无法识别
                conditionExpression += string.Format(" and CreateTime>'{0}'", ((DateTime)CurrentGetTime).ToString("yyyy-MM-dd HH:mm:ss.fff"));//要是不拼接字符串，而是用参数化，可能就不会把毫秒给去掉了。

                //var moreThan = new MoreThanCondition()
                //{
                //    FieldName = "CreateTime",
                //    Value = CurrentGetTime,
                //    IncludeEquals = false,
                //    DateTimeAccuracy = DateTimeAccuracys.DateTime
                //};//新加一个条件，时间的
                //var time=CurrentGetTime.ToString()
                //var a= ((DateTime)CurrentGetTime).ToLongTimeString();
                //var b = ((DateTime)CurrentGetTime).ToLongDateString();
                //var c = ((DateTime)CurrentGetTime).ToString("yyyy-MM-dd HH:mm:ss.fffffff");//带上七位的毫秒


                var conversationList = conversationBll.Query(new IWhereCondition[]
                        {
                            new DirectCondition(conditionExpression)
                            //,moreThan，只精确到了秒，没有到毫秒
                            }, new[]
                            {
                                new OrderBy
                                    {
                                        FieldName = "CreateTime",
                                        Direction = OrderByDirections.Desc
                                    }
                       });
                if (conversationList != null && conversationList.Length != 0)//还是用最后一条记录的时间
                {
                    CurrentGetTimeNew = conversationList[0].CreateTime;//
                    //不要处理NextTime
                }
                else
                {
                    CurrentGetTimeNew = CurrentGetTime;//还用老时间
                }
                //       CurrentGetTimeNew =DateTime.Now;
                recordCount = conversationList.Length;
                return conversationList;
            }

            //点击查看更多，查看之前的信息
            if (ReceiveType == 3)
            {
                conditionExpression += string.Format(" and CreateTime<'{0}'", ((DateTime)NextTime).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //得到回话列表
                // var lessThan = new LessThanCondition() { FieldName = "CreateTime", Value = NextTime, IncludeEquals = false, DateTimeAccuracy = DateTimeAccuracys.DateTime };//新加一个条件，时间的

                conversations = conversationBll.PagedQuery(new IWhereCondition[]
                        {
                            new DirectCondition(conditionExpression)
                          //  ,lessThan
                        }, new[]
                            {
                                new OrderBy
                                    {
                                        FieldName = "CreateTime",
                                        Direction = OrderByDirections.Desc
                                    }
                            },
                                                                (int)pageSize,
                                                                 1);
                recordCount = conversations.RowCount;
                if (conversations != null && conversations.Entities != null && conversations.Entities.Length != 0)
                {
                    //取出NextTime，如果没有数据怎么去
                    NextTimeNew = conversations.Entities[conversations.Entities.Length - 1].CreateTime;//取时间最早的一条
                    //不处理CurrentGetTime
                    //  CurrentGetTime = conversations.Entities[0].CreateTime;//日期最大的一条数据的时间
                }
                else
                { //因为只有第一次打开页面时调用这个页面，所以只有此会员没有一条客服信息时，才会出现下面的情况
                    // CurrentGetTime = DateTime.Now.AddMonths(-1);
                    //  NextTime =ne;//NextTime还用之前的
                    NextTimeNew = NextTime;
                }
                return conversations.Entities;//要到前端去倒叙查出来，因为app上都是这样处理的，不然，没法协调了***
            }


            return null;


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


        public IList<CSConversationEntity> ReceiveMessageNew(string p1, int p2, string p3, int? nullable1, int? nullable2, string p4, int p5, DateTime? nullable3, DateTime? nullable4, out int recordCount)
        {
            throw new NotImplementedException();
        }
    }

}

