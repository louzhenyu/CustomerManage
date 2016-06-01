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

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess
{
    /// <summary>
    /// 订单支付完成 队列 消息处理
    /// </summary>
    public class ProcessOrderPaySuccessMsgJobBLL
    {
        /// <summary>
        /// 出 订单支付完成 队列
        /// </summary>
        public void ProcessPaySuccessMsg()
        {
            //
            var numCount = 100;
            var commonBLL = new CommonBLL();

            //
            var customerIDs = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in customerIDs)
            {
                //
                var count = RedisOpenAPI.Instance.CCOrderPaySuccess().GetPaySuccessLength(new CC_PaySuccess
                {
                    CustomerID=customer.Key
                });
                if(count.Code != ResponseCode.Success)
                {
                    continue;
                }
                if(count.Result<=0)
                {
                    continue;
                }

                //
                if(count.Result<numCount)
                {
                    numCount = Convert.ToInt32(count.Result);
                }

                //
                for (var i = 0; i < numCount; i++)
                {
                    //
                    var response = RedisOpenAPI.Instance.CCOrderPaySuccess().GetPaySuccess(new CC_PaySuccess
                    {
                        CustomerID = customer.Key
                    });
                    if (response.Code == ResponseCode.Success)
                    {
                        var templateID = response.Result.ConfigData.TemplateID;
                        var openID = response.Result.ConfigData.OpenID;
                        var vipID = response.Result.ConfigData.VipID;
                        var loggingSessionInfo = response.Result.ConfigData.LogSession.JsonDeserialize<LoggingSessionInfo>();
                        var paySuccessData = new PaySuccess
                        {
                            first = new DataInfo { value = response.Result.PaySuccessData.first.value, color = response.Result.PaySuccessData.first.color },
                            orderAddress = new DataInfo { value = response.Result.PaySuccessData.orderAddress.value, color = response.Result.PaySuccessData.orderAddress.color },
                            orderName = new DataInfo { value = response.Result.PaySuccessData.orderName.value, color = response.Result.PaySuccessData.orderName.color },
                            orderProductName = new DataInfo { value = response.Result.PaySuccessData.orderProductName.value, color = response.Result.PaySuccessData.orderProductName.color },
                            orderProductPrice = new DataInfo { value = response.Result.PaySuccessData.orderProductPrice.value, color = response.Result.PaySuccessData.orderProductPrice.color },
                            remark = new DataInfo { value = response.Result.PaySuccessData.remark.value, color = response.Result.PaySuccessData.remark.color }
                        };

                        //
                        //return commonBLL.SendMatchWXTemplateMessage(wxTMConfigData.TemplateID, null, null, null, PaySuccessData, null, "15", OpenID, VipID, loggingSessionInfo);
                        commonBLL.SendMatchWXTemplateMessage(templateID, null, null, null, paySuccessData, null, null, null, null, "15", openID, vipID, loggingSessionInfo);
                    }
                }
            }
        }
    }
}
