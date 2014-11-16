using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Web.Cookie;
using JIT.CPOS.BS.Web.Extension;
using JIT.Utility.Reflection;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Framework.MasterPage
{
    public partial class ChildPage : System.Web.UI.MasterPage
    {
        #region 页面入口
        /// <summary>
        /// pageload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        #endregion
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
    }
}