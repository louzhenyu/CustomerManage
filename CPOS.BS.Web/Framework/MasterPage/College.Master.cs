using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.Framework.MasterPage
{
    public partial class College : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //根据不同的模块，左侧导航菜单显示不同的样式。
            //add by zyh
            string url = Request.Path;
            if (url.IndexOf("Module/College/News/") != -1)
            {
                li_news.Attributes.Add("class", "menu002 on");
            }
            else if (url.IndexOf("/Module/College/Micro/") != -1)
            {
                li_micro.Attributes.Add("class", "menu003 on");
            }
            else if (url.IndexOf("/Module/ActivityManage/") != -1)
            {
                li_act.Attributes.Add("class", "menu004 on");
            }

            //泸州老窖的客户暂时把活动菜单显示 by yehua
            LoggingSessionInfo loginInfo = (LoggingSessionInfo)Session[SessionKeyConst.CURRENT_USER_LOGIN_INFO];
            if (loginInfo.ClientID == "e703dbedadd943abacf864531decdac1")
                li_act.Visible = true;
        }
    }
}