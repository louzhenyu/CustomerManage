using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using System.Data;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.Lj.Interface
{
    public partial class RegisterData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;

            try
            {
                string dataType = Request["action"].ToString().Trim();

                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);

                switch (dataType)
                {
                    case "getIsRegistered":
                        content = GetIsRegistered();
                        break;
                    case "getIsRegisterRequired":
                        content = GetIsRegisterRequired();
                        break;
                    case "sendCode":
                        content = SendCode();
                        break;
                    case "register":
                        content = Register();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region GetIsRegistered
        /// <summary>
        /// 获取用户是否注册接口
        /// </summary>
        public string GetIsRegistered()
        {
            string content = string.Empty;
            var respData = new GetIsRegisteredRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<Default.ReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetIsRegistered: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "GetIsRegistered", reqObj, respData, reqObj.ToJSON());

                respData.content = new GetIsRegisteredRespContentData();
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                var obj = vipBLL.GetByID(reqObj.common.userId);
                if (obj != null && obj.Status == 2)
                    respData.content.IsRegistered = 1;
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
        #endregion

        #region GetIsRegisterRequired
        /// <summary>
        /// 获取该页面是否需注册
        /// </summary>
        public string GetIsRegisterRequired()
        {
            string content = string.Empty;
            var respData = new GetIsRegisterRequiredRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetIsRegisterRequiredReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetIsRegisterRequired: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "GetIsRegisterRequired", reqObj, respData, reqObj.ToJSON());

                respData.content = new GetIsRegisterRequiredRespContentData();
                var registerRequiredPageBLL = new RegisterRequiredPageBLL(loggingSessionInfo);
                var obj = registerRequiredPageBLL.GetDataByPageName(reqObj.special.pageName);

                if (obj != null)
                    respData.content.IsRegisterRequired = obj.IsRegisterRequired.Value;

                //int.TryParse(obj.User_Status, out respData.content.IsRegistered);
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

        public class GetIsRegisteredRespData : Default.LowerRespData
        {
            public GetIsRegisteredRespContentData content;
        }
        public class GetIsRegisteredRespContentData
        {
            public int IsRegistered;
        }

        public class GetIsRegisterRequiredReqData : Default.ReqData
        {
            public GetIsRegisterRequiredReqSpecialData special;
        }
        public class GetIsRegisterRequiredReqSpecialData
        {
            public string pageName;
        }

        public class GetIsRegisterRequiredRespData : Default.LowerRespData
        {
            public GetIsRegisterRequiredRespContentData content;
        }
        public class GetIsRegisterRequiredRespContentData
        {
            public int IsRegisterRequired;
        }
        #endregion

        #region sendCode
        /// <summary>
        /// 发送验证码
        /// </summary>
        public string SendCode()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<SendCodeReqData>();

                if (reqObj.special.mobile != "")
                {
                    var loggingSessionInfo = Default.GetLjLoggingSession();

                    Default.WriteLog(loggingSessionInfo, "sendCode", reqObj, respData, reqObj.ToJSON());

                    Random rd = new Random();
                    string code = rd.Next(100000, 999999).ToString();

                    RegisterValidationCodeBLL registerValidationCodeBLL = new RegisterValidationCodeBLL(loggingSessionInfo);
                    string result = registerValidationCodeBLL.SendCode(reqObj.special.mobile);

                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("sendCode: {0}, result = {1}", reqContent, result)
                    });

                    switch (result)
                    {
                        case "101":
                            respData.code = result;
                            respData.description = "验证码发送失败，请稍候再试。";
                            break;
                        case "102":
                            respData.code = result;
                            respData.description = "该手机号码已被注册，请修改手机号码。";
                            break;
                        default:
                            break;
                    }
                    
                }
                else
                {
                    respData.code = "102";
                    respData.description = "手机号码不能为空！";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();

                Loggers.Exception(new ExceptionLogInfo() {
                    ErrorMessage = string.Format("sendCode: {0}", ex.ToJSON())
                });
            }
            content = respData.ToJSON();
            return content;
        }
        #endregion

        public class SendCodeReqData : Default.ReqData
        {
            public SendCodeReqSpecialData special;
        }
        public class SendCodeReqSpecialData
        {
            public string mobile;
        }

        #region Register
        /// <summary>
        /// 用户否注册接口
        /// </summary>
        public string Register()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<RegisterReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("Register: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "Register", reqObj, respData, reqObj.ToJSON());

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                string result = vipBLL.Register(reqObj.common.userId, reqObj.special.mobile, reqObj.special.name, reqObj.special.code, reqObj.common.customerId);

                switch (result)
                {
                    case "101":
                        respData.code = result;
                        respData.description = "验证码验证失败，请重试。";
                        break;
                    case "102":
                        respData.code = result;
                        respData.description = "无法找到用户信息，请重试。";
                        break;
                    default:
                        break;
                }
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
        #endregion

        public class RegisterReqData : Default.ReqData
        {
            public RegisteReqSpecialData special;
        }
        public class RegisteReqSpecialData
        {
            public string name;
            public string mobile;
            public string code;
        }

    }
}