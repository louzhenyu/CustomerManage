using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.CS;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.SendSMSService;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.CustomerService
{
    public partial class Data : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var loggingSessionInfo = Default.GetBSLoggingSession("e703dbedadd943abacf864531decdac1", "1");
            //new CSInvokeMessageBLL(loggingSessionInfo).SendMessage(1, "3f9e2cb3c74a4c76b29196af5ee04c01", 0, null, "测试", null, null, null, 1);
            //IPushMessage pushMessage=new PushIOSMessage(loggingSessionInfo);
            //pushMessage.PushMessage("1D2EFD0B19234B3AA1B638536244B1FE", "testPush");
            //Response.End();

            Response.Clear();
            string content = string.Empty;

            try
            {
                string dataType = Request["action"].Trim();
                switch (dataType)
                {
                    case "sendMessage":
                        content = SendMessage();
                        break;
                    case "receiveMessage":
                        content = ReceiveMessage();
                        break;
                    //case "readFromQueue":
                    //    content = ReadFromQueue();
                    //    break;
                    case "sendSMS":
                        content = SendSMS();
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }

            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        private string SendSMS()
        {
            SendSMSRespData respData = new SendSMSRespData();
            try
            {
                string requestData = Request["ReqContent"];
                if (!string.IsNullOrEmpty(requestData))
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                    {
                        Message = "发送短信消息请求数据：" + requestData
                    });
                    var reqObj = requestData.DeserializeJSONTo<SendSMSReqData>();
                    ReceiveService smsService = new ReceiveService();
                    string ret = smsService.Recieve(reqObj.special.mobileNo, reqObj.special.content, reqObj.special.sign);
                    respData.code = ret;

                }
            }
            catch (Exception ex)
            {

                respData.code = "103";
                respData.description = "操作失败:" + ex.Message;
                respData.exception = ex.Message;
            }
            return respData.ToJSON();
        }
        public class SendSMSReqData : Default.ReqData
        {
            public SendSMSReqDataSpecial special;
        }
        public class SendSMSReqDataSpecial
        {
            public string mobileNo;
            public string content;
            public string sign;
        }
        public class SendSMSRespData : Default.LowerRespData
        {
        }

        #region 发送消息
        private string SendMessage()
        {
            SendMessageRespData respData = new SendMessageRespData();
            try
            {
                string requestData = Request["ReqContent"];
                if (!string.IsNullOrEmpty(requestData))
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                        {
                            Message = "发送客服消息请求数据：" + requestData
                        });
                    var reqObj = requestData.DeserializeJSONTo<SendMessageReqData>();
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    CSInvokeMessageBLL bll = new CSInvokeMessageBLL(loggingSessionInfo);
                    bll.SendMessage(reqObj.special.csPipelineId
                        , reqObj.common.userId
                        , reqObj.special.isCS, reqObj.special.messageId
                        , reqObj.special.messageContent
                        , reqObj.special.serviceTypeId
                        , reqObj.special.objectId
                        , reqObj.special.messageTypeId
                        , reqObj.special.contetTypeId
                        , reqObj.special.sign
                        , reqObj.special.mobileNo);
                }
            }
            catch (Exception ex)
            {

                respData.code = "103";
                respData.description = "操作失败:" + ex.Message;
                respData.exception = ex.Message;
            }
            respData.CurrentServerDateTime = DateTime.Now.To19FormatString();
            return respData.ToJSON();
        }
        public class SendMessageReqData : Default.ReqData
        {
            public SendMessageReqDataSpecial special;

        }
        public class SendMessageReqDataSpecial
        {
            public int csPipelineId;
            public int isCS;
            public string messageId;
            public string messageContent;
            public string objectId;
            public int? serviceTypeId;
            public string messageTypeId;
            public int? contetTypeId;
            public string sign;
            public string mobileNo;
        }
        public class SendMessageRespData : Default.LowerRespData
        {
            public string CurrentServerDateTime { get; set; }
        }
        #endregion

        #region 获取消息
        private string ReceiveMessage()
        {
            RecevieMessageRespData respData = new RecevieMessageRespData();
            try
            {
                string requestData = Request["ReqContent"];
                if (!string.IsNullOrEmpty(requestData))
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                        {
                            Message = "接收客服消息请求数据：" + requestData
                        });
                    var reqObj = requestData.DeserializeJSONTo<RecevieMessageReqData>();
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    CSInvokeMessageBLL bll = new CSInvokeMessageBLL(loggingSessionInfo);
                    int recordCount;
                    IList<CSConversationEntity> conversationEntities = bll.ReceiveMessage(reqObj.common.userId
                        , reqObj.special.isCS
                        , reqObj.special.messageId
                        , reqObj.special.pageSize
                        , reqObj.special.pageIndex
                        ,reqObj.common.customerId
                        , out recordCount);
                    respData.content = new RecevieMessageRespContentData();
                    respData.content.recordCount = recordCount;
                    respData.content.conversations = conversationEntities;
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "操作失败:" + ex.Message;
                respData.exception = ex.Message;
            }
            return respData.ToJSON();
        }
        public class RecevieMessageReqData : Default.ReqData
        {
            public RecevieMessageReqSpecialData special;
        }
        public class RecevieMessageReqSpecialData
        {
            public int isCS;
            public string messageId;
            public int pageSize;
            public int pageIndex;
            public int isReadFromQueue;
        }

        public class RecevieMessageRespData : Default.LowerRespData
        {
            public RecevieMessageRespContentData content;
        }
        public class RecevieMessageRespContentData
        {
            public int recordCount;
            public IList<CSConversationEntity> conversations;
        }
        #endregion

        /*

        #region 从队列中获取新的消息
        private string ReadFromQueue()
        {
            ReadFromQueueRespData respData = new ReadFromQueueRespData();
            try
            {
                string requestData = Request["ReqContent"];
                if (!string.IsNullOrEmpty(requestData))
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo
                    {
                        Message = "接收客服消息请求数据：" + requestData
                    });
                    var reqObj = requestData.DeserializeJSONTo<ReadFromQueueReqData>();
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    CSInvokeMessageBLL bll = new CSInvokeMessageBLL(loggingSessionInfo);
                    int recordCount;
                    IList<CSConversationEntity> conversationEntities = bll.GetFromQueue(reqObj.common.userId);
                    respData.content = new ReadFromQueueRespContentData();
                    respData.content.conversations = conversationEntities;
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "操作失败:" + ex.Message;
                respData.exception = ex.Message;
            }
            return respData.ToJSON();
        }
        public class ReadFromQueueReqData : Default.ReqData
        {

        }

        public class ReadFromQueueRespData : Default.LowerRespData
        {
            public ReadFromQueueRespContentData content;
        }
        public class ReadFromQueueRespContentData
        {
            public IList<CSConversationEntity> conversations;
        }
        #endregion
         * */
    }



}