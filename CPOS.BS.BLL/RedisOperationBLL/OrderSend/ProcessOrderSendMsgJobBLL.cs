using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity.WX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using JIT.CPOS.BS.Entity;
using RedisOpenAPIClient.Models.CC.OrderSend;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderSend
{
    public class ProcessOrderSendMsgJobBLL
    {

        /// <summary>
        /// APP/后台订单发货-发送微信模板消息
        /// </summary>
        public void ProcessOrderSendMsg()
        {
            //
            var numCount = 100;
            var commonBLL = new CommonBLL();

            //
            var customerIDs = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in customerIDs)
            {
                //
                var count = RedisOpenAPI.Instance.CCOrderSend().GetOrderSendLength(new CC_OrderSend
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
                    var response = RedisOpenAPI.Instance.CCOrderSend().GetOrderSend(new CC_OrderSend
                    {
                        CustomerID = customer.Key
                    });
                    if (response.Code == ResponseCode.Success)
                    {
                        var templateID = response.Result.ConfigData.TemplateID;
                        var openID = response.Result.ConfigData.OpenID;
                        var vipID = response.Result.ConfigData.VipID;
                        var loggingSessionInfo = response.Result.ConfigData.LogSession.JsonDeserialize<LoggingSessionInfo>();
                        var orderSendData = new CommonData
                        {
                            first = new DataInfo { value = response.Result.OrderSendData.first.value, color = response.Result.OrderSendData.first.color },
                            keyword1 = new DataInfo { value = response.Result.OrderSendData.keyword1.value, color = response.Result.OrderSendData.keyword1.color },
                            keyword2 = new DataInfo { value = response.Result.OrderSendData.keyword2.value, color = response.Result.OrderSendData.keyword2.color },
                            keyword3 = new DataInfo { value = response.Result.OrderSendData.keyword3.value, color = response.Result.OrderSendData.keyword3.color },
                            remark = new DataInfo { value = response.Result.OrderSendData.remark.value, color = response.Result.OrderSendData.remark.color }
                        };

                        //
                        //return commonBLL.SendMatchWXTemplateMessage(wxTMConfigData.TemplateID, null, null, null, PaySuccessData, null, "15", OpenID, VipID, loggingSessionInfo);
                       // commonBLL.SendMatchWXTemplateMessage(templateID, null, null, null, paySuccessData, null, "15", openID, vipID, loggingSessionInfo);
                        //return  CommonBLL.SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, CommonData, null, null, null, null, "8", OpenID, VipID, loggingSessionInfo);
                        commonBLL.SendMatchWXTemplateMessage(templateID, orderSendData, null, null, null, null, null, null, null, "8", openID, vipID, loggingSessionInfo);
                    }
                }
            }
        }
    }
}
