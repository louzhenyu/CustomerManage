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
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;
using JIT.CPOS.BS;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.Weibo
{
    public partial class Index : System.Web.UI.Page
    {
        NetDimension.Web.Cookie cookie = new NetDimension.Web.Cookie("WeiboDemo", 24, TimeUnit.Hour);

        Client Sina = null;
        OAuth oauth = new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], ConfigurationManager.AppSettings["CallbackUrl"]);
        string customerId = "29E11BDC6DAC439896958CC6866FF64E";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Sina = new Client(oauth); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
            //if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            //{
            //    var token = oauth.GetAccessTokenByAuthorizationCode(Request.QueryString["code"]);
            //    string accessToken = token.Token;

            //    cookie["AccessToken"] = accessToken;

            //    Response.Redirect("Default.aspx");
            //}
            //else
            //{
            //    oauth.CallbackUrl += "?UserId=123456";
            //    string url = oauth.GetAuthorizeURL();
            //    authUrl.NavigateUrl = url;
            //}
            //GetAccessToken("2.00UnlIIBTLuTTC07eee405f50z6Q7T");
            //SetShareWeiBo("2.00UnlII");
           //GetGoSinaWeoboLogin("4f4ef63846f646b68e796cbc3604f2ed", "Login", "29E11BDC6DAC439896958CC6866FF64E");
             LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
             //SetAccessToken("4f4ef63846f646b68e796cbc3604f2ed", "2.00UnlIIBTLuTTC07eee405f50z6Q7T", loggingSessionInfo);
            // GetUserInfo("4f4ef63846f646b68e796cbc3604f2ed", "2.00UnlIIBTLuTTC07eee405f50z6Q7T", loggingSessionInfo);

            Response.Clear();
            string content = string.Empty;
            try
            {
                 string dataType = Request["action"].ToString().Trim();
                 switch (dataType)
                 {
                     case "getSinaWbLogin":      //1.新浪微博登陆接口
                         getSinaWbLogin();
                         break;
                     case "setSinaWbShare":     //2.新浪微博一键分享
                         content = setSinaWbShare();
                         break;
                     case "getWeiboUserInfo":   //3.新浪微博用户是否登录
                         content = getWeiboUserInfo();
                         break;
                 }
            }
            catch (Exception ex) {
                content = ex.Message;
            } 
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region 对外接口
        #region 1.新浪微博登陆接口
        /// <summary>
        /// 新浪微博登陆接口
        /// </summary>
        /// <returns></returns>
        public void getSinaWbLogin()
        {
            string resultErrorUrl = Request.QueryString["resultErrorUrl"];  //出错界面
            try
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getSinaWbLogin: {0}", "")
                });

              
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(Request.QueryString["customerId"]))
                {
                    customerId = Request.QueryString["customerId"];
                }
                //var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = Request.QueryString["userId"];
                string resultUrl = Request.QueryString["resultUrl"];            //返回界面
                
                string strType = "Login";

                Sina = new Client(oauth); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
                oauth.CallbackUrl += "?userId=" + userId + "&strType=" + strType + "&customerId=" + customerId + "&resultUrl=" + resultUrl + "&resultErrorUrl=" + resultErrorUrl + "";
                string url = oauth.GetAuthorizeURL();
                Response.Redirect(url);
                
            }
            catch (Exception ex)
            {
                resultErrorUrl += "?error=" + ex.ToString() + "";
                Response.Redirect(resultErrorUrl);
            }
        }

        public class getSinaWbLoginRespData : Default.LowerRespData
        {
            
        }
        
        public class getSinaWbLoginReqData : ReqData
        {
            public getSinaWbLoginReqSpecialData special;
        }
        public class getSinaWbLoginReqSpecialData
        {
            public string resultUrl { get; set; }       //返回界面
            public string resultErrorUrl { get; set; }  //出错界面
        }
        #endregion

        #region 2.新浪微博一键分享 setSinaWbShare
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string setSinaWbShare()
        {
            string content = string.Empty;
            var respData = new setSinaWbShareRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSinaWbShare: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setSinaWbShareReqData>();
                reqObj = reqObj == null ? new setSinaWbShareReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setSinaWbShareReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "2201";
                    respData.description = "特殊参数不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.resultUrl == null || reqObj.special.resultUrl.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "返回地址不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
              
                #region
                VipExpandSinaWbBLL server = new VipExpandSinaWbBLL(loggingSessionInfo);
                VipExpandSinaWbEntity info = server.GetByID(reqObj.common.userId);
                if (info == null || info.AccessToken == null)
                {
                    respData.code = "103";
                    respData.description = "不存在对应的token";
                    return respData.ToJSON().ToString();
                }

                Sina = new Client(new OAuth(ConfigurationManager.AppSettings["AppKey"]
                , ConfigurationManager.AppSettings["AppSecret"]
                , info.AccessToken
                , null));
                string imageUrl = reqObj.special.imageUrl;
                string Text = reqObj.special.text;
                if((imageUrl == null || imageUrl.Equals("")) && (Text == null || Text.Equals("")))
                {
                    respData.code = "103";
                    respData.description = "分享信息为空";
                    return respData.ToJSON().ToString();
                }
                VipWeiboShareLogBLL logServer = new VipWeiboShareLogBLL(loggingSessionInfo);
                VipWeiboShareLogEntity logInfo = new VipWeiboShareLogEntity();
                logInfo.LogId = BaseService.NewGuidPub();
                logInfo.AccessToken = info.AccessToken;
                logInfo.VipId = info.VipId;
                logInfo.ImageUrl = imageUrl;
                logInfo.Text = Text;
                logInfo.ShareType = "Sina";
                if (imageUrl == null || imageUrl.Equals(""))
                {
                    try
                    {
                        dynamic result = Sina.API.Dynamic.Statuses.Update(Text); //分享微博文本
                        logInfo.IsSuccess = 1;
                    }
                    catch (Exception ex1) {
                        logInfo.IsSuccess = 0;
                        if (ex1.ToString().Length > 200)
                        {
                            logInfo.FailureResason = ex1.ToString().Substring(0, 199);
                        }
                        else {
                            logInfo.FailureResason = ex1.ToString();
                        }
                        respData.code = "103";
                        respData.description = ex1.ToString();
                    }
                }
                else
                {
                    JIT.CPOS.BS.BLL.WX.CommonBLL serverBLL = new BS.BLL.WX.CommonBLL();
                    string urlx = serverBLL.DownloadFile(imageUrl);
                    try
                    {
                        dynamic r = Sina.API.Dynamic.Statuses.Upload(Text, byteImage(urlx));    //分享微博内容
                        logInfo.IsSuccess = 1;
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                        logInfo.IsSuccess = 0;
                        if (ex.ToString().Length > 200)
                        {
                            logInfo.FailureResason = ex.ToString().Substring(0, 199);
                        }
                        else
                        {
                            logInfo.FailureResason = ex.ToString();
                        }
                        respData.code = "103";
                        respData.description = ex.ToString();
                    }
                }
                logServer.Create(logInfo);
                #endregion


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class setSinaWbShareRespData : Default.LowerRespData
        {
        }
        
        public class setSinaWbShareReqData : ReqData
        {
            public setSinaWbShareReqSpecialData special;
        }
        public class setSinaWbShareReqSpecialData
        {
            public string imageUrl { get; set; }    //图片链接地址
            public string text { get; set; }        //分享文本
            public string resultUrl { get; set; }   //返回界面
            public string resultErrorUrl { get; set; }  //出错界面
        }
        #endregion
        #endregion

        #region 3.新浪微博用户是否登录
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getWeiboUserInfo()
        {
            string content = string.Empty;
            var respData = new getWeiboUserInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getWeiboUserInfo: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getWeiboUserInfoReqData>();
                reqObj = reqObj == null ? new getWeiboUserInfoReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getWeiboUserInfoRespContentData();

                #region
                VipExpandSinaWbBLL server = new VipExpandSinaWbBLL(loggingSessionInfo);
                VipExpandSinaWbEntity info = server.GetByID(reqObj.common.userId);
                if (info == null || info.AccessToken == null)
                {
                    respData.content.isSinaWb = "0";
                    respData.content.isAccessToken = "0";
                }
                else {
                    DateTime dt = DateTime.Parse("01/01/1970");
                    TimeSpan ts = DateTime.Now - dt;
                    int sec = ts.Seconds; // 秒数
                    int createAt = 0;       //创建时间
                    int expireIn = 0;       //剩余有效时间
                    if (info.CreateAt != null && !info.CreateAt.Equals(""))
                    {
                        createAt = Convert.ToInt32(info.CreateAt);
                    }
                    if (info.ExpireIn != null && !info.ExpireIn.Equals(""))
                    {
                        expireIn = Convert.ToInt32(info.ExpireIn);
                    }
                    respData.content.isSinaWb = "1";
                    if (sec - createAt > expireIn)
                    {
                        respData.content.isAccessToken = "1";
                    }
                    else {
                        respData.content.isAccessToken = "0";
                    }
                }
              
                #endregion


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getWeiboUserInfoRespData : Default.LowerRespData
        {
            public getWeiboUserInfoRespContentData content { get; set; }
        }
        public class getWeiboUserInfoRespContentData
        {
            public string isSinaWb { get; set; }        //是否与新浪微博捆绑过1=是 0 =否
            public string isAccessToken { get; set; }   //当前token是否有效	1=是 0= 否
        }
        
        public class getWeiboUserInfoReqData : ReqData
        {
           
        }
       
        #endregion
        #endregion

        #endregion

        #region 跳转登录
        public void GetGoSinaWeoboLogin(string UserId,string strType,string customerId)
        {
            Sina = new Client(oauth); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
            oauth.CallbackUrl += "?userId=" + UserId + "&strType=" + strType + "&customerId=" + customerId + "";
            string url = oauth.GetAuthorizeURL();
            Response.Redirect(url);
        }
        #endregion

        #region 设置新浪微博用户信息
        public void GetUserInfo(string UserId,string AccessToken, LoggingSessionInfo loggingSessionInfo)
        {
            try
            {
                string uid = "1036433880";
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
            catch (Exception ex) {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("设置新浪微博用户信息失败:"+ ex.ToString())
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

        #region 分享微博
        public void SetShareWeiBo(string AccessToken)
        {
            Sina = new Client(new OAuth(ConfigurationManager.AppSettings["AppKey"]
                , ConfigurationManager.AppSettings["AppSecret"]
                , AccessToken
                , null));
            string url1 = "http://ww2.sinaimg.cn/thumbnail/3dc6b9d8jw1ea5iae1vkej203i00o743.jpg";
    
            //string url = "E:\\CloudPos\\svnNew\\CPOS_New\\Code\\CPOS.Web\\Weibo\\images\\240.png";
            string wbContent = "上海市静安区延平路121号三和大厦 15层D座";
            JIT.CPOS.BS.BLL.WX.CommonBLL server = new BS.BLL.WX.CommonBLL();
            string urlx = server.DownloadFile(url1);
            //dynamic result = Sina.API.Dynamic.Statuses.Update(wbContent); //分享微博文本
            try
            {
                dynamic r = Sina.API.Dynamic.Statuses.Upload(wbContent, byteImage(urlx));    //分享微博内容
            }
            catch (Exception ex) {
                Response.Write(ex.ToString());
            }
        }

        public byte[] byteImage(string Path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            Byte[] by = new Byte[fs.Length];
            fs.Read(by, 0, by.Length);
            fs.Close();
            return by;
        }

       
        #endregion

        #region 分享微博接口
        public void SetShareWeiBo()
        { 
            
        }
        #endregion

        #region ReqData

        public class ReqData
        {
            public ReqCommonData common;
        }
        public class ReqCommonData
        {
            public string locale;
            public string userId;
            public string openId;
            public string signUpId;
            public string customerId;
        }

        #endregion
    }
}