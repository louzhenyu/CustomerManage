using System;

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL.CS;
using JIT.Utility.Log;
using JIT.CPOS.Web.Interface.Data.Base;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler
{
    /// <summary>
    /// 处理提交预约单的Handler
    /// </summary>
    [Export(typeof(IGreeRequestHandler))]
    [ExportMetadata("Action", "SubmitAppOrder")]
    public class SubmitAppOrderHandler : IGreeRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SubmitAppOrder(pRequest);
        }

        /// <summary>
        /// 提交预约订单
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string SubmitAppOrder(string pRequest)
        {
            var rd = new APIResponse<SubmitAppOrderRD>();
            var rdData = new SubmitAppOrderRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<SubmitAppOrderRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            #region 提交预约单

            //根据订单号获取订单ID
            GLProductOrderBLL glpobll = new GLProductOrderBLL(loggingSessionInfo);
            GLProductOrderEntity glpoe = glpobll.GetProductOrderByOrderNo(rp.CustomerID, rp.Parameters.OrderNO);
            //更新客户VipID
            //if (glpoe != null && !string.IsNullOrEmpty(rp.Parameters.VipID))
            if (glpoe != null && !string.IsNullOrEmpty(rp.UserID))
            {
                glpoe.VipID = rp.UserID;
                glpoe.CustomerGender = rp.Parameters.Gender;
                glpoe.CustomerName = rp.Parameters.Surname;
                glpobll.Update(glpoe);
            }

            //插入预约
            GLServiceOrderBLL glsobll = new GLServiceOrderBLL(loggingSessionInfo);
            GLServiceOrderEntity glsoe = glsobll.GetGLServiceOrderEntityByOrderID(rp.CustomerID, glpoe.ProductOrderID);
            ServiceOrderDataAccess orderManager = new ServiceOrderDataAccess(loggingSessionInfo);

            if (glsoe == null)
                glsoe = new GLServiceOrderEntity();

            //订单ID
            glsoe.ProductOrderID = glpoe.ProductOrderID;
            glsoe.ServiceType = rp.Parameters.ServiceType;
            glsoe.ServiceDate = rp.Parameters.ServiceOrderDate;
            glsoe.ServiceDateEnd = rp.Parameters.ServiceOrderDateEnd;
            glsoe.ServiceAddress = rp.Parameters.ServiceAddress;
            glsoe.CustomerMessage = rp.Parameters.Msg;
            glsoe.Latitude = Convert.ToDecimal(rp.Parameters.Latitude);
            glsoe.Longitude = Convert.ToDecimal(rp.Parameters.Longitude);
            glsoe.CustomerID = rp.CustomerID;

            if (!string.IsNullOrEmpty(glsoe.ServiceOrderID))
            {
                //删除预约单
                glsobll.Update(glsoe);
                //删除预约单设备
                orderManager.DeleteInstallDeviceList(glsoe.CustomerID, glsoe.ServiceOrderID);
            }
            else
            {
                glsoe.ServiceOrderID = GreeCommon.GetGuid();
                glsobll.Create(glsoe);
            }

            //插入安装设备
            orderManager.SaveInstallDeviceList(glsoe, rp.Parameters.DeviceList);

            //预约单管理
            //SubscribeOrderViewModel order = orderManager.GetSubscribeOrder(rp.CustomerID, rp.Parameters.OrderNO);
            SubscribeOrderViewModel order = orderManager.GetSubscribeOrderDetail(rp.CustomerID, glsoe.ServiceOrderID);
            ServiceOrderManager.Instance.AddServiceOrder(order);

            rdData.ServiceOrderNO = glsoe.ServiceOrderID;
            #endregion


            #region 推送信息给服务师傅IOS,Android消息推送
            GLServicePersonInfoBLL glspibll = new GLServicePersonInfoBLL(loggingSessionInfo);
            DataSet dsPerson = glspibll.GetServicePerson(rp.CustomerID, string.Empty);
            string msg = rp.Parameters.Msg;
            if (dsPerson != null && dsPerson.Tables != null && dsPerson.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsPerson.Tables[0].Rows)
                {
                    new PushIOSMessage(loggingSessionInfo).PushMessage(row["ServicePersonID"].ToString(), msg);
                    new PushAndroidMessage(loggingSessionInfo).PushMessage(row["ServicePersonID"].ToString(), msg);
                }
            }
            #endregion

            rd.Data = rdData;
            rd.ResultCode = 0;

            return rd.ToJSON();
        }


        #region  APP push devicetoken
        public string SetIOSDeviceToken(string reqContent)
        {
            var rd = new APIResponse<SetIOSDeviceTokenRD>();

            try
            {
                #region
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUp: {0}", reqContent)
                });

                //解析请求字符串
                var rp = reqContent.DeserializeJSONTo<APIRequest<SetIOSDeviceTokenRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                string customerId = string.Empty;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(rp.CustomerID))
                {
                    customerId = rp.CustomerID;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, rp.UserID);

                if (rp.Parameters.special == null)
                {
                    rp.Parameters.special = new setIOSBasicReqSpecialData();
                }
                if (rp.Parameters.special == null)
                {
                    rd.ResultCode = 102;
                    rd.Message = "没有特殊参数";
                    return rd.ToJSON().ToString();
                }

                if (rp.Parameters.special.deviceToken == null || rp.Parameters.special.deviceToken.Equals(""))
                {
                    rd.ResultCode = 2201;
                    rd.Message = "deviceToken不能为空";
                    return rd.ToJSON().ToString();
                }
                if (rp.UserID == null || rp.UserID.Equals(""))
                {
                    rd.ResultCode = 2202;
                    rd.Message = "userId不能为空";
                    return rd.ToJSON().ToString();
                }
                #endregion

                PushUserBasicBLL service = new PushUserBasicBLL(loggingSessionInfo);
                PushUserBasicEntity basicInfo = new PushUserBasicEntity();
                basicInfo = service.GetByID(ToStr(rp.UserID));
                if (basicInfo == null)
                {
                    PushUserBasicEntity basicInfo1 = new PushUserBasicEntity();
                    basicInfo1.UserId = ToStr(rp.UserID);
                    basicInfo1.DeviceToken = ToStr(rp.Parameters.special.deviceToken);
                    basicInfo1.CustomerId = ToStr(rp.CustomerID);
                    service.Create(basicInfo1);
                }
                else
                {
                    basicInfo.DeviceToken = ToStr(rp.Parameters.special.deviceToken);
                    service.Update(basicInfo);
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.ToString();
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", rd.Message)
            });

            return rd.ToJSON();
        }

        #region  RD & RP
        public class SetIOSDeviceTokenRD : IAPIResponseData
        {
            public string deviceToken { get; set; }
        }

        public class SetIOSDeviceTokenRP : IAPIRequestParameter
        {
            public setIOSBasicReqSpecialData special;

            #region IAPIRequestParameter 成员

            public void Validate()
            {
                throw new NotImplementedException();
            }

            #endregion
        }
        public class setIOSBasicReqSpecialData
        {
            public string deviceToken { get; set; }
        }

        #endregion


        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }
        #endregion

        #region  APP push Android basic  android登录的时候调用，注册应用
        public string setAndroidBasic(string reqContent)
        {
            string content = string.Empty;
            var respData = new setAndroidBasicRespData();
            try
            {
                #region
                //解析请求字符串
                var rp = reqContent.DeserializeJSONTo<APIRequest<setAndroidBasicReqData>>();

                //判断客户ID是否传递
                string customerId = string.Empty;
                if (!string.IsNullOrEmpty(rp.CustomerID))
                {
                    customerId = rp.CustomerID;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (rp.Parameters.special == null)
                {
                    rp.Parameters.special = new setAndroidBasicReqSpecialData();
                }
                if (rp.Parameters.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (rp.Parameters.special.channelId == null || rp.Parameters.special.channelId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "channelId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (rp.Parameters.special.appId == null || rp.Parameters.special.appId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "appId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (rp.Parameters.special.userId == null || rp.Parameters.special.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "百度的userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (rp.UserID == null || rp.UserID.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                PushAndroidBasicBLL service = new PushAndroidBasicBLL(loggingSessionInfo);
                PushAndroidBasicEntity basicInfo = new PushAndroidBasicEntity();
                basicInfo = service.GetByID(ToStr(rp.UserID));
                if (basicInfo == null)
                {
                    PushAndroidBasicEntity basicInfo1 = new PushAndroidBasicEntity();
                    basicInfo1.UserID = ToStr(rp.UserID);
                    basicInfo1.Channel = "1";
                    basicInfo1.ChannelIDBaiDu = ToStr(rp.Parameters.special.channelId);
                    basicInfo1.CustomerId = ToStr(customerId);
                    basicInfo1.BaiduPushAppID = ToStr(rp.Parameters.special.appId);
                    basicInfo1.UserIDBaiDu = ToStr(rp.Parameters.special.userId);
                    service.Create(basicInfo1);
                }
                else
                {
                    basicInfo.ChannelIDBaiDu = ToStr(rp.Parameters.special.channelId);
                    basicInfo.BaiduPushAppID = ToStr(rp.Parameters.special.appId);
                    basicInfo.UserIDBaiDu = ToStr(rp.Parameters.special.userId);
                    service.Update(basicInfo, false);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }
        #region
        public class setAndroidBasicRespData : Default.LowerRespData
        {
        }

        public class setAndroidBasicReqData : IAPIRequestParameter
        {
            public setAndroidBasicReqSpecialData special;

            #region IAPIRequestParameter 成员

            public void Validate()
            {
                throw new NotImplementedException();
            }

            #endregion
        }
        public class setAndroidBasicReqSpecialData
        {
            public string appId { get; set; }
            public string channelId { get; set; }
            public string userId { get; set; }
        }
        #endregion
        #endregion
    }



    #region 提交预约单
    public class SubmitAppOrderRP : IAPIRequestParameter
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { set; get; }
        /// <summary>
        /// 1:安装，2:维修
        /// </summary>
        public int? ServiceType { set; get; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? ServiceOrderDate { set; get; }
        /// <summary>
        /// 预约结束日期
        /// </summary>
        public DateTime? ServiceOrderDateEnd { set; get; }
        /// <summary>
        /// 安装地址
        /// </summary>
        public string ServiceAddress { set; get; }
        /// <summary>
        /// 发送给安装师傅的消息
        /// </summary>
        public string Msg { set; get; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { set; get; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string Surname { set; get; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { set; get; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { set; get; }

        public string VipID { set; get; }

        /// <summary>
        /// 要安装的设备列表
        /// </summary>
        public List<InstallDeviceViewModel> DeviceList { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(OrderNO))
                throw new APIException("【OrderNO】不能为空") { ErrorCode = 101 };
            if (ServiceType == null)
                throw new APIException("【ServiceType】不能为空") { ErrorCode = 101 };
            if (ServiceOrderDate == null)
                throw new APIException("【ServiceOrderDate】不能为空") { ErrorCode = 101 };
            if (ServiceOrderDateEnd == null)
                throw new APIException("【ServiceOrderDateEnd】不能为空") { ErrorCode = 101 };
            if (string.IsNullOrEmpty(ServiceAddress))
                throw new APIException("【ServiceAddress】不能为空") { ErrorCode = 101 };
            if (Gender == null)
                throw new APIException("【Gender】不能为空") { ErrorCode = 101 };
            if (string.IsNullOrEmpty(Surname))
                throw new APIException("【Surname】不能为空") { ErrorCode = 101 };
            if (DeviceList == null && DeviceList.Count <= 0)
                throw new APIException("【DeviceList】不能为空") { ErrorCode = 101 };
            if (Longitude == null)
                throw new APIException("【Longitude】不能为空") { ErrorCode = 101 };
            if (Latitude == null)
                throw new APIException("【Latitude】不能为空") { ErrorCode = 101 };

        }
    }

    public class SubmitAppOrderRD : IAPIResponseData
    {
        /// <summary>
        /// 预约单号
        /// </summary>
        public string ServiceOrderNO { set; get; }
    }
    #endregion

}