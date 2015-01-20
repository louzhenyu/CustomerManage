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
    public partial class OAuthWXTmp : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E";
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A";
        public string goUrl = string.Empty;
        public string goUrlTemp = ConfigurationManager.AppSettings["website_url2"];//
        public string strAppId = string.Empty;//"wxeebb52e0aa813101"
        public string strAppSecret = string.Empty; //"22ac924a92e6caf176d6ba426d744adb";
        public string strRedirectUri = ConfigurationManager.AppSettings["website_WWW"] + "OAuthWXTmp.aspx";
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
                        //state = ((((state).Replace("sb", ".")).Replace("555", "/")).Replace("666", ":")).Replace("sss", ",");

                        ////Response.Write("state:" + state);

                        //string[] array = state.Split(',');
                        //customerId = array[1];
                        //applicationId = array[2];
                        //goUrl = array[0];

                        byte[] buff1 = Convert.FromBase64String(state);
                        state = Encoding.UTF8.GetString(buff1);
                        state = HttpUtility.UrlDecode(state, Encoding.UTF8);
                        string[] array = state.Split(',');
                        customerId = array[0];
                        applicationId = array[1];
                        goUrl = array[2];
                        goUrl = goUrlTemp + goUrl;

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
                        Response.End();
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
                        //Response.Write("goUrl:" + goUrl);
                        //Response.End();
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
                    strState = customerId + "," + applicationId + "," + goUrl; //
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
            else
            {
                strAppId = info.AppID;
                strAppSecret = info.AppSecret;
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
                ////strState = CommonCompress.StringCompress(strState);
                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format("strState1: {0}", strState)
                //});
                string url = "http://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=snsapi_base&state=" + strState + "#wechat_redirect";
                //Response.Write(url);
                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetOAuthCode 出错: {0}", ex.ToString())
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
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                string url = "https://api.weixin.qq.com/sns/oauth2/access_token";
                WebClient myWebClient = new WebClient();
                // 注意这种拼字符串的ContentType
                myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                // 转化成二进制数组
                var postData = "appid=" + strAppId + "&secret=" + strAppSecret + "&code=" + code + "&grant_type=authorization_code";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // 上传数据，并获取返回的二进制数据.
                byte[] responseArray = myWebClient.UploadData(url, "POST", byteArray);
                var data = System.Text.Encoding.UTF8.GetString(responseArray);
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
                //Response.Write(data);
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetAccessToken错误: {0}", ex.ToString())
                });
                Response.Write("向微信请求认证Access Token出错，请联系管理员尽快处理.");
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
                if (vipObjs == null || vipObjs.Length == 0 || vipObjs[0] == null)
                {
                    //请求获取用户信息
                    //Jermyn20130911 从总部导入vip信息
                    bool bReturn = server.GetVipInfoFromApByOpenId(OpenId, null);
                    var vipObjs1 = server.QueryByEntity(new VipEntity
                    {
                        WeiXinUserId = OpenId
                    }, null);
                    if (vipObjs1 == null || vipObjs1.Length == 0 || vipObjs1[0] == null)
                    {
                        vipId = vipObjs1[0].VIPID;
                    }
                }
                else
                {
                    vipId = vipObjs[0].VIPID;
                }
                Response.Write("vipId:" + vipId);
                Response.Write("<br/>");
                //goUrl = "http://" + HttpUtility.UrlDecode(goUrl) + "?customerId=" + customerId + "&userId=" + vipId + "&openId=" + OpenId + "";
                goUrl = "http://" + HttpUtility.UrlDecode(goUrl) + "";
                string strGotoUrl = "/OnlineClothing/tmpGoUrl.html?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "&backUrl=" + HttpUtility.UrlEncode(goUrl) + "";
                //Response.Write(goUrl);
                Response.Redirect(strGotoUrl);
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetUserIdByOpenId用户用户信息出错: {0}", ex.ToString())
                });
                Response.Write("向o2omarketing平台请求用户信息出错，请联系管理员尽快处理.");
                Response.End();
            }
        }
        #endregion

    }
}