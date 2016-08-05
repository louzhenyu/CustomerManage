using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.BS.Web
{
    public partial class L : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string domainName = HttpContext.Current.Request.Url.Host;
                string port = ":9004/";
                domainName += port;
                string traceCode = Request["c"];
                string url = "http://jp.lzlj.com:9004/WXOAuth/AuthUniversalV2.aspx?customerId=e703dbedadd943abacf864531decdac1&goUrl=jp.lzlj.com:9004/HtmlApps/auth.html?pageName=LJscanning%26traceCode=" + traceCode;
                //Response.Write(url);
                //Response.End();
                Response.Redirect(url);
            }
        }
    }
}