using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.WebServices;
using System.Configuration;

using System.ServiceModel;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 登录的服务
    /// </summary>
    public class CLoggingSessionService
    {
        /// <summary>
        /// 获取登录用户的具体信息
        /// </summary>
        /// <param name="cid">客户id</param>
        /// <param name="tid">令牌id</param>
        /// <returns></returns>
        public LoggingSessionInfo GetLoggingSessionInfo(string cid, string tid)
        {
            //获取登录管理平台的用户信息
            

            var AuthWebService = new JIT.CPOS.BS.WebServices.AuthManagerWebServices.AuthServiceSoapClient();
            AuthWebService.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                ConfigurationManager.AppSettings["sso_url"].ToString() + "/AuthService.asmx");
            string str = AuthWebService.GetLoginUserInfo(tid);


            LoggingManager myLoggingManager = (LoggingManager)cXMLService.Deserialize(str, typeof(LoggingManager));

            //判断用户是否存在,并且返回用户信息
            UserInfo login_user = new UserInfo();
            

            LoggingSessionInfo loggingSessionInfo1 = new LoggingSessionInfo();
            loggingSessionInfo1.CurrentLoggingManager = myLoggingManager;

            cUserService userService = new cUserService(loggingSessionInfo1);
            //获取用户信息
            if (userService.IsExistUser(loggingSessionInfo1))
            {
                login_user = userService.GetUserById(loggingSessionInfo1, myLoggingManager.User_Id);
            }
            else
            {
                login_user.User_Id = "1";
            }

            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();


            loggingSessionInfo.CurrentUser = login_user;
            loggingSessionInfo.CurrentLoggingManager = myLoggingManager;

            UserRoleInfo ur = new UserRoleInfo();
            ur.RoleId = "7064243380E24B0BA24E4ADC4E03968B";
            ur.UnitId = "1";
            loggingSessionInfo.CurrentUserRole = ur;

            return loggingSessionInfo;
        }
    }
}
