using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using System.Configuration;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.Login
{
    public partial class LoginManager : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string customer_id = Request["cid"];
                string token = Request["tid"];

                #region 正式发布时，请删除

                if (string.IsNullOrEmpty(customer_id))
                {
                    customer_id = ConfigurationManager.AppSettings["test_customer_id"].ToString();
                }

                if (string.IsNullOrEmpty(token))
                {
                    token = ConfigurationManager.AppSettings["test_token"].ToString();
                }

                #endregion

                this.loadUser(customer_id, token);
            }
        }

        private void loadUser(string customer_id, string token)
        {
            //try
            //{
            //获取登录管理平台的用户信息
            var AuthWebService = new JIT.CPOS.BS.WebServices.AuthManagerWebServices.AuthServiceSoapClient();
            AuthWebService.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                ConfigurationManager.AppSettings["sso_url"].ToString() + "/AuthService.asmx");
         //   AuthWebService.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://localhost:42305/AuthService.asmx");

            string str = AuthWebService.GetLoginUserInfo(token);
            //str = "<?xml version=\"1.0\"?>\r\n<data xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <customer_id>7e144bf108b94505a890ec3a7820db8d</customer_id>\r\n  <customer_code>zmdtest</customer_code>\r\n  <customer_name>\u603B\u90E8123</customer_name>\r\n  <connection_string>user id=admin;password=JtLaxT7668;data source=182.254.151.86,7433;database=cpos_bs_farm;</connection_string>\r\n  <user_id>820833030e5b493ca820c1a34109923d</user_id>\r\n  <user_name>\u7BA1\u7406\u5458</user_name>\r\n</data>";

            if (string.IsNullOrEmpty(str))
            {
                //this.lbErr.Text = "用户不存在,请与管理员联系";
                //return;
                PageLog.Current.Write("SSO登录失败，AuthWebService.asmx返回空数据");
                //Response.Write("登录失败，请重试！");
                //Response.End();
                var redirectUrl = ConfigurationManager.AppSettings["sso_url"].ToString() + "?errorinfo=" + "网络繁忙，请重新登录！";
                Response.Redirect(redirectUrl, true);
            }

            var myLoggingManager = (JIT.CPOS.BS.Entity.LoggingManager)cXMLService.Deserialize(
                str, typeof(JIT.CPOS.BS.Entity.LoggingManager));

            //判断登录进来的用户是否存在,并且返回用户信息
            LoggingSessionInfo loggingSession = new LoggingSessionInfo();
            loggingSession.CurrentLoggingManager = myLoggingManager;
            cUserService userService = new cUserService(loggingSession);
            UnitService unitService = new UnitService(loggingSession);
            if (!userService.IsExistUser(loggingSession))
            {
                this.lbErr.Text = "用户不存在,请与管理员联系";
                return;
            }
            var login_user = userService.GetUserById(loggingSession, myLoggingManager.User_Id);
            loggingSession.CurrentUser = login_user;

            // 获取角色
            string applicationId = PageBase.JITPage.GetApplicationId();
            IList<UserRoleInfo> userRoleList = userService.GetUserRoles(login_user.User_Id, applicationId);
            if (userRoleList != null && userRoleList.Count > 0)
            {
                loggingSession.CurrentUserRole = new UserRoleInfo();
                loggingSession.CurrentUserRole.UserId = login_user.User_Id;
                loggingSession.CurrentUserRole.UserName = login_user.User_Name;
                loggingSession.CurrentUserRole.RoleId = userRoleList[0].RoleId;
                loggingSession.CurrentUserRole.RoleCode = userRoleList[0].RoleCode;
                loggingSession.CurrentUserRole.RoleName = userRoleList[0].RoleName;

                loggingSession.ClientID = login_user.customer_id;
                loggingSession.CurrentLoggingManager.Customer_Id = login_user.customer_id;
                loggingSession.UserID = loggingSession.CurrentUser.User_Id;

                try
                {
                    loggingSession.CurrentUserRole.UnitId = userService.GetDefaultUnitByUserIdAndRoleId(
                        loggingSession.CurrentUserRole.UserId, loggingSession.CurrentUserRole.RoleId);
                }
                catch (Exception ex)
                {
                    PageLog.Current.Write(ex);
                    Response.Write("找不到默认单位");
                    Response.End();
                }

                try
                {
                    var unitInfo = unitService.GetUnitById(loggingSession.CurrentUserRole.UnitId);
                    loggingSession.CurrentUserRole.UnitName = unitInfo.Name;
                    loggingSession.CurrentUserRole.UnitShortName = unitInfo.ShortName;
                }
                catch (Exception ex)
                {
                    PageLog.Current.Write(ex);
                    Response.Write("找不到单位");
                    Response.End();
                }
            }
            else {
                //PageLog.Current.Write(ex);
                Response.Write("该用户没有权限登录管理平台");
                Response.End();
            }



            //this.Session["UserInfo"] = login_user;
            //this.Session["LoggingManager"] = myLoggingManager;
            //this.Session["loggingSessionInfo"] = loggingSession;


            //loggingSession.CurrentLoggingManager = myLoggingManager;
            new SessionManager().SetCurrentUserLoginInfo(loggingSession);

            //清空密码
            login_user.User_Password = null;
            //string go_url = "~/login/SelectRoleUnit.aspx?p=0";
            string go_url = "~/Default.aspx";
            if (loggingSession.CurrentUserRole != null && loggingSession.CurrentUserRole.RoleId == "860E69754D3B490F8A5B401DF3F66E15")
            {
                string eventId = string.Empty;
                //switch (loggingSession.CurrentUserRole.UserId.Trim())
                //{
                //    case "FA1BDA8937924D45AFA3123FE4DEE8FA":
                //        eventId = "0326056B219340D5B234BFAD9AF02AF5";
                //        break;
                //    case "4913B21CFD714C7986842B859EC1289B":
                //        eventId = "793150439CF94190A70CF2EC229A951D";
                //        break;
                //    case "BD8079F886BD492E90A335EBC1DE9676":
                //        eventId = "F8A7E2E8807B49558F1A516F23C34473";
                //        break;
                //    default:
                //        eventId = "793150439CF94190A70CF2EC229A951D";
                //        break;
                //}
                LEventsBLL lEventsBLL = new LEventsBLL(loggingSession);
                var eventList = lEventsBLL.QueryByEntity(new LEventsEntity() {
                    EventManagerUserId = loggingSession.CurrentUserRole.UserId
                }, null);
                if (eventList != null && eventList.Length > 0)
                {
                    eventId = eventList[0].EventID;
                    loggingSession.CurrentUserRole.RoleName = eventId;
                    Response.Redirect("~/Module/MarketEvent/EventList/EventAnalysisList4.aspx", true);
                }
            }
            else
            {
                //loggingSession.CurrentUserRole.RoleName = "793150439CF94190A70CF2EC229A951D";
                Response.Redirect(go_url, true);
            }
            //}
            //catch (Exception ex)
            //{
            //    PageLog.Current.Write(ex);
            //    lbErr.Text = "登录失败";
            //}
        }
    }
}