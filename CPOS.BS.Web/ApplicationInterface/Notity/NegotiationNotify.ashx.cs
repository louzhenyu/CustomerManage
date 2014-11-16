using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.IO;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Notity
{
    /// <summary>
    /// NegotiationNotify 的摘要说明
    /// </summary>
    public class NegotiationNotify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var OrderID = context.Request["OrderID"];
            var OrderStatus = context.Request["OrderStatus"];
            var CustomerID = context.Request["CustomerID"];
            var UserID = context.Request["UserID"];
            var ChannelID = context.Request["ChannelID"];
            var SerialPay = context.Request["SerialPay"];
            try
            {
                if (string.IsNullOrEmpty(OrderID) || string.IsNullOrEmpty(OrderStatus) || string.IsNullOrEmpty(CustomerID) || string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(ChannelID))
                {
                    throw new Exception("参数不全:OrderID,OrderStatus,CustomerID,UserID");
                }
                else
                {
                    var loggingSessionInfo = Default.GetBSLoggingSession(CustomerID, "1");
                    /// var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                    CustomerWithdrawalBLL bll = new CustomerWithdrawalBLL(loggingSessionInfo);
                    #region 更新通知代付信息
                    if (OrderStatus == "2")
                    {
                        bll.NotityTradeCenterPay(OrderID, "50");
                    }
                    else if (OrderStatus == "3")
                    {
                        bll.NotityTradeCenterPay(OrderID, "0");
                    }
                    #endregion
                    context.Response.Write("SUCCESS");
                }
            }
            catch (Exception ex)
            {
                //Loggers.Exception(new ExceptionLogInfo(ex));
                //context.Response.Write("ERROR:" + ex.Message);
                context.Response.Write("ERROR:" + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}