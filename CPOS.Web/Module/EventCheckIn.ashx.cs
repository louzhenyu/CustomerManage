using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.Module
{
    /// <summary>
    /// CheckIn 的摘要说明
    /// </summary>
    public class EventCheckIn : IHttpHandler
    {
        string customerId = "";
        string reqContent = "";

        public void ProcessRequest(HttpContext context)
        {
            reqContent = context.Request["ReqContent"];
            string action = context.Request["action"].ToString().Trim();
            string content = string.Empty;

            JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(context, context.Request, action);

            switch (action)
            {
                case "setSignUp":      //注册签到
                    content = SetSignUp();
                    break;
                default:
                    throw new Exception("未定义的接口:" + action);
            }

            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(content);
            context.Response.End();
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 注册
        public string SetSignUp()
        {
            string content = string.Empty;
            var respData = new SetSignUpRespData();
            try
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetSignUp: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<SetSignUpReqData>();

                if (reqObj.special == null)
                {
                    respData.code = "101";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "102";
                    respData.description = "电话不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                //#region 判断客户是否扫过活动二维码
                QRCodeScanLogBLL qRCodeScanLogBLL = new QRCodeScanLogBLL(loggingSessionInfo);
                if (!qRCodeScanLogBLL.CheckVipEventQRCode(reqObj.common.userId, reqObj.special.eventId))
                {
                    respData.code = "2206";
                    respData.description = "请先扫描本次活动的二维码";
                    return respData.ToJSON().ToString();
                }
                //#endregion

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                EventVipBLL eventVipBLL = new EventVipBLL(loggingSessionInfo);

                var tmpPhoneVip = vipBLL.GetVipByPhone(reqObj.special.phone, reqObj.common.userId, "2");

                Loggers.Debug(new DebugLogInfo() { Message = "tmpPhoneVip: " + tmpPhoneVip.ToJSON() });

                if (tmpPhoneVip != null && tmpPhoneVip.VIPID != reqObj.common.userId)
                {
                    respData.code = "2201";
                    respData.description = "您填写的手机号码已经被其他参会人员认证。如需帮助，请联系现场工作人员。";
                    return respData.ToJSON().ToString();
                }

                var tmpEventVipList = eventVipBLL.QueryByEntity(new EventVipEntity()
                {
                    Phone = reqObj.special.phone,
                    EventId = reqObj.special.eventId
                }, null);

                var tmpVipList = vipBLL.QueryByEntity(new VipEntity()
                {
                    VIPID = reqObj.common.userId
                    , ClientID = customerId
                }, null);

                #region 与会嘉宾中存在该手机号
                if (tmpEventVipList != null && tmpEventVipList.Length > 0)
                {
                    #region 会员表中存在该会员
                    if (tmpVipList != null && tmpVipList.Length > 0)
                    {
                        //签到
                        vipBLL.Update(new VipEntity()
                        {
                            VIPID = reqObj.common.userId,
                            VipRealName = tmpEventVipList[0].VipName,
                            Phone = reqObj.special.phone,
                            Status = 2
                        }, false);

                        respData.code = "200";
                        respData.description = "恭喜您签到成功！";
                    }
                    #endregion
                }
                #endregion
                else
                {
                    respData.code = "2202";
                    respData.description = "在参会嘉宾中未查询到您输入的手机号码，请确认您输入是否有误。如需帮助，请联系现场工作人员。";
                    return respData.ToJSON().ToString();
                }

                if (!string.IsNullOrEmpty(tmpEventVipList[0].Seat))
                {
                    respData.content = new SetSignUpRespContentData();
                    respData.description += "您的座位是：" + tmpEventVipList[0].Seat + "，请就坐。";
                }

                string error = "";
                var sendFlag = eventVipBLL.SetEventVipSeatPush(tmpVipList[0].VIPID, reqObj.special.eventId, out error);
                if (!sendFlag)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetStaffSeatsPush: {0}", error)
                    });
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();

                Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = ex.ToJSON() });
            }
            content = respData.ToJSON();
            return content;
        }

        #region 参数对象
        /// <summary>
        /// 返回对象
        /// </summary>
        public class SetSignUpRespData : Default.LowerRespData
        {
            public SetSignUpRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class SetSignUpRespContentData
        {
            public string vipId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class SetSignUpReqData : ReqData
        {
            public SetSignUpReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class SetSignUpReqSpecialData
        {
            public string phone { get; set; }
            public string vipName { get; set; }
            public string eventId { get; set; }
        }
        #endregion
        #endregion
    }

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
        public string businessZoneId;//商图ID
        public string channelId;//渠道ID
        public string eventId;
    }

    #endregion
}