using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.Common;
using JIT.CPOS.BS.WebServices;
using System.Configuration;

namespace JIT.CPOS.BS.BLL
{
    public class cLoggingManager
    {
        /// <summary>
        /// 根据客户标识，获取客户信息
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <returns></returns>
        public LoggingManager GetLoggingManager(string Customer_Id)
        {
            WebServices.AuthManagerWebServices.AuthService AuthWebService = new WebServices.AuthManagerWebServices.AuthService();
            AuthWebService.Url = System.Configuration.ConfigurationManager.AppSettings["sso_url"] + "/authservice.asmx";
            string str = AuthWebService.GetCustomerDBConnectionString(Customer_Id);

            LoggingManager myLoggingManager = (LoggingManager)cXMLService.Deserialize(str, typeof(LoggingManager));
            myLoggingManager.Customer_Id = Customer_Id;
            return myLoggingManager;
        }
    }
}
