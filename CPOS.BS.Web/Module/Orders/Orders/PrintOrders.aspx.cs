using System;
using System.Data;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.Module.Orders.Orders
{
    public partial class PrintOrders : System.Web.UI.Page
    {
        public string res = "";
        string pOrdersID = "";
        DataSet ds = new DataSet();
        TInoutDetailEntity entity = new TInoutDetailEntity();

        protected void Page_Load(object sender, EventArgs e)
        {
            pOrdersID = Request.QueryString["pOrdersID"];
            string  isHotel =Request.QueryString["isHotel"];
            if (!string.IsNullOrEmpty(pOrdersID))
            {
                entity.OrderID = pOrdersID;
                //花间堂定制酒店订单
                ds = new TInoutDetailBLL(new SessionManager().CurrentUserLoginInfo).GetOrdersDetail(entity, isHotel == null ? "" : isHotel);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 1; i < ds.Tables.Count; i++)
                    {
                        res += "<div class=\"ebooking_list_t\" style=\"width:700px;\">";
                        res += "    <h2>花间堂预订客房通知单  <br>" + ds.Tables[0].Rows[0]["StoreName"].ToString() + "<br><br></h2>";
                        res += "    <div class=\"pages_spilt\" style=\"width: 700px; font-size: 14px;\">";
                        res += "        <span id=\"lblContactName\"></span>   订单号：<span id=\"lblOrderID\">" + ds.Tables[0].Rows[0]["OrdersNo"].ToString() + "      新订</span>";
                        res += "    </div>";
                        res += "</div>";
                        res += "<div class=\"form_area_detail\" style=\"width:700px;\">";
                        res += "    <table style=\"width:700px; font-size: 14px;\">";
                        res += "        <tbody>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">订单状态:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">" + ds.Tables[0].Rows[0]["OrdersStatusText"].ToString() + "</font> </span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">客人姓名:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblClientName\">" + ds.Tables[0].Rows[0]["GuestName"].ToString() + "</span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">联系电话:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblLinkTel\">" + ds.Tables[0].Rows[0]["LinkTel"].ToString() + "</span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">住宿日期:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblStayDate\">" + ds.Tables[0].Rows[0]["StartDate"].ToString() + "   至   " + ds.Tables[0].Rows[0]["EndDate"].ToString() + "   共   " + ds.Tables[0].Rows[0]["QTY"].ToString() + "  晚</span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">预订客房:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblRoom\">" + ds.Tables[0].Rows[0]["StoreName"].ToString() + " -  " + ds.Tables[0].Rows[0]["RoomTypeName"].ToString() + "  " + ds.Tables[0].Rows[0]["RoomCount"].ToString() + "   间   </span>";
                        res += "                </td>";
                        res += "            </tr>";



                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">优惠劵抵扣金额:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">应付 RMB <font color=\"red\">   " + ds.Tables[0].Rows[0]["integral"].ToString() + "</font>  元</span><span id=\"lblTotAddOptional\"></span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">积分抵扣金额:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">应付 RMB  <font color=\"red\">   " + ds.Tables[0].Rows[0]["couponAmount"].ToString() + "</font>  元</span><span id=\"lblTotAddOptional\"></span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">余额抵扣金额:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">应付 RMB  <font color=\"red\">   " + ds.Tables[0].Rows[0]["vipEndAmount"].ToString() + "</font>  元</span><span id=\"lblTotAddOptional\"></span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">总计金额:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">应付 RMB <font color=\"red\">   " + ds.Tables[0].Rows[0]["totalamount"].ToString() + "</font>  元</span><span id=\"lblTotAddOptional\"></span>";
                        res += "                </td>";
                        res += "            </tr>";




                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">付款方式:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">到店付款, 共计 RMB <font color=\"red\">   " + ds.Tables[0].Rows[0]["Amount"].ToString() + "</font>  元</span><span id=\"lblTotAddOptional\"></span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">备注:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">" + ds.Tables[0].Rows[0]["Remark"] + "</span><span id=\"lblTotAddOptional\"></span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "            <tr>";
                        res += "                <th style=\"font-size: 14px; width: 130px;\">下单时间:</th>";
                        res += "                <td style=\"font-size: 14px;\">";
                        res += "                    <span id=\"lblPaymentTerm\">" + ds.Tables[0].Rows[0]["CreateTime"].ToString() + "</font> </span>";
                        res += "                </td>";
                        res += "            </tr>";
                        res += "        </tbody>";
                        res += "    </table>";
                        res += "</div>";
                    }
                }
            }
        }
    }
}