using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetDimension.Weibo;
using NetDimension.Web;
using System.Configuration;
using System.Text;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.Lj
{
    public partial class Auth1 : System.Web.UI.Page
    {
        //http://xxxx/lj/Auth.aspx?code=52d04dc20036dbd8 
        public string AuthUrlx = ConfigurationManager.AppSettings["website_WWW"];
        public string customerId = "e703dbedadd943abacf864531decdac1";//泸州老窖的客户标识 e703dbedadd943abacf864531decdac1
        public string applicationId = "386D08D106C849A9ACAA6E493D23E853"; //泸州老窖的微信标识 // --386D08D106C849A9ACAA6E493D23E853
        public string goUrl = "lj/d20131114/VerificationOK.html"; //跳转界面
        public string strRedirectUri = ConfigurationManager.AppSettings["website_WWW"] + "lj/LjAuth.aspx";   //回调界面
        public string strState = string.Empty;
        public string strNoFollowUrl = "lj/d20131114/VerificationOK.html";
        public LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("进入泸州老窖认证界面1<br/>");
            if (!IsPostBack)
            {
                string authCode = Request["code"];
                string url = AuthUrlx + "lj/LjAuth.aspx"
                            + "?cId=" + customerId + ""
                            + "&aId=" + applicationId + ""
                            + "&autoCode=" + authCode + ""
                            + "&goUrl=" + goUrl + "";
                Response.Redirect(url);
            }
        }
    }
}