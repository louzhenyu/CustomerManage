using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using System.IO;
using System.Text;
using System.Xml;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Pay.WeiXinPay
{
    /// <summary>
    /// 获取订单包裹
    /// <remarks>
    /// <para>微信支付平台回调业务系统以获取订单信息</para>
    /// </remarks>
    /// </summary>
    public class GetOrderPackage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo() { Message = "支付通知接口被调用" });
            

            #region 获取url参数
            //签名类型，取值： MD5MD5 、RSA RSA，默认： MD5
            var signType = context.Request.QueryString["sign_type"];
            //版本号，默认为 1.0
            var serviceVersion = context.Request.QueryString["service_version"];
            //字符编码 ,取值： GBKGBK 、UTFUTFUTF-8，默认： GBKGBK 。
            var inputCharset = context.Request.QueryString["input_charset"];
            //签名
            var sign = context.Request.QueryString["sign"];
            //多密钥支持的序号，默认 1
            var signKeyIndex = context.Request.QueryString["sign_key_index"];
            //1-即时到账 其他保留
            var tradeMode = context.Request.QueryString["trade_mode"];
            //支付结果：0—成功 其他保留
            var tradeState = context.Request.QueryString["trade_state"];
            //支付结果信息，成功时为空
            var payInfo = context.Request.QueryString["pay_info"];
            //商户号，也即之前步骤的 商户号，也即之前步骤的 partnerid,由微信统一分配的 10 位正整数 (120XXXXXXX)号
            var partner = context.Request.QueryString["partner"];
            //银行类型，在微信中使用 WX
            var bankType = context.Request.QueryString["bank_type"];
            //银行订单号
            var bankBillno = context.Request.QueryString["bank_billno"];
            //支付金额，单位为分如果 支付金额，单位为分如果 支付金额，单位为分如果 discount 有值，通知的 total_fee + discount = 请求的 total_fee 
            var totalFee = context.Request.QueryString["total_fee"];
            //现金支付币种 ,目前只支持人民币 默认值是 1-人民币
            var feeType = context.Request.QueryString["fee_type"];
            //支付结果通知 id ，对于某些特定商 户，只返回通知 id ，要求商户据此查询交易结果
            var notifyId = context.Request.QueryString["notify_id"];
            //交易号， 28 位长的数值，其中前 位长的数值，其中前 10 位为商户号，之后 8位为订单产生 的日期，如 20090415，最后 10 位是流水号。
            var transactionId = context.Request.QueryString["transaction_id"];
            //商户系统的订单号，与请求一致。
            var outTradeNo = context.Request.QueryString["out_trade_no"];
            //商户 数据包，原样返回
            var attach = context.Request.QueryString["attach"];
            //支付完成时间，格式为yyyyMMddhhmmss ，如 2009 年 12 月 27 日 9点 10 分 10 秒表示为 秒表示为 20091227091010。时区为 GMT+8 beijing
            var timeEnd = context.Request.QueryString["time_end"];
            //物流费用，单位分默认0。如果有值，必须保证 transport_fee + product_fee = total_fee
            var transportFee = context.Request.QueryString["transport_fee"];
            //物品费用，单位分。如果有值必须保证 transport_fee + product_fee = total_fee
            var productFee = context.Request.QueryString["product_fee"];
            //折扣价格，单位分.如果有值,通知的 total_fee + discount =  请求的 total_fee 
            var discount = context.Request.QueryString["discount"];
            //对应买家账号的一个加密串
            var buyerAlias = context.Request.QueryString["buyer_alias"];

            #endregion

         

            var appSignature = string.Empty;
            var appId = string.Empty;
            var isSubscribe = string.Empty;
            var timeStamp = string.Empty;
            var nonceStr = string.Empty;
            var openID = string.Empty;

            using (var stream = context.Request.InputStream)
            {
                using (var rd = new StreamReader(stream, Encoding.UTF8))
                {
                    var xmlStr = rd.ReadToEnd();
                    Loggers.Debug(new DebugLogInfo() { Message = "传入内容:" + xmlStr });
                    var doc = new XmlDocument();
                    doc.LoadXml(xmlStr);
                    nonceStr = doc.SelectSingleNode("xml/NonceStr").InnerText;
                    appSignature = doc.SelectSingleNode("xml/AppSignature").InnerText;
                    appId = doc.SelectSingleNode("xml/AppId").InnerText;
                    isSubscribe = doc.SelectSingleNode("xml/IsSubscribe").InnerText;
                    timeStamp = doc.SelectSingleNode("xml/TimeStamp").InnerText;
                    openID = doc.SelectSingleNode("xml/OpenId").InnerText;
                }
            }

            #region

            var customerWxMappingBll = new TCustomerWeiXinMappingBLL(Default.GetAPLoggingSession(""));

            var customerId = customerWxMappingBll.GetCustomerIdByAppId(appId);

            if (customerId == "")
            {
                throw new APIException("客户ID为空") { ErrorCode = 121 };
            }


            var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");


            var wxPayNoticeEntity = new WXPayNoticeEntity
            {
                SignType = signType,
                Sign = sign,
                ServiceVersion = serviceVersion,
                InputCharset = inputCharset,
                SignKeyIndex = Convert.ToInt32(signKeyIndex),
                TradeMode = Convert.ToInt32(tradeMode),
                TradeState = Convert.ToInt32(tradeState),
                PayInfo = payInfo,
                Partner = partner,
                BankType = bankType,
                BankBillno = bankBillno,
                TotalFee = Convert.ToInt32(totalFee),
                FeeType = Convert.ToInt32(feeType),
                NotifyId = notifyId,
                TransactionId = transactionId,
                OutTradeNo = outTradeNo,
                Attach = attach,
                TimeEnd = timeEnd,
                TransportFee = Convert.ToInt32(transportFee),
                ProductFee = Convert.ToInt32(productFee),
                Discount = Convert.ToInt32(discount),
                BuyerAlias = buyerAlias,
                AppId = appId,
                TimeStamp = Convert.ToInt32(timeStamp),
                NonceStr = nonceStr,
                OpenId = openID,
                AppSignature = appSignature,
                IsSubscribe = 1,
                CustomerId = customerId
            };

            var wxPayNoticeBll = new WXPayNoticeBLL(currentUserInfo);

            var entity = wxPayNoticeBll.QueryByEntity(new WXPayNoticeEntity()
            {
                OutTradeNo = outTradeNo,
                OpenId = openID
            }, null);
            if (entity != null)
            {
                wxPayNoticeBll.Delete(entity);
            }
            wxPayNoticeBll.Create(wxPayNoticeEntity);

            //将请求数据记录到表中、方便维权的时候试用
            //1.向表中记录该笔支付是否成功，如不成功，将失败原因记录下来【status = 1 成功 0 失败 2 支付金额与订单金额不符】

            //2.判断支付是否成功
            //if (tradeState == "0")//成功
            //{
            //    //3.根据订单号查询该笔订单的金额是否相符，如不符合，记录信息，查看原因

            //}
            //else//失败
            //{
            //    //
            //}

            if (tradeState == "0")
            {
                //根据customerid获取channelid;
                var channelBll = new TPaymentTypeCustomerMappingBLL(currentUserInfo);
                var channelId = channelBll.GetChannelIdByCustomerId(customerId);

                var paras = "ChannelID=" + channelId + "&outTradeNo=" + outTradeNo;

                var url = System.Configuration.ConfigurationManager.AppSettings["wxNativePayNotifyUrl"];
                JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo() {Message = url + "?" + paras});
                var response = JIT.Utility.Web.HttpClient.PostQueryString(url, paras);
                JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo() {Message = response});
            }

            #region 向表中记录调用的微信接口

            var wxInterfaceLogBll = new WXInterfaceLogBLL(currentUserInfo);
            var wxInterfaceLogEntity = new WXInterfaceLogEntity();
            wxInterfaceLogEntity.LogId = Guid.NewGuid();
            wxInterfaceLogEntity.InterfaceUrl = System.Configuration.ConfigurationManager.AppSettings["wxNativePayNotifyUrl"];
            wxInterfaceLogEntity.AppId = appId;
            wxInterfaceLogEntity.OpenId = openID;
            wxInterfaceLogEntity.RequestParam = wxPayNoticeEntity.ToJSON();
            wxInterfaceLogEntity.IsSuccess =1;
            wxInterfaceLogBll.Create(wxInterfaceLogEntity);

            #endregion


            context.Response.Write("success");


            #endregion
        }

        //public void ProcessRequest(HttpContext context)
        //{

        //    JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo() { Message = "GetOrderPackage被调用" });
        //    context.Response.ContentType = "text/plain";
        //    try
        //    {
        //        string productID = string.Empty;
        //        string customerID = string.Empty;
        //        string appSignature = string.Empty;
        //        string appID = string.Empty;
        //        string isSubscribe = string.Empty;
        //        string timeStamp = string.Empty;
        //        string nonceStr = string.Empty;
        //        string openID = string.Empty;
        //        T_InoutEntity order = null;
        //        //支付中心URL
        //        var url = System.Configuration.ConfigurationManager.AppSettings["paymentcenterUrl"];

        //        //url = "http://121.199.42.125:6002/DevPayTest.ashx";//本机测试,正式需注释此行
        //        //url = "http://localhost:1266/Gateway.ashx";

        //        int paychannelID;

        //        Loggers.Debug(new DebugLogInfo() { Message = "支付中心接口URL:" + url });

        //        #region 取出WX平台发来的XML数据
        //        using (var stream = context.Request.InputStream)
        //        {
        //            using (var rd = new StreamReader(stream, Encoding.UTF8))
        //            {
        //                var xmlStr = rd.ReadToEnd();
        //                Loggers.Debug(new DebugLogInfo() { Message = "传入内容:" + xmlStr });
        //                XmlDocument doc = new XmlDocument();
        //                doc.LoadXml(xmlStr);
        //                productID = HttpUtility.UrlDecode(doc.SelectSingleNode("xml/ProductId").InnerText);
        //                nonceStr = doc.SelectSingleNode("xml/NonceStr").InnerText;
        //                appSignature = doc.SelectSingleNode("xml/AppSignature").InnerText;
        //                appID = doc.SelectSingleNode("xml/AppId").InnerText;
        //                isSubscribe = doc.SelectSingleNode("xml/IsSubscribe").InnerText;
        //                timeStamp = doc.SelectSingleNode("xml/TimeStamp").InnerText;
        //                openID = doc.SelectSingleNode("xml/OpenId").InnerText;

        //                //客户ID根据AppID获取客户ID,cpos_ap库.表TCustomerWeiXinMapping
        //                //var userinfo = new LoggingSessionInfo() { Conn = System.Configuration.ConfigurationManager.AppSettings["AppConn"] };

        //                customerID = "24af2889e6054496b1903e0ba5dd01cf";//先写死测试
        //                //customerID = System.Configuration.ConfigurationManager.AppSettings["CustomerId"];

        //                //微信支付通道ID ？？如何获取,根据客户ID获取
        //                paychannelID = 104;
        //               // paychannelID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PayChannelId"]);
        //                #region 验证有效性  暂时未做验证
        //                var para1 = new
        //                {
        //                    PayChannelID = paychannelID,//int	支付通道ID
        //                    OpenID = openID,//string	点击链接准备购买商品的用户 openid
        //                    IsSubscribe = isSubscribe,//string	标记用户是否订阅该公众帐号，1 为关注，0 为未关注
        //                    TimeStamp = timeStamp,//string	时间戳
        //                    NonceStr = nonceStr,//string	类型，1-商品，2-订单
        //                    ObjectID = openID,//string	商品ID或者订单ID
        //                    Sign = appSignature,//string	微信平台返回的签名
        //                };
        //                var request1 = new
        //                {
        //                    AppID = 1,
        //                    ClientID = customerID,
        //                    Parameters = para1
        //                };
        //                //string json1 = string.Format("action=WXCheckSign&request={0}", request1.ToJSON());
        //                //var response1 = JIT.Utility.Web.HttpClient.PostQueryString(url, json1);
        //                //var dic1 = response1.DeserializeJSONTo<Dictionary<string, object>>();
        //                //if (Convert.ToInt32(dic1["ResultCode"]) < 100)
        //                //{
        //                //    var tempdic1 = dic1["Datas"].ToJSON().DeserializeJSONTo<Dictionary<string, string>>();
        //                //    if (tempdic1.ContainsKey("IsSuccess"))
        //                //    {
        //                //        var success = tempdic1["IsSuccess"];
        //                //        if (success.ToLower() == "false")
        //                //            throw new Exception(string.Format("未通过验证,无效的请求:", xmlStr));
        //                //    }
        //                //}
        //                #endregion
        //            }
        //        }
        //        #endregion

        //        var logginInfo = Default.GetBSLoggingSession(customerID, "1");

        //        var type = productID.Substring(0, 1);

        //        #region 根据传来的ProductID来差别是单个商品还是订单,叛别方法，第一个字符是0则为商品，第一个字符是1则为订单
        //        var skuBll = new SkuService(logginInfo);
        //        var tInOutBll = new T_InoutBLL(logginInfo);
        //        var tInOutDetailBll = new T_Inout_DetailBLL(logginInfo);
        //        switch (type)
        //        {
        //            //如果是商品则先创建一个订单,再组织数据调用支付中心生成Package内容
        //            case "1":
        //                var skuID = productID.Substring(1).ToGuidBy24Chars().ToString("N");
        //                var sku = skuBll.GetSkuInfoById(skuID);
        //                order = new T_InoutEntity()
        //                {
        //                    order_id = Guid.NewGuid().ToString("N"),
        //                    customer_id = customerID,
        //                };
        //                var orderDetail = new T_Inout_DetailEntity()
        //                {
        //                    order_detail_id = Guid.NewGuid().ToString("N"),
        //                    sku_id = sku.sku_id
        //                };
        //                tInOutBll.Create(order);
        //                tInOutDetailBll.Create(orderDetail);
        //                break;
        //            //如果是订单，从业务数据库中找到该订单的信息，然后组织数据调用支付中心生成Package内容
        //            case "2":
        //                var orderID = productID.Substring(1).ToGuidBy24Chars().ToString("N");
        //                Loggers.Debug(new DebugLogInfo() { Message = "开始查找订单:" + orderID });
        //                order = tInOutBll.GetByID(orderID);
        //                if (order == null)
        //                {
        //                    Loggers.Debug(new DebugLogInfo() { Message = string.Format("客户:{0}未找到订单:{1}", customerID, orderID) });
        //                }
        //                Loggers.Debug(new DebugLogInfo() { Message = "找到订单:" + orderID });
        //                break;
        //            default:
        //                throw new Exception("错误的ProductID:" + productID);
        //        }
        //        #endregion

        //        string package = string.Empty;
        //        //调用支付中心
        //        var dic = new Dictionary<string, object>();
        //        Loggers.Debug(new DebugLogInfo() { Message = "开始调用支付中心接口,生成Package" });
        //        dic["SpbillCreateIp"] = "127.0.0.1";
        //        //如果已支付
        //        if (order.Field1 == "1")
        //        {
        //            dic["RetCode"] = "100";
        //            dic["RetErrMsg"] = "Order has been paid!";
        //        }
        //        var para2 = new
        //        {
        //            PayChannelID = paychannelID,
        //            AppOrderID = order.order_id,
        //            AppOrderTime = DateTime.Now.ToJITFormatString(),
        //            AppOrderAmount = Convert.ToInt32(order.total_amount * 100),
        //            AppOrderDesc = "微信支付订单",//这里还需再讨论
        //            Currency = "1",
        //            MobileNO = "",
        //            DynamicID = "",
        //            DynamicIDType = "",
        //            Paras = dic
        //        };
        //        Loggers.Debug(new DebugLogInfo() { Message = "生成业务参数:" + para2.ToJSON() });
        //        var request2 = new
        //        {
        //            AppID = 1,
        //            ClientID = customerID,
        //            UserID = order.vip_no,
        //            Parameters = para2
        //        };
        //        Loggers.Debug(new DebugLogInfo() { Message = "生成请求参数:" + request2.ToJSON() });
        //        string json = string.Format("action=CreateOrder&request={0}", request2.ToJSON());
        //        Loggers.Debug(new DebugLogInfo() { Message = "发送请求:" + url.Trim('?') + json });
        //        var response = JIT.Utility.Web.HttpClient.PostQueryString(url, json);
        //        Loggers.Debug(new DebugLogInfo() { Message = "支付中心生成返回JSON:" + response });
        //        dic = response.DeserializeJSONTo<Dictionary<string, object>>();
        //        if (Convert.ToInt32(dic["ResultCode"]) < 100)
        //        {
        //            var tempdic = dic["Datas"].ToJSON().DeserializeJSONTo<Dictionary<string, string>>();
        //            if (tempdic.ContainsKey("WXPackage"))
        //                package = tempdic["WXPackage"];
        //        }
        //        Loggers.Debug(new DebugLogInfo() { Message = "Package数据" + package });
        //        context.Response.Write(package);
        //    }
        //    catch (Exception ex)
        //    {
        //        Loggers.Exception(new ExceptionLogInfo(ex));
        //        context.Response.Write(ex.Message);
        //    }
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}