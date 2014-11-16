using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace JIT.CPOS.BS.Web.lj
{
    public partial class Auth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string AuthUrl = ConfigurationManager.AppSettings["AuthUrl"].Trim();
                string code = Request["code"];
                string url = AuthUrl + "lj/Auth1.aspx"
                            + "?code=" + code + "";
                Response.Redirect(url);
            }
        }
    }
}