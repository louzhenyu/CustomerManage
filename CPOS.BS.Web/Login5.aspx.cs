using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.Web
{
    public partial class Login5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                //this.txtUsername.Focus();

                if (Request.Cookies["cpos_sso_remember"] != null)
                {
                    try
                    {
                        string cpos_remember = Request.Cookies["cpos_sso_remember"].Value;
                        //string cpos_cust = Request.Cookies["cpos_sso_cust"].Value;
                        string cpos_user = Request.Cookies["cpos_sso_user"].Value;
                        string cpos_pwd = Request.Cookies["cpos_sso_pwd"].Value;

                        if (cpos_remember == "1")
                        {
                            this.chkRemember.Checked = true;
                            //this.txtCustomerCode.Value = cpos_cust;
                            this.txtUsername.Value = cpos_user;
                            this.ClientScript.RegisterStartupScript(this.GetType(),
                                      "", "$(document).ready(function(){ fnBindPwd('" + cpos_pwd + "'); });",
                                      true);
                        }
                    }
                    catch
                    { }
                }
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //string customerCode = this.txtCustomerCode.Value.Trim();
            string account = this.txtUsername.Value.Trim();
            string pwd = this.txtPassword.Value.Trim();
            //string authCode = txtValidateCode.Text.Trim();     
            //将用户登录名密码提交到服务器检验   

            //if (string.IsNullOrEmpty(customerCode) || customerCode == "公司编码")
            //{
            //    lblInfor.Text = "请输入公司编码！";
            //    txtCustomerCode.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(account) || account == "用户名")
            {
                lblInfor.Text = "请输入用户名！";
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(pwd))
            {
                lblInfor.Text = "请输入密码！";
                txtPassword.Focus();
                return;
            }


            Hashtable ht = new Hashtable();
            UserInfo login_user = null;
            var loggingSessionInfo = GetLjLoggingSession();
            var service = new cUserService(loggingSessionInfo);
            try
            {
                int ret = 0;
                var userList = service.SearchUserList(account, "", "", "", 1, 0);
                if (userList != null && userList.UserInfoList != null && userList.UserInfoList.Count > 0)
                {
                    login_user = userList.UserInfoList[0];
                    login_user.customer_id = "ddde622fb9ff4854937c376c095d8254";
                    if (login_user.User_Status == "-1")
                    {
                        ret = -2;
                    }
                    else if (login_user.User_Password != EncryptManager.Hash(pwd, HashProviderType.MD5))
                    {
                        ret = -3;
                    }
                    else
                    {
                        ret = 1;
                    }
                }
                else
                {
                    ret = -1;
                }


                switch (ret)
                {
                    case -1:
                        lblInfor.Text = "用户不存在";
                        return;
                    case -2:
                        lblInfor.Text = "用户被停用";
                        return;
                    case -3:
                        lblInfor.Text = "密码不正确";
                        return;
                    case -4:
                        lblInfor.Text = "用户不在线";
                        return;
                    case 1:
                        //用户名和密码验证通过
                        break;
                    default:
                        lblInfor.Text = "用户名和密码不正确";
                        return;
                }
            }
            catch
            {
                lblInfor.Text = "用户名和密码不正确";
                return;
            }

            // chkRemember
            if (chkRemember.Checked)
            {
                Response.Cookies["cpos_sso_remember"].Value = "1";
                Response.Cookies["cpos_sso_remember"].Expires = DateTime.MaxValue;

                Response.Cookies["cpos_sso_user"].Value = account;
                Response.Cookies["cpos_sso_user"].Expires = DateTime.MaxValue;

                Response.Cookies["cpos_sso_pwd"].Value = pwd;
                Response.Cookies["cpos_sso_pwd"].Expires = DateTime.MaxValue;
            }
            else
            {
                Response.Cookies["cpos_sso_remember"].Value = "";
                Response.Cookies["cpos_sso_user"].Value = "";
                Response.Cookies["cpos_sso_pwd"].Value = "";
            }

            //判断登录进来的用户是否存在,并且返回用户信息
            LoggingSessionInfo loggingSession = new LoggingSessionInfo();
            loggingSession.CurrentLoggingManager = new LoggingManager();
            loggingSession.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSession.CurrentLoggingManager.Customer_Id = loggingSessionInfo.CurrentUser.customer_id;

            loggingSession.CurrentUser = login_user;

            // 获取角色
            string applicationId = PageBase.JITPage.GetApplicationId();
            IList<UserRoleInfo> userRoleList = service.GetUserRoles(login_user.User_Id, applicationId);
            if (userRoleList != null && userRoleList.Count > 0)
            {
                loggingSession.CurrentUserRole = new UserRoleInfo();
                loggingSession.CurrentUserRole.UserId = login_user.User_Id;
                loggingSession.CurrentUserRole.UserName = login_user.User_Name;
                loggingSession.CurrentUserRole.RoleId = userRoleList[0].RoleId;
                loggingSession.CurrentUserRole.RoleName = userRoleList[0].RoleName;

                loggingSession.ClientID = login_user.customer_id;
                loggingSession.CurrentLoggingManager.Customer_Id = login_user.customer_id;
                loggingSession.UserID = loggingSession.CurrentUser.User_Id;

                try
                {
                    loggingSession.CurrentUserRole.UnitId = service.GetDefaultUnitByUserIdAndRoleId(
                        loggingSession.CurrentUserRole.UserId, loggingSession.CurrentUserRole.RoleId);
                }
                catch (Exception ex)
                {
                    PageLog.Current.Write(ex);
                    Response.Write("找不到默认单位");
                    Response.End();
                }

                //try
                //{
                //    loggingSession.CurrentUserRole.UnitName = unitService.GetUnitById(
                //        loggingSessionInfo, loggingSession.CurrentUserRole.UnitId).ShortName;
                //}
                //catch (Exception ex)
                //{
                //    PageLog.Current.Write(ex);
                //    Response.Write("找不到单位");
                //    Response.End();
                //}
            }


            //this.Session["UserInfo"] = login_user;
            //this.Session["LoggingManager"] = myLoggingManager;
            //this.Session["loggingSessionInfo"] = loggingSession;


            //loggingSession.CurrentLoggingManager = myLoggingManager;
            new SessionManager().SetCurrentUserLoginInfo(loggingSession);

            //清空密码
            login_user.User_Password = null;

            string goURL = "~/Default.aspx";
            this.Response.Redirect(goURL);
        }

        //protected void btnSend_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(PUserName.Value))
        //    {
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('请输入姓名！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(PCompany.Value))
        //    {
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('请输入公司名称！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(PEmail.Value))
        //    {
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('请输入公司邮箱！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(PTel.Value))
        //    {
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('请输入公司总机+分级！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(PPhone.Value))
        //    {
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('请输入手机号！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(PIndustry.Value))
        //    {
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('请输入行业！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //        return;
        //    }

        //    var url = "http://180.153.154.21:9004/OnlineShopping/data/Data.aspx?action=setContact";
        //    url += string.Format("&reqContent={{\"common\":{{}},\"special\":{{\"userName\":\"{0}\", \"company\":\"{1}\", \"tel\":\"{2}\", \"email\":\"{3}\", \"phone\":\"{4}\", \"industry\":\"{5}\"}}}}",
        //        PUserName.Value.Trim(),
        //        PCompany.Value.Trim(),
        //        PTel.Value.Trim(),
        //        PEmail.Value.Trim(),
        //        PPhone.Value.Trim(),
        //        PIndustry.Value.Trim()
        //        );

        //    //var result = "200";
        //    var result = GetRemoteData(url, "GET", "");
        //    if (result.Contains("200"))
        //    {
        //        PUserName.Value = "";
        //        PCompany.Value = "";
        //        PTel.Value = "";
        //        PEmail.Value = "";
        //        PPhone.Value = "";
        //        PIndustry.Value = "";
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('发送完成！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //    }
        //    else
        //    {
        //        hMsg.Value = result;
        //        this.ClientScript.RegisterStartupScript(this.GetType(),
        //            "", "$(document).ready(function(){ alert('发送失败！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
        //            true);
        //    }
        //}

        #region GetRemoteData
        public static string GetRemoteData(string uri, string method, string content)
        {
            string respData = "";
            method = method.ToUpper();
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = method.ToUpper();
            req.Credentials = System.Net.CredentialCache.DefaultCredentials;
            ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
            if (method == "POST")
            {
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                req.ContentLength = buffer.Length;
                //req.ContentType = "text/json";
                Stream postStream = req.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);
                postStream.Close();
            }
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
            respData = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            resp.Close();
            return respData;
        }

        internal class AcceptAllCertificatePolicy : System.Net.ICertificatePolicy
        {
            public AcceptAllCertificatePolicy()
            { }

            public bool CheckValidationResult(ServicePoint sPoint,
                System.Security.Cryptography.X509Certificates.X509Certificate cert,
                WebRequest wRequest, int certProb)
            {
                return true;
            }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Request.Cookies["cpos_sso_remember"] != null)
            {
                string cpos_remember = Request.Cookies["cpos_sso_remember"].Value;
                if (cpos_remember == "1")
                {
                    if (Request.Cookies["cpos_sso_pwd"] != null)
                    {
                        string cpos_pwd = Request.Cookies["cpos_sso_pwd"].Value;
                        this.txtPassword.Attributes["value"] = cpos_pwd;
                        this.hdPwd.Value = cpos_pwd;
                    }
                }
            }
        }


        #region 获取登录用户信息
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetLoggingSession(string customerId, string userId)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = ConfigurationManager.AppSettings["Conn"].Trim();

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
        public static LoggingSessionInfo GetLoggingSession()
        {
            return GetLoggingSession(
                ConfigurationManager.AppSettings["customer_id"].Trim(),
                ConfigurationManager.AppSettings["user_id"].Trim());
        }
        public static LoggingSessionInfo GetLjLoggingSession(string userId, string customerId)
        {
            if (userId == null || userId.Trim().Length == 0) userId = "1";
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = ConfigurationManager.AppSettings["Conn_vanke"].Trim();

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }
        public static LoggingSessionInfo GetLjLoggingSession()
        {
            return GetLjLoggingSession("", "ddde622fb9ff4854937c376c095d8254");
        }
        public static LoggingSessionInfo GetLjLoggingSession(string userId)
        {
            return GetLjLoggingSession(userId, "");
        }

        #endregion
    }
}