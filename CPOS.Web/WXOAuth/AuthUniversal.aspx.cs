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
    public partial class AuthUniversal : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E";
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A";
        public string goUrl = string.Empty;
        public string pageName = string.Empty;

        //public string customerId = "e703dbedadd943abacf864531decdac1";
        //public string applicationId = "386D08D106C849A9ACAA6E493D23E853";
        //public string goUrl = "dev.o2omarketing.cn:9004/OnlineClothing20131217/guaguaorderLj1.html";
        //public string pageName = "";

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
                var openOAuthAppid = string.Empty;
                if (!string.IsNullOrEmpty(Request["state"]))
                {
                    #region state不为空
                    string state = Request["state"];
                    //Response.Write("state:" + state);
                    try
                    {
                        #region Jermyn20140923
                        if (state.Equals("State"))
                        {
                            state = (string)HttpContext.Current.Session["State"];
                        }
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "State-Jermyn20140923：" + state
                        });
                        #endregion
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
                        pageName = array[3];
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
                        loggingSessionInfo = Default.GetBSLoggingSession(customerId, "AuthUniversal");
                    }
                    else
                    {
                        Response.Write("<br>");
                        Response.Write("没有获取客户标识");
                    }
                    if (!string.IsNullOrEmpty(Request["applicationId"]))//微信菜单传来了applicationId，微信信息的唯一标识
                    {
                        applicationId = Request["applicationId"];
                        var wailist = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity { ApplicationId = applicationId, CustomerId = customerId,IsDelete=0 }, null).FirstOrDefault();
                        openOAuthAppid = wailist != null ? wailist.OpenOAuthAppid : string.Empty;
                    }
                    else
                    {
                        loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                        var list = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity { CustomerId = customerId,IsDelete=0 }, null).ToList();

                        if (list != null && list.Count > 0)
                        {
                            applicationId = list.FirstOrDefault().ApplicationId;
                            if (!string.IsNullOrEmpty(list.FirstOrDefault().OpenOAuthAppid))
                            {
                                openOAuthAppid = list.FirstOrDefault().OpenOAuthAppid;
                            }
                        }
                        else
                        {
                            Response.Write("<br>");
                            Response.Write("没有获取微信标识");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(Request["pageName"]))
                    {
                        this.pageName = Request["pageName"];
                    }
                    //else
                    //{
                    //    var temp = this.GetPageNameFromUrl(Request["goUrl"]);
                    //    if (!string.IsNullOrWhiteSpace(temp))
                    //    {//如果没有则尝试从goUrl的querystring中获取pageName
                    //        this.pageName = temp;
                    //    }
                    //    else
                    //    {
                    //        Response.Write("<br>");
                    //        Response.Write("没有获取跳转页面名");
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(Request["goUrl"]))
                    {
                        goUrl = Request["goUrl"];
                        Response.Write("<br>");
                        Response.Write("goUrl:" + goUrl);
                        string eventId = Request["eventId"];
                        string employeeId = Request["employeeId"];
                        string strRetailTraderId = Request["RetailTraderId"];
                        string strChannelID = Request["ChannelID"];
                        if (eventId != null && !eventId.Equals(""))//如果是跳转到活动的链接，还要加上活动标识***
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
                        if (employeeId != null && !employeeId.Equals(""))//如果是员工打赏的链接，还要加上员工标识***
                        {
                            if (goUrl.IndexOf("?") > 0)
                            {
                                goUrl = goUrl + "&employeeId=" + employeeId + "";
                            }
                            else
                            {
                                goUrl = goUrl + "?employeeId=" + employeeId + "";
                            }
                        }
                        if (strRetailTraderId != null && !strRetailTraderId.Equals(""))
                        {
                            if (goUrl.IndexOf("?") > 0)
                            {
                                goUrl = goUrl + "&RetailTraderId=" + strRetailTraderId + "";
                            }
                            else
                            {
                                goUrl = goUrl + "?RetailTraderId=" + strRetailTraderId + "";
                            }
                        }
                        if (strChannelID != null && !strChannelID.Equals(""))
                        {
                            if (goUrl.IndexOf("?") > 0)
                            {
                                goUrl = goUrl + "&channelId=" + strChannelID + "";
                            }
                            else
                            {
                                goUrl = goUrl + "?channelId=" + strChannelID + "";
                            }
                        }
                        //同样可以把商品的标识也这样处理goodsId,因为如下：
                      //  string itemUrl = weixinDomain + "/WXOAuth/AuthUniversal.aspx?customerId=" + CurrentUserInfo.ClientID
                      //+ "&applicationId=" + applicationId
                        //+ "&goUrl=" + weixinDomain + "/HtmlApps/html/public/shop/goods_detail.html?goodsId="     //把goodsId直接放在了gourl后面，系统会把它作为gourl的参数
                      //+ itemId + "&scope=snsapi_userinfo";

                        //把goodsId直接放在了gourl后面，系统会把它作为gourl的参数，不会吧他作为AuthUniversal的参数，所以不能通过 Request["的方式获取，
                        //也就不需要另外再拼接到gourl后面了***

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
                //loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                GetKeyByApp();
                string code = Request["code"];

                string scope = Request["scope"];
                if (code == null || code.Equals(""))
                {
                    //strState = customerId + "," + applicationId + "," + goUrl; //
                    //strState = HttpUtility.UrlEncode(strState, Encoding.UTF8);
                    //byte[] buff = Encoding.UTF8.GetBytes(strState);
                    //strState = Convert.ToBase64String(buff);
                    if (scope == null || scope.Equals(""))
                    {
                        strState = goUrl + "," + customerId + "," + applicationId + "," + "0" + "," + this.pageName; //

                    }
                    else
                    {
                        if (scope.Equals("snsapi_userinfo"))
                        {
                            strState = goUrl + "," + customerId + "," + applicationId + "," + "1" + "," + this.pageName; // 
                        }
                        else
                        {
                            strState = goUrl + "," + customerId + "," + applicationId + "," + "0" + "," + this.pageName; //
                        }
                    }
                    strState = HttpUtility.UrlEncode(strState, Encoding.UTF8);
                    byte[] buff = Encoding.UTF8.GetBytes(strState);
                    strState = Convert.ToBase64String(buff);

                    //Loggers.Debug(new DebugLogInfo()
                    //{
                    //    Message = "一维码扫描---：" + "SetSignIn"
                    //});
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("goUrl:{0},strRedirectUri:{1}",goUrl,strRedirectUri)
                    });
                    //获取code    ，然后跳到下一步页面获取acces_token,openid
                    if (!string.IsNullOrEmpty(openOAuthAppid))
                    {
                        authBll.GetOAuthCode(strAppId, strRedirectUri, strState, this.Response, scope, openOAuthAppid);
                    }
                    else
                    {
                        authBll.GetOAuthCode(strAppId, strRedirectUri, strState, this.Response, scope);//strRedirectUri是WXOAuth/AuthCodeReques.aspx，strState包含goUrl，并且跳转页面
                    }
                }
                else
                {
                    Response.Write("存在code:" + code);
                    string token = "";
                    string openId = string.Empty;
                    if (!string.IsNullOrEmpty(openOAuthAppid))
                        openId = authBll.GetAccessToken(code, strAppId, strAppSecret, loggingSessionInfo, iRad, out token, openOAuthAppid);
                    else
                        openId = authBll.GetAccessToken(code, strAppId, strAppSecret, loggingSessionInfo, iRad, out token);
                    Response.Write("<br>");
                    Response.Write("OpenID:" + openId);
                    PageRedict(openId);
                }
            }
        }

        #region 获取微信信息
        public void GetKeyByApp()
        {
            WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(loggingSessionInfo);
            WApplicationInterfaceEntity info = server.GetByID(applicationId);
            if (info == null)
            {
                Response.Write("不存在对应的微信标识");
            }
            else
            {
                strAppId = info.AppID;
                strAppSecret = info.AppSecret;
                if (info.AuthUrl == null || info.AuthUrl.Equals(""))
                { }
                else
                {
                    //qianzhi 2014-03-17
                    if (!info.AuthUrl.EndsWith("/"))
                        info.AuthUrl = info.AuthUrl.Insert(info.AuthUrl.Length, "/");

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
            }
            else
            {

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
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "1-1： code为空，跳转: redirecting, strGotoUrl:" + strGotoUrl
                    });
                    Response.Write(strGotoUrl);
                   
                    Response.Redirect(strGotoUrl);
                }
            }

        }

        /// <summary>
        /// 从gourl中获取pagename
        /// </summary>
        /// <param name="pGoUrl"></param>
        /// <returns></returns>
        private string GetPageNameFromUrl(string pGoUrl)
        {
            if (!string.IsNullOrWhiteSpace(pGoUrl))
            {
                string queryString = pGoUrl.Substring(pGoUrl.IndexOf("?") + 1);
                var kvPairs = queryString.Split(new String[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                if (kvPairs != null && kvPairs.Length > 0)
                {
                    foreach (var item in kvPairs)
                    {
                        var kv = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        if (kv != null && kv.Length >= 2)
                        {
                            var key = kv[0];
                            var val = kv[1];
                            if (key.ToLower() == "pagename")
                            {
                                return val;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}