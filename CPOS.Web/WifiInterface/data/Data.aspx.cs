using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.WifiSign.Response;
using JIT.CPOS.Web.ApplicationInterface.Module.VIP.WifiSign;
using JIT.CPOS.Web.Module.Log.InterfaceWebLog;
using JIT.Utility.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;

using JIT.CPOS.BS.BLL.Module.NoticeEmail;
using JIT.Utility.Log;
using JIT.Utility.Notification;
using JIT.CPOS.Web.Module.XieHuiBao;


namespace JIT.CPOS.Web.WifiInterface.data
{
    public partial class Data : System.Web.UI.Page
    {

        public string SKey { get; protected set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Clear();
            //string content = string.Empty;
            //var userInfo = new SessionManager()
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["event"]))
                {
                    //string dataType = Request.QueryString["event"].Trim();
                    SKey = Request.QueryString["skey"] == null? string.Empty: Request.QueryString["skey"].Trim();
                    
                    string wiFiUser = ConfigurationManager.AppSettings["WiFiUser"].Trim();
                    string userSession = ConfigurationManager.AppSettings["UserSession"].Trim();
                    string userLoginOk = ConfigurationManager.AppSettings["UserLoginOk"].Trim();

                    if (string.IsNullOrEmpty(wiFiUser))
                        throw new APIException("请配置WiFiUser对应的URL请求路径");
                    if (string.IsNullOrEmpty(userSession))
                        throw new APIException("请配置UserSession对应的URL请求路径");
                    if (string.IsNullOrEmpty(userLoginOk))
                        throw new APIException("请配置UserLoginOk对应的URL请求路径");

                    RequestGet(string.Format(userLoginOk, SKey));
                    /*
                    if (dataType == "LoginBefore")
                    {
                        
                        var goUrl = ConfigurationManager.AppSettings["GoUrl"];
                        if (goUrl.EndsWith("/"))
                        {
                            goUrl = goUrl.TrimEnd('/');
                        }
                        var r = string.Format(
                            "{1}?pageName=Init&eventId=BFC41A8BF8564B6DB76AE8A8E43557BA&customerId=f6a7da3d28f74f2abedfc3ea0cf65c01&openId=oUcanjrJijlQhRsq3O1qYJH-MBHc&userId=ac32f9af5fec4bdc8eaf0e1c90e0fe48&rid=17425&DeviceID={0}",
                            strkey, goUrl);
                        
                        
                        var url =
                            string.Format(@"/WXOAuth/AuthUniversal.aspx?customerId={0}&applicationId={1}&goUrl={2}",
                                ConfigurationManager.AppSettings["WiFiCustomerID"],
                                ConfigurationManager.AppSettings["WiFiApplicationID"],
                                HttpUtility.UrlEncode(r));
                       
                        Loggers.Debug(new DebugLogInfo { Message = HttpUtility.UrlEncode(r) });

                        Loggers.Debug(new DebugLogInfo { Message = url });


                        //Response.Write(r);
                        Server.Transfer(url);
                        //Response.Redirect(r);
                        //Response.End();
                        return;
                        
                    }
                    else
                    {
                        Response.Write(string.Empty);
                        Response.End();
                        return;
                    }
                    */
                }
                else
                {
                   
                    throw new Exception("未传递的接口名action");
                }
            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.ContentEncoding = Encoding.UTF8;
                Response.Write(ex.Message);
                Response.End();
            }
        }

        #region GET操作方式
        /// <summary>
        /// GET操作方式
        /// </summary>
        /// <param name="TheURL">请求URL</param>
        /// <returns></returns>
        private string RequestGet(string TheURL)
        {
            Uri uri = new Uri(TheURL);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            string page;

            try
            {
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Proxy = WebRequest.GetSystemWebProxy();

                HttpWebResponse response;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }

                Stream responseStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));

                page = readStream.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new APIException(e.Message) { };
            }

            return page;
        }
        #endregion

        #region 新版接口 2014-05-09
        #region GetEventAlbumByAlbumID    4.7 咨询详细
        public string GetEventAlbumByAlbumID()
        {
            string res = string.Empty;
            string reqContent = string.Empty;
            RequestEntity<reqLEventsAlbumEntity> requestEntity = new RequestEntity<reqLEventsAlbumEntity>();
            if (!string.IsNullOrEmpty(Request["ReqContent"]))
            {
                reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<ReqData<getLEventsAlbumEntity>>();
                if (reqObj != null)
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(reqObj.common.customerId, "1");
                    reqLEventsAlbumEntity pReqAlbumsEntity = new DynamicInterfaceBLL(loggingSessionInfo).GetEventAlbumByAlbumID(reqObj);
                    if (pReqAlbumsEntity != null)
                    {
                        requestEntity.code = "200";
                        requestEntity.description = "操作成功";
                        requestEntity.content = pReqAlbumsEntity;
                    }
                    else
                    {
                        requestEntity.code = "1";
                        requestEntity.description = "操作失败";
                        requestEntity.content = null;
                    }
                }
            }
            res = requestEntity.ToJSON();
            return res;
        }
        #endregion
        #endregion

    }
}
