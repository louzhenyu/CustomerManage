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
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace JIT.CPOS.Web.WXOAuth
{
    public partial class AuthCodeReques : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E";
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A";
        public string goUrl = string.Empty;
        public string scope = string.Empty;
        public string pageName = string.Empty;
        public string strAppId = string.Empty;//"wxeebb52e0aa813101"
        public string strAppSecret = string.Empty; //"22ac924a92e6caf176d6ba426d744adb";
        public string strWeiXinId = string.Empty;   // 微信公众号码 
        //会员分享的SourceId，ShareVipID，ObjectID

        public int SourceId = 0;// 1员工，2客服，3会员
        public string ShareVipID = string.Empty; //分享人的标识（SourceId为1，2时，这个是员工，为3时这个是会员）
        public string ObjectID = string.Empty;   //分享的链接代表的对象，活动或者商品、海报
        public string objectType = string.Empty; //分享的链接代表的对象的类型
        public string strToken = string.Empty;   //微信公众号令牌
        public DateTime strTokenExpirationTime = DateTime.Now;   //微信公众号令牌

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
                    try
                    {
                        #region Jermyn20140923
                        if (state.Equals("State"))
                        {
                            state = (string)HttpContext.Current.Session["State"];
                        }
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "State：" + state
                        });
                        #endregion

                        string ip = "";
                        if (Context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
                        {
                            ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
                        }
                        else// not using proxy or can't get the Client IP
                        {
                            ip = Context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
                        }
                        #region 参数拼成
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("2x： QueryString: {0}, From IP: {1}", Request.QueryString, ip)
                        });
                        byte[] buff1 = Convert.FromBase64String(state);
                        state = Encoding.UTF8.GetString(buff1);
                        state = HttpUtility.UrlDecode(state, Encoding.UTF8);//转码
                        string[] array = state.Split(',');
                        customerId = array[1];
                        applicationId = array[2];
                        goUrl = array[0];
                        scope = array[3];
                        if(array.Length>=5)
                            this.pageName = array[4];
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("3x:解析出回传的state的值：customerid={0};applicationid={1};goUrl={2};pageName={3};", customerId, applicationId, goUrl, pageName)
                        });
                        //从goUrl获取SourceId，ShareVipID，ObjectID 的值
                        #region 取三个新增的Id
                        string baseUrl = string.Empty;
                        NameValueCollection collection = new NameValueCollection();
                        ParseUrl(goUrl, out baseUrl, out collection);
                        foreach (string k in collection.Keys)
                        {
                            var targetValue = collection[k];
                            if (collection[k].IndexOf(',') > 0)
                            {
                                targetValue = collection[k].Split(',')[0];
                            }
                            if (k == "objectid")
                            {
                                ObjectID = targetValue;
                            }
                            if (k == "sourceid")
                            {
                                SourceId = Convert.ToInt32(targetValue);
                            }
                            if (k == "sharevipid")
                            {
                                ShareVipID = targetValue;
                            }
                            if (k == "objecttype") //专程小写了
                            {
                                objectType = targetValue;
                            }
                        }
                        #endregion
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<br/>");
                        Response.Write("statex错误:" + ex.ToString());
                    }
                    #endregion
                }

                #region
                Response.Write("<br>");
                loggingSessionInfo = Default.GetBSLoggingSession(customerId, "system");

                GetKeyByApp();
                string code = Request["code"];
                if (code == null || code.Equals(""))  //没有获取到相应的openid，sns_userinfo情况下，用户没有授权。
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "code为空"
                    });

                    Response.Write("code为空不应该进入");
                    Response.End();
                }
                else
                {
                    Response.Write("存在code:" + code);
                    string token = "";
                    string openId = authBll.GetAccessToken(code, strAppId, strAppSecret, loggingSessionInfo, iRad, out token);//
                    //scope = ''
                    
                    Response.Write("<br>");
                    Response.Write("OpenID:" + openId);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "3xx: OpenID:" + openId + " , token: " + strToken
                    });
                    PageRedict(applicationId, openId, strToken);//页面跳转，带着获取的openid和token
                }
                #endregion
            }
        }

        /// <summary>
        /// Url处理工具
        /// </summary>
        /// <param name="url"></param>
        /// <param name="baseUrl"></param>
        /// <param name="nvc"></param>
        public static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            nvc = new NameValueCollection();
            baseUrl = "";
            if (url == "")
                return;
            int questionMarkIndex = url.IndexOf('?');
            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);
            // 开始分析参数对  
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);
            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }
        #region 获取微信信息
        public void GetKeyByApp()
        {
            WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(loggingSessionInfo);
            WApplicationInterfaceEntity info = server.GetByID(applicationId);
            if (info == null)
            {
                Response.Write("xx不存在对应的微信标识");
            }
            else
            {
                strAppId = info.AppID;
                strAppSecret = info.AppSecret;
                strWeiXinId = info.WeiXinID;
                strToken = info.RequestToken;
                strTokenExpirationTime = info.ExpirationTime == null ? strTokenExpirationTime : (DateTime)info.ExpirationTime;
            }
        }
        #endregion

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="openId"></param>
        private void PageRedict(string applicationId, string openId,string token)
        {

            //if (goUrl.IndexOf("HtmlApp/Lj/auth.html?pageName=GoodsList")>0)
            //{
            //    goUrl = goUrl.Replace("HtmlApp/Lj/auth.html?pageName=GoodsList", "HtmlApps/auth.html?pageName=GoodsList");
            //    Loggers.Debug(new DebugLogInfo()
            //    {
            //        Message = string.Format("url替换：old={0};new={1};", "HtmlApp/Lj/auth.html?pageName=GoodsList", "HtmlApps/auth.html?pageName=GoodsList")
            //    });
            //}
            //if (goUrl.IndexOf("HtmlApp/Lj/auth.html?pageName=MyOrder") > 0)
            //{
            //    goUrl = goUrl.Replace("HtmlApp/Lj/auth.html?pageName=MyOrder", "HtmlApps/auth.html?pageName=MyOrder");
            //    Loggers.Debug(new DebugLogInfo()
            //    {
            //        Message = string.Format("url替换：old={0};new={1};", "HtmlApp/Lj/auth.html?pageName=MyOrder", "HtmlApps/auth.html?pageName=MyOrder")
            //    });
            //}
            //if (goUrl.IndexOf("HtmlApp/Lj/auth.html?pageName=JiFenShop") > 0)
            //{
            //    goUrl = goUrl.Replace("HtmlApp/Lj/auth.html?pageName=JiFenShop", "HtmlApps/auth.html?pageName=JiFenShop");
            //    Loggers.Debug(new DebugLogInfo()
            //    {
            //        Message = string.Format("url替换：old={0};new={1};", "HtmlApp/Lj/auth.html?pageName=JiFenShop", "HtmlApps/auth.html?pageName=JiFenShop")
            //    });
            //}

            var decodeUrl = HttpUtility.UrlDecode(goUrl);//解码
            if (!decodeUrl.StartsWith("http://"))
            {
                goUrl = "http://" + decodeUrl;
            }
            else
                goUrl = decodeUrl;
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("zk goUrl:{0}", goUrl) });
            if (!string.IsNullOrWhiteSpace(this.pageName))
            {
                //根据pageName进行路径替换
                var configBll = new WeiXinH5ConfigBLL(loggingSessionInfo);
                var pagePath = configBll.GetPagePathByPageName(customerId, pageName);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("4x: pagePath：{0};goUrl:{1}", pagePath, goUrl)
                });
                goUrl = goUrl.Replace("_pageName_", pagePath);
            }
            if (openId == null || openId.Equals(""))//没有获取到相应的openid，也跳转
            {
                Random rad = new Random();
                if (goUrl.IndexOf("?") > 0)
                {
                    goUrl = goUrl + "&CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                }
                else
                {
                    goUrl = goUrl + "?CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                }
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("没有获得会员的OPEN ID时的跳转URL：{0}", goUrl)
                });
                Response.Redirect(goUrl);
            }
            else
            {
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();

                vipInfo = vipServer.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = openId,
                    ClientID = loggingSessionInfo.ClientID
                }, null).FirstOrDefault();
                if (vipInfo == null)
                {
                    //从支持多号运营的表中取
                    var wxUserInfoBLL = new WXUserInfoBLL(loggingSessionInfo);
                    var wxUserInfo = wxUserInfoBLL.QueryByEntity(new WXUserInfoEntity() { CustomerID = loggingSessionInfo.ClientID, WeiXinUserID = openId }, null).FirstOrDefault();
                    if (wxUserInfo != null)
                    {
                        vipInfo = vipServer.QueryByEntity(new VipEntity() { ClientID = loggingSessionInfo.ClientID, UnionID = wxUserInfo.UnionID }, null).FirstOrDefault();
                    }
                }

                //vipInfo = authBll.GetUserIdByOpenId(loggingSessionInfo, openId);
                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format("66x: openId：{0};", "进来了" + openId + "--openId")
                //});
                #region 本系统中不存在这个会员  或者未关注
                if (vipInfo == null || vipInfo.VIPID.Equals("") || vipInfo.Status == 0) //会员不存在或没有关注，从微信里查找会员信息，新增一条vip数据
                {

                    Response.Write("会员不存在或没有关注");
                    VipEntity vipInfotmp = new VipEntity();
                    #region snsapi_userinfo 这种模式的
                    if (scope.Equals("1"))//snsapi_userinfo 这种模式的
                    {
                        //封装方法，调用第四步，根据第四步的数据，新增一条vip数据
                        if (null == vipInfo || vipInfo.VIPID.Equals(""))
                        {
                            vipInfotmp.VIPID = authBll.SetVipInfoByToken(token, openId, loggingSessionInfo, this.Response);//根据token和openid获取会员信息


                            if (objectType.Equals("Coupon"))//如果是优惠券根据给定的VipSourceId=27给定
                            {
                                vipInfotmp.VipSourceId = "26";
                                //   vipInfo.VipSourceName = "优惠券二维码";
                            }
                            else if (objectType.Equals("SetoffPoster"))//如果是海报根据给定的VipSourceId=29给定
                            {
                                vipInfotmp.VipSourceId = "28";
                                //  vipInfo.VipSourceName = "集客海报二维码";
                            }
                            else if (objectType.Equals("Goods"))
                            {
                                vipInfotmp.VipSourceId = "31";
                                //vipInfo.VipSourceName = "员工集客";
                            }
                            else if (objectType.Equals("CTW"))
                            {
                                vipInfotmp.VipSourceId = "24";
                            }
                            else if (objectType.Equals("Material"))
                            {
                                vipInfotmp.VipSourceId = "32";
                            }

                            vipServer.Update(vipInfotmp,false);

                        }
                        else   //状态为未关注，vipInfo.Status == 0
                        {
                            if (vipInfo.Status == 0)
                            {
                                if (objectType.Equals("Coupon"))//如果是优惠券根据给定的VipSourceId=27给定
                                {
                                    vipInfo.VipSourceId = "26";
                                    //   vipInfo.VipSourceName = "优惠券二维码";
                                }
                                else if (objectType.Equals("SetoffPoster"))//如果是海报根据给定的VipSourceId=29给定
                                {
                                    vipInfo.VipSourceId = "28";
                                    //  vipInfo.VipSourceName = "集客海报二维码";
                                }
                                else if (objectType.Equals("Goods"))
                                {
                                    vipInfo.VipSourceId = "31";
                                    //vipInfo.VipSourceName = "员工集客";
                                }
                                else if (objectType.Equals("CTW"))
                                {
                                    vipInfo.VipSourceId = "24";
                                }
                                else if (objectType.Equals("Material"))
                                {
                                    vipInfo.VipSourceId = "32";
                                }
                            }
                            vipInfo.IsDelete = 0;
                            //   vipInfo.Status = 1;  //潜在用户？这个时候如果没关注，也算潜在用户吗？没关注不算潜在用户，所以不改变他的状态**
                            vipServer.Update(vipInfo, false);
                            vipInfotmp.VIPID = vipInfo.VIPID;
                        }
                    }
                    #endregion
                    #region snsapi_base这种情况下，，获取不到会员的详细信息，只能获取到openid
                    else
                    {
                        #region Jermyn20140604,会员没有关注，先采集信息

                        if (vipInfo == null || vipInfo.VIPID == null)
                        {
                            //snsapi_base 这种模式下，获取不到会员的详细信息，只能获取到openid
                            vipInfotmp.VIPID = authBll.SetVipInfoByToken(token, openId, loggingSessionInfo, this.Response);
                            #region previous logic
                            /******
                            vipInfotmp.VIPID = Guid.NewGuid().ToString().Replace("-", "");
                            vipInfotmp.VipCode = vipServer.GetVipCode();
                            vipInfotmp.WeiXinUserId = openId;
                            vipInfotmp.Status = 0;
                            vipInfotmp.VipSourceId = "3";
                            vipInfotmp.ClientID = customerId;
                            vipInfotmp.WeiXin = strWeiXinId;
                            vipInfotmp.VipPasswrod = "e10adc3949ba59abbe56e057f20f883e";
                            vipInfotmp.Col50 = "通过Auth认证时，未关注，主动采取的信息。";
                            vipServer.Create(vipInfotmp);
                            ********/
                            #endregion

                            if (objectType.Equals("Coupon"))//如果是优惠券根据给定的VipSourceId=27给定
                            {
                                vipInfo.VipSourceId = "26";
                                //   vipInfo.VipSourceName = "优惠券二维码";
                            }
                            else if (objectType.Equals("SetoffPoster"))//如果是海报根据给定的VipSourceId=29给定
                            {
                                vipInfo.VipSourceId = "28";
                                //  vipInfo.VipSourceName = "集客海报二维码";
                            }
                            else if (objectType.Equals("Goods"))
                            {
                                vipInfo.VipSourceId = "31";
                                //vipInfo.VipSourceName = "员工集客";
                            }
                            else if (objectType.Equals("CTW"))
                            {
                                vipInfo.VipSourceId = "24";
                            }
                            else if (objectType.Equals("Material"))
                            {
                                vipInfo.VipSourceId = "32";
                            }
                            vipServer.Update(vipInfotmp,false);


                        }
                        else
                        {
                            vipInfotmp.VIPID = vipInfo.VIPID;//设置vipid
                        }
                        #endregion
                    }
                    #endregion

                    Random rad = new Random();
                    //if (goUrl.IndexOf("?") > 0)
                    //{
                    //    goUrl = goUrl + "&customerId=" + customerId + "&applicationId=" + applicationId + "&openId=" + openId + "&userId=" + vipInfotmp.VIPID + "&CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                    //}
                    //else
                    //{
                    //    goUrl = goUrl + "?customerId=" + customerId + "&applicationId=" + applicationId + "&openId=" + openId + "&userId=" + vipInfotmp.VIPID + "&CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                    //}
                    if (goUrl.IndexOf("?") > 0)
                    {
                        goUrl = goUrl + "&customerId=" + customerId + "&applicationId=" + applicationId + "&CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    else
                    {
                        goUrl = goUrl + "?customerId=" + customerId + "&applicationId=" + applicationId + "&CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    Response.Cookies["openId_" + customerId].Value = openId;
                    Response.Cookies["userId_" + customerId].Value = vipInfotmp.VIPID;//vipInfo.VIPID;
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("会员没有关注时的跳转URL：{0}", goUrl)
                    });


                 


                    //在这里建立上下线关系
                    SetShareVip(vipInfotmp.VIPID, openId);


                    Response.Redirect(goUrl);

                }
                #endregion
                #region  会员已经存在（关注过）
                else
                {

                    Response.Write("获取vip信息");
                    Response.Write("</br>");
                    //
                    string strGotoUrl = "";//"/OnlineClothing/tmpGoUrl.html?customerId=" + customerId + "&openId=" + OpenId + "&userId=" + vipId + "&backUrl=" + HttpUtility.UrlEncode(goUrl) + "";
                    Random rad = new Random();

                    if (goUrl.IndexOf("?") > 0)
                    {
                        strGotoUrl = goUrl + "&customerId=" + customerId + "&applicationId=" + applicationId + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    else
                    {
                        strGotoUrl = goUrl + "?customerId=" + customerId + "&applicationId=" + applicationId + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    Response.Cookies["openId_" + customerId].Value = openId;
                    Response.Cookies["userId_" + customerId].Value = vipInfo.VIPID;
                    Response.Write(strGotoUrl);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("有会员信息时的跳转URL：{0}", strGotoUrl)
                    });
                    //在这里建立上下线关系
                    //SetShareVip(vipInfotmp.VIPID);
                    SetShareVip(vipInfo.VIPID, openId);
                    Response.Redirect(strGotoUrl);
                }
                #endregion

            }

        }

        //
        /// <summary>
        /// 处理会员的上下线关系
        /// 会员状态        已经有上级关系    有新的附带上级关系  
        /// 从未关注的Vip          1                  1             重建关系
        /// 从未关注的Vip          0                  1             重建关系
        /// 取消关注的Vip          0                  1             重建关系

        /// 从未关注的Vip          0                  0               
        /// 从未关注的Vip          1                  0
        /// 取消关注的Vip          1                  0 
        /// 取消关注的Vip          0                  0 
        /// 取消关注的Vip          1                  1

        /// ShareVipID 没内容的 return
        public void SetShareVip(string vipid,string openId)
        {
            #region 验证
            if (string.IsNullOrEmpty(ShareVipID))//如果没有上级分享人员
            {
                return;
            }
            #endregion

            #region Vip实体
            VipBLL vipBll = new VipBLL(loggingSessionInfo);
            VipEntity vipInfotmp = null;
            if (string.IsNullOrEmpty(vipid))
            {
                return;
            }
            else
            {
                vipInfotmp = vipBll.GetByID(vipid);
            }
            if (vipInfotmp == null)
            {
                return;
            }
            #endregion

            #region UnitId
            string UnitId = "";
            //获取分享人的门店信息
            //员工 或者 客服

           string shareVipOpenid="";
            if (SourceId == 1 || SourceId == 2)//获取分享员工的默认门店
            {
                UnitId = vipBll.GetUnitByUserId(ShareVipID);//获取员工的默认门店
            }
            else
            {
                //获取分享会员的门店
                VipEntity shareVip = vipBll.GetByID(ShareVipID);
                if (shareVip != null)
                {
                    UnitId = shareVip.CouponInfo;//会员的会籍店
                    shareVipOpenid=shareVip.WeiXinUserId;
                }
            }
            #endregion

            #region 判断用户类型

            #region 关注的
            if (vipInfotmp.Status >= 1)//关注过的
            {
                //当前没有上线，才给他建立一个上线
                if (string.IsNullOrEmpty(vipInfotmp.SetoffUserId) && string.IsNullOrEmpty(vipInfotmp.HigherVipID) && string.IsNullOrEmpty(vipInfotmp.Col20))
                {
                    //会员 或者 客服
                    if (SourceId == 1 || SourceId == 2)//获取分享员工的默认门店
                    {
                        vipInfotmp.SetoffUserId = ShareVipID;
                        vipInfotmp.Col21 = System.DateTime.Now.ToString();
                    }
                    else if (SourceId == 4)
                    {
                        vipInfotmp.Col20 = ShareVipID;
                        vipInfotmp.Col21 = System.DateTime.Now.ToString();
                    }
                    //会员
                    else
                    {
                        vipInfotmp.HigherVipID = ShareVipID;
                        vipInfotmp.Col21 = System.DateTime.Now.ToString();
                    }
                }
            }
            #endregion

            #region 取消关注的
            else if (vipInfotmp.Status == 0 && vipInfotmp.Col25 == "1")//取消关注的
            {
                if (string.IsNullOrEmpty(vipInfotmp.SetoffUserId) && string.IsNullOrEmpty(vipInfotmp.HigherVipID) && string.IsNullOrEmpty(vipInfotmp.Col20))
                {
                    if (SourceId == 1 || SourceId == 2)//获取分享员工的默认门店
                    {
                        vipInfotmp.SetoffUserId = ShareVipID;
                        vipInfotmp.HigherVipID = null;
                        vipInfotmp.Col20 = "";
                        vipInfotmp.Col21 = System.DateTime.Now.ToString();
                    }
                    else if (SourceId == 4)
                    {
                        vipInfotmp.Col20 = ShareVipID;
                        vipInfotmp.HigherVipID = "";
                        vipInfotmp.SetoffUserId = "";
                        vipInfotmp.Col21 = System.DateTime.Now.ToString();
                    }
                    else
                    {
                        vipInfotmp.HigherVipID = ShareVipID;
                        vipInfotmp.SetoffUserId = null;
                        vipInfotmp.Col20 = "";
                        vipInfotmp.Col21 = System.DateTime.Now.ToString();
                    }
                }

            }
            #endregion

            #region 未关注的
            else
            {   //未关注的（oauth认证获取的）
                //客服 员工
                if (SourceId == 1 || SourceId == 2)//获取分享员工的默认门店
                {
                    vipInfotmp.SetoffUserId = ShareVipID;
                    vipInfotmp.HigherVipID = null;
                    vipInfotmp.Col20 = "";
                    vipInfotmp.Col21 = System.DateTime.Now.ToString();
                }
                else if (SourceId == 4)
                {
                    vipInfotmp.Col20 = ShareVipID;
                    vipInfotmp.HigherVipID = "";
                    vipInfotmp.SetoffUserId = "";
                    vipInfotmp.Col21 = System.DateTime.Now.ToString();
                }
                //会员
                else
                {
                    vipInfotmp.HigherVipID = ShareVipID;
                    vipInfotmp.SetoffUserId = null;
                    vipInfotmp.Col20 = "";
                    vipInfotmp.Col21 = System.DateTime.Now.ToString();
                }
            }
            #endregion

            #endregion

            if (string.IsNullOrEmpty(UnitId))
            {
                UnitService unitServer = new UnitService(loggingSessionInfo);
                UnitId=unitServer.GetUnitByUnitTypeForWX("总部", null).Id; //获取总部门店标识                
            }
            vipInfotmp.CouponInfo = UnitId;
            vipInfotmp.Col24 = ObjectID;
            vipInfotmp.Col23 = SourceId.ToString();
            vipBll.Update(vipInfotmp,false);

            //分享记录
            T_LEventsSharePersonLogBLL t_LEventsSharePersonLogBLL = new T_LEventsSharePersonLogBLL(loggingSessionInfo);

            //先查看这个会员之前是否已经打开这个图文素材
            var t_LEventsSharePersonLogTemp = new T_LEventsSharePersonLogEntity();
            t_LEventsSharePersonLogTemp.BusTypeCode = objectType;
            t_LEventsSharePersonLogTemp.ObjectId = ObjectID;////分享的链接代表的对象，活动或者商品
            t_LEventsSharePersonLogTemp.ShareVipType = SourceId;// 1员工，2客服，3会员  4超级分销商         
            t_LEventsSharePersonLogTemp.ShareVipID = ShareVipID;
            t_LEventsSharePersonLogTemp.BeShareVipID = vipid;//被分享人
            var t_LEventsSharePersonLogOld = t_LEventsSharePersonLogBLL.QueryByEntity(t_LEventsSharePersonLogTemp,null);
            //如果已经有这样的记录，就不要再写了
            if (t_LEventsSharePersonLogOld != null && t_LEventsSharePersonLogOld.Length > 0)
            {
                return;
            }


         //第一次打开的时候才创建
            var t_LEventsSharePersonLog = new T_LEventsSharePersonLogEntity();
            t_LEventsSharePersonLog.SharePersonLogId = Guid.NewGuid();
            t_LEventsSharePersonLog.BusTypeCode = objectType;         
            t_LEventsSharePersonLog.ObjectId = ObjectID;////分享的链接代表的对象，活动或者商品
            t_LEventsSharePersonLog.ShareVipType = SourceId;// 1员工，2客服，3会员           
            t_LEventsSharePersonLog.ShareVipID = ShareVipID;

            t_LEventsSharePersonLog.ShareOpenID =shareVipOpenid;//如果是会员，取出来
            t_LEventsSharePersonLog.BeShareVipID = vipid;//新建的会员会员的vipid
             t_LEventsSharePersonLog.BeShareOpenID = openId;//本分享人的id
          
            t_LEventsSharePersonLog.ShareURL = goUrl;//分享的链接
            t_LEventsSharePersonLog.CreateTime = System.DateTime.Now;
            t_LEventsSharePersonLog.CreateBy = "";
            t_LEventsSharePersonLog.LastUpdateBy = "";
            t_LEventsSharePersonLog.LastUpdateTime = System.DateTime.Now;
            t_LEventsSharePersonLog.CustomerId = loggingSessionInfo.ClientID;
            t_LEventsSharePersonLog.IsDelete = 0;
            t_LEventsSharePersonLogBLL.Create(t_LEventsSharePersonLog);



        }

    }
}