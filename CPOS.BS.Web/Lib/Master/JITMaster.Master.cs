using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility;

namespace JIT.ManagementPlatform.Web.Lib.Master
{
    public partial class JITMaster : System.Web.UI.MasterPage
    {
        public CommonMenuButton[] MenuList;
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion
    }
}