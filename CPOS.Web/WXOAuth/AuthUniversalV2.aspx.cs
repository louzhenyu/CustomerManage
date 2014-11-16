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
    public partial class AuthUniversalV2 : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E";
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A";
        public string goUrl = string.Empty;
        //public string goUrlTemp = ConfigurationManager.AppSettings["website_url2"];//
        public string strAppId = string.Empty;//"wxeebb52e0aa813101"
        public string strAppSecret = string.Empty; //"22ac924a92e6caf176d6ba426d744adb";
        public string strRedirectUri = ConfigurationManager.AppSettings["website_WWW"] + "WXOAuth/AuthCodeReques.aspx";
        public string strState = string.Empty;
        public LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        JIT.CPOS.BS.BLL.WX.AuthBLL authBll = new BS.BLL.WX.AuthBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("进入认证界面<br/>");
            if (!IsPostBack)
            {
                Random rad = new Random();
                int iRad = rad.Next(1000, 100000);
                if (!string.IsNullOrEmpty(Request["state"]))
                {
                    #region state不为空
                    string state = Request["state"];
                    //Response.Write("state:" + state);
                    try
                    {
                        #region 参数拼成
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("strState2: {0}", state)
                        });
                        //byte[] buff1 = Convert.FromBase64String(state);
                        //state = Encoding.UTF8.GetString(buff1);
                        //state = HttpUtility.UrlDecode(state, Encoding.UTF8);
                        //string[] array = state.Split(',');
                        //customerId = array[0];
                        //applicationId = array[1];
                        //goUrl = array[2];
                        //goUrl = goUrlTemp + goUrl;

                        byte[] buff1 = Convert.FromBase64String(state);
                        state = Encoding.UTF8.GetString(buff1);
                        state = HttpUtility.UrlDecode(state, Encoding.UTF8);
                        string[] array = state.Split(',');
                        customerId = array[1];
                        applicationId = array[2];
                        goUrl = array[0];
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<br/>");
                        Response.Write("state错误:" + ex.ToString());
                    }
                    #endregion
                }
                else
                {
                    #region 参数
                    //判断客户ID是否传递
                    if (!string.IsNullOrEmpty(Request["customerId"]))
                    {
                        customerId = Request["customerId"];
                    }
                    else
                    {
                        Response.Write("<br>");
                        Response.Write("没有获取客户标识");
                    }

                    if (!string.IsNullOrEmpty(Request["applicationId"]))
                    {
                        applicationId = Request["applicationId"];
                    }
                    else
                    {
                        loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                        var list = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity { CustomerId = customerId }, null).ToList();

                        if (list != null && list.Count > 0)
                        {
                            applicationId = list.FirstOrDefault().ApplicationId;
                        }
                        else
                        {
                            Response.Write("<br>");
                            Response.Write("没有获取微信标识,请联系管理员尽快处理.");
                            Response.End();
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(Request["goUrl"]))
                    {
                        goUrl = Request["goUrl"];
                        Response.Write("<br>");
                        Response.Write("goUrl:" + goUrl);
                        string eventId = Request["eventId"];
                        if (eventId != null && !eventId.Equals(""))
                        {
                            if (goUrl.IndexOf("?") > 0)
                            {
                                goUrl = goUrl + "&eventId=" + eventId + "";
                            }
                            else
                            {
                                goUrl = goUrl + "?eventId=" + eventId + "";
                            }
                        }
                        //Response.End();
                    }
                    else
                    {
                        Response.Write("<br>");
                        Response.Write("没有获取goUrl");
                        Response.End();
                    }
                    #endregion
                }
                Response.Write("<br>");
                Response.Write("goUrl:" + goUrl);
                //Response.End();
                loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                
                //Get keys about Wechat
                GetKey();

                string code = Request["code"];
                if (code == null || code.Equals(""))
                {
                    //strState = customerId + "," + applicationId + "," + goUrl; //
                    //strState = HttpUtility.UrlEncode(strState, Encoding.UTF8);
                    //byte[] buff = Encoding.UTF8.GetBytes(strState);
                    //strState = Convert.ToBase64String(buff);

                    strState = goUrl + "," + customerId + "," + applicationId; //
                    strState = HttpUtility.UrlEncode(strState, Encoding.UTF8);
                    byte[] buff = Encoding.UTF8.GetBytes(strState);
                    strState = Convert.ToBase64String(buff);

                    authBll.GetOAuthCode(strAppId,strRedirectUri,strState,this.Response);
                }
                else
                {
                    Response.Write("存在code:" + code);
                    string token = "";
                    string openId = authBll.GetAccessToken(code, strAppId, strAppSecret, loggingSessionInfo, iRad, out token);
                    Response.Write("<br>");
                    Response.Write("OpenID:" + openId);
                    PageRedict(openId);
                }
            }
        }

        #region 获取微信信息
        public void GetKey()
        {
            WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(loggingSessionInfo);
            WApplicationInterfaceEntity info = null;

            if (string.IsNullOrEmpty(applicationId))
                info = server.Query(
                new IWhereCondition[] { new EqualsCondition() { FieldName = "CustomerId", Value = customerId } }
                , null)[0];
            else
                info = server.GetByID(applicationId);

            if (info == null)
            {
                Response.Write("不存在对应的微信标识");
                Response.End();
            }
            else
            {
                applicationId = info.ApplicationId;
                strAppId = info.AppID;
                strAppSecret = info.AppSecret;
                if (info.AuthUrl == null || info.AuthUrl.Equals(""))
                { }
                else { 
                    strRedirectUri = info.AuthUrl + "WXOAuth/AuthCodeReques.aspx";
                }
            }
        }
        #endregion

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="openId"></param>
        private void PageRedict(string openId)
        {
            if (openId == null || openId.Equals(""))
            {
                Response.Write("未获取合法的OpenId");
                Response.Redirect(ConfigurationManager.AppSettings["website_WWW"] + "/HtmlApps/auth.html?pageName=LJscanFail", true);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "1-1： code为空，跳转: redirecting, strGotoUrl:{0}" + ConfigurationManager.AppSettings["website_WWW"] + "/HtmlApps/auth.html?pageName=LJscanFail"
                });
                Response.End();
            }
            else {
                
                VipEntity vipInfo = new VipEntity();
                vipInfo = authBll.GetUserIdByOpenId(loggingSessionInfo, openId);
                if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                {
                    Response.Write("未获取合法的vip信息");
                }
                else
                {
                    Response.Write("获取vip信息");
                    Response.Write("</br>");
                    goUrl = "http://" + HttpUtility.UrlDecode(goUrl) + "";
                    string strGotoUrl = "";//"/OnlineClothing/tmpGoUrl.html?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "&backUrl=" + HttpUtility.UrlEncode(goUrl) + "";
                    Random rad = new Random();
                    if (goUrl.IndexOf("?") > 0)
                    {
                        strGotoUrl = goUrl + "&customerId=" + customerId + "&openId=" + openId + "&userId=" + vipInfo.VIPID + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    else
                    {
                        strGotoUrl = goUrl + "?customerId=" + customerId + "&openId=" + openId + "&userId=" + vipInfo.VIPID + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    Response.Write(strGotoUrl);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "1-1： code为空，跳转: redirecting, strGotoUrl:" + strGotoUrl
                    });
                    Response.Redirect(strGotoUrl, false);
                }
            }

        }

    }
}