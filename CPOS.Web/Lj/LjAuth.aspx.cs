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
    public partial class LjAuth : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E"; //"e703dbedadd943abacf864531decdac1";//
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A"; //"386D08D106C849A9ACAA6E493D23E853";//
        public string goUrl = string.Empty;
        public string goUrlTemp = ConfigurationManager.AppSettings["website_url2"];//
        public string authCode = string.Empty;
        public string strAppId = string.Empty;//"wx8f74386d57405ec5"
        public string strAppSecret = string.Empty; //"2af3c935fc66e2087bff1064cde3a708";
        public string strRedirectUri = ConfigurationManager.AppSettings["website_WWW"] + "lj/LjAuth.aspx";
        public string strState = string.Empty;
        public string strNoFollowUrl = ConfigurationManager.AppSettings["website_WWW"] + "lj/d20131114/VerificationNoFollow.html";//VerificationOK VerificationNoFollow
        public LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("进入认证界面Lj<br/>");
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
                        authCode = array[3];

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
                        authCode = Request["autoCode"];
                        strState = customerId + "," + applicationId + "," + goUrl + "," + authCode; //
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
                catch (Exception ex)
                {
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
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("泸州老窖:APPID： {0}，AppSecret：{1}", strAppId, strAppSecret)
                });
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
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=snsapi_base&state=" + stateTmp + "#wechat_redirect";
                            //https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect

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
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                string url = "https://api.weixin.qq.com/sns/oauth2/access_token";
                            //https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
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
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetAccessToken: {0}", data)
                });
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
                    else
                    {
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
                string iType = CheckAuthCode(loggingSessionInfo, vipId, authCode, "");
                switch (iType){
                    case "1": // VerificationWaring.html 已被验证过
                        goUrlTemp = goUrlTemp + "/lj/d20131114/VerificationWaring.html";
                        break;
                    case "2": // VerificationOK.html  关注成功
                        goUrlTemp = goUrlTemp + "/lj/d20131114/VerificationOK.html";
                        break;
                    case "3":  // VerificationFail.html  未通过验证
                        goUrlTemp = goUrlTemp + "/lj/d20131114/VerificationFail.html";
                        break;
                    default:
                        goUrlTemp = goUrlTemp + "/lj/d20131114/VerificationFail.html";
                        break;
                }
                goUrl = "http://" + HttpUtility.UrlDecode(goUrlTemp) + "?customerId=" + customerId + "&userId=" + vipId + "&openId=" + OpenId + "";
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

        #region 商品认证
        /// <summary>
        /// 商品认证
        /// 
        /// 返回结果
        /// 1.	已验证过
        /// 2.	验证成功
        /// 3.	验证失败
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userId"></param>
        /// <param name="authCode"></param>
        /// <param name="captchaCode"></param>
        /// <returns></returns>
        public string CheckAuthCode(LoggingSessionInfo loggingSessionInfo, string userId, string authCode, string captchaCode)
        {
            string result = "";
            LItemAuthBLL lItemAuthBLL = new LItemAuthBLL(loggingSessionInfo);
            var itemAuthList = lItemAuthBLL.QueryByEntity(new LItemAuthEntity()
            {
                AuthCode = authCode
            }, null);
            LItemAuthEntity itemAuthObj = null;
            if (itemAuthList != null && itemAuthList.Length > 0)
            {
                itemAuthObj = itemAuthList[0];
                itemAuthObj.AuthCount += 1;
                lItemAuthBLL.Update(itemAuthObj, false);
                result = "1";
                return result;
            }
            else
            {
                SaturnServiceReference.SaturnServiceSoapClient client = new SaturnServiceReference.SaturnServiceSoapClient();
                var resultStr = client.getAuthenticationCode("{\"common\":{\"userId\":\"" + userId + "\",\"version\":\"3.1\"},\"special\":{\"authCode\":\"" + authCode + "\",\"captchaCode\":\"" + captchaCode + "\"}}");
                var authObj = resultStr.DeserializeJSONTo<CheckAuthCodeResp>();
                if (authObj.code == "200")
                {
                    if (authObj.content.isAuthCode == "1")
                    {
                        result = "2";
                    }
                    else
                    {
                        result = "3";
                    }
                    var newItemAuthObj = new LItemAuthEntity();
                    newItemAuthObj.ItemAuthId = Utils.NewGuid();
                    newItemAuthObj.AuthCode = authCode;
                    newItemAuthObj.CaptchaCode = captchaCode;
                    newItemAuthObj.ItemName = authObj.content.itemName;
                    newItemAuthObj.Norm = authObj.content.norm;
                    newItemAuthObj.Alcohol = authObj.content.alcohol;
                    newItemAuthObj.BaseWineYear = authObj.content.baseWineYear;
                    newItemAuthObj.AgePitPits = authObj.content.agePitPits;
                    newItemAuthObj.Barcode = authObj.content.barcode;
                    newItemAuthObj.IsAuthCode = ToInt(authObj.content.isAuthCode);
                    newItemAuthObj.CategoryName = authObj.content.categoryName;
                    newItemAuthObj.CategoryId = authObj.content.categoryId;
                    newItemAuthObj.BrandName = authObj.content.brandName;
                    newItemAuthObj.DealerName = authObj.content.dealerName;
                    newItemAuthObj.DealerId = authObj.content.dealerId;
                    newItemAuthObj.AuthCount = 1;
                    newItemAuthObj.StoreCode = authObj.content.storeCode;
                    newItemAuthObj.VipId = userId;

                    lItemAuthBLL.Create(newItemAuthObj);
                }
                else
                {
                    result = authObj.description;
                }
            }
            return result;
        }
        public class CheckAuthCodeResp
        {
            public string LItemAuthEntity;
            public string code;
            public string description;
            public CheckAuthCodeRespContent content;
        }
        public class CheckAuthCodeRespContent
        {
            public string itemName;
            public string norm;
            public string alcohol;
            public string baseWineYear;
            public string agePitPits;
            public string barcode;
            public string isAuthCode;
            public string categoryName;
            public string categoryId;
            public string brandName;
            public string dealerName;
            public string dealerId;
            public string storeCode;
            public CheckAuthCodeRespContentImage imageList;
        }
        public class CheckAuthCodeRespContentImage
        {
            public string imageURL;
            public string displayIndex;
        }

        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        #endregion
    }
}