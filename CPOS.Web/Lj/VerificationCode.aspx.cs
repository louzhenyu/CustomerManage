using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace JIT.CPOS.Web.Lj
{
    public partial class VerificationCode : System.Web.UI.Page
    {
        private string customerId = string.Empty;
        private string userId = string.Empty;
        private string openId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 获取基本参数
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                customerId = Request["customerId"];
                Response.Write("<br>");
                Response.Write("customerId:"+ customerId);
            }
            else
            {
                Response.Write("<br>");
                Response.Write("没有获取客户标识");
            }
            if (!string.IsNullOrEmpty(Request["userId"]))
            {
                userId = Request["userId"];
                Response.Write("<br>");
                Response.Write("userId:" + userId);
            }
            else
            {
                Response.Write("<br>");
                Response.Write("没有获取userId标识");
            }
            if (!string.IsNullOrEmpty(Request["openId"]))
            {
                openId = Request["openId"];
                Response.Write("<br>");
                Response.Write("openId:" + openId);
            }
            else
            {
                Response.Write("<br>");
                Response.Write("没有获取openId标识");
            }
            #endregion
            string webUrl = ConfigurationManager.AppSettings["website_url"];
            string strNoFollowUrl = webUrl + "lj/d20131114/VerificationWaring.html";
            Response.Redirect(strNoFollowUrl);
        }
    }
}