using System;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.AlipayWapTrade;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.AlipayWapTrade
{
    /// <summary>
    /// 功能：无线交易创建接入页
    /// </summary>
    public partial class Trade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Submit();
            }
        }

        private void Submit()
        {
            BaseService.WriteLog("支付宝交易开始-----------------------Trade.aspx");
            BaseService.WriteLog("初始化Service");

            //初始化Service
            Service ali = new Service();
            string order_id = string.Empty;
            string call_back_url = Config.Merchant_url;

            //订单号
            if (Request["order_id"] != null)
            {
                order_id = Request["order_id"].Trim();
                BaseService.WriteLog("order_id:  " + Request["order_id"]);
            }
            else
            {
                BaseService.WriteLog("请求参数order_id is null!!!!!");
            }
            //产品名称
            if (Request["prod_name"] != null)
            {
                Config.Subject = Request["prod_name"].Trim();
                BaseService.WriteLog("prod_name:  " + Request["prod_name"]);
            }
            else
            {
                BaseService.WriteLog("请求参数prod_name is null!!!!!");
            }
            //产品价格
            if (Request["prod_price"] != null)
            {
                Config.Total_fee = Request["prod_price"].Trim();
                BaseService.WriteLog("prod_price:  " + Request["prod_price"]);
            }
            else
            {
                BaseService.WriteLog("请求参数prod_price is null!!!!!");
            }
            //用户付款中途退出返回URL
            if (Request["merchant_url"] != null)
            {
                Config.Merchant_url = Request["merchant_url"].Trim();
                BaseService.WriteLog("merchant_url:  " + Request["merchant_url"]);
            }
            else
            {
                BaseService.WriteLog("请求参数merchant_url is null!!!!!");
            }
            //用户付款成功同步返回URL
            if (Request["call_back_url"] != null)
            {
                call_back_url = Request["call_back_url"].Trim();
                BaseService.WriteLog("call_back_url:  " + Request["call_back_url"]);
            }
            else
            {
                BaseService.WriteLog("请求参数call_back_url is null!!!!!");
            }

            Config.Req_id = System.Guid.NewGuid().ToString().Replace("-", "");
            Config.Out_trade_no = System.Guid.NewGuid().ToString().Replace("-", "");

            BaseService.WriteLog("创建交易接口");
            //创建交易接口
            string token = ali.alipay_wap_trade_create_direct(
               Config.Req_url,
               Config.Subject,
               Config.Out_trade_no,
               Config.Total_fee,
               Config.Seller_account_name,
               Config.Notify_url,
               Config.Out_user,
               Config.Merchant_url,
               Config.Call_back_url,
               Config.Service_Create,
               Config.Sec_id,
               Config.Partner,
               Config.Req_id,
               Config.Format,
               Config.V,
               Config.Req_url,
               Config.PrivateKey,
               Config.Input_charset_UTF8);

            BaseService.WriteLog("Config.Req_url: " + Config.Req_url);
            BaseService.WriteLog("Config.Subject: " + Config.Subject);
            BaseService.WriteLog("Config.Out_trade_no: " + Config.Out_trade_no);
            BaseService.WriteLog("Config.Total_fee: " + Config.Total_fee);
            BaseService.WriteLog("Config.Seller_account_name: " + Config.Seller_account_name);
            BaseService.WriteLog("Config.Notify_url: " + Config.Notify_url);
            BaseService.WriteLog("Config.Out_user: " + Config.Out_user);
            BaseService.WriteLog("Config.Merchant_url: " + Config.Merchant_url);
            BaseService.WriteLog("Config.Call_back_url: " + Config.Call_back_url);
            BaseService.WriteLog("Config.Service_Create: " + Config.Service_Create);
            BaseService.WriteLog("Config.Sec_id: " + Config.Sec_id);
            BaseService.WriteLog("Config.Partner: " + Config.Partner);
            BaseService.WriteLog("Config.Req_id: " + Config.Req_id);
            BaseService.WriteLog("Config.Format: " + Config.Format);
            BaseService.WriteLog("Config.V: " + Config.V);
            BaseService.WriteLog("Config.Req_url: " + Config.Req_url);
            BaseService.WriteLog("Config.PrivateKey: " + Config.PrivateKey);
            BaseService.WriteLog("Config.Input_charset_UTF8: " + Config.Input_charset_UTF8);
            BaseService.WriteLog("返回token: " + token);
            BaseService.WriteLog("创建交易接口结束");

            BaseService.WriteLog("构造，重定向URL");
            //构造，重定向URL
            string url = ali.alipay_Wap_Auth_AuthAndExecute(
                Config.Req_url,
                Config.Sec_id,
                Config.Partner,
                Config.Call_back_url,
                Config.Format,
                Config.V,
                Config.Service_Auth,
                token,
                Config.Req_url,
                Config.PrivateKey,
                Config.Input_charset_UTF8);

            BaseService.WriteLog("最终要跳转的URL字符串: " + url);
            BaseService.WriteLog("构造，重定向URL结束");

            var alipayEntity = new AlipayWapTradeResponseEntity()
            {
                ResponseID = System.Guid.NewGuid().ToString().Replace("-", ""),
                OrderID = order_id,
                OutTradeNo = Config.Out_trade_no,
                Subject = Config.Subject,
                TotalFee = Config.Total_fee,
                MerchantUrl = Config.Merchant_url,
                CallBackUrl = call_back_url,
                Status = "1"
            };

            //保存交易记录到数据库
            AlipayWapTradeResponseBLL alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());
            alipayServer.Create(alipayEntity);

            //跳转收银台支付页面
            Response.Redirect(url);
        }
    }
}