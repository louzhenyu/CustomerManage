using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.AppDownload
{
    public partial class download : System.Web.UI.Page
    {
        public string IOSUrl = string.Empty;
        public string AndroidUrl = string.Empty;
        public string ImageUrl = "images/img.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string customerId = "29E11BDC6DAC439896958CC6866FF64E";
                if (!string.IsNullOrEmpty(Request["CustomerId"]))
                {
                    customerId = Request["CustomerId"];
                }
                GetAppDownloadInfo(customerId);
            }
        }

        private void GetAppDownloadInfo(string customerId)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            APPDownloadBLL server = new APPDownloadBLL(loggingSessionInfo);
            var list = server.QueryByEntity(new APPDownloadEntity
            {
                CustomerId = customerId
                ,IsDelete = 0
            }, null);
            if (list != null && list.Length > 0 && list[0] != null)
            {
                APPDownloadEntity info = list[0];
                IOSUrl = info.IOSDownloadUrl.ToString().Trim();
                AndroidUrl = info.AndroidDownloadUrl.ToString().Trim();
                ImageUrl = info.ImageUrl.Trim();
            }
        }

    }
}