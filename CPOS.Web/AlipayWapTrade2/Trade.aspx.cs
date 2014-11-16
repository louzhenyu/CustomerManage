using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.AlipayWapTrade2;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.AlipayWapTrade2
{
    /// <summary>
    /// 功能：手机网页支付接口接入页
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// /////////////////注意///////////////////////////////////////////////////////////////
    /// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
    /// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
    /// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
    /// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
    /// 
    /// 如果不想使用扩展功能请把扩展功能参数赋空值。
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
            BaseService.WriteLog("支付宝交易开始-----------------------AlipayWapTrade2/Trade.aspx");
            BaseService.WriteLog("调用授权接口alipay.wap.trade.create.direct获取授权码token");
            ////////////////////////////////////////////调用授权接口alipay.wap.trade.create.direct获取授权码token////////////////////////////////////////////

            string order_id = string.Empty;
            string call_back_url = AlipayConfig.Merchant_url;

            //订单号
            if (!string.IsNullOrEmpty(Request["order_id"]))
            {
                order_id = Request["order_id"].Trim();
                BaseService.WriteLog("order_id:  " + Request["order_id"]);
            }
            else
            {
                BaseService.WriteLog("请求参数order_id is null!!!!!");
            }
            //产品名称
            if (!string.IsNullOrEmpty(Request["prod_name"]))
            {
                AlipayConfig.Subject = Request["prod_name"].Trim();
                BaseService.WriteLog("prod_name:  " + Request["prod_name"]);
            }
            else
            {
                BaseService.WriteLog("请求参数prod_name is null!!!!!");
            }
            //产品价格
            if (!string.IsNullOrEmpty(Request["prod_price"]))
            {
                AlipayConfig.Total_fee = Request["prod_price"].Trim();
                BaseService.WriteLog("prod_price:  " + Request["prod_price"]);
            }
            else
            {
                BaseService.WriteLog("请求参数prod_price is null!!!!!");
            }
            //用户付款中途退出返回URL
            if (!string.IsNullOrEmpty(Request["merchant_url"]))
            {
                AlipayConfig.Merchant_url = Request["merchant_url"].Trim();
                BaseService.WriteLog("merchant_url:  " + Request["merchant_url"]);
            }
            else
            {
                BaseService.WriteLog("请求参数merchant_url is null!!!!!");
            }
            //用户付款成功同步返回URL
            if (!string.IsNullOrEmpty(Request["call_back_url"]))
            {
                call_back_url = Request["call_back_url"].Trim();
                BaseService.WriteLog("call_back_url:  " + Request["call_back_url"]);
            }
            else
            {
                BaseService.WriteLog("请求参数call_back_url is null!!!!!");
            }

            AlipayConfig.Req_id = System.Guid.NewGuid().ToString().Replace("-", "");
            AlipayConfig.Out_trade_no = System.Guid.NewGuid().ToString().Replace("-", "");

            //分润参数
            if (!string.IsNullOrEmpty(Request["params"]))
            {
                BaseService.WriteLog("params:  " + Request["params"]);

                var entity = new AlipayRoyaltyEntity()
                {
                    RoyaltyID = System.Guid.NewGuid().ToString().Replace("-", ""),
                    OutTradeNo = AlipayConfig.Out_trade_no,
                    RoyaltyParameters = Request["params"]
                };

                //保存分润参数到数据库
                var royaltyServer = new AlipayRoyaltyBLL(Default.GetLoggingSession());
                royaltyServer.Create(entity);
            }
            else
            {
                BaseService.WriteLog("请求参数params is null!!!!!");
            }

            //请求业务参数详细
            string req_dataToken = ""
                + "<direct_trade_create_req>"
                + "<notify_url>" + AlipayConfig.Notify_url + "</notify_url>"
                + "<call_back_url>" + AlipayConfig.Call_back_url + "</call_back_url>"
                + "<seller_account_name>" + AlipayConfig.Seller_account_name + "</seller_account_name>"
                + "<out_trade_no>" + AlipayConfig.Out_trade_no + "</out_trade_no>"
                + "<subject>" + AlipayConfig.Subject + "</subject>"
                + "<total_fee>" + AlipayConfig.Total_fee + "</total_fee>"
                + "</direct_trade_create_req>";
            BaseService.WriteLog("请求业务参数详细： " + req_dataToken);

            //把请求参数打包成数组
            Dictionary<string, string> sParaTempToken = new Dictionary<string, string>();
            sParaTempToken.Add("partner", AlipayConfig.Partner);
            sParaTempToken.Add("_input_charset", AlipayConfig.Input_charset.ToLower());
            sParaTempToken.Add("sec_id", AlipayConfig.Sign_type.ToUpper());
            sParaTempToken.Add("service", AlipayConfig.Service_Create);
            sParaTempToken.Add("format", AlipayConfig.Format);
            sParaTempToken.Add("v", AlipayConfig.V);
            sParaTempToken.Add("req_id", AlipayConfig.Req_id);
            sParaTempToken.Add("req_data", req_dataToken);

            //建立请求
            string sHtmlTextToken = AlipaySubmit.BuildRequest(AlipayConfig.Req_url, sParaTempToken);
            //URLDECODE返回的信息
            Encoding code = Encoding.GetEncoding(AlipayConfig.Input_charset);
            sHtmlTextToken = HttpUtility.UrlDecode(sHtmlTextToken, code);

            //解析远程模拟提交后返回的信息
            Dictionary<string, string> dicHtmlTextToken = AlipaySubmit.ParseResponse(sHtmlTextToken);

            //获取token
            string request_token = dicHtmlTextToken["request_token"];

            BaseService.WriteLog("根据授权码token调用交易接口alipay.wap.auth.authAndExecute");
            ////////////////////////////////////////////根据授权码token调用交易接口alipay.wap.auth.authAndExecute////////////////////////////////////////////

            //业务详细
            string req_data = ""
                + "<auth_and_execute_req>"
                + "<request_token>" + request_token + "</request_token>"
                + "</auth_and_execute_req>";
            BaseService.WriteLog("业务详细： " + req_data);

            //把请求参数打包成数组
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("partner", AlipayConfig.Partner);
            sParaTemp.Add("_input_charset", AlipayConfig.Input_charset.ToLower());
            sParaTemp.Add("sec_id", AlipayConfig.Sign_type.ToUpper());
            sParaTemp.Add("service", AlipayConfig.Service_Execute);
            sParaTemp.Add("format", AlipayConfig.Format);
            sParaTemp.Add("v", AlipayConfig.V);
            sParaTemp.Add("req_data", req_data);

            BaseService.WriteLog("保存交易记录到数据库");

            var alipayEntity = new AlipayWapTradeResponseEntity()
            {
                ResponseID = System.Guid.NewGuid().ToString().Replace("-", ""),
                OrderID = order_id,
                OutTradeNo = AlipayConfig.Out_trade_no,
                Subject = AlipayConfig.Subject,
                TotalFee = AlipayConfig.Total_fee,
                MerchantUrl = AlipayConfig.Merchant_url,
                CallBackUrl = call_back_url,
                Status = "1"
            };

            //保存交易记录到数据库
            AlipayWapTradeResponseBLL alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());
            alipayServer.Create(alipayEntity);

            //建立请求
            string sHtmlText = AlipaySubmit.BuildRequest(AlipayConfig.Req_url, sParaTemp, "get", "确认");

            Response.Write(sHtmlText);
        }
    }
}