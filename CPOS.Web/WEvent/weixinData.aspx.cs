using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;


namespace JIT.CPOS.Web.WEvent
{
    public partial class weixinData : System.Web.UI.Page
    {
        string customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";//e703dbedadd943abacf864531decdac1
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["method"].ToString().Trim();
                switch (dataType)
                {
                    case "getEventDetail":
                        content = getEventDetail();
                        break;
                    case "getEventApplyQues":
                        content = getEventApplyQues();
                        break;
                    case "submitEventApply":
                        content = submitEventApply();
                        break;
                    default:
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

        #region 活动详细内容获取
        /// <summary>
        /// 活动详细内容获取
        /// </summary>
        public string getEventDetail()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new getEventDetailRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["ReqContent"];
                ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<getEventDetailReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventDetail ReqContent:{0}",
                        ReqContent)
                });

                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var service = new LEventsBLL(loggingSessionInfo);
                
                //var service = new EventsBLL(Default.GetBasicUserInfo(reqContentObj));

                GetResponseParams<LEventsEntity> returnDataObj = service.WEventGetEventDetail(
                    reqContentObj.special.eventId,
                    reqContentObj.common.userId);

                var contentObj = new getEventDetailRespContentData();
                respObj.code = returnDataObj.Code;
                respObj.description = returnDataObj.Description;
                //
                if (returnDataObj.Flag == "1" && returnDataObj.Params != null)
                {
                    contentObj.eventId = returnDataObj.Params.EventID;
                    contentObj.title = returnDataObj.Params.Title;
                    contentObj.city = returnDataObj.Params.CityID;
                    contentObj.address = Default.ToStr(returnDataObj.Params.Address);
                    contentObj.contact = Default.ToStr(returnDataObj.Params.Content);
                    contentObj.email = Default.ToStr(returnDataObj.Params.Email);
                    //qianzhi 2013-05-25  添加结束时间
                    if (returnDataObj.Params.BeginTime == null || Convert.ToDateTime(returnDataObj.Params.BeginTime).ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    {
                        contentObj.timeStr = "待定";
                    }
                    else {
                        if (returnDataObj.Params.EndTime == null || returnDataObj.Params.EndTime.Equals(""))
                        {
                            contentObj.timeStr = Default.ToStr(Convert.ToDateTime(returnDataObj.Params.BeginTime).ToString("yyyy-MM-dd HH:mm"));
                        }
                        else {
                            contentObj.timeStr = Default.ToStr(Convert.ToDateTime(returnDataObj.Params.BeginTime).ToString("yyyy-MM-dd HH:mm")) + " 至 " +
                                Default.ToStr(Convert.ToDateTime(returnDataObj.Params.EndTime).ToString("yyyy-MM-dd HH:mm"));
                        }
                        
                    }

                    contentObj.imageUrl = Default.ToStr(returnDataObj.Params.ImageURL);

                    contentObj.organizer = "";
                    contentObj.organizerType = "";
                    contentObj.applyCount = Default.ToStr(returnDataObj.Params.signUpCount);        //报名数量
                    contentObj.checkinCount = Default.ToStr(returnDataObj.Params.CheckinsCount);    //签到数量
                    contentObj.hasPrize = "";
                    contentObj.intervalDays = Default.ToStr(returnDataObj.Params.IntervalDays);
                    contentObj.description =  HttpUtility.HtmlDecode(returnDataObj.Params.Description);

                    contentObj.longitude = Default.ToStr(returnDataObj.Params.Longitude);
                    contentObj.latitude = Default.ToStr(returnDataObj.Params.Latitude);
                }

                respObj.content = contentObj;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventDetail RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class getEventDetailReqData : Default.ReqData
        {
            public getEventDetailReqSpecialData special;
        }
        public class getEventDetailReqSpecialData
        {
            public string eventId;
        }

        public class getEventDetailRespData 
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public getEventDetailRespContentData content;
        }
        public class getEventDetailRespContentData
        {
            public string eventId;
            public string title;
            public string city;
            public string address;
            public string contact;
            public string email;
            public string imageUrl;
            public string timeStr;
            public string organizer;
            public string organizerType;
            public string applyCount;
            public string checkinCount;
            public string hasPrize;
            public string intervalDays;
            public string description;
            public string longitude;
            public string latitude;
        }
        #endregion

        #region 活动报名表数据获取
        /// <summary>
        /// 活动报名表数据获取 
        /// </summary>
        public string getEventApplyQues()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new getEventApplyQuesRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["ReqContent"];
                ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<getEventApplyQuesReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventApplyQues ReqContent:{0}",
                        ReqContent)
                });
                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var service = new WEventUserMappingBLL(loggingSessionInfo);

                GetResponseParams<QuestionnaireEntity> returnDataObj = service.getEventApplyQues(
                    reqContentObj.special.eventId);

                var contentObj = new getEventApplyQuesRespContentData();
                respObj.code = returnDataObj.Code;
                respObj.description = returnDataObj.Description;

                
                if (returnDataObj.Flag == "1" && returnDataObj.Params != null)
                {
                    contentObj.questionCount = Default.ToStr(returnDataObj.Params.QuestionCount);

                    // questions
                    if (returnDataObj.Params.QuesQuestionEntityList != null)
                    {
                        contentObj.questions = new List<getEventApplyQuesRespQuestionData>();
                        foreach (var tmpQuestion in returnDataObj.Params.QuesQuestionEntityList)
                        {
                            if (tmpQuestion == null) continue;
                            var tmpQues = new getEventApplyQuesRespQuestionData();
                            tmpQues.questionId = Default.ToStr(tmpQuestion.QuestionID);
                            tmpQues.isSaveOutEvent = Default.ToStr(tmpQuestion.IsSaveOutEvent);
                            tmpQues.cookieName = Default.ToStr(tmpQuestion.CookieName);
                            tmpQues.questionText = Default.ToStr(tmpQuestion.QuestionDesc);
                            tmpQues.questionType = Default.ToStr(tmpQuestion.QuestionType);
                            tmpQues.minSelected = Default.ToStr(tmpQuestion.MinSelected);
                            tmpQues.maxSelected = Default.ToStr(tmpQuestion.MaxSelected);
                            tmpQues.isRequired = Default.ToStr(tmpQuestion.IsRequired);
                            tmpQues.isFinished = Default.ToStr(tmpQuestion.IsFinished);

                            // options
                            if (tmpQuestion.QuesOptionEntityList != null)
                            {
                                tmpQues.options = new List<getEventApplyQuesRespOptionData>();
                                foreach (var tmpOption in tmpQuestion.QuesOptionEntityList)
                                {
                                    if (tmpOption == null) continue;
                                    var tmpOp = new getEventApplyQuesRespOptionData();
                                    tmpOp.optionId = Default.ToStr(tmpOption.OptionsID);
                                    tmpOp.optionText = Default.ToStr(tmpOption.OptionsText);
                                    tmpOp.isSelected = Default.ToStr(tmpOption.IsSelect);
                                    tmpQues.options.Add(tmpOp);
                                }
                            }

                            contentObj.questions.Add(tmpQues);
                        }
                    }

                }

                respObj.content = contentObj;
                LEventsBLL eventServer = new LEventsBLL(loggingSessionInfo);
                LEventsEntity eventInfo = new LEventsEntity();
                eventInfo = eventServer.GetByID(reqContentObj.special.eventId);
                respObj.content.imageUrl = eventInfo.URL;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventApplyQues RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                //respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class getEventApplyQuesReqData : Default.ReqData
        {
            public getEventApplyQuesReqSpecialData special;
        }
        public class getEventApplyQuesReqSpecialData
        {
            public string eventId;
        }

        public class getEventApplyQuesRespData 
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public getEventApplyQuesRespContentData content;
        }
        public class getEventApplyQuesRespContentData
        {
            public string userName;
            public string userId;
            public string classValue; // class
            public string mobile;
            public string email;
            public string questionCount;
            public string imageUrl;     //标题图片
            public IList<getEventApplyQuesRespQuestionData> questions;
        }
        public class getEventApplyQuesRespQuestionData
        {
            public string questionId;
            public string isSaveOutEvent;
            public string cookieName;
            public string questionText;
            public string questionType;
            public string minSelected;
            public string maxSelected;
            public string isRequired;
            public string isFinished;
            public IList<getEventApplyQuesRespOptionData> options;
        }
        public class getEventApplyQuesRespOptionData
        {
            public string optionId;
            public string optionText;
            public string isSelected;
        }
        #endregion

        #region 活动报名表数据提交
        /// <summary>
        /// 活动报名表数据提交
        /// </summary>
        public string submitEventApply()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new submitEventApplyRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["Form"];

                //ReqContent = "{\"common\":{\"locale\":\"zh\",\"userId\":\"4f4ef63846f646b68e796cbc3604f2ed\",\"openId\":\"o8Y7Ejv3jR5fEkneCNu6N1_TIYIM\",\"customerId\":\"f6a7da3d28f74f2abedfc3ea0cf65c01\"},\"special\":{\"eventId\":\"8D41CDD7D5E4499195316E4645FCD7B9\",\"questions\":[{\"questionId\":\"87871FCE7117481DB2F72F28D627579F\",\"isSaveOutEvent\":\"0\",\"cookieName\":\"110801\",\"questionValue\":\"E9EAAE121543475EB57B1936EB98B4B7\"},{\"questionId\":\"CF21F654796F4E0B8F6F47D9D05B9407\",\"isSaveOutEvent\":\"0\",\"cookieName\":\"110802\",\"questionValue\":\"81E327E3252F4071AD9556F89580DCE2\"},{\"questionId\":\"4A73FEA6C1484ED4B1730A1EBC54E5B8\",\"isSaveOutEvent\":\"0\",\"cookieName\":\"110803\",\"questionValue\":\"11778879013148F2A424D5220FB02E09\"}],\"userName\":\"\",\"mobile\":\"\",\"email\":\"\"}}";

                //ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<submitEventApplyReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "submitEventApply ReqContent:{0}",
                        ReqContent)
                });
                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var service = new LEventsBLL(loggingSessionInfo);

                // WEventUserMappingEntity
                WEventUserMappingEntity userMappingEntity = new WEventUserMappingEntity();
                userMappingEntity.UserName = reqContentObj.special.userName;
                userMappingEntity.Mobile = reqContentObj.special.mobile;
                userMappingEntity.Email = reqContentObj.special.email;

                // quesAnswerList
                IList<QuesAnswerEntity> quesAnswerList = new List<QuesAnswerEntity>();
                if (reqContentObj.special.questions != null)
                {
                    foreach (var question in reqContentObj.special.questions)
                    {
                        QuesAnswerEntity quesAnswerEntity = new QuesAnswerEntity();
                        quesAnswerEntity.QuestionID = question.questionId;
                        quesAnswerEntity.QuestionValue = question.questionValue;
                        quesAnswerList.Add(quesAnswerEntity);
                    }
                }

                GetResponseParams<bool> returnDataObj = service.WEventSubmitEventApply(
                    reqContentObj.special.eventId,
                    reqContentObj.common.userId,
                    userMappingEntity,
                    quesAnswerList);

                respObj.code = returnDataObj.Code;
                respObj.description = returnDataObj.Description;
                //Jermyn20131108 提交问题之后微信推送 Jermyn20131209 更改了业务逻辑，暂时关闭
                //PushWeiXin(reqContentObj.common.openId, loggingSessionInfo, reqContentObj.special.eventId, reqContentObj.common.userId);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "submitEventApply RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                //respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class submitEventApplyReqData : Default.ReqData
        {
            public submitEventApplyReqSpecialData special;
        }
        public class submitEventApplyReqSpecialData
        {
            public string eventId;
            public string userName;
            public string mobile;
            public string email;
            public IList<submitEventApplyReqQuestionData> questions;
        }
        public class submitEventApplyReqQuestionData
        {
            public string questionId;
            public string questionValue;
        }

        public class submitEventApplyRespData 
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public submitEventApplyRespContentData content;
        }
        public class submitEventApplyRespContentData
        {

        }
        #endregion

        #region 推送信息
        public void PushWeiXin(string OpenId,LoggingSessionInfo loggingSessionInfo,string OrderId,string VipId)
        {
            string webUrl = ConfigurationManager.AppSettings["website_url"];
            string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"]; ;
            Random rad = new Random();
            string msgText = "<a href='" + webUrl + "wap/Event/20131109/aboutEvent.htm'>点击查看本次活动议程</a>";
            string msgData = "<xml><OpenID><![CDATA[" + OpenId + "]]></OpenID>"
                            +"<Content><![CDATA[" + msgText + "]]></Content></xml>";

            //var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
            #region 发送日志
            MarketSendLogBLL logServer = new MarketSendLogBLL(loggingSessionInfo);
            MarketSendLogEntity logInfo = new MarketSendLogEntity();
            logInfo.LogId = BaseService.NewGuidPub();
            logInfo.IsSuccess = 1;
            logInfo.MarketEventId = OrderId;
            logInfo.SendTypeId = "2";
            logInfo.TemplateContent = msgData;
            logInfo.VipId = VipId;
            logInfo.WeiXinUserId = OpenId;
            logInfo.CreateTime = System.DateTime.Now;
            logServer.Create(logInfo);
            #endregion

            #region
            msgText = "亲爱的，为答谢您参加本次活动，主办方推出多个奖品的刮刮卡等你来刮，快来试试运气，<a href='" + webUrl + "OnlineClothing/1109guagua.html?customerId=" 
                + loggingSessionInfo.CurrentUser.customer_id 
                + "&userId="
                + VipId
                + "&openId="
                + OpenId
                + "&eventId="+OrderId+"'>点击试试手气</a>";
            //msgData = "<xml><OpenID><![CDATA[" + OpenId + "]]></OpenID>"
            //                + "<Content><![CDATA[" + msgText + "]]></Content></xml>";
            LEventsBLL eventServer = new LEventsBLL(loggingSessionInfo);
            LEventsEntity eventInfo = new LEventsEntity();
            eventInfo = eventServer.GetByID(OrderId);
            if (eventInfo != null && eventInfo.EventID != null)
            {
                string picUrl1 = eventInfo.Content;
                string Url1 = "" + webUrl + "OnlineClothing/1109guagua.html?customerId="
                + loggingSessionInfo.CurrentUser.customer_id
                + "&userId="
                + VipId
                + "&openId="
                + OpenId
                + "&eventId=" + OrderId + "";
                msgData = "<xml>"
                        + "<OpenID><![CDATA[" + OpenId + "]]></OpenID>"
                        + "<MsgType><![CDATA[news]]></MsgType>"
                        + "<Articles>"
                        + "<item>"
                        + "<Title><![CDATA[活动刮刮卡，惊喜享不停！]]></Title> "
                        + "<Description><![CDATA[亲！为答谢您来参加本次活动，我们特别推出刮刮卡，大量精美奖品等你来取，快来试试运气吧。]]></Description> "
                        + "<Url><![CDATA[" + Url1 + "]]></Url> "
                        + "<PicUrl><![CDATA[" + picUrl1 + "]]></PicUrl> "
                        + "</item>"
                        + "</Articles>"
                        + "</xml>";

                var msgResult1 = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
            }
            #endregion

            #region 发送日志
            MarketSendLogEntity logInfo1 = new MarketSendLogEntity();
            logInfo1.LogId = BaseService.NewGuidPub();
            logInfo1.IsSuccess = 1;
            logInfo1.MarketEventId = OrderId;
            logInfo1.SendTypeId = "2";
            logInfo1.TemplateContent = msgData;
            logInfo1.VipId = VipId;
            logInfo1.WeiXinUserId = OpenId;
            logInfo1.CreateTime = System.DateTime.Now;
            logServer.Create(logInfo1);
            #endregion

        }
        #endregion
    }
}