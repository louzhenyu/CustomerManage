using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.Web.ApplicationInterface.Project.HuaAn;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    public class ScanWxPayAH : BaseActionHandler<ScanWxPayRP, ScanWxPayRD>
    {
        /// <summary>
        /// 微信扫码支付
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override ScanWxPayRD ProcessRequest(DTO.Base.APIRequest<ScanWxPayRP> pRequest)
        {
            var rd = new ScanWxPayRD();
            var par = pRequest.Parameters;

            try
            {
                //获取登录信息,数据库的一些链接与基础信息
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.OpenID);

                var rechargeOrderBLL = new RechargeOrderBLL(loggingSessionInfo);
                decimal appOrderAmount = 0;  //实际支付金额

                if (par.PaymentScenarios == 1) //根据支付场景区分对应的订单表获取实际支付金额
                {
                    //直充订单支付金额
                    var rechargeOrderInfo = rechargeOrderBLL.GetByID(par.OrderId);
                    if (rechargeOrderInfo != null)
                        appOrderAmount = rechargeOrderInfo.ActuallyPaid.Value;
                }
                else
                {
                    var inout = new T_InoutBLL(loggingSessionInfo);

                    //售卡订单支付金额
                    var inoutOrderInfo = inout.GetByID(par.OrderId);
                    if (inoutOrderInfo != null)
                        appOrderAmount = inoutOrderInfo.actual_amount.Value;
                }

                //交易中心请求参数
                var para = new
                {
                    PayChannelID = par.ChannelID,
                    AppOrderID = par.OrderId,
                    AppOrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"),
                    AppOrderAmount = ToInt(appOrderAmount * 100),
                    AppOrderDesc = par.OrderDesc,
                    Currency = 1,
                    MobileNO = par.Mobile,
                    ReturnUrl = string.Empty,
                    DynamicID = string.Empty,
                    DynamicIDType = string.Empty,
                    Paras = new Dictionary<string, object>(),
                    OpenId = pRequest.OpenID,
                    ClientIP = Utils.GetHostAddress(),
                    PaymentMode = par.PaymentMode
                };

                var request = new
                {
                    AppID = 1,
                    ClientID = pRequest.CustomerID,
                    UserID = pRequest.UserID,
                    Parameters = para
                };

                //Json参数准备
                string pUrlPath = ConfigurationManager.AppSettings["paymentcenterUrl"];
                string jsonString = string.Format("action=CreateOrder&request={0}", request.ToJSON());

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "创建订单到交易中心，请求地址：" + pUrlPath + ",请求参数：" + jsonString
                });

                string httpResponse = HttpHelper.SendHttpRequest(pUrlPath, jsonString);

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("交易中心返回结果：{0}", httpResponse.ToJSON())
                });

                //反序列化
                var payres = httpResponse.DeserializeJSONTo<RespCreateOrder>();

                if (payres.ResultCode == 0)
                {
                    //根据微信返回的CodeUrl生成二维码
                    string currentDomain = ConfigurationManager.AppSettings["website_WWW"].ToString();
                    string sourcePath = HttpContext.Current.Server.MapPath("/Images/qrcode2.jpg");
                    string targetPath = HttpContext.Current.Server.MapPath("/file/images/");

                    var payQrCodeUrl = Utils.GenerateQRCodeWx(payres.Datas.QrCodeUrl,
                        currentDomain, sourcePath, targetPath); //生成二维码方法
                    rd.QrCodeUrl = payQrCodeUrl;
                }

            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("交易中心代码异常信息：{0}", ex.Message + ";堆栈信息：" + ex.StackTrace)
                });
            }

            return rd;
        }
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
    }

    public class RespCreateOrder
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public RespCreateOrderData Datas { get; set; }
    }
    public class RespCreateOrderData
    {
        public string OrderID { get; set; }
        public string PayUrl { get; set; }
        public string QrCodeUrl { get; set; }
        public string Message { get; set; }
        public string WXPackage { get; set; }//微信支付包信息
    }

}