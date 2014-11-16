using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.WifiSign.Request;
using JIT.CPOS.DTO.Module.VIP.WifiSign.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Reflection;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.Entity;
using System.Configuration;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.WifiSign
{
    /// <summary>
    /// 获取表单列表
    /// </summary>
    public class WifiSignAH : BaseActionHandler<WifiSignRP, WifiSignRD>
    {

        #region 错误码
        const int ERROR_FAILURE = 330;
        const int ERROR_URL_ISNULL = 110;
        #endregion

        protected override WifiSignRD ProcessRequest(DTO.Base.APIRequest<WifiSignRP> pRequest)
        {

            //调试日志1
            Loggers.Debug(new DebugLogInfo
            {
                Message = "<1>VIP.WifiSign.WifiSign接口开始执行___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });


            WifiSignRD rd = new WifiSignRD();

            pRequest.CustomerID = ConfigurationManager.AppSettings["WiFiCustomerID"].Trim();
            pRequest.Parameters.Validate();

            string wiFiUser = ConfigurationManager.AppSettings["WiFiUser"].Trim();
            string userSession = ConfigurationManager.AppSettings["UserSession"].Trim();
            string userLoginOk = ConfigurationManager.AppSettings["UserLoginOk"].Trim();

            if (string.IsNullOrEmpty(pRequest.UserID))
                throw new Exception("UserID不能为空");
            if (string.IsNullOrEmpty(wiFiUser))
                throw new APIException("请配置WiFiUser对应的URL请求路径") { ErrorCode = ERROR_URL_ISNULL };
            if (string.IsNullOrEmpty(userSession))
                throw new APIException("请配置UserSession对应的URL请求路径") { ErrorCode = ERROR_URL_ISNULL };
            if (string.IsNullOrEmpty(userLoginOk))
                throw new APIException("请配置UserLoginOk对应的URL请求路径") { ErrorCode = ERROR_URL_ISNULL };

            #region 获取表单列表
            try
            {

                //调试日志2
                Loggers.Debug(new DebugLogInfo
                {
                    Message = "<2>获取SK参数值：" + pRequest.Parameters.DeviceID + ",获取用户ID参数值：" + pRequest.UserID + ",开始执行第三方接口___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });


                #region 调用第三方接口登录认证获取设备ID和连接时间
                //调用第三方接口通知WiFiCenter登录成功
                //RequestGet(string.Format(userLoginOk, pRequest.Parameters.deviceId));


                //调试日志3
                //Loggers.Debug(new DebugLogInfo
                //{
                //    Message = "<3>调用第三方接口通知WiFiCenter登录成功(" + string.Format(userLoginOk, pRequest.Parameters.deviceId) + "通过)___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                //});


                //调用第三方接口从WiFiCenter获取指定WiFiCenterSessionKey对应的用户信息
                string json = RequestGet(string.Format(wiFiUser, pRequest.Parameters.DeviceID));


                //调试日志4
                Loggers.Debug(new DebugLogInfo
                {
                    Message = "<4>调用第三方接口从WiFiCenter获取指定WiFiCenterSessionKey对应的用户信息(" + string.Format(wiFiUser, pRequest.Parameters.DeviceID) + "通过,返回值：" + json + ")___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });


                //调用第三方接口根据指定的WiFiCenterSessionKey获得用户会话信息
                string json2 = RequestGet(string.Format(userSession, pRequest.Parameters.DeviceID));


                //调试日志5
                Loggers.Debug(new DebugLogInfo
                {
                    Message = "<5>调用第三方接口根据指定的WiFiCenterSessionKey获得用户会话信息(" + string.Format(userSession, pRequest.Parameters.DeviceID) + "通过,返回值：" + json2 + ")___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });


                JavaScriptSerializer serializer = new JavaScriptSerializer();
                WiFiUser info = serializer.Deserialize<WiFiUser>(json);
                WiFiUserSession info2 = serializer.Deserialize<WiFiUserSession>(json2);


                //调试日志6
                Loggers.Debug(new DebugLogInfo
                {
                    Message = "<6>反序列化通过，获取节点编号：" + info.NodeSn + "，连接时间：" + info2.TimeSessionCreated + "___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });



                //节点编号
                string nodeSn = info.NodeSn;
                //连接时间
                DateTime beginTime = info2.TimeSessionCreated;

                if (string.IsNullOrEmpty(nodeSn))
                    throw new APIException("获取节点编号为空") { ErrorCode = ERROR_URL_ISNULL };
                if (beginTime == null)
                    throw new APIException("获取会话时间为空") { ErrorCode = ERROR_URL_ISNULL };
                #endregion

                #region 保存用户连接WiFi相关数据
                //WiFi设备表
                WiFiDeviceEntity wiFiInfo = new WiFiDeviceBLL(base.CurrentUserInfo).GetByNodeSn(nodeSn);

                if (wiFiInfo == null)
                    throw new Exception("无WiFi设备");

                if (wiFiInfo.DeviceID == null)
                    throw new Exception("无WiFi设备数据");

                //调试日志7
                Loggers.Debug(new DebugLogInfo
                {
                    Message = "<7>根据节点编号获取WiFi设备数据通过(设备ID：" + wiFiInfo.DeviceID == null ? "NULL" : wiFiInfo.DeviceID + ")，开始执行插入数据___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });

                string where = string.Format(" and  UnitID='{0}' and VIPID='{1}' ", wiFiInfo.UnitID, pRequest.UserID);
                var uvbll = new WiFiUserVisitBLL(base.CurrentUserInfo);

                if (uvbll.IsExists(where))
                {
                    //更新当前设备ID
                    WiFiUserVisitEntity visitInfo = uvbll.GetByWhere(where);
                    visitInfo.CurrentDeviceID = wiFiInfo.DeviceID;
                    uvbll.Update(visitInfo);

                    //扩展：更新或添加详细信息

                }
                else
                {

                    //用户连接WiFi表
                    WiFiUserVisitEntity visitInfo = new WiFiUserVisitEntity();
                    visitInfo.VisitID = Guid.NewGuid();
                    visitInfo.VIPID = pRequest.UserID;
                    visitInfo.UnitID = wiFiInfo.UnitID;
                    visitInfo.CurrentDeviceID = wiFiInfo.DeviceID;
                    visitInfo.CurrentDate = DateTime.Now;
                    visitInfo.BeginTime = DateTime.Parse(beginTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    visitInfo.CustomerID = CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                    uvbll.Create(visitInfo);


                    //调试日志8
                    Loggers.Debug(new DebugLogInfo
                    {
                        Message = "<8>用户连接WiFi表添加数据成功___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });


                    //用户连接WiFi详细信息表
                    WiFiUserVisitDetailEntity visitDetailInfo = new WiFiUserVisitDetailEntity();
                    visitDetailInfo.VisitDetailID = Guid.NewGuid();
                    visitDetailInfo.VisitID = visitInfo.VisitID;
                    visitDetailInfo.VIPID = pRequest.UserID;
                    visitDetailInfo.DeviceID = wiFiInfo.DeviceID;
                    visitDetailInfo.BeginTime = DateTime.Parse(beginTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    visitDetailInfo.CustomerID = CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                    new WiFiUserVisitDetailBLL(base.CurrentUserInfo).Create(visitDetailInfo);


                    //调试日志9
                    Loggers.Debug(new DebugLogInfo
                    {
                        Message = "<9>用户连接WiFi详细信息表添加数据成功___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });

                }


                #endregion

                #region 返回门店某个会员详细信息
                StringBuilder strWhere = new StringBuilder();

                if (!string.IsNullOrEmpty(pRequest.UserID) && !string.IsNullOrEmpty(pRequest.OpenID))
                {
                    strWhere.AppendFormat(" and (V.VIPID='{0}' or P.WeiXinUserId='{1}') ", pRequest.UserID, pRequest.OpenID);
                }
                else if (!string.IsNullOrEmpty(pRequest.UserID))
                {
                    strWhere.AppendFormat(" and V.VIPID='{0}' ", pRequest.UserID);
                }
                else if (!string.IsNullOrEmpty(pRequest.OpenID))
                {
                    strWhere.AppendFormat(" and P.WeiXinUserId='{0}' ", pRequest.OpenID);
                }
                if (!string.IsNullOrEmpty(wiFiInfo.DeviceID.ToString()))
                {
                    strWhere.AppendFormat(" and V.CurrentDeviceID='{0}' ", wiFiInfo.DeviceID);
                }

                var ds = uvbll.GetVipDetailList(strWhere.ToString());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rd.Items = DataLoader.LoadFrom<VipInfo>(ds.Tables[0]);
                }


                //调试日志10
                Loggers.Debug(new DebugLogInfo
                {
                    Message = "<10>VIP.WifiSign.WifiSign接口成功结束___" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });


                #endregion

            }
            catch (Exception ex)
            {

                Loggers.Exception(new ExceptionLogInfo(ex));
                //throw new APIException("查询数据错误") { ErrorCode = ERROR_FAILURE };
                throw new APIException(ex.Message) { ErrorCode = ERROR_FAILURE };
                
            }
            #endregion

            return rd;
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
                throw new APIException(e.Message) { ErrorCode = ERROR_FAILURE };
            }

            return page;
        }
        #endregion


    }

    public class WiFiUser
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        public string NodeSn { get; set; }
    }

    public class WiFiUserSession
    {
        /// <summary>
        /// 会话创建时间
        /// </summary>
        public DateTime TimeSessionCreated { get; set; }
    }

}