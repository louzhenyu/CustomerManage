using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.AlipayWapTrade
{
    public partial class PaySuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseService.WriteLog("支付成功页面---------------------------PaySuccess.aspx");

            if (!IsPostBack)
            {
                if (Request["order_id"] != null)
                {
                    BaseService.WriteLog("order_id:  " + Request["order_id"]);
                }
                if (Request["result"] != null)
                {
                    BaseService.WriteLog("result:  " + Request["result"]);
                }
                if (Request["out_trade_no"] != null)
                {
                    BaseService.WriteLog("out_trade_no:  " + Request["out_trade_no"]);
                }
            }
        }
    }
}