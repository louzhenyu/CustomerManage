/*
 * Author		:roy.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:19/2/2012 10:03:10 AM
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using JIT.Utility.Log;
using System.Configuration;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Web.Cookie;
using JIT.CPOS.BS.Web.Extension;
using JIT.Utility.Reflection;
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.Web.PageBase
{
    public partial class JITPage : Page
    {

        protected string StaticUrl
        {
            get
            {
                string staticUrl = ConfigurationManager.AppSettings["staticUrl"];
                if (string.IsNullOrEmpty(staticUrl))
                {
                    staticUrl = "";
                }
                return staticUrl;
            }
        }
        //SessionManager sessionManage = new SessionManager();
        public static int PageSize = 15;

        //得到本项目的ApplicationId
        public static string GetApplicationId()
        {
            return "D8C5FF6041AA4EA19D83F924DBF56F93"; // A2BF354A4E5E4DE7907DCD25200A0879
        }

        //得到POS项目的ApplicationId
        public static string GetPosApplicationId()
        {
            return "7C7CC257927D44BD8CF4F9CD5AC5BDCD";
        }
        //得到APP项目的ApplicationId
        public static string GetAppApplicationId()
        {
            return "649F8B8BDA9840D6A18130A5FF4CB9C8";
        }
        public JITPage()
            : base()
        {
            pageType = 1;
            _rSession = new SessionManager();

            this.InfoBox = new InfoBox(this);
        }
        protected int pageType = 1;//页面类型1菜单页，2子页
        private SessionManager _rSession;
        protected SessionManager RSession
        {
            get { return _rSession; }
        }
        public LoggingSessionInfo CurrentUserInfo
        {
            get { return _rSession.CurrentUserLoginInfo; }
        }

        #region 页面入口
        /// <summary>
        /// 页请求
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            /*
             * 保持用户登录状态
             * 验证用户是否登录
             * 验证用户是否有当前菜单权限
             */
            RememberMember();
            if (!CheckUserLogin())
            {
                NotAuthenticateRedirect();
            }
            //int pageRight = CheckUserPageRight(pageType);
            //if (pageRight != 2)
            //{
            //    string msg = (pageRight == 1 ? "缺少必要参数菜单ID(mid)" : "没有权限");
            //    //NotHasRightRedirect(msg);
            //}
        }

        /// <summary>
        /// 页加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            /*
             * 输出引用资源
             * 输出菜单按钮资源
             * 输出语言资源
             */

            //InitFrameWork(0);
            InitRightInfo(0);
            //InitLangInfo();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 保持用户登录状态（开发阶段保留，发布后删除）
        /// </summary>
        private void RememberMember()
        {
            //if (CookieManager.GetCookie(CookieKeyConst.USERNAME) != null)
            //{
            //    //判断session是否存在，不存在则实现自动登录
            //    SessionManager rSession = new SessionManager();
            //    if (rSession.CurrentUserLoginInfo != null)
            //    {
            //        return;
            //    }
            //    string customer = DEncrypt.Decrypt(CookieManager.GetCookieValue(CookieKeyConst.CUSTOMERNAME));
            //    string username = DEncrypt.Decrypt(CookieManager.GetCookieValue(CookieKeyConst.USERNAME));
            //    string userpwd = "";
            //    if (CookieManager.GetCookie(CookieKeyConst.USERPWD) != null)
            //    {
            //        userpwd = CookieManager.GetCookieValue(CookieKeyConst.USERPWD);
            //    }

            //    JITClientLoginService.JitClientLoginServiceClient service = new JITClientLoginService.JitClientLoginServiceClient();
            //    JITClientLoginService.ClientUserLoginResult result = new JITClientLoginService.ClientUserLoginResult();
            //    result = service.UserLogin(customer, StringUtils.SqlReplace(username), DEncrypt.Decrypt(userpwd), null);
            //    if (result.ResultCode == "0")
            //    {
            //        BasicTenantUserInfo userInfo = new BasicTenantUserInfo();
            //        userInfo.UserID = result.UserID.ToString();
            //        userInfo.ConnectionString = result.ResultDetail["DBConnectionString"].ToString();
            //        userInfo.ClientID = result.ClientID.ToString();
            //        /////////userInfo.UserOPRight = new ClientUserBLL(userInfo).GetUserOPRight(result.UserID.ToInt());
            //        new SessionManager().CurrentUserLoginInfo = userInfo;
            //    }
            //}
            //else
            //{
            //    //未登录
            //}
        }

        /// <summary>
        /// 验证用户是否登陆
        /// </summary>
        /// <returns></returns>
        public bool CheckUserLogin()
        {
            if (this.RSession.CurrentUserLoginInfo == null ||
                this.RSession.CurrentUserLoginInfo.CurrentUser.User_Id.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证用户页权限
        /// 返回1.缺少必要参数菜单ID，2.有权限，3.无权限
        /// </summary>
        /// <param name="pageType">页面类型1菜单页，2子页</param>
        /// <returns>1.缺少必要参数菜单ID，2.有权限，3.无权限</returns>
        private int CheckUserPageRight(int pageType)
        {
            int res = 1;
            //主页
            if (System.IO.Path.GetFileName(HttpContext.Current.Request.Path).ToString().ToLower() == "default.aspx")
            {
                res = 2;
            }
            else
            {
                //其它页面
                if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["mid"]))
                {
                    //缺少必须的参数 菜单ID
                    res = 1;
                    return res;
                }

                string filePath = null;
                if (pageType == 1)
                {
                    filePath = HttpContext.Current.Request.Path.ToString();
                }
                if (CheckUserHasPageRight(HttpContext.Current.Request.QueryString["mid"].ToString(), filePath))
                {
                    res = 2;
                }
                else
                {
                    res = 3;
                }
            }
            return res;
        }

        /// <summary>
        /// 验证用户页权限
        /// </summary>
        /// <param name="menuCode">页面标识</param>
        /// <returns></returns>
        private bool CheckUserHasPageRight(string menuID, string filePath)
        {
            return true;
            //int currentMenuCount = 0;
            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    //菜单页
            //    currentMenuCount = RSession.CurrentUserLoginInfo.UserOPRight.Tables[0].Select(
            //          "ClientMenuID='" + menuID + "'"
            //          + " and MenuUrl='" + filePath + "'"
            //          + " and ClientButtonID is null").Length;
            //}
            //else
            //{
            //    //子页
            //    currentMenuCount = RSession.CurrentUserLoginInfo.UserOPRight.Tables[0].Select(
            //          "ClientMenuID='" + menuID + "'"
            //          + " and ClientButtonID is null").Length;
            //}
            //if (currentMenuCount > 0)
            //{
            //    //有权限
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// 加载引用资源
        /// </summary>
        private void InitFrameWork(int index)
        {
            Literal ltlFW = new Literal();
            ltlFW.Text =
                "<link href=\"/Lib/Javascript/Ext4.1.0/Resources/css/ext-all.css\" rel=\"stylesheet\" type=\"text/css\" />"
                + "<script src=\"/Lib/Javascript/Ext4.1.0/ext-all.js\" type=\"text/javascript\"></script>";
            try
            {
                Page pCurrent = HttpContext.Current.CurrentHandler as Page;
                pCurrent.Header.Controls.AddAt(index, ltlFW);

                //HttpContext.Current.Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert('请登录');window.top.location='/login.aspx'</script>");
                //HttpContext.Current.Response.End();
            }
            catch { }
        }

        /// <summary>
        /// 加载页面权限信息
        /// </summary>
        private void InitRightInfo(int index)
        {
            StringBuilder sbRes = new StringBuilder();
            sbRes.Append("<script type=\"text/javascript\" language=\"javascript\">");
            sbRes.Append("var __button=eval(\"" + GetUserMenuButton() + "\");");
            sbRes.Append("function __getHidden(__code){");

            sbRes.Append("  if (__button == undefined) return;");

            sbRes.Append("  var res=true;".Trim());
            sbRes.Append("  for(var i=0;i<__button.length;i++){".Trim());
            sbRes.Append("      if(__button[i].ButtonCode==__code){".Trim());
            sbRes.Append("          res=false;".Trim());
            sbRes.Append("      }".Trim());
            sbRes.Append("  }".Trim());
            sbRes.Append("  return res;".Trim());
            sbRes.Append("};");
            sbRes.Append("function __getText(__code){");
            sbRes.Append("  var res='';".Trim());
            sbRes.Append("  for(var i=0;i<__button.length;i++){".Trim());
            sbRes.Append("      if(__button[i].ButtonCode==__code){".Trim());
            sbRes.Append("          res=__button[i].ButtonText;".Trim());
            sbRes.Append("      }".Trim());
            sbRes.Append("  }".Trim());
            sbRes.Append("  return res;".Trim());
            sbRes.Append("};");
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["mid"]))
            {
                sbRes.Append("__mid=\"" + HttpContext.Current.Request.QueryString["mid"] + "\";");
            }
            sbRes.Append("</script>");
            Literal ltlFW = new Literal();
            ltlFW.Text = sbRes.ToString();
            try
            {
                Page pCurrent = HttpContext.Current.CurrentHandler as Page;
                pCurrent.Header.Controls.AddAt(index, ltlFW);
            }
            catch { }
        }

        /// <summary>
        /// 通过Menu获取用户页按钮信息
        /// </summary>
        /// <returns></returns>
        private string GetUserMenuButton()
        {
            return string.Empty;

            //string mid = HttpContext.Current.Request.QueryString["mid"];
            //var entity = DataLoader.LoadFrom<ClientMenuButtonEntity>(
            //    new SessionManager().CurrentUserLoginInfo.UserOPRight.Tables[0],
            //    new DirectPropertyNameMapping())
            //    .Where(c => c.ClientMenuID != null
            //        && c.ClientMenuID.ToString().ToLower() == mid
            //        && c.ClientButtonID != null)
            //    .Select(c => new { c.ButtonText, c.ButtonCode })
            //    .ToArray();
            //return entity.ToJSON().Replace("\"", "\\\"");
        }
        #endregion

        #region 页面出口
        /// <summary>
        /// 错误出口
        /// </summary>
        /// <param name="e"></param>
        protected override void OnError(EventArgs e)
        {
            //根据配置文件,判断是否需要当出现错误的时候跳转
            bool isPageOnErrorNotRedirect = false;
            string strPageOnErrorNotRedirect = ConfigurationManager.AppSettings["PageOnErrorNotRedirect"];
            if (!string.IsNullOrEmpty(strPageOnErrorNotRedirect))
            {
                bool temp;
                if (bool.TryParse(strPageOnErrorNotRedirect, out temp))
                {
                    isPageOnErrorNotRedirect = temp;
                }
            }
            //2.处理未捕获的异常
            Exception ex = Server.GetLastError();
            if (ex != null)
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
            if (!isPageOnErrorNotRedirect)
                this.Response.Redirect("/Error.aspx?msg=" + ex.Message);
        }

        /// <summary>
        /// 未登录出口
        /// </summary>
        public void NotAuthenticateRedirect()
        {
            HttpContext.Current.Response.Write(
                "<script type=\"text/javascript\" language=\"javascript\">window.top.location='/GoSso.aspx'</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 无权限
        /// </summary>
        public void NotHasRightRedirect(string msg)
        {
            string res = "没有权限";
            if (!string.IsNullOrEmpty(msg))
            {
                res = msg;
            }
            HttpContext.Current.Response.Write(
                "<script type=\"text/javascript\" language=\"javascript\">alert('" + res +
                "');window.top.location='/default.aspx'</script>");
            HttpContext.Current.Response.End();
        }
        #endregion


        #region Properties
        public UserInfo UserInfo
        {
            get { return _rSession.UserInfo; }
            set { _rSession.UserInfo = value; }
        }

        public InfoBox InfoBox
        {
            get;
            private set;
        }

        public UserRoleInfo UserRoleInfo
        {
            get { return _rSession.UserRoleInfo; }
            set { _rSession.UserRoleInfo = value; }
        }
        #endregion

        #region LoggingManager
        public LoggingManager LoggingManagerInfo
        {
            get { return _rSession.LoggingManager; }
            set { _rSession.LoggingManager = value; }
        }
        #endregion

        #region LoggingSessionInfo 登录信息类集合
        public LoggingSessionInfo loggingSessionInfo
        {
            //get { return sessionManage.loggingSessionInfo; }
            //set { sessionManage.loggingSessionInfo = value; }
            get { return (LoggingSessionInfo)this.Session["loggingSessionInfo"]; }
            set { this.Session["loggingSessionInfo"] = value; }
        }
        #endregion

        public void Redirect(string msg, InfoType type, string go)
        {
            //string url = string.Format("~/InfoBoxPage.aspx?info={0}&type={1}&go={2}"
            //    , System.Web.HttpContext.Current.Server.UrlEncode(msg)
            //    , (int)type
            //    , System.Web.HttpContext.Current.Server.UrlEncode(go));
            //this.Response.Redirect(url);
            string script = @"
            alert('{0}');
            location.href='{1}';
        ";
            this.ClientScript.RegisterClientScriptBlock(typeof(int), "redirect", string.Format(script, msg.Replace("\r", @"\r")
                .Replace("\n", @"\n")
                .Replace("\t", @"\t")
                .Replace("'", @"\'")
                , go), true);
        }

    }
}

    
public class JsonData
{
    public string id { get; set; }
    public string totalCount { get; set; }
    public object data { get; set; }
    public string status { get; set; }

    public string topics { get; set; }
    public bool success { get; set; }
    public string msg { get; set; }
}
public class ResponseData
{
    public bool success { get; set; }
    public string msg { get; set; }
    public object data { get; set; }
    public string status { get; set; }
}

public class InfoBox
{
    private System.Web.UI.Page _page;
    public InfoBox(System.Web.UI.Page page)
    {
        _page = page;
    }

    public void ShowPopInfo(string msg)
    {
        _page.ClientScript.RegisterStartupScript(this.GetType(), "show_pop_" + DateTime.Now.Ticks,
            string.Format("infobox.showPop('{0}','{1}');", (msg ?? "").Replace("'", @"\'").Replace("\r", @"\r").Replace("\n", @"\n"), "info")
            , true);
    }

    public void ShowPopError(string msg)
    {
        _page.ClientScript.RegisterStartupScript(this.GetType(), "show_pop_" + DateTime.Now.Ticks,
           string.Format("infobox.showPop('{0}','{1}');", (msg ?? "").Replace("'", @"\'").Replace("\r", @"\r").Replace("\n", @"\n"), "error")
           , true);
    }
}

public enum InfoType : int
{
    Info = 1,
    Warning = 2,
    Error = 3,
}
public class PageLog
{
    private static PageLog _log;
    public static PageLog Current { get { if (_log == null) { _log = new PageLog(); } return _log; } }
    public void Write(Exception ex)
    {
        Write(string.Format("{0}", ex));
    }
    public void Write(string content)
    {
        record(string.Format("{0}\t{1}\r\n", DateTime.Now, content));
    }

    private void record(string log)
    {
        try
        {
            var path = "~/PageLog";
            path = System.Web.HttpContext.Current.Server.MapPath(path);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            path += string.Format("/{0:yyyy-MM-dd}.txt", DateTime.Now);
            lock (this)
            {
                System.IO.File.AppendAllText(path, log);
            }
        }
        catch { }
    }

}