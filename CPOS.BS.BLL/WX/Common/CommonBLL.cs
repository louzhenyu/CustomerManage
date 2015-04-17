using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using JIT.CPOS.BS.BLL.WX.Enum;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.DataAccess;
using JIT.Utility.Cache2;
using System.Text.RegularExpressions;

namespace JIT.CPOS.BS.BLL.WX
{
    /// <summary>
    /// 微信公共类
    /// </summary>
    public class CommonBLL
    {
        #region 构造函数

        public CommonBLL() { }

        #endregion

        #region 提交表单数据

        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="uri">提交数据的URI</param>
        /// <param name="method">GET, POST</param>
        /// <param name="content">提交内容</param>
        /// <returns></returns>
        public static string GetRemoteData(string uri, string method, string content)
        {
            string respData = "";
            method = method.ToUpper();
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = method.ToUpper();
            req.Credentials = System.Net.CredentialCache.DefaultCredentials;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            if (method == "POST")
            {
                //byte[] buffer = Encoding.ASCII.GetBytes(content);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);
                req.ContentLength = buffer.Length;
                req.ContentType = "application/x-www-form-urlencoded";
                Stream postStream = req.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);
                postStream.Close();
            }
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = System.Text.Encoding.GetEncoding("UTF-8");
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
            respData = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            resp.Close();
            return respData;
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        #endregion

        #region 验证token

        /// <summary>
        /// 验证token
        /// </summary>
        public void ValidToken(HttpContext httpContext, string token)
        {
            BaseService.WriteLogWeixin("开始执行token验证");

            if (httpContext.Request["echoStr"] != null)
            {
                var echostr = httpContext.Request["echoStr"].ToString();

                BaseService.WriteLogWeixin("echoStr = " + echostr);

                if (CheckSignature(httpContext, token) && !string.IsNullOrEmpty(echostr))
                {
                    BaseService.WriteLogWeixin("结束执行token验证");

                    //推送...不然微信平台无法验证token
                    httpContext.Response.Write(echostr);
                }
            }
            else
            {
                BaseService.WriteLogWeixin("echoStr is null");
            }
        }

        /// <summary>
        /// 加密/校验流程：
        /// 1. 将token、timestamp、nonce三个参数进行字典序排序
        /// 2. 将三个参数字符串拼接成一个字符串进行sha1加密
        /// 3. 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信
        /// </summary>
        /// <returns></returns>
        private bool CheckSignature(HttpContext httpContext, string token)
        {
            var signature = httpContext.Request["signature"].ToString();
            var timestamp = httpContext.Request["timestamp"].ToString();
            var nonce = httpContext.Request["nonce"].ToString();

            BaseService.WriteLogWeixin("token = " + token);
            BaseService.WriteLogWeixin("加密前的 signature = " + signature + "   \n");

            //字典排序
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);

            //sha1加密
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();

            BaseService.WriteLogWeixin("加密后的 signature = " + tmpStr + "   \n");

            if (tmpStr == signature)
            {
                BaseService.WriteLogWeixin("token验证成功");
                return true;
            }
            else
            {
                BaseService.WriteLogWeixin("token验证失败");
                return false;
            }
        }

        #endregion

        #region 获取凭证接口
        /// <summary>
        /// 在使用通用接口前，你需要做以下两步工作:
        /// 1.拥有一个微信公众账号，并获取到appid和appsecret
        /// 2.通过获取凭证接口获取到access_token
        /// access_token是第三方访问微信公众平台api资源的票据。
        /// </summary>
        /// <param name="appID">AppId</param>
        /// <param name="appSecret">AppSecret</param>
        /// <returns></returns>
        public AccessTokenEntity GetAccessTokenByCache(string appID, string appSecret, LoggingSessionInfo loggingSessionInfo)
        {
            MarketSendLogBLL logServer = new MarketSendLogBLL(loggingSessionInfo);
            MarketSendLogEntity logInfo = new MarketSendLogEntity();
            logInfo.LogId = BaseService.NewGuidPub();
            logInfo.VipId = appID;
            logInfo.MarketEventId = appSecret;
            logInfo.TemplateContent = loggingSessionInfo.CurrentUser.customer_id.ToString();
            logInfo.IsSuccess = 1;
            logInfo.SendTypeId = "2";
            logInfo.CreateTime = System.DateTime.Now;

            logServer.Create(logInfo);

            WApplicationInterfaceBLL wApplicationInterfaceBLL = new WApplicationInterfaceBLL(loggingSessionInfo);
            WApplicationInterfaceEntity appObj = null;
            var appList = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity
            {
                AppID = appID ,
                AppSecret = appSecret,
                CustomerId = loggingSessionInfo.CurrentUser.customer_id.ToString()
            }, null);
            //var appList = wApplicationInterfaceBLL.GetWebWApplicationInterface(new WApplicationInterfaceEntity() {
            //    AppID = appID,
            //    AppSecret = appSecret
            //}, 0, 1);
            ////WApplicationInterfaceEntity appObj = null;
            if (appList != null && appList.Length > 0)
            {
                if (appList[0].IsHeight == 0)
                {
                    //获取云店公众号信息 Add by Henry 
                    appList = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity
                    {
                        AppID = appID,
                        AppSecret = appSecret,
                        CustomerId = ConfigurationManager.AppSettings["CloudCustomerId"]
                    }, null);
                }
                appObj = appList[0];
            }
            else
            {
                //获取云店公众号信息 Add by Henry
                appList = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity
                {
                    AppID = appID,
                    AppSecret = appSecret,
                    CustomerId = ConfigurationManager.AppSettings["CloudCustomerId"]
                }, null);
                appObj = appList[0];
                //throw new Exception("未查询到公众号");
            }
            AccessTokenEntity accessToken = null;
            if (appObj.ExpirationTime == null || appObj.ExpirationTime <= DateTime.Now)
            {
                BaseService.WriteLogWeixin("获取凭证接口： ");
                BaseService.WriteLogWeixin("appID： " + appID);
                BaseService.WriteLogWeixin("appSecret： " + appSecret);
                string uri = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appID + "&secret=" + appSecret;
                string method = "GET";
                string data = GetRemoteData(uri, method, string.Empty);

                BaseService.WriteLogWeixin("调用获取凭证接口返回值： " + data);
                Loggers.Debug(new DebugLogInfo() { Message = "调用获取凭证接口返回值： " + data });

                accessToken = data.DeserializeJSONTo<AccessTokenEntity>();
                //AccessTokenEntity accessToken = new AccessTokenEntity();
                //accessToken.access_token = "jDsQzSF8o68i-YqVyNZUaorxpA4-EMhliWBi5Y1XKNuHB_bjGS3UYlwc_G5iHkv_FKdbheftp_FMZk1StB7gfSFkkjnKZGJP78fZ104DsSXw-6WzNl_Os_HnbEoonx9Sz2mcxSJMssZ02WndXZfedw";
                //accessToken.expires_in = "7200";

                appObj.RequestToken = accessToken.access_token;
                appObj.ExpirationTime = DateTime.Now.AddHours(1);
                wApplicationInterfaceBLL.Update(appObj, false);
            }
            else
            {
                accessToken = new AccessTokenEntity();
                accessToken.access_token = appObj.RequestToken;
                accessToken.expires_in = "7200";

                Loggers.Debug(new DebugLogInfo() { Message = "使用未过期的access token:" + appObj.RequestToken + ", 到期时间：" + appObj.ExpirationTime });
            }
            logInfo.LogId = BaseService.NewGuidPub();
            logInfo.VipId = appObj.AppID;
            logInfo.MarketEventId = appObj.AppSecret;
            logInfo.TemplateContent = appObj.RequestToken;
            logInfo.IsSuccess = 1;
            logInfo.SendTypeId = "2";
            logInfo.WeiXinUserId = appObj.WeiXinID;

            logServer.Create(logInfo);
            return accessToken;
        }

        #endregion

        #region 获取jsapi_ticket
        /// <summary>
        /// 在使用通用接口前，你需要做以下两步工作:
        /// 1.拥有一个微信公众账号，并获取到appid和appsecret
        /// 2.通过获取凭证接口获取到access_token
        /// access_token是第三方访问微信公众平台api资源的票据。
        /// </summary>
        /// <param name="appID">AppId</param>
        /// <param name="appSecret">AppSecret</param>
        /// <returns></returns>
        public JsApiTicketEntity GetJsApiTicketByCache(string appID, string appSecret, LoggingSessionInfo loggingSessionInfo)
        {
            MarketSendLogBLL logServer = new MarketSendLogBLL(loggingSessionInfo);
            MarketSendLogEntity logInfo = new MarketSendLogEntity();
            logInfo.LogId = BaseService.NewGuidPub();
            logInfo.VipId = appID;
            logInfo.MarketEventId = appSecret;
            logInfo.TemplateContent = loggingSessionInfo.CurrentUser.customer_id.ToString();
            logInfo.IsSuccess = 1;
            logInfo.SendTypeId = "2";
            logInfo.CreateTime = System.DateTime.Now;

            logServer.Create(logInfo);

            WApplicationInterfaceBLL wApplicationInterfaceBLL = new WApplicationInterfaceBLL(loggingSessionInfo);
            WApplicationInterfaceEntity appObj = null;
            var appList = wApplicationInterfaceBLL.QueryByEntity(new WApplicationInterfaceEntity
            {
                AppID = appID
                ,
                AppSecret = appSecret
                ,
                CustomerId = loggingSessionInfo.CurrentUser.customer_id.ToString()
            }, null);

            if (appList != null && appList.Length > 0)
            {
                appObj = appList[0];
            }
            else
            {
                throw new Exception("未查询到公众号");
            }
            JsApiTicketEntity jsApiTicket = null;
            if (appObj.TicketExpirationTime == null || appObj.TicketExpirationTime <= DateTime.Now)
            {
                BaseService.WriteLogWeixin("获取jsapi_ticket接口： ");
                BaseService.WriteLogWeixin("appID： " + appID);
                BaseService.WriteLogWeixin("appSecret： " + appSecret);

                AccessTokenEntity token = this.GetAccessTokenByCache(appID, appSecret, loggingSessionInfo);

                string uri = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token=" + token.access_token;
                string method = "GET";
                string data = GetRemoteData(uri, method, string.Empty);

                BaseService.WriteLogWeixin("调用获取jsapi_ticket接口返回值： " + data);
                Loggers.Debug(new DebugLogInfo() { Message = "调用获取jsapi_ticket接口返回值： " + data });

                jsApiTicket = data.DeserializeJSONTo<JsApiTicketEntity>();

                appObj.JsApiTicket = jsApiTicket.ticket;
                appObj.TicketExpirationTime = DateTime.Now.AddHours(1);
                wApplicationInterfaceBLL.Update(appObj, false);
            }
            else
            {
                jsApiTicket = new JsApiTicketEntity();
                jsApiTicket.ticket = appObj.JsApiTicket;
                jsApiTicket.expires_in = "7200";

                Loggers.Debug(new DebugLogInfo() { Message = "使用未过期的jsapi_ticket:" + appObj.JsApiTicket + ", 到期时间：" + appObj.TicketExpirationTime });
            }
            logInfo.LogId = BaseService.NewGuidPub();
            logInfo.VipId = appObj.AppID;
            logInfo.MarketEventId = appObj.AppSecret;
            logInfo.TemplateContent = appObj.JsApiTicket;
            logInfo.IsSuccess = 1;
            logInfo.SendTypeId = "2";
            logInfo.WeiXinUserId = appObj.WeiXinID;

            logServer.Create(logInfo);
            return jsApiTicket;
        }

        #endregion

        #region 创建自定义菜单

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public ResultEntity CreateMenu(LoggingSessionInfo loggingSessionInfo, string applicationId)
        {
            BaseService.WriteLogWeixin("创建自定义菜单");

            var appService = new WApplicationInterfaceBLL(loggingSessionInfo);
            var appEntity = appService.GetByID(applicationId);
            var result = new ResultEntity();

            //获取access_token
            var accessToken = this.GetAccessTokenByCache(appEntity.AppID, appEntity.AppSecret, loggingSessionInfo);

            if (accessToken.errcode == null || accessToken.errcode.Equals(string.Empty))
            {
                string content = string.Empty;
                string uri = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + accessToken.access_token);
                string method = "POST";

                #region 动态生成菜单项

                var menuDAO = new WMenuDAO(loggingSessionInfo);
                var dsFirst = menuDAO.GetFirstMenus(appEntity.WeiXinID);

                MenusEntity menusEntity = new MenusEntity();
                menusEntity.button = new List<MenuEntity>();
                if (dsFirst != null && dsFirst.Tables.Count > 0 && dsFirst.Tables[0].Rows.Count > 0)
                {
                    var button = "{\"button\":[";
                    foreach (DataRow dr in dsFirst.Tables[0].Rows)
                    {
                        MenuEntity menu = new MenuEntity();
                        menu.type = dr["type"].ToString();
                        menu.name = dr["name"].ToString();
                        menu.key = dr["key"].ToString();
                        menu.url = dr["menuURL"].ToString();

                        button += "{";
                        button += "\"type\": \"" + dr["type"].ToString() + "\",";
                        button += "\"name\": \"" + dr["name"].ToString() + "\",";
                        button += "\"key\": \"" + dr["key"].ToString() + "\",";
                        button += "\"url\": \"" + dr["menuURL"].ToString() + "\",";
                        button += "\"sub_button\": ";

                        var dsSecond = menuDAO.GetSecondMenus(appEntity.WeiXinID, dr["ID"].ToString());
                        if (dsSecond != null && dsSecond.Tables.Count > 0 && dsSecond.Tables[0].Rows.Count > 0)
                        {
                            var subButton = "[";
                            foreach (DataRow drSecond in dsSecond.Tables[0].Rows)
                            {
                                subButton += "{";
                                subButton += "\"type\": \"" + drSecond["type"].ToString() + "\",";
                                subButton += "\"name\": \"" + drSecond["name"].ToString() + "\",";
                                subButton += "\"key\": \"" + drSecond["key"].ToString() + "\",";
                                subButton += "\"url\": \"" + drSecond["menuURL"].ToString() + "\"";
                                subButton += "},";
                            }

                            if (dsSecond.Tables[0].Rows.Count > 0)
                            {
                                subButton = subButton.Substring(0, subButton.Length - 1);
                            }

                            subButton += "]";
                            button += subButton;
                            menu.sub_button = subButton;
                        }
                        else
                        {
                            button += "[]";
                            menu.sub_button = "[]";
                        }

                        button += "},";

                        menusEntity.button.Add(menu);
                    }

                    if (dsFirst.Tables[0].Rows.Count > 0)
                    {
                        button = button.Substring(0, button.Length - 1);
                    }

                    button += "]}";

                    content = button;
                }

                #endregion

                BaseService.WriteLogWeixin("content：" + content);

                string data = GetRemoteData(uri, method, content);
                BaseService.WriteLogWeixin("创建自定义菜单返回值： " + data);
                result = data.DeserializeJSONTo<ResultEntity>();
            }

            return result;
        }

        #endregion

        #region 回复文本消息

        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="weixinID">开发者微信号</param>
        /// <param name="openID">接收方帐号（收到的OpenID）</param>
        /// <param name="content">文本消息内容</param>
        public void ResponseTextMessage(string weixinID, string openID, string content, HttpContext httpContext)
        {
            var response = "<xml>";
            response += "<ToUserName><![CDATA[" + openID + "]]></ToUserName>";
            response += "<FromUserName><![CDATA[" + weixinID + "]]></FromUserName>";
            response += "<CreateTime>" + new BaseService().ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            response += "<MsgType><![CDATA[text]]></MsgType>";
            response += "<Content><![CDATA[" + content + "]]></Content> ";
            response += "<FuncFlag>0</FuncFlag>";
            response += "</xml>";

            BaseService.WriteLogWeixin("公众平台返回给用户的文本消息:  " + response);
            BaseService.WriteLogWeixin("回复文本消息结束-------------------------------------------\n");

            httpContext.Response.Write(response);
        }

        #endregion

        #region 回复图片消息

        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="weixinID">开发者微信号</param>
        /// <param name="openID">接收方帐号（收到的OpenID）</param>
        /// <param name="content">文本消息内容</param>
        public void ResponseImageMessage(string weixinID, string openID, string mediaID, HttpContext httpContext)
        {
            var response = "<xml>";
            response += "<ToUserName><![CDATA[" + openID + "]]></ToUserName>";
            response += "<FromUserName><![CDATA[" + weixinID + "]]></FromUserName>";
            response += "<CreateTime>" + new BaseService().ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            response += "<MsgType><![CDATA[image]]></MsgType>";
            response += "<Image>";
            response += "<MediaId><![CDATA[" + mediaID + "]]></MediaId>";
            response += "</Image>";
            response += "<FuncFlag>0</FuncFlag>";
            response += "</xml>";

            BaseService.WriteLogWeixin("公众平台返回给用户的图片消息:  " + response);
            BaseService.WriteLogWeixin("回复图片消息结束-------------------------------------------\n");

            httpContext.Response.Write(response);
        }

        #endregion

        #region 回复图文消息

        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="weixinID">开发者微信号</param>
        /// <param name="openID">接收方帐号（收到的OpenID）</param>
        /// <param name="newsList">图文消息实体类集合</param>
        public void ResponseNewsMessage(string weixinID, string openID, List<WMaterialTextEntity> newsList, HttpContext httpContext)
        {
            if (newsList != null && newsList.Count > 0)
            {
                var response = "<xml>";
                response += "<ToUserName><![CDATA[" + openID + "]]></ToUserName>";
                response += "<FromUserName><![CDATA[" + weixinID + "]]></FromUserName>";
                response += "<CreateTime>" + new BaseService().ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
                response += "<MsgType><![CDATA[news]]></MsgType>";
                response += "<ArticleCount>" + newsList.Count + "</ArticleCount>";
                response += "<Articles>";

                foreach (var item in newsList)
                {
                    response += "<item>";
                    response += "<Title><![CDATA[" + item.Title + "]]></Title> ";
                    response += "<Description><![CDATA[" + item.Text + "]]></Description>";
                    response += "<PicUrl><![CDATA[" + item.CoverImageUrl + "]]></PicUrl>";
                    response += "<Url><![CDATA[" + item.OriginalUrl + "]]></Url>";
                    response += "</item>";
                }

                response += "</Articles>";
                response += "<FuncFlag>1</FuncFlag>";
                response += "</xml>";

                BaseService.WriteLogWeixin("公众平台返回给用户的图文消息:  " + response);
                BaseService.WriteLogWeixin("回复图文消息结束-------------------------------------------\n");

                httpContext.Response.Write(response);
            }
        }

        #endregion

        #region 保存用户信息

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="openID">发送方帐号（一个OpenID）</param>
        /// <param name="weixinID">开发者微信号</param>
        /// <param name="isShow">1： 关注  0： 取消关注</param>
        public void SaveUserInfo(string openID, string weixinID, string isShow, string appId, string appSecret, string qrcodeId, LoggingSessionInfo loggingSessionInfo)
        {
            //获取调用微信接口的凭证
            var accessToken = GetAccessTokenByCache(appId, appSecret, loggingSessionInfo);

            if (accessToken.errcode == null || accessToken.errcode.Equals(string.Empty))
            {
                //通过openID获取用户信息
                var userInfo = GetUserInfo(accessToken.access_token, openID);

                if (userInfo.errcode == null || userInfo.errcode.Equals(string.Empty))
                {
                    //记录用户信息
                    BaseService.WriteLogWeixin("userInfo.subscribe:  " + userInfo.subscribe);
                    BaseService.WriteLogWeixin("userInfo.openid:  " + userInfo.openid);
                    BaseService.WriteLogWeixin("userInfo.nickname:  " + userInfo.nickname);
                    BaseService.WriteLogWeixin("userInfo.sex:  " + userInfo.sex);
                    BaseService.WriteLogWeixin("userInfo.city:  " + userInfo.city);
                    BaseService.WriteLogWeixin("userInfo.language:  " + userInfo.language);
                    BaseService.WriteLogWeixin("userInfo.headimgurl:  " + userInfo.headimgurl);

                    string webUrl = ConfigurationManager.AppSettings["website_url3"];
                    var qrcode = webUrl + "/Member.aspx?weixin_id=" + weixinID + "&open_id=" + openID;

                    webUrl = ConfigurationManager.AppSettings["website_WWW"];

                    string uri = webUrl + "/weixin/data.aspx?datatype=SignIn";//调用用户关注事件
                    uri += "&openID=" + HttpUtility.UrlEncode(openID);
                    uri += "&weixin_id=" + HttpUtility.UrlEncode(weixinID);
                    uri += "&gender=" + (string.IsNullOrEmpty(userInfo.sex) ? "0" : HttpUtility.UrlEncode(userInfo.sex));
                    uri += "&city=" + (string.IsNullOrEmpty(userInfo.city) ? "0" : HttpUtility.UrlEncode(userInfo.city));
                    uri += "&vipName=" + (string.IsNullOrEmpty(userInfo.nickname) ? "0" : HttpUtility.UrlEncode(userInfo.nickname));
                    uri += "&headimgurl=" + HttpUtility.UrlEncode(userInfo.headimgurl);
                    uri += "&isShow=" + HttpUtility.UrlEncode(isShow);
                    uri += "&qrcode=" + HttpUtility.UrlEncode(qrcode);
                    uri += "&qrcode_id=" + HttpUtility.UrlEncode(qrcodeId);
                    string method = "GET";
                    string data = CommonBLL.GetRemoteData(uri, method, string.Empty);

                    BaseService.WriteLogWeixin("uri:  " + uri);
                    BaseService.WriteLogWeixin("调用保存用户信息接口返回值:  " + data);
                }
                else
                {
                    BaseService.WriteLogWeixin("userInfo.errcode:  " + userInfo.errcode);
                    BaseService.WriteLogWeixin("userInfo.errmsg:  " + userInfo.errmsg);
                }
            }
            else
            {
                BaseService.WriteLogWeixin("accessToken.errcode:  " + accessToken.errcode);
                BaseService.WriteLogWeixin("accessToken.errmsg:  " + accessToken.errmsg);
            }
        }

        #endregion

        #region 通过工作平台给会员推送文本消息并保存日志，仅限48小时内有互动的会员
        //add by Willie Yan 2014-01-09
        public static string SendWeixinMessage(string message, string fromVipId, LoggingSessionInfo loggingSessionInfo, VipEntity vip)
        {
            string code = "";
            JIT.CPOS.BS.BLL.WX.CommonBLL commonService;
            WUserMessageBLL wUserMessageBLL;
            WUserMessageEntity queryObj;

            Loggers.Debug(new DebugLogInfo() { Message = "loggingSessionInfo: " + loggingSessionInfo.ToJSON() });

            //保存消息日志
            commonService = new JIT.CPOS.BS.BLL.WX.CommonBLL();
            wUserMessageBLL = new WUserMessageBLL(loggingSessionInfo);

            queryObj = new WUserMessageEntity();
            queryObj.MessageId = JIT.CPOS.Common.Utils.NewGuid();
            queryObj.VipId = vip.VIPID;
            queryObj.Text = message;
            queryObj.OpenId = vip.WeiXinUserId;
            queryObj.DataFrom = 2;
            queryObj.CreateTime = DateTime.Now;
            queryObj.CreateBy = fromVipId;
            queryObj.LastUpdateBy = fromVipId;
            queryObj.LastUpdateTime = DateTime.Now;
            queryObj.MaterialTypeId = "1";
            queryObj.IsDelete = 0;

            wUserMessageBLL.Create(queryObj);

            string appID = "";
            string appSecret = "";
            //获取appid, appsecret
            WApplicationInterfaceBLL waServer = new WApplicationInterfaceBLL(loggingSessionInfo);
            var waObj = waServer.QueryByEntity(new WApplicationInterfaceEntity
            {
                WeiXinID = vip.WeiXin
            }, null);
            if (waObj == null || waObj.Length == 0 || waObj[0] == null)
            {
                code = "103";
            }
            else
            {
                appID = waObj[0].AppID;
                appSecret = waObj[0].AppSecret;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("appID:{0}, appSecret, {1}", appID, appSecret)
                });

                //发送消息
                SendMessageEntity sendInfo = new SendMessageEntity();
                sendInfo.msgtype = "text";
                sendInfo.touser = vip.WeiXinUserId;
                //sendInfo.articles = newsList;
                sendInfo.content = message;

                JIT.CPOS.BS.Entity.WX.ResultEntity msgResultObj = new JIT.CPOS.BS.Entity.WX.ResultEntity();
                msgResultObj = commonService.SendMessage(sendInfo, appID, appSecret, loggingSessionInfo);

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult:{0}", msgResultObj)
                });

                //更新消息日志
                if (msgResultObj != null)
                {
                    queryObj.IsPushWX = 1;
                    queryObj.PushWXTime = DateTime.Now;
                    if (msgResultObj.errcode == "0")
                    {
                        queryObj.IsPushSuccess = 1;
                    }
                    else
                    {
                        queryObj.IsPushSuccess = 0;
                        queryObj.FailureReason = msgResultObj.ToJSON();
                    }
                    wUserMessageBLL.Update(queryObj, false);

                    code = "200";
                }
                else
                {
                    code = "203";
                }
            }

            return code;
        }
        #endregion

        #region 高级账号功能

        #region 获取用户信息接口

        /// <summary>
        /// 第三方通过openid获取用户信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openID">普通用户的标识，对当前公众号唯一</param>
        /// <returns></returns>
        public UserInfoEntity GetUserInfo(string accessToken, string openID)
        {
            string uri = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + accessToken + "&openid=" + openID;
            string method = "GET";
            string data = GetRemoteData(uri, method, string.Empty);

            var userInfo = data.DeserializeJSONTo<UserInfoEntity>();
            return userInfo;
        }

        #endregion

        #region 主动推送消息接口

        /// <summary>
        /// 主动推送消息
        /// </summary>
        /// <param name="sendMessage">发送消息实体</param>
        /// <param name="appID"></param>
        /// <param name="appSecret"></param>
        /// <param name="isCustomMsg">
        /// 是否为客服消息  true：发送客服消息(默认值)  false：发送不受限制的消息
        /// </param>
        /// <returns></returns>
        public ResultEntity SendMessage(SendMessageEntity sendMessage, string appID, string appSecret, LoggingSessionInfo loggingSessionInfo, bool isCustomMsg = true)
        {
            var result = new ResultEntity();
            var accessToken = this.GetAccessTokenByCache(appID, appSecret, loggingSessionInfo);

            if (accessToken.errcode == null || accessToken.errcode.Equals(string.Empty))
            {
                string uri = string.Empty;
                string method = "POST";
                string content = string.Empty;
                if (isCustomMsg)
                {
                    uri = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + accessToken.access_token;
                }
                else
                {
                    uri = "https://api.weixin.qq.com/cgi-bin/message/send?access_token=" + accessToken.access_token;
                }

                switch (sendMessage.msgtype.ToLower())
                {
                    case "image": break;    //图片消息
                    case "voice": break;    //语音信息
                    case "video": break;    //视频信息
                    case "music": break;    //音乐信息
                    case "news":    //图文信息
                        content = "{\"touser\":\"" + sendMessage.touser + "\",\"msgtype\":\"news\",\"news\":{\"articles\": [";
                        foreach (var news in sendMessage.articles)
                        {
                            content += "{";
                            content += "\"title\": \"" + news.title + "\",";
                            content += "\"description\": \"" + news.description + "\",";
                            content += "\"url\": \"" + news.url + "\",";
                            content += "\"picurl\": \"" + news.picurl + "\"";
                            content += "},";
                        }
                        content += "]}}";
                        break;
                    default:    //默认发送文本消息
                        content = "{\"touser\":\"" + sendMessage.touser + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + sendMessage.content + "\"}}";
                        break;
                }

                string data = GetRemoteData(uri, method, content);
                result = data.DeserializeJSONTo<ResultEntity>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("中欧扫描--关注SendMessage 进入:data:{0},content {1}", data, content)
                });
            }

            return result;
        }

        #endregion

        #region 主动推送模板消息接口
        /// <summary>
        /// 主动推送模板消息接口
        /// </summary>
        /// <param name="sendMessage">发送消息实体</param>
        /// <param name="accessToken">调用微信公众平台接口的凭证</param>
        /// <returns></returns>
        public string SendTemplateMessage(string weixinID, string message)
        {
            string data = string.Empty;
            LoggingSessionInfo loggingSessionInfo = BaseService.GetWeixinLoggingSession(weixinID);

            var appService = new WApplicationInterfaceBLL(loggingSessionInfo);
            var appList = appService.QueryByEntity(new WApplicationInterfaceEntity { WeiXinID = weixinID }, null);

            if (appList != null && appList.Length > 0)
            {
                var appEntity = appList.FirstOrDefault();
                //获取access_token
                var accessToken = this.GetAccessTokenByCache(appEntity.AppID, appEntity.AppSecret, loggingSessionInfo);
                string uri = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken.access_token;
                string method = "POST";
                string content = message;
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("发送模板消息使用的token:{0}", accessToken.access_token) });
                data = GetRemoteData(uri, method, content);
            }

            return data;
        }

        #endregion

        #region 媒体文件上传接口

        /// <summary>
        /// 媒体文件上传接口
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="mediaUri">媒体文件的 URI</param>
        /// <param name="mediaType">媒体文件类型</param>
        /// <returns>上传成功后，返回mediaID</returns>
        public UploadMediaEntity UploadMediaFile(string accessToken, string mediaUri, MediaType mediaType)
        {
            BaseService.WriteLogWeixin("开始调用媒体文件上传接口UploadMediaFile");

            //下载媒体文件
            string filePath = DownloadFile(mediaUri);

            if (!string.IsNullOrEmpty(filePath))
            {
                //上传图片
                string uriString = "http://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + accessToken + "&type=" + mediaType.ToString().ToLower();

                //创建一个新的 WebClient 实例.
                WebClient myWebClient = new WebClient();

                //直接上传，并获取返回的二进制数据
                byte[] responseArray = myWebClient.UploadFile(uriString, "POST", filePath);
                var data = System.Text.Encoding.Default.GetString(responseArray);
                BaseService.WriteLogWeixin("调用微信平台媒体上传接口返回值： " + data);

                var media = data.DeserializeJSONTo<UploadMediaEntity>();
                return media;
            }
            else
            {
                BaseService.WriteLogWeixin("下载失败，请检查媒体文件的URI是否正确");
                return new UploadMediaEntity() { errcode = "400", errmsg = "下载失败，请检查媒体文件的URI是否正确" };
            }
        }

        /// <summary>
        /// 将具有指定 URI 的资源下载到本地文件。
        /// </summary>
        /// <param name="address">从中下载数据的 URI。</param>
        /// <returns>本地文件保存路径</returns>
        public string DownloadFile(string address)
        {
            BaseService.WriteLogWeixin("将具有指定 URI 的资源下载到本地文件");
            BaseService.WriteLogWeixin("文件URI： " + address);

            try
            {
                WebClient webClient = new WebClient();

                //创建下载根文件夹
                var dirPath = @"C:\DownloadFile\";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //根据年月日创建下载子文件夹
                var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + @"\";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //下载到本地文件
                var fileExt = Path.GetExtension(address).ToLower();
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                var filePath = dirPath + newFileName;

                webClient.DownloadFile(address, filePath);

                BaseService.WriteLogWeixin("文件下载成功！");
                BaseService.WriteLogWeixin("文件保存路径： " + filePath);

                return filePath;
            }
            catch (Exception ex)
            {
                BaseService.WriteLogWeixin("图片下载异常信息：  " + ex.Message);
                return string.Empty;
            }
        }

        #endregion

        #region 获取二维码图片地址

        /// <summary>
        /// 获取二维码图片地址
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="type">二维码类型  0： 临时二维码  1：永久二维码</param>
        /// <param name="sceneId">场景值ID，临时二维码时为32位整型，永久二维码时最大值为1000</param>
        /// <returns></returns>
        public string GetQrcodeUrl(string appId, string appSecret, string type, int sceneId, LoggingSessionInfo loggingSessionInfo)
        {
            BaseService.WriteLogWeixin("获取二维码图片地址");
            var qrcodeUrl = string.Empty;

            //获取access_token
            var accessToken = this.GetAccessTokenByCache(appId, appSecret, loggingSessionInfo);

            if (accessToken.errcode == null || accessToken.errcode.Equals(string.Empty))
            {
                string uri = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + accessToken.access_token;
                string method = "POST";
                string content = string.Empty;

                if (type.Equals("0"))
                {
                    content = "{\"expire_seconds\": 1800, \"action_name\": \"QR_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + sceneId + "}}}";
                }
                else if (type.Equals("1"))
                {
                    content = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + sceneId + "}}}";
                }

                string data = GetRemoteData(uri, method, content);

                if (data.IndexOf("40001") > -1 && data.ToLower().IndexOf("invalid credential") > -1)
                    Loggers.Debug(new DebugLogInfo() { Message = "获取二维码失败，40001:invalid credential, accessToken.access_token=" + accessToken.access_token });

                var qrcode = data.DeserializeJSONTo<QrCodeEntity>();

                BaseService.WriteLogWeixin("获取二维码图片返回值：" + data);

                if (string.IsNullOrEmpty(qrcode.errcode))
                {
                    qrcodeUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + qrcode.ticket;
                }
                else
                {
                    throw new Exception(qrcode.errcode + ":" + qrcode.errmsg);
                }
            }

            BaseService.WriteLogWeixin("qrcodeUrl：" + qrcodeUrl);

            return qrcodeUrl;
        }

        #endregion

        #region 导入微信公众账号用户信息

        /// <summary>
        /// 导入微信公众账号用户信息
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="weixinId"></param>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="vipCode">当前VIP表vipCode的最大值</param>
        /// <returns></returns>
        public bool ImportUserInfo(string appId, string appSecret, string weixinId, LoggingSessionInfo loggingSessionInfo, int vipCode)
        {
            try
            {
                BaseService.WriteLogWeixin("导入微信公众账号用户信息:  ImportUserInfo()");
                var accessToken = GetAccessTokenByCache(appId, appSecret, loggingSessionInfo);
                string uri = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + accessToken.access_token;
                string method = "GET";
                string data = GetRemoteData(uri, method, string.Empty);
                BaseService.WriteLogWeixin("data:  " + data);

                var entity = data.DeserializeJSONTo<GetUserInfoEntity>();

                if (entity != null)
                {
                    //获取所有用户openId集合
                    if (entity.count == "10000")
                    {
                        //数量大于1万时，递归调用
                        AddUserInfo(entity, accessToken.access_token);
                    }
                    else
                    {
                        userList.Add(entity);
                    }

                    BaseService.WriteLogWeixin("请求微信，获取用户信息:  ");
                    DateTime startTime = DateTime.Now;
                    BaseService.WriteLogWeixin("开始时间:  " + startTime);

                    List<UserInfoEntity> userInfoList = new List<UserInfoEntity>();
                    //通过openId获取用户信息
                    if (userList.Count > 0)
                    {
                        UserInfoEntity userInfo = new UserInfoEntity();
                        foreach (var user in userList)
                        {
                            if (user.data != null)
                            {
                                foreach (var id in user.data.openid)
                                {
                                    uri = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + accessToken.access_token + "&openid=" + id;
                                    method = "GET";
                                    data = GetRemoteData(uri, method, string.Empty);

                                    userInfo = data.DeserializeJSONTo<UserInfoEntity>();

                                    userInfoList.Add(userInfo);
                                }
                            }
                        }
                    }

                    DateTime endTime = DateTime.Now;
                    BaseService.WriteLogWeixin("结束时间:  " + endTime);
                    BaseService.WriteLogWeixin("总共耗时:  " + (endTime - startTime));

                    //将用户信息写入本地数据库
                    BaseService.WriteLogWeixin("将用户信息写入本地数据库");
                    startTime = DateTime.Now;
                    BaseService.WriteLogWeixin("开始时间:  " + startTime);

                    string sql = string.Empty;

                    int count = vipCode;
                    string code = "Vip";
                    switch (vipCode.ToString().Length)
                    {
                        case 1: code += "0000000"; break;
                        case 2: code += "000000"; break;
                        case 3: code += "00000"; break;
                        case 4: code += "0000"; break;
                        case 5: code += "000"; break;
                        case 6: code += "00"; break;
                        case 7: code += "0"; break;
                    }

                    foreach (var item in userInfoList)
                    {
                        var nickname = item.nickname.Replace("'", "''");
                        var city = item.country + " " + item.province + " " + item.city;
                        var tempCode = code + count;
                        count++;

                        sql += " INSERT INTO dbo.Vip( ";
                        sql += " VIPID ,VipName ,VipLevel ,VipCode , ";
                        sql += " WeiXin ,WeiXinUserId ,Gender ,Status , ";
                        sql += " VipSourceId ,ClientID ,CreateTime ,CreateBy , ";
                        sql += " LastUpdateTime ,LastUpdateBy ,IsDelete ,City ,HeadImgUrl) ";
                        sql += " VALUES  ( ";
                        sql += " REPLACE(NEWID(),'-','') , '" + item.nickname + "', 1, '" + tempCode + "', ";
                        sql += " '" + weixinId + "','" + item.openid + "','" + item.sex + "','1', ";
                        sql += " '3','86a575e616044da3ac2c3ab492e44445', GETDATE(), '1', ";
                        sql += " GETDATE(), '1', 0, '" + city + "','" + item.headimgurl + "' ";
                        sql += " ) ";
                    }

                    DefaultSQLHelper sqlHelper = new DefaultSQLHelper(loggingSessionInfo.Conn);

                    BaseService.WriteLogWeixin("sql:  " + sql);
                    var result = sqlHelper.ExecuteScalar(sql);
                    BaseService.WriteLogWeixin("result:  " + result);

                    endTime = DateTime.Now;
                    BaseService.WriteLogWeixin("结束时间:  " + endTime);
                    BaseService.WriteLogWeixin("总共耗时:  " + (endTime - startTime));

                    var tmp = string.Empty;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        List<GetUserInfoEntity> userList = new List<GetUserInfoEntity>();
        private void AddUserInfo(GetUserInfoEntity entity, string accessToken)
        {
            if (entity != null)
            {
                if (entity.count == "10000")
                {
                    var uri = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + accessToken + "&next_openid=" + entity.next_openid;
                    var method = "GET";
                    var data = GetRemoteData(uri, method, string.Empty);
                    BaseService.WriteLogWeixin("data:  " + data);

                    var tmpEntity = data.DeserializeJSONTo<GetUserInfoEntity>();

                    //递归调用
                    AddUserInfo(tmpEntity, accessToken);

                    userList.Add(entity);
                }
                else
                {
                    userList.Add(entity);
                }
            }
        }

        /// <summary>
        /// 用户集合
        /// </summary>
        private class GetUserInfoEntity
        {
            /// <summary>
            /// 关注该公众账号的总用户数
            /// </summary>
            public string total { get; set; }
            /// <summary>
            /// 拉取的OPENID个数，最大值为10000
            /// </summary>
            public string count { get; set; }
            /// <summary>
            /// 列表数据，OPENID的列表
            /// </summary>
            public UserEntity data { get; set; }
            /// <summary>
            /// 拉取列表的后一个用户的OPENID
            /// </summary>
            public string next_openid { get; set; }
        }

        private class UserEntity
        {
            public List<string> openid { get; set; }
        }

        #endregion


        #region New导入微信公众号用户信息
        public bool NewImportUserInfo(string appId, string appSecret, string weixinId, LoggingSessionInfo loggingSessionInfo, int vipCode)
        {

            try
            {
                BaseService.WriteLogWeixin("导入微信公众账号用户信息:  ImportUserInfo()");

                var accessToken = GetAccessTokenByCache(appId, appSecret, loggingSessionInfo);
                string uri = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + accessToken.access_token;
                string method = "GET";
                string data = GetRemoteData(uri, method, string.Empty);
                BaseService.WriteLogWeixin("data:  " + data);

                var entity = data.DeserializeJSONTo<GetUserInfoEntity>();

                if (entity != null)
                {
                    //获取所有用户openId集合
                    if (entity.count == "10000")
                    {
                        //数量大于1万时，递归调用
                        NewAddUserInfo(entity, accessToken.access_token);
                    }
                    else
                    {
                        newuserList.Add(entity);
                    }

                    BaseService.WriteLogWeixin("请求微信，获取用户信息:  ");
                    DateTime startTime = DateTime.Now;
                    BaseService.WriteLogWeixin("开始时间:  " + startTime);

                    List<UserInfoEntity> userInfoList = new List<UserInfoEntity>();
                    //通过openId获取用户信息
                    if (newuserList.Count > 0)
                    {
                        UserInfoEntity userInfo = new UserInfoEntity();
                        foreach (var user in newuserList)
                        {
                            if (user.data != null)
                            {
                                foreach (var id in user.data.openid)
                                {
                                    uri = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + accessToken.access_token + "&openid=" + id;
                                    method = "GET";
                                    data = GetRemoteData(uri, method, string.Empty);
                                    userInfo = data.DeserializeJSONTo<UserInfoEntity>();

                                    userInfoList.Add(userInfo);
                                }
                            }
                        }
                    }

                    DateTime endTime = DateTime.Now;
                    BaseService.WriteLogWeixin("结束时间:  " + endTime);
                    BaseService.WriteLogWeixin("总共耗时:  " + (endTime - startTime));

                    //将用户信息写入本地数据库
                    BaseService.WriteLogWeixin("将用户信息写入本地数据库");
                    startTime = DateTime.Now;
                    BaseService.WriteLogWeixin("开始时间:  " + startTime);

                    string sql = string.Empty;

                    int count = vipCode;
                    string code = "Vip";
                    //switch (vipCode.ToString().Length)
                    //{
                    //    case 1: code += "0000000"; break;
                    //    case 2: code += "000000"; break;
                    //    case 3: code += "00000"; break;
                    //    case 4: code += "0000"; break;
                    //    case 5: code += "000"; break;
                    //    case 6: code += "00"; break;
                    //    case 7: code += "0"; break;
                    //}
                    bool bl = Replace(loggingSessionInfo, userInfoList, code, vipCode,weixinId);

                    // BaseService.WriteLogWeixin("result:  " + result);
                    //   DataTable dt = ConvertListToDatatTable(userInfoList);

                    //  InsertDataBase(loggingSessionInfo, dt);

                    //foreach (var item in userInfoList)
                    //{
                    //    var nickname = item.nickname.Replace("'", "''");
                    //    var city = item.country + " " + item.province + " " + item.city;
                    //    var tempCode = code + count;
                    //    count++;

                    //    sql += " INSERT INTO dbo.Vip( ";
                    //    sql += " VIPID ,VipName ,VipLevel ,VipCode , ";
                    //    sql += " WeiXin ,WeiXinUserId ,Gender ,Status , ";
                    //    sql += " VipSourceId ,ClientID ,CreateTime ,CreateBy , ";
                    //    sql += " LastUpdateTime ,LastUpdateBy ,IsDelete ,City ,HeadImgUrl) ";
                    //    sql += " VALUES  ( ";
                    //    sql += " REPLACE(NEWID(),'-','') , '" + item.nickname + "', 1, '" + tempCode + "', ";
                    //    sql += " '" + weixinId + "','" + item.openid + "','" + item.sex + "','1', ";
                    //    sql += " '3','86a575e616044da3ac2c3ab492e44445', GETDATE(), '1', ";
                    //    sql += " GETDATE(), '1', 0, '" + city + "','" + item.headimgurl + "' ";
                    //    sql += " ) ";
                    //    DefaultSQLHelper sqlHelper = new DefaultSQLHelper(loggingSessionInfo.Conn);
                    //    var result = sqlHelper.ExecuteScalar(sql);
                    //}

                    //
                    //BaseService.WriteLogWeixin("sql:  " + sql);
                    //
                    endTime = DateTime.Now;
                    BaseService.WriteLogWeixin("结束时间:  " + endTime);
                    BaseService.WriteLogWeixin("总共耗时:  " + (endTime - startTime));

                    var tmp = string.Empty;
                }

                return true;
            }
            catch
            {
                return false;
            }



        }
        List<GetUserInfoEntity> newuserList = new List<GetUserInfoEntity>();
        private void NewAddUserInfo(GetUserInfoEntity entity, string accessToken)
        {
            if (entity != null)
            {
                if (entity.count == "10000")
                {
                    var uri = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + accessToken + "&next_openid=" + entity.next_openid;
                    var method = "GET";
                    var data = GetRemoteData(uri, method, string.Empty);
                    BaseService.WriteLogWeixin("data:  " + data);

                    var tmpEntity = data.DeserializeJSONTo<GetUserInfoEntity>();

                    //递归调用
                    NewAddUserInfo(tmpEntity, accessToken);

                    newuserList.Add(entity);
                }
                else
                {
                    newuserList.Add(entity);
                }
            }
        }

        #region 处理数据
        private static bool Replace(LoggingSessionInfo loggingSessionInfo, List<UserInfoEntity> data, string Code, int vipCode, string weixinId)
        {
            VipBLL bll = new VipBLL(loggingSessionInfo);
            try
            {
                List<UserInfoEntity> list = data;
                string BatNo = Guid.NewGuid().ToString().Replace("-", "");
                CPOS.Common.DownloadImage downloadServer = new JIT.CPOS.Common.DownloadImage();
                foreach (UserInfoEntity item in list)
                {
                    var tempCode = Code + vipCode;
                    vipCode++;
                    item.BatNo = BatNo;
                    item.VipCode = tempCode;
                    item.WeXin = weixinId;

                    string downloadImageUrl = ConfigurationManager.AppSettings["website_WWW"];

                    if (!string.IsNullOrEmpty(item.headimgurl))
                    {
                        item.headimgurl = downloadServer.DownloadFile(item.headimgurl, downloadImageUrl);   //处理图片

                    }
                    item.IsDelete = "0";
                    item.nickname = ReplaceStr(item.nickname);
                    item.VipId = Guid.NewGuid().ToString().Replace("-", "");
                    item.CustomerId = loggingSessionInfo.ClientID;
                   // item.pa
                    bll.AddVipWXDownload(item);
                }
                int result = bll.WXToVip(BatNo);
                BaseService.WriteLogWeixin("导入数据记录: " + result);
                return true;
            }
            catch (Exception ex)
            {
                BaseService.WriteLogWeixin("导入失败");
                return false;
            }
        }
        #endregion

        #region 将微信信息导入数据库
        private static void InsertDataBase(LoggingSessionInfo loggingSessionInfo, DataTable dt)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(loggingSessionInfo.Conn))
            {
                conn.Open();
                System.Data.SqlClient.SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                using (System.Data.SqlClient.SqlBulkCopy copy = new System.Data.SqlClient.SqlBulkCopy(conn, System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints, sqlbulkTransaction))
                {
                    copy.DestinationTableName = "VipWXDownload";
                    copy.BulkCopyTimeout = 100;
                    copy.ColumnMappings.Add("VipId", "VipId");
                    copy.ColumnMappings.Add("VipCode", "VipCode");
                    copy.ColumnMappings.Add("nickname", "VipCode");
                    copy.ColumnMappings.Add("openid", "OpenId");
                    copy.ColumnMappings.Add("openid", "WeiXin");
                    copy.ColumnMappings.Add("sex", "Gender");
                    copy.ColumnMappings.Add("city", "City");
                    //  copy.ColumnMappings.Add("city", "Age");
                    copy.ColumnMappings.Add("BatNo", "BatNo");
                    copy.ColumnMappings.Add("headimgurl", "HeadImgUrl");
                    copy.ColumnMappings.Add("CustomerId", "CustomerId");
                    try
                    {
                        copy.WriteToServer(dt);
                        sqlbulkTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        sqlbulkTransaction.Rollback();
                        BaseService.WriteLogWeixin("Vip信息插入数据库失败");
                    }
                }
                conn.Close();
            }
        }
        #endregion

        #region 替换特殊字符
        /// <summary>
        ///替换特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ReplaceStr(string str)
        {
            string pStr = string.Empty;
            // Regex regChina = new Regex("^[^\x00-\xFF]");
            Regex regChina = new Regex("^[\u4e00-\u9fa5]+$");
            Regex regEnglish = new Regex("^[a-zA-Z]");
            Regex regInter = new Regex(@"^\d*$");
            foreach (char item in str.ToArray())
            {
                if (regChina.IsMatch(item.ToString()) || regEnglish.IsMatch(item.ToString()) || regInter.IsMatch(item.ToString()))
                {
                    pStr += item.ToString();
                }

            }
            return pStr;


            //string[] aryReg ={
            //            @"<script[^>]*?>.*?</script>",
            //            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            //            @"([\r\n])[\s]+",
            //            @"-->",
            //            @"<!--.*\n"
            //            };
            //string resStr = str.Replace("'", "");
            //for (int i = 0; i < aryReg.Length; i++)
            //{
            //    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //    resStr = regex.Replace(resStr, "");
            //}
            //return resStr;
        }
        #endregion

        #region 将List转为DataTable
        private static DataTable ConvertListToDatatTable<T>(List<T> entitys)
        {
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            Type entityType = entitys[0].GetType();
            System.Reflection.PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
        #endregion
        #endregion

        #endregion

        #region 发货通知接口
        /// <summary>
        /// 发货通知接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="postData">发货通知的参数，包含appid,openid,out_trade_no,transid,deliver_timestamp,deliver_status,deliver_msg,app_signature 的json体</param>
        /// <returns></returns>
        public string DeliverNotify(string accessToken, LoggingSessionInfo loggingSessionInfo, string postData)
        {
            var url = "https://api.weixin.qq.com/pay/delivernotify";
            url = url + "?access_token=" + accessToken;

            Loggers.Debug(new DebugLogInfo() { Message = "微信发货通知请求URL:" + url });

            Loggers.Debug(new DebugLogInfo() { Message = "微信发货通知请求PostData:" + postData });

            var method = "POST";
            string result = GetRemoteData(url, method, postData);

            var data = result.DeserializeJSONTo<WxErrMessage>();

            #region 向表中记录调用的微信接口

            var wxInterfaceLogBll = new WXInterfaceLogBLL(loggingSessionInfo);
            var wxInterfaceLogEntity = new WXInterfaceLogEntity();
            wxInterfaceLogEntity.LogId = Guid.NewGuid();
            wxInterfaceLogEntity.InterfaceUrl = "https://api.weixin.qq.com/pay/delivernotify";
            wxInterfaceLogEntity.RequestParam = postData;
            wxInterfaceLogEntity.ResponseParam = result;
            wxInterfaceLogEntity.IsSuccess = data.errcode == 0 ? 1 : 0; //errcode = 0 标识成功
            wxInterfaceLogBll.Create(wxInterfaceLogEntity);

            #endregion
            return result;

        }

        #endregion

        #region 通知微信维权

        public string UpdatePayFeedBack(string accessToken, LoggingSessionInfo loggingSessionInfo, string openId, string feedbackId)
        {
            var url = "https://api.weixin.qq.com/payfeedback/update";
            url = url + "?access_token=" + accessToken + "&openid=" + openId + "&feedbackid=" + feedbackId;

            Loggers.Debug(new DebugLogInfo() { Message = "微信维权更新URL:" + url });

            var result = GetRemoteData(url, "GET", string.Empty);

            var data = result.DeserializeJSONTo<WxErrMessage>();

            #region 向表中记录调用的微信接口

            var wxInterfaceLogBll = new WXInterfaceLogBLL(loggingSessionInfo);
            var wxInterfaceLogEntity = new WXInterfaceLogEntity();
            wxInterfaceLogEntity.LogId = Guid.NewGuid();
            wxInterfaceLogEntity.InterfaceUrl = "https://api.weixin.qq.com/payfeedback/update";
            wxInterfaceLogEntity.OpenId = openId;
            wxInterfaceLogEntity.RequestParam = "&openid=" + openId + "&feedbackid=" + feedbackId;
            wxInterfaceLogEntity.ResponseParam = result;
            wxInterfaceLogEntity.IsSuccess = data.errcode == 0 ? 1 : 0; //errcode = 0 标识成功
            wxInterfaceLogEntity.CustomerId = loggingSessionInfo.ClientID;
            wxInterfaceLogBll.Create(wxInterfaceLogEntity);

            #endregion

            return result;
        }

        #endregion

        /// <summary>
        /// 根据用户发送的二维码去二维码表中VipDCode匹配
        /// </summary>
        /// <param name="content"></param>
        /// <param name="vipID"></param>
        public static void StoreRebate(string content,string SalesAmount,string PushInfo, decimal ReturnAmount, string vipID,string openId,System.Data.SqlClient.SqlTransaction tran, LoggingSessionInfo LoggingSessionInfo)
        {

            VipDCodeBLL bll = new VipDCodeBLL(LoggingSessionInfo);
            
            try
            {
                //var temp = bll.QueryByEntity(new VipDCodeEntity { IsDelete = 0, DCodeId = content }, null);
                var temp = bll.GetByID(content);
              
                    //if (temp != null && temp.Length > 0)   //如果可以匹配，则更新二维码表中的会员ID，OpenId
                    if(temp != null)
                    {
                        var vipBll = new VipBLL(LoggingSessionInfo);

                        var vipEntity = vipBll.GetByID(vipID);

                        #region 添加返现码过期和被领取的消息提醒
                        if (temp.IsReturn == 1)
                        {
                            //发送消息

                            JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage("对不起，该返利码已经被领取", "1", LoggingSessionInfo, vipEntity);
                            return;
                        }

                        if (DateTime.Now > (temp.CreateTime ?? DateTime.Now).AddDays(1))
                        {
                            //发送消息
                            JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage("对不起，您的返利码已经过期，请在收到返利码后的24小时内使用", "1", LoggingSessionInfo, vipEntity);
                            return;
                        }
                        #endregion
                        #region 1.更新返现金额。更新返现状态
                        VipDCodeEntity entity = new VipDCodeEntity();
                        entity = temp;
                        entity.OpenId = openId;
                        entity.VipId = vipID;
                        entity.ReturnAmount = ReturnAmount;
                        entity.DCodeId = content;
                        entity.IsReturn = 1;
                        bll.Update(entity,tran); //更新返现金额
                        #endregion
                       

                        var vipamountBll = new VipAmountBLL(LoggingSessionInfo);
                        var endAmount = vipamountBll.GetVipByEndAmount(vipID, tran);
                        var message = PushInfo.Replace("#SalesAmount#",SalesAmount.ToString()).Replace("#ReturnAmount#", ReturnAmount.ToString("0.00")).Replace("#EndAmount#", endAmount.ToString("0.00")).Replace("#VipName#", vipEntity.VipName);
                        #region 插入门店返现推送消息日志表
                        WXSalesPushLogBLL PushLogbll = new WXSalesPushLogBLL(LoggingSessionInfo);
                        WXSalesPushLogEntity pushLog = new WXSalesPushLogEntity();
                        pushLog.LogId = Guid.NewGuid();
                        //pushLog.WinXin = requestParams.WeixinId;
                        pushLog.OpenId = openId;
                        pushLog.VipId = vipID;
                        pushLog.PushInfo = message;
                        pushLog.DCodeId = content;
                        pushLog.RateId = Guid.NewGuid();
                        PushLogbll.Create(pushLog,tran);
                        #endregion

                        string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, "1", LoggingSessionInfo, vipEntity);

                        Loggers.Debug(new DebugLogInfo() { Message = "消息推送完成，code=" + code + ", message=" + message });
                    }
                
            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
