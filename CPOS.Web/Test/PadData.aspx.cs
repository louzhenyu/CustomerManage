using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility.Log;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Configuration;

using System.Web.UI.WebControls;
using JIT.CPOS.Common;
using System.Text;

namespace JIT.CPOS.Web.Test
{
    public partial class PadData : System.Web.UI.Page
    {
        public string webUrl = ConfigurationManager.AppSettings["website_url"];
        protected void Page_Load(object sender, EventArgs e)
        {
            //string str = Convert.ToString(880.ToString().TrimEnd('.', '0'));
            //GetShowCount();
            //SearchInoutDetailInfoByVip();
            //TestStr();
            //SetOrderInfo();
            //GetURL();
            string strState = "29E11BDC6DAC439896958CC6866FF64,24F084EDA94648E4BEFBDB11597EC42A,http://xxx:9012/OnlineClothing/index.html";
            //strState = CommonCompress.StringCompress(strState);
            //strState = CommonCompress.StringDeCompress(strState);
            //string state = "180sb153sb154sb216669012555OnlineClothing555indexsbhtmlsss29E11BDC6DAC439896958CC6866FF64Esss24F084EDA94648E4BEFBDB11597EC42A";
            //state = ((((state).Replace("sb", ".")).Replace("555", "/")).Replace("666", ":")).Replace("sss", ",");
            //string[] array = state.Split(',');
            //string customerId = array[1];
            //string applicationId = array[2];
            //string goUrl = array[0];
            string str = string.Empty;
            str = HttpUtility.UrlEncode(strState, Encoding.UTF8);
            byte[] buff = Encoding.UTF8.GetBytes(str);
            str = Convert.ToBase64String(buff);
            byte[] buff1 = Convert.FromBase64String(str);
            str = Encoding.UTF8.GetString(buff1);
            str = HttpUtility.UrlDecode(str, Encoding.UTF8);
        }

        private void GetURL()
        {

            var dataStr = "";
            var url = "http://112.124.43.61:8009/GetNumService.asmx/GetNum?date=2013-10-14 17:14:43";
            //Encoding code = Encoding.GetEncoding(AlipayConfig.Input_charset);
            string urlTm = "xx:9012/OnlineClothing/index.html";
            url = webUrl + "OAuthWX.aspx?customerId=29E11BDC6DAC439896958CC6866FF64E&applicationId=24F084EDA94648E4BEFBDB11597EC42A&goUrl=" + HttpUtility.UrlEncode(urlTm) + "";
            dataStr = url;
            //dataStr = Utils.GetRemoteData(url, "GET", "");
        }

        private void TestStr()
        {
            string str = "&lt;a href=\""+webUrl+"wap/shopDetail.html?serviceId=6&amp;openId=#openID#&amp;eventID=4\"&gt;点击查询&lt;/a&gt;";
            str = BaseService.NoHTML(BaseService.unHtml(str));

        }

        private void GetRecentfollowers()
        { 
            
        }

        private void GetShowCount()
        {
            string Timestamp = "1368781342950";
            long NewTimestamp = 0;
            VipBLL vipService = new VipBLL(Default.GetLoggingSession());
            int count = vipService.GetShowCount(Convert.ToInt64(Timestamp),out NewTimestamp);
        }

        private void SearchInoutDetailInfoByVip()
        {
            var inoutService = new InoutService(Default.GetLoggingSession());
            OrderSearchInfo queryInfo = new OrderSearchInfo();
            queryInfo.vip_no = "0334f76bad484c02af4dd8c32802025b"; ;
            queryInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            queryInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            queryInfo.red_flag = "1";
            queryInfo.StartRow = 0;
            queryInfo.EndRow = 0 + 15;
            queryInfo.unit_id = "bae1ed3ce4db4524a6d2398299075fbf";
            var data = inoutService.SearchInoutDetailInfoByVip(queryInfo);
        }

        #region
        private string SetOrderInfo()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = "{\"special\":{\"EventId\":\"1\" ,\"skuId\":\"18A5685F3B0D43909740EEC20AEFBC73\" ,\"userName\":\"邵志峰\" ,\"phone\":\"13764683490\" ,\"individuationInfo\":\"一统江湖\" ,\"salesPrice\":\"18000\",\"tableNumber\":\"4\" },\"common\":{\"weiXinId\":\"gh_bf70d7900c28\",\"openId\":\"o8Y7Ejm0kL4QB8-h_Z0Bncl619v4\"}} ";

            
                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                InoutService inoutService = new InoutService(loggingSessionInfo);
                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = inoutService.SetWapPosInoutInfo(reqObj.special.skuId.Trim()
                                                            , reqObj.special.eventId.Trim()
                                                            , OpenID.Trim()
                                                            , WeiXin.Trim()
                                                            , reqObj.special.userName.Trim()
                                                            , reqObj.special.phone.Trim()
                                                            , reqObj.special.individuationInfo.Trim()
                                                            , reqObj.special.salesPrice.Trim()
                                                            , reqObj.special.tableNumber.Trim()
                                                            , loggingSessionInfo
                                                            , out strError,out strMsg);
                if (bReturn)
                {
                    respData.code = "200";
                }
                else
                {
                    respData.code = "101";
                }
                respData.description = strError;
                return respData.ToJSON();
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setOrderInfoReqData : Default.ReqData
        {
            public setOrderInfoReqSpecialData special;
        }
        public class setOrderInfoReqSpecialData
        {
            public string eventId;		//活动标识
            public string skuId;	    //sku标识
            public string userName;	    //姓名
            public string phone;		//手机号
            public string individuationInfo;	//个性化信息
            public string salesPrice;	//价格
            public string tableNumber;  //座号
        }
        #endregion
    }
}