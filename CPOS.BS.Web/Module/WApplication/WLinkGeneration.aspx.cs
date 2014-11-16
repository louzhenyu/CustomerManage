using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.BS.Web.Module.WApplication
{
    public partial class WLinkGeneration : JIT.CPOS.BS.Web.PageBase.JITChildPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string CustomerID { get {
            return this.CurrentUserInfo.ClientID;
        } }
    }
}