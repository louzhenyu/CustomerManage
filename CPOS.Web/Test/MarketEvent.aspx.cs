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
    public partial class MarketEvent : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //int str = string.Compare("10.0.1", "1.0.0");
            //GetMarketEventById();
            //SetEventPush();
            //GetWaveBandData();
            //SetVipTags();
            SetMarketPushApp();
        }

        private void SetMarketPushApp()
        {
            string EventID = "FC573AEC440C4BC7A530351A3D841E6C";
            PushIOSMessageBLL bll = new PushIOSMessageBLL(Default.GetLoggingSession());

            bool b = bll.SetMarketPushApp(EventID);
        }

        private void GetMarketEventById()
        {
            string EventID = "FC573AEC440C4BC7A530351A3D841E6C";
            MarketEventBLL bll = new MarketEventBLL(Default.GetLoggingSession());
            MarketEventEntity eventInfo = new MarketEventEntity();
            eventInfo = bll.GetMarketEventInfoById(EventID);
        }
        /// <summary>
        /// 测试发送
        /// </summary>
        private void SetEventPush()
        {
            MarketPersonBLL bll = new MarketPersonBLL(Default.GetLoggingSession());
            string EventID = "41D86F55613649539C480E697ADA9EBB";
            string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
            bool b = bll.SetEventPush(EventID, msgUrl, "1", true, true, true);
        }

        private void GetWaveBandData()
        {
            string content = string.Empty;
            var waveBandServer = new MarketWaveBandBLL(Default.GetLoggingSession());
            string EventID = "111";  //活动标识 1C3D38B1E7BF4A6090B8ED0EC7E0BCE6
            int Page = 0;//页码
            if (EventID != null && !EventID.Equals(""))
            {
                var data = waveBandServer.GetMarketWaveBandByEventID(EventID, Page, 15);
                content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.MarketWaveBandInfoList.ToJSON(),
               data.ICount);
            }
            
            
        }

        private void SetVipTags()
        {
            string Tags = ";C1839FAC577940958130A99D283B85A9,3;ABA28EBA210447ED8F634265D1BCF916,1";
            string[] tagsArr = Tags.Split(';');
            foreach (string tag in tagsArr) {
                if (!tag.Trim().Equals("")) {
                    string[] tagArr = tag.Split(',');
                    string tagId = tagArr[0];
                    string strlinkType = tagArr[1];
                }
            }
        }
    }
}