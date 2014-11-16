using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.Java
{
    public partial class Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;

            try
            {
                string dataType = Request["dataType"].ToString().Trim();
                switch (dataType)
                {
                    case "MarketEventSignIn":   //市场活动签到
                        content = MarketEventSignIn();
                        break;
                    case "SubmitQuestions":     //提交问题
                        content = SubmitQuestions();
                        break;
                    case "MarketEventResponse": //市场活动响应
                        content = MarketEventResponse();
                        break;
                    case "MarketEventPurchase": //市场活动购买
                        content = MarketEventPurchase();
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

        #region 市场活动签到

        /// <summary>
        /// 市场活动签到
        /// </summary>
        /// <returns></returns>
        public string MarketEventSignIn()
        {
            var loggingSesssionInfo = Default.GetLoggingSession();
            string content = string.Empty;

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Code = "200";
            response.Description = "操作成功";

            try
            {
                string openID = Request["openID"].ToString().Trim();
                string eventID = Request["eventID"].ToString().Trim();

                if (!string.IsNullOrEmpty(openID) && !string.IsNullOrEmpty(eventID))
                {
                    //添加签到信息
                    MarketSignInBLL signInServer = new MarketSignInBLL(loggingSesssionInfo);
                    signInServer.SignIn(openID, eventID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }

            content = string.Format("{{\"description\":\"{0}\",\"code\":\"{1}\"}}",
                response.Description, response.Code);
            return content;
        }

        #endregion

        #region 提交问题

        /// <summary>
        /// 提交问题
        /// </summary>
        /// <returns></returns>
        public string SubmitQuestions()
        {
            var loggingSesssionInfo = Default.GetLoggingSession();
            string content = string.Empty;

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Code = "200";
            response.Description = "操作成功";

            try
            {
                string openID = Request["openID"].ToString().Trim();
                string eventID = Request["eventID"].ToString().Trim();
                string questionID = Request["questionID"].ToString().Trim();
                string answerID = Request["answerID"].ToString().Trim();

                if (!string.IsNullOrEmpty(openID) && 
                    !string.IsNullOrEmpty(eventID) &&
                    !string.IsNullOrEmpty(questionID) &&
                    !string.IsNullOrEmpty(answerID))
                {
                    //添加问题信息
                    MarketQuesAnswerBLL quesServer = new MarketQuesAnswerBLL(loggingSesssionInfo);
                    quesServer.SubmitQuestions(openID, eventID, questionID, answerID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }

            content = string.Format("{{\"description\":\"{0}\",\"code\":\"{1}\"}}",
                response.Description, response.Code);
            return content;
        }

        #endregion

        #region 市场活动响应

        /// <summary>
        /// 市场活动响应
        /// </summary>
        /// <returns></returns>
        public string MarketEventResponse()
        {
            var loggingSesssionInfo = Default.GetLoggingSession();
            string content = string.Empty;

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Code = "200";
            response.Description = "操作成功";

            try
            {
                string openID = Request["openID"].ToString().Trim();
                string eventID = Request["eventID"].ToString().Trim();

                if (!string.IsNullOrEmpty(openID) && !string.IsNullOrEmpty(eventID))
                {
                    MarketEventResponseBLL responseServer = new MarketEventResponseBLL(loggingSesssionInfo);
                    responseServer.MarketEventResponse(openID, eventID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }

            content = string.Format("{{\"description\":\"{0}\",\"code\":\"{1}\"}}",
                response.Description, response.Code);
            return content;
        }

        #endregion

        #region 市场活动购买

        /// <summary>
        /// 市场活动购买
        /// </summary>
        /// <returns></returns>
        public string MarketEventPurchase()
        {
            var loggingSesssionInfo = Default.GetLoggingSession();
            string content = string.Empty;

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Code = "200";
            response.Description = "操作成功";

            try
            {
                string openID = Request["openID"].ToString().Trim();
                string eventID = Request["eventID"].ToString().Trim();
                string productName = Request["productName"].ToString().Trim();
                string purchaseAmount = Request["purchaseAmount"].ToString().Trim();

                if (!string.IsNullOrEmpty(openID) &&
                    !string.IsNullOrEmpty(eventID) &&
                    !string.IsNullOrEmpty(productName) &&
                    !string.IsNullOrEmpty(purchaseAmount))
                {
                    //添加商品购买信息
                    MarketEventResponseBLL responseServer = new MarketEventResponseBLL(loggingSesssionInfo);
                    responseServer.MarketEventPurchase(openID, eventID, productName, purchaseAmount);
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }

            content = string.Format("{{\"description\":\"{0}\",\"code\":\"{1}\"}}",
                response.Description, response.Code);
            return content;
        }

        #endregion
    }
}