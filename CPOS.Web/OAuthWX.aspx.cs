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

namespace JIT.CPOS.Web
{
    public partial class OAuthWX : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E";
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A";
        public string goUrl = string.Empty;
        public string strAppId = string.Empty;//"wxeebb52e0aa813101"
        public string strAppSecret = string.Empty; //"22ac924a92e6caf176d6ba426d744adb";
        public string strRedirectUri = ConfigurationManager.AppSettings["website_WWW"] + "OAuthWX.aspx"; 
        public string strState = string.Empty;
        public LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("进入认证界面<br/>");
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["state"]))
                {
                    string state = Request["state"];
                    //Response.Write("state:" + state);
                    try
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("strState2: {0}", state)
                        });
                        //state = ((((state).Replace("sb", ".")).Replace("555", "/")).Replace("666", ":")).Replace("sss",",");                
                        //string[] array = state.Split(',');
                        //customerId = array[1];
                        //applicationId = array[2];
                        //goUrl = array[0];

                        byte[] buff1 = Convert.FromBase64String(state);
                        state = Encoding.UTF8.GetString(buff1);
                        state = HttpUtility.UrlDecode(state, Encoding.UTF8);
                        string[] array = state.Split(',');
                        customerId = array[1];
                        applicationId = array[2];
                        goUrl = array[0];
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("认证之后的goUrl: goUrl = {0}", goUrl)
                        });

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<br/>");
                        Response.Write("state错误:请联系管理员尽快处理." + ex.ToString());
                    }
                }
                else
                {
                    //判断客户ID是否传递
                    if (!string.IsNullOrEmpty(Request["customerId"]))
                    {
                        customerId = Request["customerId"];
                    }
                    else
                    {
                        Response.Write("<br>");
                        Response.Write("没有获取客户标识,请联系管理员尽快处理.");
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
                        }  
                    }
                    if (!string.IsNullOrEmpty(Request["goUrl"]))
                    {
                        goUrl = Request["goUrl"];
                        string eventId = Request["eventId"];
                        if (eventId != null && !eventId.Equals(""))
                        {
                            if (goUrl.IndexOf("?") > 0)
                            {
                                goUrl = goUrl + "&eventId=" + eventId + "";
                            }else{
                                goUrl = goUrl + "?eventId=" + eventId + "";
                            }
                        }
                        Response.Write("<br>");
                        Response.Write("goUrl:" + goUrl);
                        //Response.End();
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("认证之前的goUrl: goUrl = {0}", goUrl)
                        });
                    }
                    else
                    {
                        Response.Write("<br>");
                        Response.Write("没有获取goUrl,请联系管理员尽快处理.");
                        Response.End();
                    }
                }
                Response.Write("<br>");
                Response.Write("goUrl:" + goUrl);
                //Response.End();
                loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                GetKeyByApp();
                string code = Request["code"];
                if (code == null || code.Equals(""))
                {
                    strState = goUrl + "," + customerId + "," + applicationId; //
                    strState = HttpUtility.UrlEncode(strState, Encoding.UTF8);
                    byte[] buff = Encoding.UTF8.GetBytes(strState);
                    strState = Convert.ToBase64String(buff);

                    GetOAuthCode(strState);
                }
                else
                {
                    //Response.Write("存在code:" + code);
                    GetAccessToken(code);
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
                Response.Write("不存在对应的微信标识,请联系管理员尽快处理.");
                Response.End();
            }
            else {
                strAppId = info.AppID;
                strAppSecret = info.AppSecret;
                if (info.AuthUrl == null || info.AuthUrl.Equals(""))
                { }
                else
                {
                    strRedirectUri = info.AuthUrl + "OAuthWX.aspx";
                }
            }
        }
        #endregion

        #region 第一步 获取code
        /// <summary>
        /// 获取code
        /// </summary>
        private void GetOAuthCode(string strState)
        {
            try
            {
                //判断客户ID是否传递
                //if (!string.IsNullOrEmpty(Request["customerId"]))
                //{
                //    customerId = Request["customerId"];
                //}
                //if (!string.IsNullOrEmpty(Request["applicationId"]))
                //{
                //    applicationId = Request["applicationId"];
                //}
                //strState = (((goUrl1).Replace(".", "sb")).Replace("/", "555")).Replace(":", "666") + "sss" + customerId + "sss" + applicationId;
                //strState = CommonCompress.StringCompress(strState);
                string url = "http://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=snsapi_base&state=" + strState + "#wechat_redirect";
                //Response.Write(url);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetOAuthCode: url = {0}", url)
                });

                Response.Redirect(url,false);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo()
                {
                   ErrorMessage = string.Format("GetOAuthCode 出错: {0}", ex.ToString())
                });
                Response.Write("向微信请求认证Code出错，请联系管理员尽快处理.");
                Response.End();
            }
         }
        
        #endregion

        #region 第二步 获取Access Token
        /// <summary>
        /// 获取Access Token
        /// </summary>
        /// <param name="code"></param>
        private void GetAccessToken(string code)
        {
            MarketSendLogBLL sendServer = new MarketSendLogBLL(loggingSessionInfo);
            MarketSendLogEntity sendInfo = new MarketSendLogEntity();
            Random rad = new Random();
            int iRad = rad.Next(1000, 100000);
            try
            {
                sendInfo.LogId = BaseService.NewGuidPub();
                sendInfo.IsSuccess = 1;
                sendInfo.MarketEventId = "GetAccessToken";
                sendInfo.SendTypeId = "220";
                sendInfo.Phone = iRad.ToString();
                sendInfo.TemplateContent = "GetAccessToken:code:" + code + ",strAppId:" + strAppId + ",strAppSecret:" + strAppSecret;
                sendInfo.VipId = code;
                sendInfo.WeiXinUserId = "1111";
                sendInfo.CreateTime = System.DateTime.Now;
                sendServer.Create(sendInfo);
                 var sendObjList = sendServer.QueryByEntity(new MarketSendLogEntity
                {
                    VipId = code
                    ,WeiXinUserId = "AccessToken"
                    ,IsDelete = 0
                }, null);
                string data = string.Empty;
                if (sendObjList == null || sendObjList.Length == 0 || sendObjList[0] == null)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                    string url = "https://api.weixin.qq.com/sns/oauth2/access_token";
                    WebClient myWebClient = new WebClient();
                    // 注意这种拼字符串的ContentType
                    myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    // 转化成二进制数组
                    var postData = "appid=" + strAppId + "&secret=" + strAppSecret + "&code=" + code + "&grant_type=authorization_code";
                    sendInfo.LogId = BaseService.NewGuidPub();
                    sendInfo.IsSuccess = 1;
                    sendInfo.MarketEventId = "GetAccessToken转化成二进制数组";
                    sendInfo.SendTypeId = "220";
                    sendInfo.Phone = iRad.ToString();
                    sendInfo.TemplateContent = string.Format("{0}", postData);
                    sendInfo.VipId = code;
                    sendInfo.WeiXinUserId = "AccessToken-Para";
                    sendInfo.CreateTime = System.DateTime.Now;
                    sendServer.Create(sendInfo);
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    // 上传数据，并获取返回的二进制数据.
                    byte[] responseArray = myWebClient.UploadData(url, "POST", byteArray);
                    data = System.Text.Encoding.UTF8.GetString(responseArray);

                    if (data.IndexOf("errcode") > 1)
                    {
                        var sendObjList1 = sendServer.QueryByEntity(new MarketSendLogEntity
                        {
                            VipId = code
                            ,
                            WeiXinUserId = "AccessToken"
                            ,
                            IsDelete = 0
                            ,
                            IsSuccess = 1
                        }, null);
                        if (sendObjList1 == null || sendObjList1.Length == 0 || sendObjList1[0] == null)
                        { }
                        else
                        {
                            data = sendObjList1[0].TemplateContent.ToString().Trim();
                        }
                    }
                    sendInfo.LogId = BaseService.NewGuidPub();
                    if (data.IndexOf("errcode") > 1)
                    {
                        sendInfo.IsSuccess = 0;
                    }
                    else
                    {
                        sendInfo.IsSuccess = 1;
                    }
                    sendInfo.MarketEventId = "GetAccessToken-3";
                    sendInfo.SendTypeId = "220";
                    sendInfo.Phone = iRad.ToString();
                    sendInfo.TemplateContent = string.Format("{0}", data);
                    sendInfo.VipId = code;
                    sendInfo.WeiXinUserId = "AccessToken";
                    sendInfo.CreateTime = System.DateTime.Now;
                    sendServer.Create(sendInfo);
                }
                else
                {
                    data = sendObjList[0].TemplateContent.ToString().Trim();
                    sendInfo.LogId = BaseService.NewGuidPub();
                    sendInfo.IsSuccess = 1;
                    sendInfo.MarketEventId = "GetAccessToken-3-2";
                    sendInfo.SendTypeId = "220";
                    sendInfo.Phone = iRad.ToString();
                    sendInfo.TemplateContent = string.Format("{0}", data);
                    sendInfo.VipId = code;
                    sendInfo.WeiXinUserId = "AccessToken--重复2";
                    sendInfo.CreateTime = System.DateTime.Now;
                    sendServer.Create(sendInfo);
                }
                var tokenInfo = data.DeserializeJSONTo<cAccessTokenReturn>();
                //Response.Write("<br/>");
                //Response.Write("获取Access Token");
                if (tokenInfo != null)
                {
                    //Response.Write("<br/>");
                    //Response.Write("获取Access Token不为空");
                    GetUserIdByOpenId(tokenInfo.openid);
                }
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetAccessToken: {0}", data)
                });
              
            }
            catch (Exception ex) {
                Loggers.Exception(new ExceptionLogInfo()
                {
                    ErrorMessage = string.Format("GetAccessToken错误: {0}", ex.ToString())
                });
                Response.Write("向微信请求认证Access Token出错，请联系管理员尽快处理.");
                sendInfo.LogId = BaseService.NewGuidPub();
                sendInfo.IsSuccess = 0;
                sendInfo.MarketEventId = "向微信请求认证Access Token出错";
                sendInfo.SendTypeId = "200";
                sendInfo.Phone = iRad.ToString();
                sendInfo.TemplateContent = string.Format("GetAccessToken错误: {0}", ex.ToString());
                sendInfo.VipId = code;
                sendInfo.WeiXinUserId = "1111";
                sendInfo.CreateTime = System.DateTime.Now;
                sendServer.Create(sendInfo);
                Response.End();
            }
        }

        public class cAccessTokenReturn
        {
            public string access_token { get; set; }    //网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
            public string expires_in { get; set; }      //access_token接口调用凭证超时时间，单位（秒）
            public string refresh_token { get; set; }   //用户刷新access_token
            public string openid { get; set; }          //用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
            public string scope { get; set; }           //用户授权的作用域，使用逗号（,）分隔
        }

        #endregion

        #region 第三部 获取用户标识
        public void GetUserIdByOpenId(string OpenId)
        {
            try
            {
                Response.Write("<br/>");
                //Response.Write("获取用户标识");
                string vipId = string.Empty;
                VipBLL server = new VipBLL(loggingSessionInfo);
                var vipObjs = server.QueryByEntity(new VipEntity
                {
                    WeiXinUserId = OpenId
                }, null);
                if (vipObjs == null || vipObjs.Length == 0 )
                {
                    //请求获取用户信息
                    //Jermyn20130911 从总部导入vip信息
                    bool bReturn = server.GetVipInfoFromApByOpenId(OpenId, null);
                    var vipObjs1 = server.QueryByEntity(new VipEntity
                    {
                        WeiXinUserId = OpenId
                    }, null);
                    if (vipObjs1 == null || vipObjs1.Length == 0)
                    {
                        vipId = vipObjs1[0].VIPID;
                    }
                    else {
                        Response.Write("系统找不到你关注公众号的记录，请尝试取消关注公众号，然后重新关注该公众号！</br>给您带来不便，尽请谅解，谢谢！");
                        Response.End();
                    }
                }
                else
                {
                    vipId = vipObjs[0].VIPID;
                }
                Response.Write("vipId:"+ vipId);
                Response.Write("<br/>");
                //goUrl = "http://" + HttpUtility.UrlDecode(goUrl) + "?customerId=" + customerId + "&userId=" + vipId + "&openId=" + OpenId + "";
                goUrl = "http://" + HttpUtility.UrlDecode(goUrl) + "";
                if (goUrl.IndexOf("lj/register.html") > 0)
                {
                    goUrl = goUrl.Replace("lj/register.html", "HtmlApps/auth.html?pageName=Register&eventId=BFC41A8BF8564B6DB76AE8A8E43557BA");
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("[OAuthWX.aspx]url替换：old={0};new={1};", "lj/register.html?customerId=e703dbedadd943abacf864531decdac1", "HtmlApps/auth.html?pageName=Register&eventId=BFC41A8BF8564B6DB76AE8A8E43557BA")
                    });
                }
                string strGotoUrl = string.Empty;
                if (goUrl.IndexOf("Fuxing") > 0 || goUrl.IndexOf("HtmlApp") > 0)
                {
                    if (goUrl.IndexOf("?") > 0)
                    {
                        strGotoUrl = goUrl + "&customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "";//&backUrl=" + HttpUtility.UrlEncode(goUrl) + "
                    }
                    else
                    {
                        strGotoUrl = goUrl + "?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "";//&backUrl=" + HttpUtility.UrlEncode(goUrl) + "
                    }
                }
                else
                {
                    if (goUrl.IndexOf("20131217") > 0)
                    {
                        strGotoUrl = "/OnlineClothing20131217/go.htm?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "&backUrl=" + HttpUtility.UrlEncode(goUrl) + "";
                    }
                    else
                    {
                        strGotoUrl = "/OnlineClothing/go.htm?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "&backUrl=" + HttpUtility.UrlEncode(goUrl) + "";
                    }
                }
                    //Response.Write(goUrl);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetUserIdByOpenId: strGotoUrl = {0}", strGotoUrl)
                });
                Response.Redirect(strGotoUrl, false);
            }
            catch (Exception ex) {
                Loggers.Exception(new ExceptionLogInfo()
                {
                    ErrorMessage = string.Format("GetUserIdByOpenId用户用户信息出错: {0}", ex.ToString())
                });
              
                Response.Write("向o2omarketing平台请求用户信息出错，请联系管理员尽快处理.");
                Response.End();
            }
        }
        #endregion
    }
}