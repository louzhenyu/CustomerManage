using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.Web.WebServices;
using System.Configuration;

namespace JIT.CPOS.Web.Test
{
    public partial class ShortUrlChangeService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetShortUrlChange();
        }

        private void SetShortUrlChange()
        {
            string webUrl = ConfigurationManager.AppSettings["website_url3"];
            WebServices.ShortUrlChangeService server = new WebServices.ShortUrlChangeService();
            string strError = string.Empty;
            string ShortUrl = string.Empty;
            string strOldUrl = webUrl + ":9011/OnlineAppFood/detail.html?itemId=8D37947C924547169CEDFE72A68E53E6&customerId=dcb18a6e3e2a45cc96bef60aeb51cc96&storeId=201008b33c6249669194bc6a774efaf4&tableNum=&sukey=d53eb90b56603534524c0f3c99ba401ebe7416799a41e8b9634edb1c61f2c47bda629cb7e302d2c15d5fd5df7613a7d3ad80a6691a5fce00";
            bool bReturn = server.SetShortUrlChange(strOldUrl, out ShortUrl, out strError);
            if (bReturn)
            {
                this.lb1.Text = ShortUrl;
            }
            else {
                this.lb1.Text = "失败";
                this.lb2.Text = strError;

            }
        }
    }
}