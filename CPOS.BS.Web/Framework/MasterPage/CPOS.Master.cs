using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Web.Cookie;
using JIT.CPOS.BS.Web.Extension;
using JIT.Utility.Reflection;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Framework.MasterPage
{
    public partial class CPOS : System.Web.UI.MasterPage
    {

        //protected ClientMenuButtonEntity[] MenuList;
        protected IList<MenuModel> MenuList;
        public string strWebLogo = "/framework/image/logo.gif";
        protected string Mid
        {
            get
            {
                return Request.QueryString["mid"] == null ? "" : Request.QueryString["mid"].ToString();

            }
        }
        #region LoggingSessionInfo 登录信息类集合

        protected string CustomerID
        {
            get
            {
               return  new SessionManager().CurrentUserLoginInfo.ClientID; 
            }
        }
        protected string adminUserID
        {
            get
            {
                return new SessionManager().CurrentUserLoginInfo.UserID;
                

            }
        }
        #endregion

       
        protected string PMenuID
        {
            get;
            set;
            //{
            //   // string _PMenuID=Request.QueryString["PMenuID"] == null ? "" : Request.QueryString["PMenuID"].ToString();

            //}
        }
        protected string StaticUrl
        {
            get
            {
                string staticUrl = ConfigurationManager.AppSettings["staticUrl"];
                if (string.IsNullOrEmpty(staticUrl))
                { 
                  staticUrl="";
                }
                return staticUrl;
            }
        }

        protected string UnitID
        {
            get
            {
                return new SessionManager().CurrentUserLoginInfo.CurrentUserRole.UnitId;
            }
        }
        protected string UnitName
        {
            get
            {
                return new SessionManager().CurrentUserLoginInfo.CurrentUserRole.UnitName;
            }
        }
        protected string UnitShortName
        {
            get
            {
                return new SessionManager().CurrentUserLoginInfo.CurrentUserRole.UnitShortName;
            }
        }
        protected string RoleCode
        {
            get { return new SessionManager().CurrentUserLoginInfo.CurrentUserRole.RoleCode; }
        }
        protected string RoleName{
            get{return new SessionManager().CurrentUserLoginInfo.CurrentUserRole.RoleName;}
        }
      
        #region 页面入口
        /// <summary>
        /// pageload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string content = string.Empty;
            if (string.IsNullOrEmpty(Request["method"]))
            {
                //MenuList = DataLoader.LoadFrom<ClientMenuButtonEntity>(
                //    new SessionManager().CurrentUserLoginInfo.UserOPRight.Tables[0], 
                //    new DirectPropertyNameMapping()).Where(c => 
                //        string.IsNullOrEmpty(c.ButtonText)).ToArray();

                //var loggingSessionInfo = this.Session["loggingSessionInfo"] as LoggingSessionInfo;
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                if (loggingSessionInfo == null)
                {
                    this.Response.Redirect("~/GoSso.aspx");
                }
                else {
                    if (lblLoginUserName!=null)
                    {
                        lblLoginUserName.InnerText = loggingSessionInfo.CurrentUser.User_Name;//因为ChildPage.Master的前台页面Inherits="JIT.CPOS.BS.Web.Framework.MasterPage.CPOS" 但是不含有 lblLoginUserName
                    }
                }
                AppSysService appSysService = new AppSysService(loggingSessionInfo);
                this.MenuList = appSysService.GetRoleMenus(loggingSessionInfo, 
                    loggingSessionInfo.CurrentUserRole.RoleId);//根据当前用户的角色，来取他拥有的页面
                PMenuID = Request.QueryString["PMenuID"] == null ? "" : Request.QueryString["PMenuID"].ToString();
                if (PMenuID == "")
                {
                    var currentMenu =MenuList.Where(p=>p.Menu_Id==Mid).SingleOrDefault();
                    if (currentMenu!=null)
                    {
                        PMenuID = currentMenu.Parent_Menu_Id;
                    }
                   
                }
                if (!IsPostBack)
                {
                    //Jermyn20140703 修改logo图片来源
                    CustomerBasicSettingBLL customerBSServer = new CustomerBasicSettingBLL(loggingSessionInfo);
                    var customerBSList = customerBSServer.QueryByEntity(new CustomerBasicSettingEntity
                    {
                        CustomerID = loggingSessionInfo.CurrentUser.customer_id.Trim()
                        ,SettingCode = "WebLogo"
                        ,IsDelete = 0
                    }, null);
                    if (customerBSList == null || customerBSList.Length == 0 || customerBSList[0] == null
                        || customerBSList[0].SettingValue == null
                        || customerBSList[0].SettingValue.Equals(""))
                    {
                        //Jermyn20131202 添加logo图片
                        PropService propServer = new PropService(loggingSessionInfo);
                        string strLogo = propServer.GetWebLogo();
                        if (strLogo == null || strLogo.Equals(""))
                        {

                        }
                        else
                        {
                            strWebLogo = strLogo;
                        }
                    }
                    else {
                        strWebLogo = customerBSList[0].SettingValue;
                    }

                   
                }
            }
            else
            {
                switch (Request["method"])
                {
                    case "LogOut": content = LogOut(); break;
                    case "KeepSession": content = ""; break;
                }
                Response.Write(content); ;
                Response.End();
            }

            

        }
        #endregion

        #region 方法
        private string LogOut()
        {
            //new SessionManager().CurrentUserLoginInfo.UserID = string.Empty;
            //new SessionManager().CurrentUserLoginInfo.UserOPRight = null;
            CookieManager.SetCookie(CookieKeyConst.USERNAME, "", DateTime.Now.AddDays(-1));
            CookieManager.SetCookie(CookieKeyConst.USERPWD, "", DateTime.Now.AddDays(-1));
            //return "<script type=\"text/javascript\">window.location='/login.aspx';</script>";

            this.Session.Clear();
            this.Response.Redirect("~/GoSso.aspx");
            return string.Empty;
        }
        #endregion
    }
}