using JIT.CPOS.BS.BLL.WX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.OrderSend;
using JIT.CPOS.BS.BLL.Utility;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using RedisOpenAPIClient.Models.CC.OrderNotPay;
using System;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderNotPay
{
    //读取缓存的job
    public class ProcessOrderNotPayMsgJobBLL
    {
        /// <summary>
        /// 订单未支付-发送微信模板消息
        /// </summary>
        public void ProcessNotPayMsg()
        {
            //
            var numCount = 100;
            var commonBLL = new CommonBLL();

            //
            var customerIDs = CustomerBLL.Instance.GetCustomerList();//获取所有商户
            foreach (var customer in customerIDs)
            {
                //
                var count = RedisOpenAPI.Instance.CCOrderNotPay().GetOrderNotPayLength(new CC_OrderNotPay
                {
                    CustomerID = customer.Key
                });
                if (count.Code != ResponseCode.Success)
                {
                    continue;
                }
                if (count.Result <= 0)
                {
                    continue;
                }

                //
                if (count.Result < numCount)
                {
                    numCount = Convert.ToInt32(count.Result);
                }

                //
                for (var i = 0; i < numCount; i++)
                {
                    //
                    var response = RedisOpenAPI.Instance.CCOrderNotPay().GetOrderNotPay(new CC_OrderNotPay
                    {
                        CustomerID = customer.Key
                    });
                    if (response.Code == ResponseCode.Success)
                    {
                        var templateID = response.Result.ConfigData.TemplateID;
                        var openID = response.Result.ConfigData.OpenID;
                        var vipID = response.Result.ConfigData.VipID;
                        var loggingSessionInfo = response.Result.ConfigData.LogSession.JsonDeserialize<LoggingSessionInfo>();
                        var Data = new NotPay
                        {
                            first = new DataInfo { value = response.Result.OrderNotPayData.first.value, color = response.Result.OrderNotPayData.first.color },
                            orderProductPrice = new DataInfo { value = response.Result.OrderNotPayData.orderProductPrice.value, color = response.Result.OrderNotPayData.orderProductPrice.color },
                            orderProductName = new DataInfo { value = response.Result.OrderNotPayData.orderProductName.value, color = response.Result.OrderNotPayData.orderProductName.color },
                            orderAddress = new DataInfo { value = response.Result.OrderNotPayData.orderAddress.value, color = response.Result.OrderNotPayData.orderAddress.color },
                            orderName = new DataInfo { value = response.Result.OrderNotPayData.orderName.value, color = response.Result.OrderNotPayData.orderName.color },
                            remark = new DataInfo { value = response.Result.OrderNotPayData.remark.value, color = response.Result.OrderNotPayData.remark.color }
                        };

                        commonBLL.SendMatchWXTemplateMessage(templateID, null, null, null, null, null, null, null, Data, "2", openID, null, loggingSessionInfo);

                    }
                }
            }
        }

    }
}
