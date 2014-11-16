using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.wap
{
    public partial class TerminalSalesInfo2 : System.Web.UI.Page
    {
        public static string strDiv = string.Empty;
        public static string strDiv2 = string.Empty;
        public static string topDataStr = "";
        public static string topDataStr2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadData2();
            }
        }

        protected void LoadData()
        {
            var currentUser = Default.GetBSLoggingSession("f6a7da3d28f74f2abedfc3ea0cf65c01", null); //03201e18799a47179d2f6bf8424d86c9
            string unitId = Request.QueryString["unit_id"];// "8c41446fe80d4f2e9e3d659df01641fa";
            UnitService unitService = new UnitService(currentUser);
            var unitObj = unitService.GetUnitById(unitId);
            string unitName = unitObj.Name;
            decimal totalAmount = 0;
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            //string today = (new DateTime(2013, 10, 11)).ToString("yyyy-MM-dd");
            string topOrderStr = "";

            InoutService service = new InoutService(currentUser);
            OrderSearchInfo queryInfo = new OrderSearchInfo();
            queryInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            queryInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            queryInfo.red_flag = "1";
            queryInfo.StartRow = 0;
            queryInfo.EndRow = 1000;
            queryInfo.unit_id = unitId;
            queryInfo.order_date_begin = today;
            //花间堂定制酒店 非花间堂使用false
            var list = service.SearchInoutInfo(queryInfo,false);

            int n = 1;
            if (list != null && list.InoutInfoList != null)
            {
                foreach (var item in list.InoutInfoList)
                {
                    totalAmount += item.total_amount;
                    if (n <= 2) topOrderStr += GetOrderStr(item);
                    n++;
                }
            }

            topDataStr = "[";
            var topData = service.GetInoutDetailInfoByTop3(queryInfo);
            if (topData != null && topData.Count > 0)
            {
                decimal total = 0;
                for (var i = 0; i < topData.Count; i++)
                {
                    total += topData[i].retail_amount;
                }
                for (var i = 0; i < topData.Count; i++) 
                {
                    var topItem = topData[i];
                    //string percent = string.Format("{0,25:N}", (topItem.retail_amount / total * 100));
                    string percent = (topItem.retail_amount / total * 100).ToString("f1");
                    if (i > 0) topDataStr += ",";
                    topDataStr += "{";
                    topDataStr += string.Format(" name:\"{0}\", data1:\"{1}\", data2:\"{2}\" ",
                        topItem.item_name != null && topItem.item_name.Length > 10 ?
                        topItem.item_name.Substring(0, 10) + "..." : topItem.item_name, 
                        topItem.retail_amount,
                        percent + "%");
                    topDataStr += "}";
                }
            }
            topDataStr += "]";


            //if (Request["store_name"] != null && Request["store_name"].Trim().Length > 0)
            //{
            //    unitName = HttpUtility.HtmlDecode(Request["store_name"].Trim());
            //}

            this.lblUnitName.InnerHtml = unitName;
            this.lblDate.InnerHtml = today;
            this.lblTotalAmount.InnerHtml = string.Format("{0,25:N}", totalAmount);
            strDiv = topOrderStr;
        }

        protected void LoadData2()
        {
            var dataStr = "";
            var url = "http://112.124.43.61:8009/GetTopOrderInfo.asmx/GetTopOrdersInfo";
            dataStr = Utils.GetRemoteData(url, "GET", "");
            var dataObj = dataStr.DeserializeJSONTo<OrderInfoData>();

            //var currentUser = Default.GetBSLoggingSession("03201e18799a47179d2f6bf8424d86c9", null);
            string unitId = "f10329cd8c1147769568ab4790fa01e5";
            string unitName = "体验店";
            decimal totalAmount = 0;
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            //string today = (new DateTime(2013, 10, 11)).ToString("yyyy-MM-dd");
            string topOrderStr2 = "";

            //InoutService service = new InoutService(currentUser);
            //OrderSearchInfo queryInfo = new OrderSearchInfo();
            //queryInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            //queryInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            //queryInfo.red_flag = "1";
            //queryInfo.StartRow = 0;
            //queryInfo.EndRow = 1000;
            //queryInfo.unit_id = unitId;
            //queryInfo.order_date_begin = today;
            //var list = service.SearchInoutInfo(queryInfo);

            var list = new InoutInfo();
            list.InoutInfoList = new List<InoutInfo>();
            foreach (var dataItem in dataObj.orders)
            {
                list.InoutInfoList.Add(new InoutInfo()
                {
                    order_no = dataItem.order_no,
                    vip_name = dataItem.vip_name,
                    total_amount = dataItem.total_amount != null && dataItem.total_amount.Length > 0 ? Convert.ToDecimal(dataItem.total_amount) : 0,
                    Field8 = dataItem.Field8,
                    Field4 = dataItem.Field4,
                    modify_time = dataItem.modify_time != null ? dataItem.modify_time : ""
                });
            }

            totalAmount = dataObj.totalAmount != null && dataObj.totalAmount.Length > 0 ? Convert.ToDecimal(dataObj.totalAmount) : 0;
            int n = 1;
            if (list != null && list.InoutInfoList != null)
            {
                foreach (var item in list.InoutInfoList)
                {
                    //totalAmount += item.total_amount;
                    if (n <= 2) topOrderStr2 += GetOrderStr2(item);
                    n++;
                }
            }

            topDataStr2 = "[";
            //var topData = service.GetInoutDetailInfoByTop3(queryInfo);
            var topData = new List<InoutDetailInfo>();
            foreach (var dataItem in dataObj.topItems)
            {
                topData.Add(new InoutDetailInfo()
                {
                    retail_amount = dataItem.retail_amount != null && dataItem.retail_amount.Length > 0 ? Convert.ToDecimal(dataItem.retail_amount) : 0,
                    item_name = dataItem.item_name
                });
            }
            if (topData != null && topData.Count > 0)
            {
                decimal total = 0;
                for (var i = 0; i < topData.Count; i++)
                {
                    total += topData[i].retail_amount;
                }
                if (total == 0) total = 1;
                for (var i = 0; i < topData.Count; i++)
                {
                    var topItem = topData[i];
                    //string percent = string.Format("{0,25:N}", (topItem.retail_amount / total * 100));
                    string percent = (topItem.retail_amount / total * 100).ToString("f1");
                    if (i > 0) topDataStr2 += ",";
                    topDataStr2 += "{";
                    topDataStr2 += string.Format(" name:\"{0}\", data1:\"{1}\", data2:\"{2}\" ",
                        topItem.item_name != null && topItem.item_name.Length > 10 ?
                        topItem.item_name.Substring(0, 10) + "..." : topItem.item_name,
                        topItem.retail_amount,
                        percent + "%");
                    topDataStr2 += "}";
                }
            }
            topDataStr2 += "]";


            //if (Request["store_name"] != null && Request["store_name"].Trim().Length > 0)
            //{
            //    unitName = HttpUtility.HtmlDecode(Request["store_name"].Trim());
            //}

            this.lblUnitName2.InnerHtml = unitName;
            this.lblDate2.InnerHtml = today;
            this.lblTotalAmount2.InnerHtml = string.Format("{0,25:N}", totalAmount);
            strDiv2 = topOrderStr2;
        }

        //protected string GetOrderStr(InoutInfo order)
        //{
        //    string str = "<li>";
        //    str += string.Format("<div style=\"float: left; width: 30%;\">{0}</div>", order.order_no);
        //    str += string.Format("<div style=\"float: left; width: 20%; text-align:right;\">{0,25:N}&nbsp;&nbsp;</div>", order.total_amount);
        //    str += string.Format("<div style=\"float: left; width: 20%;\">{0}</div>", "交易成功");
        //    str += string.Format("<div style=\"float: left; width: 25%;\">{0}</div>",
        //        order.modify_time == null && order.modify_time.Trim().Length > 0 ? string.Empty :
        //        Convert.ToDateTime(order.modify_time).ToString("yyyy-MM-dd HH:mm"));
        //    str += string.Format("</li>");
        //    return str;
        //}

        protected string GetOrderStr(InoutInfo order)
        {
            string str = "<li>";
            str += string.Format("<div style=\"float: left; width: 30%; text-align:center;\">{0}&nbsp;</div>", order.order_no);
            str += string.Format("<div style=\"float: left; width: 10%; text-align:center;\">{0}&nbsp;</div>", order.vip_name);
            str += string.Format("<div style=\"float: left; width: 17%; text-align:center;\">{0,25:N}元&nbsp;&nbsp;</div>", order.total_amount);
            str += string.Format("<div style=\"float: left; width: 25%; text-align:center;\">{0}<br/>{1}</div>", order.Field8, order.Field4);
            str += string.Format("<div style=\"float: left; width: 18%; text-align:center;\">{0}&nbsp;</div>",
                order.modify_time == null || order.modify_time.Trim().Length == 0 ? string.Empty :
                Convert.ToDateTime(order.modify_time).ToString("MM-dd HH:mm"));
            str += string.Format("</li>");
            return str;
        }

        protected string GetOrderStr2(InoutInfo order)
        {
            string str = "<li>";
            str += string.Format("<div style=\"float: left; width: 30%; text-align:center;\">{0}&nbsp;</div>", order.order_no);
            str += string.Format("<div style=\"float: left; width: 10%; text-align:center;\">{0}&nbsp;</div>", order.vip_name);
            str += string.Format("<div style=\"float: left; width: 17%; text-align:center;\">{0,25:N}元&nbsp;&nbsp;</div>", order.total_amount);
            str += string.Format("<div style=\"float: left; width: 25%; text-align:center;\">{0}<br/>{1}</div>", order.Field8, order.Field4);
            str += string.Format("<div style=\"float: left; width: 18%; text-align:center;\">{0}&nbsp;</div>",
                order.modify_time == null || order.modify_time.Trim().Length == 0 ? string.Empty :
                Convert.ToDateTime(order.modify_time).ToString("MM-dd HH:mm"));
            str += string.Format("</li>");
            return str;
        }

        public class OrderInfoData
        {
            public IList<OrderData> orders;
            public IList<TopItemData> topItems;
            public string totalAmount;
        }
        public class OrderData
        {
            public string order_no;
            public string vip_name;
            public string total_amount;
            public string Field8;
            public string Field4;
            public string modify_time;
        }
        public class TopItemData
        {
            public string retail_amount;
            public string item_name;
        }
    }
}