using System;
using System.Collections.Generic;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.AlipayWapTrade;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.AlipayWapTrade
{
    /// <summary>
    /// 功能：服务器异步通知页面
    /// 
    /// ///////////////////页面功能说明///////////////////
    /// 
    /// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
    /// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
    /// TRADE_FINISHED(表示交易已经成功结束);
    /// 该通知页面主要功能是：对于返回页面（call_back_url.aspx）做补单处理。如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
    /// </summary>
    public partial class Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseService.WriteLog("服务器异步通知页面-----------------------Notify.aspx");

            //前台页面别忘记加这句指令，否则会报特殊字符的异常 ValidateRequest="false"

            //获取加密的notify_data数据
            string notify_data = Request.Form["notify_data"];
            BaseService.WriteLog("加密的notify_data数据: " + notify_data);

            //通过商户私钥进行解密
            notify_data = Function.Decrypt(notify_data, Config.PrivateKey, Config.Input_charset_UTF8);
            BaseService.WriteLog("解密后的notify_data数据: " + notify_data);

            //获取签名
            string sign = Request.Form["sign"];
            BaseService.WriteLog("sign签名: " + sign);

            //创建待签名数组，注意Notify这里数组不需要进行排序，请保持以下顺序
            Dictionary<string, string> sArrary = new Dictionary<string, string>();

            //组装验签数组
            sArrary.Add("service", Request.Form["service"]);
            sArrary.Add("v", Request.Form["v"]);
            sArrary.Add("sec_id", Request.Form["sec_id"]);
            sArrary.Add("notify_data", notify_data);

            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string content = Function.CreateLinkString(sArrary);

            BaseService.WriteLog("开始验证签名");
            //验证签名
            bool vailSign = Function.Verify(content, sign, Config.Alipaypublick, Config.Input_charset_UTF8);
            BaseService.WriteLog("结束验证签名");
            BaseService.WriteLog("验证签名结果： " + vailSign);

            if (!vailSign)
            {
                Response.Write("fail");
                return;
            }

            BaseService.WriteLog("开始获取交易状态");
            //获取交易状态
            string trade_status = Function.GetStrForXmlDoc(notify_data, "notify/trade_status");
            BaseService.WriteLog("结束获取交易状态");
            BaseService.WriteLog("交易状态trade_status： " + trade_status);

            if (!trade_status.Equals("TRADE_FINISHED"))
            {
                Response.Write("fail");
            }
            else
            {
                ///////////////////////////////处理数据/////////////////////////////////
                // 用户这里可以写自己的商业逻辑
                // 例如：修改数据库订单状态
                // 以下数据仅仅进行演示如何调取
                // 参数对照请详细查阅开发文档
                // 里面有详细说明

                var alipayEntity = new AlipayWapTradeResponseEntity()
                {
                    OutTradeNo = Function.GetStrForXmlDoc(notify_data, "notify/out_trade_no"),
                    Subject = Function.GetStrForXmlDoc(notify_data, "notify/subject"),
                    TotalFee = Function.GetStrForXmlDoc(notify_data, "notify/total_fee"),
                    PaymentType = Function.GetStrForXmlDoc(notify_data, "notify/payment_type"),
                    TradeNo = Function.GetStrForXmlDoc(notify_data, "notify/trade_no"),
                    BuyerEmail = Function.GetStrForXmlDoc(notify_data, "notify/buyer_email"),
                    GmtCreate = Function.GetStrForXmlDoc(notify_data, "notify/gmt_create"),
                    NotifyType = Function.GetStrForXmlDoc(notify_data, "notify/notify_type"),
                    Quantity = Function.GetStrForXmlDoc(notify_data, "notify/quantity"),
                    NotifyTime = Function.GetStrForXmlDoc(notify_data, "notify/notify_time"),
                    SellerID = Function.GetStrForXmlDoc(notify_data, "notify/seller_id"),
                    TradeStatus = Function.GetStrForXmlDoc(notify_data, "notify/trade_status"),
                    IsTotalFeeAdjust = Function.GetStrForXmlDoc(notify_data, "notify/is_total_fee_adjust"),
                    GmtPayment = Function.GetStrForXmlDoc(notify_data, "notify/gmt_payment"),
                    SellerEmail = Function.GetStrForXmlDoc(notify_data, "notify/seller_email"),
                    GmtClose = Function.GetStrForXmlDoc(notify_data, "notify/gmt_close"),
                    Price = Function.GetStrForXmlDoc(notify_data, "notify/price"),
                    BuyerID = Function.GetStrForXmlDoc(notify_data, "notify/buyer_id"),
                    NotifyID = Function.GetStrForXmlDoc(notify_data, "notify/notify_id"),
                    UseCoupon = Function.GetStrForXmlDoc(notify_data, "notify/use_coupon"),
                    Status = "2"
                };

                BaseService.WriteLog("交易成功，更新支付宝交易状态");

                AlipayWapTradeResponseBLL alipayServer = new AlipayWapTradeResponseBLL(new Utility.BasicUserInfo());
                alipayServer.UpdateAlipayWapTrade(alipayEntity);
                ////////////////////////////////////////////////////////////////////////////

                Response.Write("success");
            }
        }
    }
}