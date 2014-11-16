using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ShortUrl
{
    public partial class ShortUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string str = GenerateRandom(6);
            //this.lb1.Text = str;
            //string url = HttpContext.Current.Request.Url.Query;
            //string url1 = HttpContext.Current.Request.Url.ToString();
            //this.lb2.Text = url1;
            if (!IsPostBack) {
                pageGo();
            }
        }

        private void pageGo()
        {
            string ShortUrl = HttpContext.Current.Request.Url.ToString();
            int i = ShortUrl.IndexOf("&sukey=");
            if (i > 0)
            {
                ShortUrl = ShortUrl.Substring(0, ShortUrl.IndexOf("&sukey="));
            }
            string pare = HttpContext.Current.Request.Url.Query.ToString();
            LoggingSessionInfo loggingSessionInfo = Default.GetAPLoggingSession("");
            string strError = string.Empty;
            string OldUrl = string.Empty;
            ShortUrlChangeBLL server = new ShortUrlChangeBLL(loggingSessionInfo);
            bool bReturn = server.GetShortUrlChange(ShortUrl, out OldUrl, out strError);
            if (bReturn)
            {
                Response.Redirect(OldUrl);
            }
            else {
                this.lb1.Text = strError;
            }
        }

       
    }
}