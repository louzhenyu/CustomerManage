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
    public partial class AuthCodeReques : System.Web.UI.Page
    {
        public string customerId = string.Empty;//"29E11BDC6DAC439896958CC6866FF64E";
        public string applicationId = string.Empty;//"24F084EDA94648E4BEFBDB11597EC42A";
        public string goUrl = string.Empty;
        public string scope = string.Empty;
        public string pageName = string.Empty;
        public string strAppId = string.Empty;//"wxeebb52e0aa813101"
        public string strAppSecret = string.Empty; //"22ac924a92e6caf176d6ba426d744adb";
        public string strWeiXinId = string.Empty;   //Jermyn20140604 微信公众号码 

        //public string customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";
        //public string applicationId = "24F084EDA94648E4BEFBDB11597EC42A";
        //public string goUrl = "dev.o2omarketing.cn:9004/_pageName_";
        //public string pageName = "GoodsList";
        //public string strAppId = "wxeebb52e0aa813101";
        //public string strAppSecret = "22ac924a92e6caf176d6ba426d744adb";
       
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
                            Message = "State-Jermyn20140923：" + state
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
                        state = HttpUtility.UrlDecode(state, Encoding.UTF8);
                        string[] array = state.Split(',');
                        customerId = array[1];
                        applicationId = array[2];
                        goUrl = array[0];
                        scope = array[3];
                        if(array.Length>=5)
                            this.pageName = array[4];
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("3x:解析出回传的state的值：customerid={0};applicationid={1};goUrl={2};pageName={3};",customerId,applicationId,goUrl,pageName)
                        });
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
                loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                GetKeyByApp();
                string code = Request["code"];
                if (code == null || code.Equals(""))
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
                    string openId = authBll.GetAccessToken(code, strAppId, strAppSecret, loggingSessionInfo,iRad,out token);
                    //scope = ''
                    
                    Response.Write("<br>");
                    Response.Write("OpenID:" + openId);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "3xx: OpenID:" + openId
                    });
                    PageRedict(openId,token);
                }
                #endregion
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
            }
        }
        #endregion

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="openId"></param>
        private void PageRedict(string openId,string token)
        {

            if (goUrl.IndexOf("HtmlApp/Lj/auth.html?pageName=GoodsList")>0)
            {
                goUrl = goUrl.Replace("HtmlApp/Lj/auth.html?pageName=GoodsList", "HtmlApps/auth.html?pageName=GoodsList");
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("url替换：old={0};new={1};", "HtmlApp/Lj/auth.html?pageName=GoodsList", "HtmlApps/auth.html?pageName=GoodsList")
                });
            }
            if (goUrl.IndexOf("HtmlApp/Lj/auth.html?pageName=MyOrder") > 0)
            {
                goUrl = goUrl.Replace("HtmlApp/Lj/auth.html?pageName=MyOrder", "HtmlApps/auth.html?pageName=MyOrder");
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("url替换：old={0};new={1};", "HtmlApp/Lj/auth.html?pageName=MyOrder", "HtmlApps/auth.html?pageName=MyOrder")
                });
            }
            if (goUrl.IndexOf("HtmlApp/Lj/auth.html?pageName=JiFenShop") > 0)
            {
                goUrl = goUrl.Replace("HtmlApp/Lj/auth.html?pageName=JiFenShop", "HtmlApps/auth.html?pageName=JiFenShop");
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("url替换：old={0};new={1};", "HtmlApp/Lj/auth.html?pageName=JiFenShop", "HtmlApps/auth.html?pageName=JiFenShop")
                });
            }

            var decodeUrl = HttpUtility.UrlDecode(goUrl);
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
            if (openId == null || openId.Equals(""))
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

                vipInfo = authBll.GetUserIdByOpenId(loggingSessionInfo, openId);
                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format("66x: openId：{0};", "进来了" + openId + "--openId")
                //});
                if (vipInfo == null || vipInfo.VIPID.Equals("") || vipInfo.Status == 0)
                {
                    //Loggers.Debug(new DebugLogInfo()
                    //{
                    //    Message = string.Format("77x: openId：{0};", "进来了" + openId + "--openId")
                    //});
                    Response.Write("会员不存在或没有关注");
                    VipEntity vipInfotmp = new VipEntity();
                    if (scope.Equals("1"))
                    {
                        //封装方法，调用第四步，根据第四步的数据，新增一条vip数据
                        if (null == vipInfo || vipInfo.VIPID.Equals(""))
                        {
                            vipInfotmp.VIPID = authBll.SetVipInfoByToken(token, openId, loggingSessionInfo, this.Response);
                        }
                        else
                        {
                            vipInfo.IsDelete = 0;
                            vipInfo.Status = 1;
                            vipServer.Update(vipInfo);
                            vipInfotmp.VIPID = vipInfo.VIPID;
                        }
                    }
                    else
                    {
                        #region Jermyn20140604,会员没有关注，先采集信息
                        
                        if (vipInfo == null || vipInfo.VIPID == null)
                        {
                            
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
                        }
                        else
                        {
                            vipInfotmp.VIPID = vipInfo.VIPID;
                        }
                        #endregion
                    }
                    //Loggers.Debug(new DebugLogInfo()
                    //{
                    //    Message = string.Format("777x: openId：{0};", "进来了" + openId + "--openId")
                    //});
                    Random rad = new Random();
                    if (goUrl.IndexOf("?") > 0)
                    {
                        goUrl = goUrl + "&customerId=" + customerId + "&openId=" + openId + "&userId=" + vipInfotmp.VIPID + "&CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    else
                    {
                        goUrl = goUrl + "?customerId=" + customerId + "&openId=" + openId + "&userId=" + vipInfotmp.VIPID + "&CheckOAuth=unAttention" + "&rid=" + rad.Next(1000, 100000) + "";
                    }
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("会员没有关注时的跳转URL：{0}", goUrl)
                    });
                    Response.Redirect(goUrl);
                }
                else
                {
                    //Loggers.Debug(new DebugLogInfo()
                    //{
                    //    Message = string.Format("88x: openId：{0};", "进来了" + openId + "--openId")
                    //});
                    Response.Write("获取vip信息");
                    Response.Write("</br>");
                    //
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
                    Response.Write(strGotoUrl);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("有会员信息时的跳转URL：{0}", strGotoUrl)
                    });
                    Response.Redirect(strGotoUrl);
                }
            }

        }
    }
}