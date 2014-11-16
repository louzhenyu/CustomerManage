using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.BS.Web
{
    public partial class Error : System.Web.UI.Page
    {
        protected string msg="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["msg"]))
            {
                msg = Request["msg"].ToString();
            }
        }
    }
}