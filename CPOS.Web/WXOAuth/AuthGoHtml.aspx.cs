using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NetDimension.Weibo;
using NetDimension.Web;
using System.Configuration;
using System.Text;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.WXOAuth
{
    public partial class AuthGoHtml : System.Web.UI.Page
    {
        public string state = string.Empty;
        public string code = string.Empty;
        public string goUrl = string.Empty;

        public string strError = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
             //Response.Write("进入认证界面<br/>");
             if (!IsPostBack)
             {
                 if (!string.IsNullOrEmpty(Request["state"]))
                 {
                     state = Request["state"];
                     //Response.Write("state"+state);
                     Response.Write("<br>");
                 }
                 if (!string.IsNullOrEmpty(Request["code"]))
                 {
                     code = Request["code"];
                     //Response.Write("code:"+ code);
                     Response.Write("<br>");
                 }
                 if (!string.IsNullOrEmpty(Request["goUrl"]))
                 {
                     goUrl = Request["goUrl"];
                     Response.Write("goUrl:" + goUrl);
                     Response.Write("<br>");
                 }
                 SetToken();
             }
        }

        private void SetToken()
        { 
            string customerId = string.Empty;
            string applicationId = string.Empty;
            string userId = string.Empty;
            string openId = string.Empty;
            string eventId = string.Empty;
            string strAppId = string.Empty;
             string strAppSecret = string.Empty;
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            try
            {
                #region 解析参数
                byte[] buff1 = Convert.FromBase64String(state);
                state = Encoding.UTF8.GetString(buff1);
                state = HttpUtility.UrlDecode(state, Encoding.UTF8);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetToken--state: {0}", state.ToString())
                });
                string[] array = state.Split(',');
                customerId = array[0];
                applicationId = array[1];
                userId = array[2];
                openId = array[3];
                eventId = array[4];
                goUrl = array[5];
                #endregion
                loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(loggingSessionInfo);
                WApplicationInterfaceEntity info = server.GetByID(applicationId);
                if (info == null)
                {
                    Response.Write("不存在对应的微信标识");
                }
                else
                {
                    
                    Random rad = new Random();
                    int iRad = rad.Next(1000, 100000);
                    strAppId = info.AppID;
                    strAppSecret = info.AppSecret;
                    JIT.CPOS.BS.BLL.WX.AuthBLL authServer = new BS.BLL.WX.AuthBLL();
                    string token = "";
                    string openIdNew = authServer.GetAccessToken(code, strAppId, strAppSecret, loggingSessionInfo, iRad, out token);
                    //Response.Write("openId:" + openId);
                    //Response.Write("------------");
                    //Response.Write("openIdNew:" + openIdNew);
                    if (!openIdNew.Equals(""))
                    {
                        if (openId.Equals(openIdNew))
                        {
                            goUrl = HttpUtility.UrlDecode(goUrl) + "?customerId=" + customerId + "&openId=" + openId + "&userId=" + userId + "&eventId=" + eventId + "&code=" + code + "&rid=" + rad.Next(1000, 100000) + "";
                            //"http://" + HttpUtility.UrlDecode(goUrl) + "";
                            //Response.Write(goUrl);
                            Response.Redirect(goUrl);
                        }
                    }
                    else {
                        Response.Write("请关注微讯网（o2omarketing）公众平台，在登录该界面操作。");
                    }
                }
            }
            catch (Exception ex) {
                strError = ex.ToString();
            }
        }
    }
}