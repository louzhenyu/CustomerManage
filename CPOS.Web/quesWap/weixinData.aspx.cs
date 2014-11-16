using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.quesWap
{
    public partial class weixinData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["method"].ToString().Trim();
                switch (dataType)
                {
                    //case "getEventDetail":
                    //    content = getEventDetail();
                    //    break;
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

                var service = new QuestionnaireBLL(Default.GetLoggingSession());

                GetResponseParams<QuestionnaireEntity> returnDataObj = service.getEventApplyQues(reqContentObj.special.eventId);
               // GetResponseParams<QuestionnaireEntity> returnDataObj = service.getEventApplyQues("1");

                var contentObj = new getEventApplyQuesRespContentData();
                respObj.Code = returnDataObj.Code;
                respObj.Description = returnDataObj.Description;

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

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventApplyQues RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.Code = "103";
                respObj.Description = "数据库操作错误";
                respObj.Exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class getEventApplyQuesReqData : Default.RespData
        {
            public getEventApplyQuesReqSpecialData special;
        }
        public class getEventApplyQuesReqSpecialData
        {
            public string eventId;
        }

        public class getEventApplyQuesRespData : Default.RespData
        {
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

                //ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<submitEventApplyReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "submitEventApply ReqContent:{0}",
                        ReqContent)
                });

                var service = new QuestionnaireBLL(Default.GetLoggingSession());

                // WEventUserMappingEntity
                //WEventUserMappingEntity userMappingEntity = new WEventUserMappingEntity();
                //userMappingEntity.UserName = reqContentObj.special.userName;
                //userMappingEntity.Mobile = reqContentObj.special.mobile;
                //userMappingEntity.Email = reqContentObj.special.email;

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

                string userId = reqContentObj.common.userId;
                if (userId == null || userId.Trim().Length == 0) 
                    userId = "1";
                //Jermyn20130621 判断特殊的openId=123456,作为公众平台编辑模式进来的判断逻辑
                if (reqContentObj.common.openId.Equals("123456") || reqContentObj.common.openId.Equals("111"))
                {
                    reqContentObj.common.openId = System.Guid.NewGuid().ToString().Replace("-", "");
                }
                if (reqContentObj.special.userName == null || reqContentObj.special.userName.Trim().Length == 0)
                {
                    reqContentObj.special.userName = System.Guid.NewGuid().ToString().Replace("-", "");
                }
                GetResponseParams<bool> returnDataObj = service.WEventSubmitEventApply(
                    reqContentObj.common.openId,
                    reqContentObj.special.eventId,
                    userId,
                    //userMappingEntity,
                    quesAnswerList, reqContentObj.special.userName);

                respObj.Code = returnDataObj.Code;
                respObj.Description = returnDataObj.Description;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "submitEventApply RespContent:{0}",
                        respObj.ToJSON())
                });

                #region 推送消息

                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = "感谢您积极参与我们的活动。";
                string msgData = "<xml><OpenID><![CDATA[" + reqContentObj.common.openId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                #endregion
            }
            catch (Exception ex)
            {
                respObj.Code = "103";
                respObj.Description = "数据库操作错误";
                respObj.Exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class submitEventApplyReqData : Default.RespData
        {
            public submitEventApplyReqSpecialData special;
            public ReqCommonData common { get; set; }
        }

        public class ReqCommonData
        {
            public string locale { get; set; }
            public string userId { get; set; }
            public string sessionId { get; set; }
            public string version { get; set; }
            public string plat { get; set; }

            public string deviceToken { get; set; }

            public string osInfo { get; set; }

            public string channel { get; set; }

            public string baiduPushAppId { get; set; }

            public string baiduPushChannelId { get; set; }

            public string baiduPushUserId { get; set; }
            public string openId { get; set; } 
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

        public class submitEventApplyRespData : Default.RespData
        {
            public submitEventApplyRespContentData content;
        }
        public class submitEventApplyRespContentData
        {

        }
        #endregion
    }
}