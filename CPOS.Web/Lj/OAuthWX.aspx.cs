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

namespace JIT.CPOS.Web.Lj
{
    public partial class OAuthWX : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E"; //
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A"; //
        public string goUrl = string.Empty;
        public string goUrlTemp = ConfigurationManager.AppSettings["website_url2"];//
        public string strAppId = string.Empty;//"wxeebb52e0aa813101"
        public string strAppSecret = string.Empty; //"22ac924a92e6caf176d6ba426d744adb";
        public string strRedirectUri = ConfigurationManager.AppSettings["website_WWW"] + "lj/OAuthWX.aspx";
        public string strState = string.Empty;
        public string strNoFollowUrl = ConfigurationManager.AppSettings["website_WWW"] + "lj/d20131114/VerificationNoFollow.html";//VerificationOK VerificationNoFollow
        public LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("进入认证界面<br/>");
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["state"]))
                {
                    #region 第二次请求该界面
                    string state = Request["state"];
                    try
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("strState2: {0}", state)
                        });
                        //state = ((((state).Replace("sb", ".")).Replace("555", "/")).Replace("666", ":")).Replace("sss", ",");
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
                        Response.Write("state错误:" + ex.ToString());
                    }
                    #endregion
                }
                else
                {
                    #region 第一次请求该界面，解析参数
                    //判断客户ID是否传递
                    if (!string.IsNullOrEmpty(Request["cId"]))
                    {
                        customerId = Request["cId"];
                    }
                    else
                    {
                        Response.Write("<br>");
                        Response.Write("没有获取客户标识");
                    }
                    if (!string.IsNullOrEmpty(Request["aId"]))
                    {
                        applicationId = Request["aId"];
                    }
                    else
                    {
                        Response.Write("<br>");
                        Response.Write("没有获取微信标识");
                    }
                    if (!string.IsNullOrEmpty(Request["goUrl"]))
                    {
                        goUrl = Request["goUrl"];
                        //goUrl = goUrl.Replace("/", "999999");
                        Response.Write("<br>");
                        Response.Write("goUrl:" + goUrl);
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
                //获取微信基本信息
                GetKeyByApp();
                try
                {
                    //微信认证 第一步：用户同意授权，获取code
                    string code = Request["code"];
                    if (code == null || code.Equals(""))
                    {
                        strState = customerId + "," + applicationId + "," + goUrl; //
                        strState = HttpUtility.UrlEncode(strState, Encoding.UTF8);
                        byte[] buff = Encoding.UTF8.GetBytes(strState);
                        strState = Convert.ToBase64String(buff);
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("压缩64位码: {0}", strState)
                        });
                        //第一步获取code
                        GetOAuthCode(strState);//strState
                    }
                    else
                    {
                        //第二部获取token
                        //Response.Write("存在code:" + code);
                        GetAccessToken(code);
                    }
                }
                catch (Exception ex) {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("OAuthWX初始化: {0}", ex.ToString())
                    });
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
            }
        }
        #endregion


        #region 第一步 获取code
        /// <summary>
        /// 获取code
        /// </summary>
        private void GetOAuthCode(string stateTmp)
        {
            try
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("stateTmp: {0}", stateTmp)
                });
                string url = "http://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=snsapi_base&state=" + stateTmp + "#wechat_redirect";
                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetOAuthCode 出错: {0}", ex.ToString())
                });
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
                    if (tokenInfo.openid == null || tokenInfo.openid.Equals(""))
                    {
                        Response.Redirect(strNoFollowUrl);
                    }
                    else
                    {
                        GetUserIdByOpenId(tokenInfo.openid);
                    }
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
                string status = "0"; 
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

                    }
                    else {
                        vipId = vipObjs1[0].VIPID;
                        status = vipObjs1[0].Status.ToString();
                    }
                }
                else
                {
                    vipId = vipObjs[0].VIPID;
                    status = vipObjs[0].Status.ToString();
                }
                //用户不存在或者取消关注，请处理
                if (vipId == null || vipId.Equals("") || status.Equals("0"))
                {
                    Response.Redirect(strNoFollowUrl);
                }
                Response.Write("vipId:" + vipId);
                Response.Write("<br/>");
                goUrl = "http://" + HttpUtility.UrlDecode(goUrl) + "?customerId=" + customerId + "&userId=" + vipId + "&openId=" + OpenId + "";
                //goUrl = "http://" + HttpUtility.UrlDecode(goUrl) + "";
                //string strGotoUrl = "/OnlineClothing/go.htm?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "&backUrl=" + HttpUtility.UrlEncode(goUrl) + "";
                //Response.Write(goUrl);
                Response.Redirect(goUrl);
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetUserIdByOpenId用户用户信息出错: {0}", ex.ToString())
                });
                Response.Write("GetUserIdByOpenId用户用户信息出错:" + ex.ToString());
            }
        }
        #endregion


    }
}