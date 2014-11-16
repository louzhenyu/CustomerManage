using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace JIT.CPOS.Web.Lj
{
    public partial class VerificationFail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string webUrl = ConfigurationManager.AppSettings["website_url"];
            string strNoFollowUrl = webUrl + "lj/d20131114/VerificationFail.html";
            Response.Redirect(strNoFollowUrl);
        }
    }
}