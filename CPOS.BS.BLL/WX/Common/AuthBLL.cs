using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;
using JIT.Utility.Web;
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL.WX
{
    public class AuthBLL
    {
        #region 第一步 获取code
        /// <summary>
        /// 第一步：用户同意授权，获取code
        /// </summary>
        /// <param name="strAppId">公众号的唯一标识</param>
        /// <param name="strRedirectUri">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="strState">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值</param>
        /// <param name="Response"></param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）默认为snsapi_base</param>
        public void GetOAuthCode(string strAppId, string strRedirectUri, string strState, HttpResponse Response, string scope = null, string openOAuthAppid = null)
        {
            try
            {
                if (scope == null || scope.Equals(""))
                {
                    scope = "snsapi_base";

                }
                else
                {
                    if (strState.Length > 64)
                    {
                        HttpContext.Current.Session["State"] = strState;
                        strState = "State";
                    }
                }
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                string url = string.Empty;
                if (!string.IsNullOrEmpty(openOAuthAppid))
                {
                    //HttpUtility.UrlEncode(,这里对redirect_uri做了编码设置，以保证gourl的所有参数都在redirect_uri的值里，而不被当成open.weixin.qq.com/connect/oauth2/authorize的参数
                    url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=" + scope + "&state=" + strState + "&component_appid=" + openOAuthAppid + "#wechat_redirect";
                }
                else
                {
                    url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=" + scope + "&state=" + strState + "#wechat_redirect";
                }
                //string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=" + scope + "&state=" + strState + "&component_appid=wx691c2f2bbac04b4b#wechat_redirect";
                //string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strAppId + "&redirect_uri=" + HttpUtility.UrlEncode(strRedirectUri) + "&response_type=code&scope=" + scope + "";
                //string postString = "state=" + strState + "#wechat_redirect";
                //string method = "POST";
                //string data = CommonBLL.GetRemoteData(url, method, postString);
                //Response.Write("</br>");
                //Response.Write(url);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "1： GetOAuthCode成功: redirecting, url:" + url
                });
                Response.Redirect(url, false);//跳转页面
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetOAuthCode 出错: {0}", ex.ToString())
                });
                Response.Write("GetOAuthCode 出错: " + ex.ToString());
            }
        }

        #endregion

        #region 第二步 获取Access Token
        /// <summary>
        /// 第二步：通过code换取网页授权access_token
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <param name="strAppId">公众号的唯一标识</param>
        /// <param name="strAppSecret">公众号的appsecret</param>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="iRad"></param>
        /// <returns></returns>
        public string GetAccessToken(string code, string strAppId, string strAppSecret, LoggingSessionInfo loggingSessionInfo, int iRad, out string token, string openOAuthAppid = null)
        {
            MarketSendLogBLL sendServer = new MarketSendLogBLL(loggingSessionInfo);
            MarketSendLogEntity sendInfo = new MarketSendLogEntity();
            //Random rad = new Random();
            //int iRad = rad.Next(1000, 100000);
            try
            {
                sendInfo.LogId = BaseService.NewGuidPub();
                sendInfo.IsSuccess = 1;
                sendInfo.MarketEventId = "GetAccessToken-1";
                sendInfo.SendTypeId = "200";
                sendInfo.Phone = iRad.ToString();
                sendInfo.TemplateContent = "GetAccessToken:code:" + code + ",strAppId:" + strAppId + ",strAppSecret:" + strAppSecret;
                sendInfo.VipId = code;
                sendInfo.WeiXinUserId = "1111";
                sendInfo.CreateTime = System.DateTime.Now;
                sendServer.Create(sendInfo);

                var sendObjList = sendServer.QueryByEntity(new MarketSendLogEntity
                {
                    VipId = code
                    ,
                    WeiXinUserId = "AccessToken"
                    ,
                    IsDelete = 0
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
                    var postData = string.Empty;
                    if (string.IsNullOrEmpty(strAppSecret))//已登录授权了
                    {
                        if (string.IsNullOrEmpty(openOAuthAppid))
                        {
                            var wailist = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity { AppID = strAppId, CustomerId = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                            openOAuthAppid = wailist != null ? wailist.OpenOAuthAppid : string.Empty;
                        }

                        //openOAuthAppid = !string.IsNullOrEmpty(openOAuthAppid) ? openOAuthAppid : "wx691c2f2bbac04b4b";
                        var openOAuthUrl = string.IsNullOrEmpty(ConfigurationManager.AppSettings["openOAuthUrl"]) ? "http://open.chainclouds.com" : ConfigurationManager.AppSettings["openOAuthUrl"];
                        var openuri = openOAuthUrl + "/OpenOAuth/GetComponentAccessToken";
                        var opendata = CommonBLL.GetRemoteData(openuri, "GET", string.Empty).Replace("\"", "");//先获取第三方平台的component_access_token
                        var url2 = "https://api.weixin.qq.com/sns/oauth2/component/access_token?appid="+ strAppId + "&code=" + code + "&grant_type=authorization_code&component_appid=" + openOAuthAppid + "&component_access_token=" + opendata + "";
                        opendata = CommonBLL.GetRemoteData(url2, "GET", string.Empty);
                        data = opendata;
                    }
                    else//未授权的情况下
                    {
                        postData = "appid=" + strAppId + "&secret=" + strAppSecret + "&code=" + code + "&grant_type=authorization_code";
                        sendInfo.LogId = BaseService.NewGuidPub();
                        sendInfo.IsSuccess = 1;
                        sendInfo.MarketEventId = "GetAccessToken-2";
                        sendInfo.SendTypeId = "200";
                        sendInfo.Phone = iRad.ToString();
                        sendInfo.TemplateContent = string.Format("{0}", postData);
                        sendInfo.VipId = code;
                        sendInfo.WeiXinUserId = "GetAccessToken-Para";
                        sendInfo.CreateTime = System.DateTime.Now;
                        sendServer.Create(sendInfo);

                        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                        // 上传数据，并获取返回的二进制数据.
                        byte[] responseArray = myWebClient.UploadData(url, "POST", byteArray);
                        data = System.Text.Encoding.UTF8.GetString(responseArray);
                    }
                    
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
                    sendInfo.SendTypeId = "200";
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
                    sendInfo.SendTypeId = "200";
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
                token = "";
                if (tokenInfo != null)
                {
                    //Response.Write("<br/>");
                    //Response.Write("获取Access Token不为空");
                    token = tokenInfo.access_token;
                    return tokenInfo.openid;
                }
                else
                {
                    return "";
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
                sendInfo.LogId = BaseService.NewGuidPub();
                sendInfo.IsSuccess = 0;
                sendInfo.MarketEventId = "GetAccessToken-4";
                sendInfo.SendTypeId = "200";
                sendInfo.Phone = iRad.ToString();
                sendInfo.TemplateContent = string.Format("GetAccessToken错误: {0}", ex.ToString());
                sendInfo.VipId = code;
                sendInfo.WeiXinUserId = "1111";
                sendInfo.CreateTime = System.DateTime.Now;
                sendServer.Create(sendInfo);
                token = "";
                return "";
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

        #region 第三部- 获取用户标识
        public VipEntity GetUserIdByOpenId(LoggingSessionInfo loggingSessionInfo, string OpenId)
        {
            VipEntity vipInfo = new VipEntity();
            try
            {

                string vipId = string.Empty;
                string status = "0";
                VipBLL server = new VipBLL(loggingSessionInfo);
                WXUserInfoBLL wxUserInfoBLL = new WXUserInfoBLL(loggingSessionInfo);
                //var vipObjs = server.QueryByEntityAbsolute(new VipEntity
                //{
                //    WeiXinUserId = OpenId
                //}, null);
                var vipObjs = server.QueryByEntity(new VipEntity   //先从会员表里取
                {
                    WeiXinUserId = OpenId,
                    ClientID = loggingSessionInfo.ClientID
                }, null);

                if (vipObjs == null || vipObjs.Length == 0 || vipObjs[0] == null)//找不到会员信息
                {
                    //优先从支持多号运营的表中取
                    var wxUserInfo = wxUserInfoBLL.QueryByEntity(new WXUserInfoEntity() { CustomerID = loggingSessionInfo.ClientID, WeiXinUserID = OpenId }, null).FirstOrDefault();
                    if (wxUserInfo != null)
                    {
                        var vipEntity = server.QueryByEntity(new VipEntity() { ClientID = loggingSessionInfo.ClientID, UnionID = wxUserInfo.UnionID }, null).FirstOrDefault();//从会员表里取
                        if (vipEntity != null)
                        {
                            vipId = vipEntity.VIPID;
                            status = vipEntity.Status.ToString();
                            vipInfo = vipEntity;
                        }
                        else
                            vipInfo = null;
                    }
                    else
                    {

                        //请求获取用户信息
                        //Jermyn20130911 从总部导入vip信息
                        bool bReturn = server.GetVipInfoFromApByOpenId(OpenId, null);
                        var vipObjs1 = server.QueryByEntityAbsolute(new VipEntity
                        {
                            WeiXinUserId = OpenId
                        }, null);
                        if (vipObjs1 == null || vipObjs1.Length == 0 || vipObjs1[0] == null)
                        {
                            vipInfo = null;
                        }
                        else
                        {
                            vipId = vipObjs1[0].VIPID;
                            status = vipObjs1[0].Status.ToString();
                            vipInfo = vipObjs1[0];
                        }
                    }
                }
                else  //查到会员信息了
                {
                    vipId = vipObjs[0].VIPID;
                    status = vipObjs[0].Status.ToString();
                    vipInfo = vipObjs[0];
                    //获取UnionID
                    if (string.IsNullOrEmpty(vipInfo.UnionID))
                    {
                        var vipService = new VipBLL(loggingSessionInfo);
                        var vipEntity = new VipEntity();
                        var commonBll = new CommonBLL();
                        var application = new WApplicationInterfaceDAO(loggingSessionInfo);
                        var appEntity = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = vipInfo.WeiXin, CustomerId = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                        if (appEntity != null)
                        {
                            //获取调用微信接口的凭证(普通的获取accestoken的地方)
                            var accessToken = commonBll.GetAccessTokenByCache(appEntity.AppID, appEntity.AppSecret, loggingSessionInfo);
                            //通过openID获取用户信息
                          //  （这种情况下，因为已经有会员信息了，并且已经关注了，才能获取到会员信息）
                            var userInfo = commonBll.GetUserInfo(accessToken.access_token, vipInfo.WeiXinUserId);
                            if (!string.IsNullOrEmpty(userInfo.unionid))
                            {
                                var vipEntitys = vipService.QueryByEntity(new VipEntity { UnionID = userInfo.unionid, ClientID = loggingSessionInfo.ClientID }, null);
                                if (vipEntitys != null && vipEntitys.Length > 0)//已经存在有UnionID的数据
                                {
                                    var wxUserInfo = wxUserInfoBLL.QueryByEntity(new WXUserInfoEntity() { CustomerID = loggingSessionInfo.ClientID, VipID = vipEntitys[0].VIPID, WeiXinUserID = OpenId, UnionID = userInfo.unionid }, null).FirstOrDefault();
                                    if (wxUserInfo == null)
                                    {
                                        var wxuiEntity = new WXUserInfoEntity()
                                        {
                                            WXUserID = Guid.NewGuid(),
                                            VipID = vipEntitys[0].VIPID,//vipInfo.VIPID,
                                            WeiXin = vipInfo.WeiXin,
                                            WeiXinUserID = vipInfo.WeiXinUserId,
                                            UnionID = userInfo.unionid,
                                            CustomerID = vipInfo.ClientID,
                                            CreateBy = "auth",
                                            LastUpdateBy = "auth"
                                        };
                                        wxUserInfoBLL.Create(wxuiEntity);
                                    }

                                    //删除冗余vip记录
                                    vipInfo.LastUpdateBy = "auth-delete";
                                    vipService.Delete(vipInfo);
                                }
                                else
                                {
                                    //更新微信用户信息
                                    vipInfo.VipName = userInfo.nickname;
                                    vipInfo.City = userInfo.city;
                                    vipInfo.Gender = Convert.ToInt32(userInfo.sex);
                                    vipInfo.HeadImgUrl = userInfo.headimgurl;
                                    vipInfo.UnionID = userInfo.unionid;
                                    server.Update(vipInfo);
                                }
                                
                            }
                        }
                        
                    }
                }
                return vipInfo;

            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetUserIdByOpenId用户用户信息出错: {0}", ex.ToString())
                });
                //Response.Write("GetUserIdByOpenId用户用户信息出错:" + ex.ToString());
                return vipInfo;
            }
        }
        #endregion

        #region 第四步 设置VIP信息
        public string SetVipInfoByToken(string token, string openId, LoggingSessionInfo loggionSesionInfo, HttpResponse Response)
        {
            string vipId = string.Empty;
            var url = "https://api.weixin.qq.com/sns/userinfo?";

            var postData = "access_token=" + token + "&openid=" + openId + "&lang=zh_CN";

            // Response.Write(url + postData);

            string method = "GET";
            // var data = HttpWebClient.DoHttpRequest(url, postData);
            var data = CommonBLL.GetRemoteData(url + postData, method, string.Empty);
            var openInfo = data.DeserializeJSONTo<WxOpenInfoResponse>();

            if (openInfo == null)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "获取微信用户信息失败：" + data.ToJSON()
                });
                return vipId;
            }
            else
            {
                try
                {
                    //insert into vip
                    CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                    string downloadImageUrl = ConfigurationManager.AppSettings["website_WWW"];
                    var headimgurl = downloadServer.DownloadFile(openInfo.headimgurl, downloadImageUrl);

                    vipId = BaseService.NewGuidPub();
                    VipBLL vipServiceUnion = new VipBLL(loggionSesionInfo);

                    var vipInfo = new VipEntity();
                    vipInfo.VIPID = vipId;
                    vipInfo.WeiXinUserId = openId;  //openId微信提供
                    vipInfo.City = openInfo.city;			 //城市，微信提供
                    vipInfo.Gender = Convert.ToInt32(openInfo.sex);  //性别，微信提供
                    vipInfo.VipName = openInfo.nickname;  //微信昵称，微信提供
                    vipInfo.VipCode = vipServiceUnion.GetVipCode();
                    vipInfo.UnionID = openInfo.unionid;
                    vipInfo.VipSourceId = "3";		//写死
                    vipInfo.HeadImgUrl = headimgurl;   //注意，需要先传到我们本地服务器，可以参考（需要download下来）
                    vipInfo.ClientID = loggionSesionInfo.ClientID;		//客户标识
                    vipInfo.RegistrationTime = DateTime.Now;
                    vipInfo.Status = 0;			//客户没有关注
                    vipInfo.VipPasswrod = "e10adc3949ba59abbe56e057f20f883e";  //初始密码123456
                    UnitService unitServer = new UnitService(loggionSesionInfo);
                    vipInfo.CouponInfo = unitServer.GetUnitByUnitTypeForWX("总部", null).Id; //获取总部门店标识

                    var wappBll = new WApplicationInterfaceBLL(loggionSesionInfo);
                    string weixinId = "";
                    var wappEntity = wappBll.QueryByEntity(new WApplicationInterfaceEntity()
                    {
                        CustomerId = loggionSesionInfo.ClientID
                    }, null);
                    if (wappEntity.Length > 0)
                    {
                        weixinId = wappEntity[0].WeiXinID;
                    }
                    vipInfo.WeiXin = weixinId;		//微信号码，通过数据库可以查出


                    vipServiceUnion.Create(vipInfo);

                }
                catch (Exception ex)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "新增微信用户信息失败：" + ex.ToString()
                    });
                }

                return vipId;
            }
        }

        #endregion
    }

    public class WxOpenInfoResponse
    {

        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }

        public WxOpenPrivilege[] privilege { get; set; }

    }
    public class WxOpenPrivilege
    {
        public string PRIVILEGE1 { get; set; }
        public string PRIVILEGE2 { get; set; }
    }
}
