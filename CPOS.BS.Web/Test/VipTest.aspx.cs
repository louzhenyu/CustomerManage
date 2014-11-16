using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.Test
{
    public partial class VipTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SearchVipInfo();
            //GetAll();
           // SetCopyTags();
            //SetPushInfo();
            //GetUnitIntegral();
            GetVipIntegral();
        }

        private void SearchVipInfo()
        {
            VipSearchEntity vipSearchInfo = new VipSearchEntity();
            vipSearchInfo.Page = 1;
            vipSearchInfo.PageSize = 15;
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "7d4cda48970b4ed0aa697d8c2c2e4af3");
            VipBLL vipServer = new VipBLL(loggingSessionInfo);
            VipEntity vipInfo = new VipEntity();
            vipInfo = vipServer.SearchVipInfo(vipSearchInfo);
            if (vipInfo != null)
            {
                this.lb1.Text = vipInfo.ICount.ToString();
                if (vipInfo.ICount > 0)
                {
                    this.GridView1.DataSource = vipInfo.vipInfoList;
                    this.GridView1.DataBind();
                }
            }

        }

        private void GetAll()
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "7d4cda48970b4ed0aa697d8c2c2e4af3");

            SysVipSourceBLL vipSourceServer = new SysVipSourceBLL(loggingSessionInfo);
            IList<SysVipSourceEntity> list = new List<SysVipSourceEntity>();
            list = vipSourceServer.GetAll();
        }

        private void SetCopyTags()
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "00078c78c22b4e7696391ed32f011255");

            TagsBLL tagsServer = new TagsBLL(loggingSessionInfo);
            bool bReturnTags = tagsServer.setCopyTag("111");
        }

        private void SetPushInfo()
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo("f6a7da3d28f74f2abedfc3ea0cf65c01", "17e02a6dd0094de9b18da1ca73d37027");

            string strOrderId = "1b4c3bfb63884f0b86bd4b5c771bccaf";
            cUserService userServer = new cUserService(loggingSessionInfo);
            userServer.SendOrderMessage(strOrderId);
        }

        private void GetUnitIntegral()
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo("f6a7da3d28f74f2abedfc3ea0cf65c01", "17e02a6dd0094de9b18da1ca73d37027");
            VipEntity vipSearchInfo = new VipEntity();
            VipEntity info = new VipEntity();
            VipBLL vipServer = new VipBLL(loggingSessionInfo);
            string strError = string.Empty;
            info = vipServer.GetUnitIntegral(vipSearchInfo,out strError);
        }

        private void GetPurchasingGuideIntegral()
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo("f6a7da3d28f74f2abedfc3ea0cf65c01", "17e02a6dd0094de9b18da1ca73d37027");
            VipEntity vipSearchInfo = new VipEntity();
            VipEntity info = new VipEntity();
            VipBLL vipServer = new VipBLL(loggingSessionInfo);
            string strError = string.Empty;
            info = vipServer.GetPurchasingGuideIntegral(vipSearchInfo, out strError);
        }

        private void GetVipIntegral()
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo("f6a7da3d28f74f2abedfc3ea0cf65c01", "17e02a6dd0094de9b18da1ca73d37027");
            VipEntity vipSearchInfo = new VipEntity();
            VipEntity info = new VipEntity();
            VipBLL vipServer = new VipBLL(loggingSessionInfo);
            string strError = string.Empty;
            info = vipServer.GetVipIntegral(vipSearchInfo, out strError);
        }
    }
}