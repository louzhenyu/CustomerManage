using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    public class LoggingSessionManager
    {
        //private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo { get { return new SessionManager().CurrentUserLoginInfo; } }
        private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo = new Entity.LoggingSessionInfo()
        {
            CurrentUser = new Entity.User.UserInfo() { User_Id = "fec9eccd2be748c590a57aee841f06dc", customer_id = "3084cd58e4144cceb1faaac1c515f151" },
            CurrentLoggingManager = new Entity.LoggingManager()
            {
                Connection_String = "user id=dev;password=jit!2014;data source=112.124.68.147;database=cpos_bs_jit;",
                Customer_Id = "3084cd58e4144cceb1faaac1c515f151",
                Customer_Code = "admin",
                Customer_Name = "",
                User_Name = "管理员"
            },
            ClientID = "3084cd58e4144cceb1faaac1c515f151"
        };

        public JIT.CPOS.BS.Entity.LoggingSessionInfo CurrentSession
        {
            get { return PrivateLoggingSessionInfo; }
        }
    }
}