using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web
{
    public partial class t : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Response.Write("进入短连接转换界面<br/>");
                pageGo();
            }
        }

        private void pageGo()
        {
            string ShortUrl = HttpContext.Current.Request.Url.ToString();
            Response.Write(ShortUrl);
            int i = ShortUrl.IndexOf("&sukey=");
            if (i > 0)
            {
                ShortUrl = ShortUrl.Substring(0, ShortUrl.IndexOf("&sukey="));
            }
            Response.Write("<br/>");
            Response.Write(ShortUrl);
            //string pare = HttpContext.Current.Request.Url.Query.ToString();
            LoggingSessionInfo loggingSessionInfo = Default.GetAPLoggingSession("");
            string strError = string.Empty;
            string OldUrl = string.Empty;
            ShortUrlChangeBLL server = new ShortUrlChangeBLL(loggingSessionInfo);
            bool bReturn = server.GetShortUrlChange(ShortUrl, out OldUrl, out strError);
            if (bReturn)
            {
                Response.Redirect(OldUrl);
            }
            else
            {
                this.lb1.Text = strError;
            }
        }
    }
}