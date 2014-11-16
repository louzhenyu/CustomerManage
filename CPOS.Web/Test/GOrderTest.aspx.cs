using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Configuration;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.Test
{
    public partial class GOrderTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SetGOrderPushAll();
            //GetReceiptConfirm();
            DateTime dt = DateTime.Parse("01/01/1970");
            TimeSpan ts = DateTime.Now - dt;
            int sec = ts.Seconds; // 秒数
        }

        /// <summary>
        /// 所有订单的推送
        /// </summary>
        private void SetGOrderPushAll()
        {
            GOrderBLL orderServer = new GOrderBLL(Default.GetLoggingSession());
            string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
            string strError = string.Empty;
            bool b = orderServer.SetGOrderPushAll( msgUrl, out strError);
            this.lbCode.Text = b.ToString();
            this.lbDesc.Text = strError;
        }

        private void GetReceiptConfirm()
        {
            GOrderBLL orderServer = new GOrderBLL(Default.GetLoggingSession());
            string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
            string strError = string.Empty;
            bool b = orderServer.GetReceiptConfirm("4C2BFAD08409499FB08DED7BC5F874F3", "70413b931e6840d8898cfd69c62d3eb6", msgUrl, out strError);
            this.lbCode.Text = b.ToString();
            this.lbDesc.Text = strError;
        }
    }
}