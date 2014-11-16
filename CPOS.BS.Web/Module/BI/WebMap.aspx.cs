using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.BS.Web.Module.BI
{
    public partial class WebMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string ReportUrl = "http://map.jitmarketing.cn:8083/default.aspx?kpiid=1034";
                lblReport.Text = "<iframe src=\"" + ReportUrl + "\" frameborder=\"0\" id=\"frm\" scrolling=\"auto\" width=\"100%\" height=\"620\" />";
            }
        }
    }
}