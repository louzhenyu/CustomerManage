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
using System.Text;

namespace JIT.CPOS.Web.wap
{
    public partial class TerminalSalesInfo : System.Web.UI.Page
    {
        public static string strDiv = string.Empty;
        public static string strUnit = string.Empty;
        public static string strToday = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            StringBuilder sb = new StringBuilder();
            var currentUser = Default.GetBSLoggingSession("f6a7da3d28f74f2abedfc3ea0cf65c01", null); //03201e18799a47179d2f6bf8424d86c9
            string unitId = Request.QueryString["unit_id"];// "8c41446fe80d4f2e9e3d659df01641fa";
            hUnitId.Value = unitId;
            if (unitId == null || unitId.Length == 0)
                unitId = "8c41446fe80d4f2e9e3d659df01641fa";
            UnitService unitService = new UnitService(currentUser);
            var unitObj = unitService.GetUnitById(unitId);
            string unitName = unitObj.Name;
            hUnitId.Value = unitId;
            strUnit = unitName;
            strToday = DateTime.Now.ToString("yyyy-MM-dd");

            var list = unitService.GetPosOrder(unitId, 7);

            sb.AppendFormat("<table style=\"border:1px solid #ccc; border-collapse:collapse;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td rowspan=\"2\" class=\"ts_td\">会员</td>");
            sb.AppendFormat("<td rowspan=\"2\" class=\"ts_td\">销售金额</td>");
            sb.AppendFormat("<td rowspan=\"2\" class=\"ts_td\">奖励金额</td>");
            sb.AppendFormat("<td rowspan=\"2\" class=\"ts_td\">配送方式</td>");
            sb.AppendFormat("<td colspan=\"3\" class=\"ts_td\">分成</td>");
            sb.AppendFormat("</tr><tr>");
            sb.AppendFormat("<td class=\"ts_td2\">粉丝奖励</td>");
            sb.AppendFormat("<td class=\"ts_td2\">交易奖励</td>");
            sb.AppendFormat("<td class=\"ts_td2\">总奖励金额</td>");
            sb.AppendFormat("</tr>");

            for (var i = 0; i < list.Count; i++)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"ts_td3\">{0}</td>", list[i].VipName);
                sb.AppendFormat("<td class=\"ts_td3\">{0}</td>", list[i].TotalAmount);
                sb.AppendFormat("<td class=\"ts_td3\">{0}</td>", list[i].RewardAmount);
                sb.AppendFormat("<td class=\"ts_td3\">{0}</td>", list[i].DeliveryName);
                sb.AppendFormat("<td class=\"ts_td3\">{0}%</td>", list[i].FansAwards);
                sb.AppendFormat("<td class=\"ts_td3\">{0}%</td>", list[i].TransactionAwards);
                sb.AppendFormat("<td class=\"ts_td3\">{0}元</td>", list[i].RewardTotalAmount);
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</table>");
            
            strDiv = sb.ToString();
        }

    }
}