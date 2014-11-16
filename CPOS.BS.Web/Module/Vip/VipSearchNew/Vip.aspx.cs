using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Module.Vip.VipSearchNew
{
    public partial class Vip : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}