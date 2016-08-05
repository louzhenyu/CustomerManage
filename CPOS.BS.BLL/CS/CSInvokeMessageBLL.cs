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
using System.Globalization;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.BLL.WX.Enum;
using JIT.CPOS.Common;
using System.Configuration;
using System.Net;
using System.IO;
using System.Drawing;
using JIT.CPOS.BS.Entity.WX;

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
                if (conversationsInit != null && conversationsInit.Entities != null && conversationsInit.Entities.Length > 0)
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
                    messageEntityInit = messageBll.GetByID(messageId);//获取关联的message主信息标识
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
                else
                {
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
        /// 发送消息(修改发送图文)
        /// </summary>
        /// <param name="csPipelineId">****消息通道ID1:微信2:短信3:IOS4:Android</param>
        /// <param name="userId">发送者ID</param>
        /// <param name="isCS">是否是客服1：是0：否</param>
        /// <param name="messageId">要回复的消息ID，如果为首次请求，请传NULL</param>
        /// <param name="messageContent">消息内容</param>
        /// <param name="serviceTypeId">服务类型，用于特殊类型的服务，如订单咨询</param>
        /// <param name="objectId">服务对象ID，请求对的对象ID，如订单ID</param>
        /// <param name="messageTypeId">消息类型，默认为NULL，如果是特殊类型的消息，则传特殊类型定义ID，现在暂定=5的ID为 微信优先、短信其次、App再次发送</param>
        /// <param name="Articles"></param>
        /// <param name="Title">标题  这里目前可能海报名称或者券的名称</param>
        /// <param name="Description">描述</param>
        /// <param name="PicUrl">图片路径</param>
        /// <param name="Url">跳转路径</param>
        /// <param name="contentTypeId">消息内容类型默认NULL或 1文本 2图片 3语音 4 视频 </param>
        /// <param name="sign">短信签名</param>
        /// <param name="mobileNo"></param>
        /// <param name="VipIDInit">员工(isCs=1)主动跟会员发起聊天时，会员的id</param>
        public void SendMessage(int csPipelineId, string userId, int isCS, string messageId, string messageContent, int? serviceTypeId
            , string objectId, string messageTypeId, string Articles, string Title, string Description, string PicUrl, string Url, string objectType, int? contentTypeId, string sign = null, string mobileNo = null, string VipIDInit = null)
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
            //开始推送之前  更新次员工的已读信息 商品的除外
            if (!string.IsNullOrEmpty(objectType) && objectType.ToLower() != "product")
            {
                var setoffToolUserViewBLL = new SetoffToolUserViewBLL(loggingSessionInfo);
                var setOffToolInfo = new SetoffToolsBLL(loggingSessionInfo).QueryByEntity(new SetoffToolsEntity() { SetoffToolID = new Guid(objectId) }, null).FirstOrDefault();
                var UserViewData = setoffToolUserViewBLL.QueryByEntity(new SetoffToolUserViewEntity() { ObjectId = objectId, UserID = userId }, null);
                if (!string.IsNullOrEmpty(objectId))
                {
                    UserViewData = setoffToolUserViewBLL.QueryByEntity(new SetoffToolUserViewEntity() { ObjectId = objectId, UserID = userId, SetoffToolID = new Guid(objectId) }, null);
                }
                //如果不存在已读数据则在推送之前 进行已读数据入库
                if (UserViewData.Length == 0 && !string.IsNullOrEmpty(objectId) && setOffToolInfo != null)
                {
                    var SetoffToolUserView = new SetoffToolUserViewEntity();
                    SetoffToolUserView.SetoffToolUserViewID = Guid.NewGuid();
                    SetoffToolUserView.SetoffEventID = setOffToolInfo.SetoffEventID;
                    SetoffToolUserView.ObjectId = setOffToolInfo.ObjectId;
                    SetoffToolUserView.SetoffToolID = new Guid(objectId);
                    SetoffToolUserView.ToolType = objectType;
                    SetoffToolUserView.NoticePlatformType = 2;
                    SetoffToolUserView.UserID = userId;
                    SetoffToolUserView.IsOpen = 1;
                    SetoffToolUserView.OpenTime = System.DateTime.Now;
                    SetoffToolUserView.CustomerId = userId;
                    SetoffToolUserView.CreateTime = System.DateTime.Now;
                    SetoffToolUserView.CreateBy = userId;
                    SetoffToolUserView.LastUpdateTime = System.DateTime.Now;
                    SetoffToolUserView.LastUpdateBy = userId;
                    SetoffToolUserView.IsDelete = 0;
                    setoffToolUserViewBLL.Create(SetoffToolUserView);
                }

                if (setOffToolInfo != null)//已读工具入库之后，将ObjectID给到工具对象的ID 优惠券、海报、活动
                {
                    objectId = setOffToolInfo.ObjectId;
                }
            }

            CSConversationBLL conversationBll = new CSConversationBLL(loggingSessionInfo);
            CSMessageBLL messageBll = new CSMessageBLL(loggingSessionInfo);
            //员工主动发起聊天，在isCS=1的情况下，新加一个字段VipID，用于查找           
            //根据会员id查找csMessage信息，分为三种情况
            //1.有csMessage&connationtime没超过1小时&currentcsid不等于传过来的userid，报“有人正在跟该会员会话，不能发起会话”
            //2、（1）有csMessage，&connationtime没超过1小时并且&currentcsid等于传过来的userid，（2）*** 有csMessage， 超过1小时直接就用这个csMessage的id作为csmessageid继续下面的会话。
            //3 、没有csMessage，都需要创建新的csmessage（需要根据vipid去查会员信息，作为csMessage的memberid等信息），保存到数据库，把这个csmesage的csmessageid作为标识继续下面的操作。

            if ((isCS == 1 || isCS == 3) && !string.IsNullOrEmpty(VipIDInit))//员工主动发起聊天
            {
                messageId = "";//设为空
                var conversationsInit = conversationBll.PagedQueryByEntity(new CSConversationEntity//从会话里查，而不是从message表里查，这主要从客户作为主动权查看来作为会话的主导者
                {
                    PersonID = VipIDInit,
                    IsCS = 0//查询非客服的信息
                }, new[] { new OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }, 1, 1);//1，1代表只取第一条，按照创建时间倒叙排列
                if (conversationsInit != null && conversationsInit.Entities != null && conversationsInit.Entities.Length > 0)
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
                    messageEntityInit = messageBll.GetByID(messageId);//获取关联的message主信息标识
                }
                var personInit = new VipBLL(loggingSessionInfo).GetByID(VipIDInit);//取会员的信息，如果是员工发的，就取到是空
                if (messageEntityInit == null)
                {

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
                else
                {
                    if ((isCS == 1 || isCS == 3) && !string.IsNullOrEmpty(messageEntityInit.CurrentCSID) && messageEntityInit.ConnectionTime != null && messageEntityInit.CurrentCSID.ToLower() != userId.ToLower())//换成userid
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
            SendMessageEntity sendMessageEntity = new SendMessageEntity();//初始化微信内容发送实体
            if (contentTypeId != null)
            {
                switch (contentTypeId)
                {
                    case 1:
                        sendMessageEntity.msgtype = "text";
                        break;
                    case 2:
                        sendMessageEntity.msgtype = "image";
                        //如果是图片，首先要生成临时二维码
                        #region 获取微信帐号
                        var imageUrl = string.Empty;
                        var backImageUrl = string.Empty;//定义一张背景图片
                        Random ro = new Random();
                        var iUp = 100000000;
                        var iDown = 50000000;
                        var rpVipDCode = 0;                 //临时二维码
                        var iResult = ro.Next(iDown, iUp);  //随机数
                        var commonServer = new CommonBLL();
                        var server = new WApplicationInterfaceBLL(loggingSessionInfo);
                        var imgBll = new ObjectImagesBLL(loggingSessionInfo);
                        var setOffPosterBLL = new SetoffPosterBLL(loggingSessionInfo);
                        var SetOffPosterInfo = setOffPosterBLL.QueryByEntity(new SetoffPosterEntity() { SetoffPosterID = new Guid(objectId) }, null);
                        if (SetOffPosterInfo != null)
                        {
                            //如果不为空就获取海报的图片信息
                            var backImgInfo = imgBll.QueryByEntity(new ObjectImagesEntity() { ObjectId = SetOffPosterInfo[0].ImageId }, null);
                            backImageUrl = backImgInfo[0].ImageURL;
                        }
                        var wxObj = new WApplicationInterfaceEntity();
                        wxObj = server.QueryByEntity(new WApplicationInterfaceEntity { CustomerId = loggingSessionInfo.CurrentUser.customer_id }, null).FirstOrDefault();
                        if (wxObj == null)
                        {
                            throw new Exception("不存在对应的微信帐号");
                        }
                        else
                        {
                            imageUrl = commonServer.GetQrcodeUrl(wxObj.AppID
                                , wxObj.AppSecret
                                , rpVipDCode.ToString("")//二维码类型  0： 临时二维码  1：永久二维码
                                , iResult, loggingSessionInfo);//iResult作为场景值ID，临时二维码时为32位整型，永久二维码时只支持1--100000     
                            //供本地测试时使用  "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=gQGN7zoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL1dreENCS1htX0xxQk94SEJ6MktIAAIEUk88VwMECAcAAA==";

                            if (imageUrl != null && !imageUrl.Equals(""))
                            {
                                CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                                string downloadImageUrl = ConfigurationManager.AppSettings["website_WWW"];
                                imageUrl = downloadServer.DownloadFile(imageUrl, downloadImageUrl);
                                //把临时二维码图片放在一定的背景图下面                                   
                                //string apiDomain = ConfigurationManager.AppSettings["website_url"];
                                //CombinImage(backImageUrl, imageUrl, RP.Parameters.RetailTraderName + "合作二维码");
                                imageUrl = CombinImage(backImageUrl, imageUrl, "");
                            }
                            //生成二维码图片后需要存入VipDcode里面
                            #region 创建临时匹配表
                            VipDCodeBLL vipDCodeServer = new VipDCodeBLL(loggingSessionInfo);
                            VipDCodeEntity info = new VipDCodeEntity();
                            info.DCodeId = iResult.ToString();//记录传过去的二维码场景值****（保存到数据库时没加空格）
                            info.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                            VipBLL vipBll = new VipBLL(loggingSessionInfo);
                            info.UnitId = "";
                            info.Status = "0";
                            info.IsReturn = 0;
                            info.DCodeType = 6;//因为是海报临时二维码  DCodeType=6
                            info.CreateBy = loggingSessionInfo.UserID;
                            info.ImageUrl = imageUrl;
                            info.VipId = "";
                            info.ObjectId = objectId;//工具对象ID（优惠券或集客海报对象ID）
                            info.OwnerUserId = loggingSessionInfo.UserID;//当前发送人ID
                            vipDCodeServer.Create(info);
                            #endregion
                            //增加VipDcode记录之后，再返回MediaId参数
                            //首先要获取口令
                            var accessToken = commonServer.GetAccessTokenByCache(wxObj.AppID, wxObj.AppSecret, loggingSessionInfo);
                            MediaType mediaType = MediaType.Image;
                            if (accessToken != null)
                            {
                                var UploadMediaInfo = commonServer.UploadMediaFile(accessToken.access_token, imageUrl, mediaType);
                                if (UploadMediaInfo != null)
                                {
                                    sendMessageEntity.media_id = UploadMediaInfo.media_id;
                                }
                            }
                            else
                            {
                                throw new Exception("获取口令失败");
                            }

                        }
                        #endregion
                        break;
                    case 3:
                        sendMessageEntity.msgtype = "voice";
                        break;
                    case 4:
                        sendMessageEntity.msgtype = "video";
                        break;
                    case 5:
                        sendMessageEntity.msgtype = "news";
                        NewsEntity newsItem = new NewsEntity();
                        newsItem.title = Title;
                        newsItem.description = Description;
                        newsItem.picurl = PicUrl;
                        string strHost = ConfigurationManager.AppSettings["website_url"].Trim();
                        string goUrl = string.Empty;
                        string SourceID = string.Empty;
                        switch (isCS)
                        {
                            case 1:
                                SourceID = "2";//1表示员工
                                break;
                            case 3:
                                SourceID = "1";//1表示员工
                                break;
                        }
                        string OAuthUrl = strHost + "/WXOAuth/AuthUniversal.aspx?scope=snsapi_userinfo&SourceId=" + SourceID + "&customerId=";//微信授权页面
                        string goItemUrl = strHost + "/HtmlApps/html/public/shop/goods_detail.html?customerId=";//商品详细页跳转页
                        string goCouponUrl = strHost + "/HtmlApps/html/common/GatheringClient/Coupon.html?customerId=";//优惠券详细页跳转
                        string goPosterUrl = strHost + "/HtmlApps/html/common/GatheringClient/poster.html?customerId=";//海报详细页跳转
                        var t_LEventsSharePersonLogBLL = new T_LEventsSharePersonLogBLL(loggingSessionInfo);
                        var bllSpreadSetting = new T_CTW_SpreadSettingBLL(loggingSessionInfo);//供查找创意仓库活动描述使用
                        int flag = 0;//处理是否需要插入 通用分享记录(T_LEventsSharePersonLog)表中
                        var personInfo = new VipBLL(loggingSessionInfo).GetByID(VipIDInit);//取会员的信息，如果是员工发的，就取到是空
                        string giverID = string.Empty;
                        if (personInfo.Status <= 1)
                        {
                            giverID = VipIDInit;//没注册给giverID
                        }
                        else
                        {
                            giverID = "";//注册了不给giverID
                        }
                        if (objectType != null && objectType.ToLower() == "product")
                        {
                            //如果是商品则需要查找出商品
                            goUrl = goItemUrl + loggingSessionInfo.CurrentUser.customer_id + "&pushType=IsAppPush&giverId=" + giverID + "&goodsId=" + objectId;
                            Url = OAuthUrl + loggingSessionInfo.CurrentUser.customer_id + "&objectType=Goods&ObjectID=" + objectId + "&ShareVipID=" + user.User_Id + "&goUrl=" + System.Web.HttpUtility.UrlEncode(goUrl);
                            var GoodsShareInfo = t_LEventsSharePersonLogBLL.QueryByEntity(new T_LEventsSharePersonLogEntity() { ObjectId = objectId, ShareVipID = user.User_Id, BeShareVipID = VipIDInit, BusTypeCode = "Goods" }, null);
                            if (GoodsShareInfo.Length == 0)
                            {
                                flag = 1;//如果flag为1则需执行插入操作；
                            }
                        }
                        if (objectType != null && objectType.ToLower() == "ctw")
                        {

                            string CTWUrl = string.Empty;
                            var T_CTW_LEventBLL = new T_CTW_LEventBLL(loggingSessionInfo);
                            var T_CTW_LEventInfo = T_CTW_LEventBLL.QueryByEntity(new T_CTW_LEventEntity() { CustomerId = loggingSessionInfo.CurrentUser.customer_id, CTWEventId = new Guid(objectId) }, null);
                            if (T_CTW_LEventInfo != null)
                            {
                                CTWUrl = T_CTW_LEventInfo[0].OnLineRedirectUrl;//活动不用拼goUrl 需加Oauth认证；
                            }
                            Url = OAuthUrl + loggingSessionInfo.CurrentUser.customer_id + "&objectType=CTW&ObjectID=" + objectId + "&ShareVipID=" + user.User_Id + "&goUrl=" + System.Web.HttpUtility.UrlEncode(CTWUrl + "&pushType=IsAppPush&giverId=" + giverID);
                            var CTWShareInfo = t_LEventsSharePersonLogBLL.QueryByEntity(new T_LEventsSharePersonLogEntity() { ObjectId = objectId, ShareVipID = user.User_Id, BeShareVipID = VipIDInit, BusTypeCode = "CTW" }, null);
                            if (CTWShareInfo.Length == 0)
                            {
                                flag = 1;//如果flag为1则需执行插入操作；
                            }
                            //获取推送时的描述和标题
                            DataSet dsShare = bllSpreadSetting.GetSpreadSettingQRImageByCTWEventId(objectId, "Share");
                            if (dsShare != null && dsShare.Tables.Count > 0 && dsShare.Tables[0].Rows.Count > 0)
                            {
                                newsItem.title = dsShare.Tables[0].Rows[0]["Title"].ToString();
                                newsItem.description = dsShare.Tables[0].Rows[0]["Summary"].ToString();
                                newsItem.picurl = dsShare.Tables[0].Rows[0]["BGImageUrl"].ToString();
                            }
                        }
                        if (objectType != null && objectType.ToLower() == "coupon")
                        {
                            goUrl = goCouponUrl + loggingSessionInfo.CurrentUser.customer_id + "&pushType=IsAppPush&giverId=" + giverID + "&ShareVipId=" + user.User_Id + "&couponId=" + objectId + "&version=";
                            Url = OAuthUrl + loggingSessionInfo.CurrentUser.customer_id + "&objectType=Coupon&ObjectID=" + objectId + "&ShareVipID=" + user.User_Id + "&goUrl=" + System.Web.HttpUtility.UrlEncode(goUrl);
                            newsItem.picurl = strHost + "/Images/CouponImage.jpg";
                            //查找优惠券种相关信息
                            var CouponTypeBLL = new CouponTypeBLL(loggingSessionInfo);
                            var CouponTypeInfo = CouponTypeBLL.QueryByEntity(new CouponTypeEntity() { CouponTypeID = new Guid(objectId) }, null).FirstOrDefault();
                            if (CouponTypeInfo != null)
                            {
                                newsItem.description = CouponTypeInfo.CouponTypeDesc;//给出券摘要信息
                            }
                            var CouponShareInfo = t_LEventsSharePersonLogBLL.QueryByEntity(new T_LEventsSharePersonLogEntity() { ObjectId = objectId, ShareVipID = user.User_Id, BeShareVipID = VipIDInit, BusTypeCode = "Coupon" }, null);
                            if (CouponShareInfo.Length == 0)
                            {
                                flag = 1;//如果flag为1则需执行插入操作；
                            }
                        }
                        if (objectType != null && objectType.ToLower() == "setoffposter")
                        {
                            goUrl = goPosterUrl + loggingSessionInfo.CurrentUser.customer_id + "&pushType=IsAppPush&giverId=" + giverID + "&ShareVipId=" + user.User_Id + "&ObjectId=" + objectId + "&version=";
                            Url = OAuthUrl + loggingSessionInfo.CurrentUser.customer_id + "&objectType=SetoffPoster&ObjectID=" + objectId + "&ShareVipID=" + user.User_Id + "&goUrl=" + System.Web.HttpUtility.UrlEncode(goUrl);
                            //海报目前推送图文时用的是一张静态图片
                            newsItem.picurl = strHost + "/Images/SetOffPosterImg.jpg";
                            newsItem.title = "快来成为我的集客小伙伴！";
                            newsItem.description = "打开后识别二维码，加入微信并注册会员，成为我的小伙伴，带你集客带你飞~";
                            var CouponShareInfo = t_LEventsSharePersonLogBLL.QueryByEntity(new T_LEventsSharePersonLogEntity() { ObjectId = objectId, ShareVipID = user.User_Id, BeShareVipID = VipIDInit, BusTypeCode = "SetoffPoster" }, null);
                            if (CouponShareInfo.Length == 0)
                            {
                                flag = 1;//如果flag为1则需执行插入操作；
                            }
                        }
                        newsItem.url = Url; //System.Web.HttpUtility.UrlDecode(Url, System.Text.Encoding.GetEncoding("GB2312")); ;
                        List<NewsEntity> listnews = new List<NewsEntity>();
                        listnews.Add(newsItem);
                        sendMessageEntity.articles = listnews;
                        string newmessageContent = string.Empty;
                        newmessageContent = messageContent.Substring(0, messageContent.Length - 1);
                        newmessageContent = newmessageContent + "," + "\"hURL\":" + "\"" + Url + "\"}";
                        conversationEntity.Content = newmessageContent + (!string.IsNullOrEmpty(sign) ? "[|" + sign : "") + (!string.IsNullOrEmpty(mobileNo) ? "," + mobileNo : "");
                        if (flag == 1)//若为1则需要插入分享记录
                        {

                            #region 处理发送时的分享记录
                            // var t_LEventsSharePersonLog = new T_LEventsSharePersonLogEntity();
                            //t_LEventsSharePersonLog.SharePersonLogId = Guid.NewGuid();
                            //switch(objectType.ToLower())
                            //{
                            //    case "product":
                            //        t_LEventsSharePersonLog.BusTypeCode = "Goods";
                            //        break;
                            //    case "ctw":
                            //        t_LEventsSharePersonLog.BusTypeCode = "CTW";
                            //        break;
                            //    case "coupon":
                            //        t_LEventsSharePersonLog.BusTypeCode = "Coupon";
                            //        break;
                            //    case "setoffposter":
                            //        t_LEventsSharePersonLog.BusTypeCode = "SetoffPoster";
                            //        break;
                            //}
                            //t_LEventsSharePersonLog.ObjectId = objectId;
                            //if (isCS == 1)
                            //{
                            //    t_LEventsSharePersonLog.ShareVipType = 2;
                            //}
                            //else
                            //{
                            //    t_LEventsSharePersonLog.ShareVipType = 1;
                            //}
                            //t_LEventsSharePersonLog.ShareVipID = user.User_Id;
                            //t_LEventsSharePersonLog.ShareOpenID = "";
                            //t_LEventsSharePersonLog.BeShareVipID = VipIDInit;
                            //if (personInfo != null && !string.IsNullOrEmpty(personInfo.WeiXinUserId))
                            //{
                            //    t_LEventsSharePersonLog.BeShareOpenID = personInfo.WeiXinUserId;
                            //}
                            //else
                            //{
                            //    t_LEventsSharePersonLog.BeShareOpenID = "";
                            //}                            
                            //t_LEventsSharePersonLog.ShareURL = Url;
                            //t_LEventsSharePersonLog.CreateTime =System.DateTime.Now;
                            //t_LEventsSharePersonLog.CreateBy = user.User_Id;
                            //t_LEventsSharePersonLog.LastUpdateBy = user.User_Id;
                            //t_LEventsSharePersonLog.LastUpdateTime = System.DateTime.Now;
                            //t_LEventsSharePersonLog.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                            //t_LEventsSharePersonLog.IsDelete = 0;
                            //t_LEventsSharePersonLogBLL.Create(t_LEventsSharePersonLog);
                            #endregion
                        }
                        break;
                }


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
            if (!string.IsNullOrEmpty(messageId) && messageTypeId == "5")//主动推送客服信息？
            {
                new PushMessageType5(loggingSessionInfo).PushMessage(conversationEntity, sendMessageEntity, VipIDInit);
                return;
            }

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
            cUserService cuserservice = new cUserService(loggingSessionInfo);
            T_UserBLL UserService = new T_UserBLL(loggingSessionInfo);
            VipBLL VipService = new VipBLL(loggingSessionInfo);
            var vipentity = VipService.GetByID(messageEntity.MemberID);
            String SetoffUserId = string.Empty;
            if (vipentity != null)
            {
                //对应集客员工
                if (!String.IsNullOrEmpty(vipentity.SetoffUserId))
                {
                    SetoffUserId = vipentity.SetoffUserId;
                }
            }

            IList<UserInfo> SetoffUserList = cuserservice.GetUserInfoByMenuCode("CustomerService", SetoffUserId);
            //SetoffUserList 给员工和店长发送消息
            foreach (var userInfo in SetoffUserList)
            {
                var csPipelineIDs = GetCurrentCSPipeline(userInfo.User_Id);
                foreach (var csPipelineID in csPipelineIDs)
                {
                    PushNotificationMessage(userInfo.User_Id, csPipelineID, new CSConversationEntity { Content = "您有新的客服请求" });
                }
            }

            if (SetoffUserList.Count() == 0) //给总部发送客服消息
            {
                //待定 给总部有客服权限的员工推送消息

                IList<UserInfo> list = cuserservice.GetUserListByMenuNameAndTypeName("CustomerService");

                foreach (var userInfo in list)
                {
                    var csPipelineIDs = GetCurrentCSPipeline(userInfo.User_Id);
                    foreach (var csPipelineID in csPipelineIDs)
                    {
                        PushNotificationMessage(userInfo.User_Id, csPipelineID, new CSConversationEntity { Content = "您有新的客服请求" });
                    }
                }
            }

            if (!String.IsNullOrEmpty(vipentity.CouponInfo))
            {
                //给 会籍店 发送客服消息
                IList<UserInfo> CouponInfoList = cuserservice.GetUserListByMenuCode("CustomerService", vipentity.CouponInfo);
                //发送消息
                foreach (var userInfo in CouponInfoList)
                {
                    if (userInfo.User_Id != SetoffUserId)  //避免重复发送消息
                    {
                        var csPipelineIDs = GetCurrentCSPipeline(userInfo.User_Id);
                        foreach (var csPipelineID in csPipelineIDs)
                        {
                            PushNotificationMessage(userInfo.User_Id, csPipelineID, new CSConversationEntity { Content = "您有新的客服请求" });
                        }
                    }
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

        public static string CombinImage(string imgBack, string destImg, string strData)
        {
            //1、上面的图片部分
            HttpWebRequest request_qrcode = (HttpWebRequest)WebRequest.Create(destImg);
            WebResponse response_qrcode = null;
            Stream qrcode_stream = null;
            response_qrcode = request_qrcode.GetResponse();
            qrcode_stream = response_qrcode.GetResponseStream();//把要嵌进去的图片转换成流


            Bitmap _bmpQrcode1 = new Bitmap(qrcode_stream);//把流转换成Bitmap
            Bitmap _bmpQrcode = new Bitmap(_bmpQrcode1, 327, 327);//缩放图片           
            //把二维码由八位的格式转为24位的
            Bitmap bmpQrcode = new Bitmap(_bmpQrcode.Width, _bmpQrcode.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb); //并用上面图片的尺寸做了一个位图
            //用上面空的位图生成了一个空的画板
            Graphics g3 = Graphics.FromImage(bmpQrcode);
            g3.DrawImageUnscaled(_bmpQrcode, 0, 0);//把原来的图片画了上去


            //2、背景部分
            HttpWebRequest request_backgroup = (HttpWebRequest)WebRequest.Create(imgBack);
            WebResponse response_keleyi = null;
            Stream backgroup_stream = null;
            response_keleyi = request_backgroup.GetResponse();
            backgroup_stream = response_keleyi.GetResponseStream();//把背景图片转换成流

            Bitmap bmp = new Bitmap(backgroup_stream);
            Graphics g = Graphics.FromImage(bmp);//生成背景图片的画板

            //3、画上文字
            //  String str = "文峰美容";
            Font font = new Font("黑体", 25);
            SolidBrush sbrush = new SolidBrush(Color.White);
            SizeF sizeText = g.MeasureString(strData, font);

            g.DrawString(strData, font, sbrush, (bmp.Width - sizeText.Width) / 2, 490);


            // g.DrawString(str, font, sbrush, new PointF(82, 490));


            g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);//又把背景图片的位图画在了背景画布上。必须要这个，否则无法处理阴影

            //4.合并图片
            g.DrawImage(bmpQrcode, 130, 118, bmpQrcode.Width, bmpQrcode.Height);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            System.Drawing.Image newImg = Image.FromStream(ms);//生成的新的图片
            //把新图片保存下来
            string DownloadUrl = ConfigurationManager.AppSettings["website_WWW"];
            string host = DownloadUrl + "/HeadImage/";
            //创建下载根文件夹
            //var dirPath = @"C:\DownloadFile\";
            var dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "HeadImage\\";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            //根据年月日创建下载子文件夹
            var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + @"\";
            host += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            //下载到本地文件
            var fileExt = Path.GetExtension(destImg).ToLower();
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + ".jpg";//+ fileExt;
            var filePath = dirPath + newFileName;
            host += newFileName;

            newImg.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return host;
        }
    }

}

