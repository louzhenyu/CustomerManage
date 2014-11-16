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

namespace JIT.CPOS.Web.Weibo
{
    public partial class CallBack : System.Web.UI.Page
    {
        NetDimension.Web.Cookie cookie = new NetDimension.Web.Cookie("WeiboDemo", 24, TimeUnit.Hour);

        Client Sina = null;
        OAuth oauth = new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], ConfigurationManager.AppSettings["CallbackUrl"]);


        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Request.QueryString["code"]);
            //Response.Write("<br>");
            //Response.Write("userId:" + Request.QueryString["userId"]);
            //Response.Write("<br>");
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                    {
                        var token = oauth.GetAccessTokenByAuthorizationCode(Request.QueryString["code"]);
                        string accessToken = token.Token;

                        cookie["AccessToken"] = accessToken;
                        //Response.Write("AccessToken:" + accessToken);
                        //获取回调类型
                        string strType = Request.QueryString["strType"];
                        string userId = Request.QueryString["userId"];
                        string customerId = Request.QueryString["customerId"];
                        string resultUrl = Request.QueryString["resultUrl"];
                        string resultErrorUrl = Request.QueryString["resultErrorUrl"];
                        if (customerId == null || customerId.Equals(""))
                        {
                            customerId = "29E11BDC6DAC439896958CC6866FF64E";
                        }
                        
                        LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                        if (strType == null)
                        {
                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("新浪回调页面1-类型为空")
                            });
                            Response.Write("新浪回调页面1-类型为空");
                            return;
                        }
                        switch (strType)
                        {
                            case "Login":
                                SetAccessToken(userId, accessToken, loggingSessionInfo, resultErrorUrl);
                                GetAccessToken(userId, accessToken, loggingSessionInfo, resultErrorUrl);
                                SetUrlGo(resultUrl, customerId);
                                break;
                        }
                        
                    }
                    else
                    {
                        Response.Write("没得到回调信息");
                    }
                }
                catch (Exception ex) {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("新浪回调页面-起始出错：" + ex.ToString())
                    });
                }
            }
        }

        #region 设置用户token信息
        /// <summary>
        /// 设置用户token信息
        /// </summary>
        /// <param name="UserId">用户标识</param>
        /// <param name="AccessToken">token不存在</param>
        private void SetAccessToken(string UserId, string AccessToken, LoggingSessionInfo loggingSessionInfo, string resultErrorUrl)
        {
            try
            {
                if (UserId == null || UserId.Trim().Equals(""))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("新浪回调页面2-用户标识为空")
                    });
                }
                Response.Write("设置用户token信息1");
                #region 处理业务
                #region 判断用户是否存在会员表中
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = vipServer.GetByID(UserId);
                if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                {
                    vipInfo.VIPID = UserId;
                    vipInfo.VipCode = vipServer.GetVipCode();
                    vipInfo.ClientID = loggingSessionInfo.CurrentUser.customer_id;
                    vipInfo.Status = 1;
                    vipServer.Create(vipInfo);
                }
                #endregion
                #region 判断用户是否存在会员的新浪微博扩展表中
                VipExpandSinaWbBLL vipSinaWbServer = new VipExpandSinaWbBLL(loggingSessionInfo);
                VipExpandSinaWbEntity vipSinaWbInfo = new VipExpandSinaWbEntity();
                vipSinaWbInfo = vipSinaWbServer.GetByID(UserId);
                if (vipSinaWbInfo != null && vipSinaWbInfo.VipId != null && !vipSinaWbInfo.VipId.Equals(""))
                {
                    vipSinaWbInfo.AccessToken = AccessToken;
                    vipSinaWbServer.Update(vipSinaWbInfo, false);
                }
                else
                {
                    VipExpandSinaWbEntity vipSinaWbInfo1 = new VipExpandSinaWbEntity();
                    vipSinaWbInfo1.VipId = UserId;
                    vipSinaWbInfo1.AccessToken = AccessToken;
                    vipSinaWbServer.Create(vipSinaWbInfo1);
                }
                #endregion
                #endregion

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("新浪回调页面3-设置用户信息成功.")
                });
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("新浪回调页面4-错误信息提示 {0}:" + ex.ToString())
                });
                Response.Write(ex.ToString());
            }
        }
        #endregion

        #region 授权查询
        /// <summary>
        /// 授权查询
        /// </summary>
        private void GetAccessToken(string UserId, string AccessToken, LoggingSessionInfo loggingSessionInfo, string resultErrorUrl)
        {
            try
            {
                #region 获取授权信息
                var uriString = "https://api.weibo.com/oauth2/get_token_info";

                var postData = "access_token=" + AccessToken + "";
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("支付跳转-链接字符串: {0}", postData)
                });
                WebClient myWebClient = new WebClient();
                // 注意这种拼字符串的ContentType
                myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                // 转化成二进制数组
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // 上传数据，并获取返回的二进制数据.
                byte[] responseArray = myWebClient.UploadData(uriString, "POST", byteArray);
                var data = System.Text.Encoding.UTF8.GetString(responseArray);
                var tokenInfo = data.DeserializeJSONTo<TokenInfo>();
                if (tokenInfo == null)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("授权查询3-授权成功--但是没有返回信息:" + data.ToString())
                    });
                    return;
                }
                #endregion
                #region 更新授权信息
                VipExpandSinaWbBLL vipSinaWbServer = new VipExpandSinaWbBLL(loggingSessionInfo);
                VipExpandSinaWbEntity vipSinaWbInfo = new VipExpandSinaWbEntity();
                vipSinaWbInfo = vipSinaWbServer.GetByID(UserId);
                if (vipSinaWbInfo != null && vipSinaWbInfo.VipId != null && !vipSinaWbInfo.VipId.Equals(""))
                {
                    vipSinaWbInfo.AccessToken = AccessToken;
                    if (tokenInfo != null)
                    {
                        vipSinaWbInfo.UID = ToStr(tokenInfo.uid);
                        vipSinaWbInfo.Appkey = ToStr(tokenInfo.appkey);
                        vipSinaWbInfo.Scope = ToStr(tokenInfo.scope);
                        vipSinaWbInfo.CreateAt = ToStr(tokenInfo.create_at);
                        vipSinaWbInfo.ExpireIn = ToStr(tokenInfo.expire_in);
                    }
                    vipSinaWbServer.Update(vipSinaWbInfo, false);
                }
                else
                {
                    VipExpandSinaWbEntity vipSinaWbInfo1 = new VipExpandSinaWbEntity();
                    vipSinaWbInfo1.VipId = UserId;
                    if (tokenInfo != null)
                    {
                        vipSinaWbInfo1.UID = ToStr(tokenInfo.uid);
                        vipSinaWbInfo1.Appkey = ToStr(tokenInfo.appkey);
                        vipSinaWbInfo1.Scope = ToStr(tokenInfo.scope);
                        vipSinaWbInfo1.CreateAt = ToStr(tokenInfo.create_at);
                        vipSinaWbInfo1.ExpireIn = ToStr(tokenInfo.expire_in);
                    }
                    vipSinaWbServer.Create(vipSinaWbInfo1);
                }
                #endregion

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("授权查询3-授权成功:")
                });
                //设置新浪微博用户信息
                GetUserInfo(UserId, AccessToken, loggingSessionInfo, ToStr(tokenInfo.uid));
            }
            catch (Exception ex) {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("授权查询4-错误信息提示 {0}:" + ex.ToString())
                });
                Response.Write(ex.ToString());
            }

        }
        //{\"uid\":1036433880,\"appkey\":\"2267906895\",\"scope\":null,\"create_at\":1383277444,\"expire_in\":157672919}
        public class TokenInfo
        {
            public string uid { get; set; }         //授权用户的uid。
            public string appkey { get; set; }      //access_token所属的应用appkey。
            public string scope { get; set; }       //用户授权的scope权限。
            public string create_at { get; set; }   //access_token的创建时间，从1970年到创建时间的秒数。
            public string expire_in { get; set; }   //access_token的剩余时间，单位是秒数。
        }
        #endregion

        #region 设置新浪微博用户信息
        public void GetUserInfo(string UserId, string AccessToken, LoggingSessionInfo loggingSessionInfo,string uid)
        {
            try
            {
                //string uid = "1036433880";
                Sina = new Client(new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], AccessToken, null)); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
                var user = Sina.API.Dynamic.Users.Show(uid);
                string strUser = user.ToString();
                var userInfo = strUser.DeserializeJSONTo<SinaWeiBoUserInfo>();
                #region 更新用户信息
                VipExpandSinaWbBLL vipSinaWbServer = new VipExpandSinaWbBLL(loggingSessionInfo);
                VipExpandSinaWbEntity vipSinaWbInfo = new VipExpandSinaWbEntity();
                vipSinaWbInfo.VipId = UserId;
                if (userInfo != null && userInfo.id != null && !userInfo.id.Equals(""))
                {
                    vipSinaWbInfo.ScreenName = userInfo.screen_name;
                    vipSinaWbInfo.LabelName = userInfo.name;
                    vipSinaWbInfo.Province = userInfo.province;
                    vipSinaWbInfo.City = userInfo.city;
                    vipSinaWbInfo.Location = userInfo.location;
                    vipSinaWbInfo.Description = userInfo.description;
                    vipSinaWbInfo.Url = userInfo.url;
                    vipSinaWbInfo.ProfileImageUrl = userInfo.profile_image_url;
                    vipSinaWbInfo.ProfileUrl = userInfo.profile_url;
                    vipSinaWbInfo.Gender = userInfo.gender;
                    vipSinaWbInfo.Weihao = userInfo.weihao;
                    vipSinaWbServer.Update(vipSinaWbInfo, false);
                }
                #endregion
                //Response.Write(string.Format("{0}", user));
                Response.Write("设置新浪微博用户信息成功.");
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("设置新浪微博用户信息失败:" + ex.ToString())
                });
                Response.Write( ex.ToString());
            }
        }

        public class SinaWeiBoUserInfo
        {
            public string id { get; set; } //: 1036433880,
            public string idstr { get; set; } //: "1036433880",
            //public string class { get; set; } //: 1,
            public string screen_name { get; set; } //: "J峰子",
            public string name { get; set; } //: "J峰子",
            public string province { get; set; } //: "31",
            public string city { get; set; } //: "15",
            public string location { get; set; } //: "上海 浦东新区",
            public string description { get; set; } //: "",
            public string url { get; set; } //: "",
            public string profile_image_url { get; set; } //: "http:\/\/tp1.sinaimg.cn\/1036433880\/50\/0\/1",
            public string profile_url { get; set; } //: "u\/1036433880",
            public string domain { get; set; } // "",
            public string weihao { get; set; } //: "",
            public string gender { get; set; } //: "m",
            public string followers_count { get; set; } //: 0,
            public string friends_count { get; set; } //: 1,
            public string statuses_count { get; set; } //: 8,
            public string favourites_count { get; set; } //: 0,
            public string created_at { get; set; } //: "Sun Apr 07 16:23:21 +0800 2013",
            public string following { get; set; } //: false,
            public string allow_all_act_msg { get; set; } //: false,
            public string geo_enabled { get; set; } //: true,
            public string verified { get; set; } //: false,
            public string verified_type { get; set; } //: -1,
            public string remark { get; set; } //: "",
            public string ptype { get; set; } //: 0,
            public string allow_all_comment { get; set; } //: true,
            public string avatar_large { get; set; } //: "http:\/\/tp1.sinaimg.cn\/1036433880\/180\/0\/1",
            public string avatar_hd { get; set; } // "http:\/\/tp1.sinaimg.cn\/1036433880\/180\/0\/1",
            public string verified_reason { get; set; } //: "",
            public string follow_me { get; set; } //: false,
            public string online_status { get; set; } //: 0,
            public string bi_followers_count { get; set; } //: 0,
            public string lang { get; set; } //: "zh-cn",
            public string star { get; set; } //: 0,
            public string mbtype { get; set; } //: 0,
            public string mbrank { get; set; } //: 0,
            public string block_word { get; set; } //: 0

        }
        #endregion

        #region 页面跳转
        public void SetUrlGo(string url, string customerId)
        {
            if (url != null)
            {
                url += "?customerId= " + customerId + "";
                Response.Redirect(url);
            }
        }
        #endregion

        #region 公共方法
        #region ToStr
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
        #endregion
    }


}