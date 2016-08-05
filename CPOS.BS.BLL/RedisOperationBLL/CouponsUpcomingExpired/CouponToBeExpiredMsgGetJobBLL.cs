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
using RedisOpenAPIClient.Models.CC.CouponToBeExpired;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.CouponsUpcomingExpired
{
    public class CouponToBeExpiredMsgGetJobBLL
    {
        //读取缓存的job

        /// <summary>
        /// 订单未支付-发送微信模板消息
        /// </summary>
        public void ProcessCouponToBeExpiredMsgGetMsg()
        {
            //
            var numCount = 100;
            var commonBLL = new CommonBLL();

            //
            var customerIDs = CustomerBLL.Instance.GetCustomerList();//获取所有商户
            foreach (var customer in customerIDs)
            {
                //
                var count = RedisOpenAPI.Instance.CCCouponToBeExpired().GetCouponToBeExpiredLength(new CC_CouponToBeExpired
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
                    var response = RedisOpenAPI.Instance.CCCouponToBeExpired().GetCouponToBeExpired(new CC_CouponToBeExpired
                    {
                        CustomerID = customer.Key
                    });
                    if (response.Code == ResponseCode.Success)
                    {
                        var templateID = response.Result.ConfigData.TemplateID;
                        var openID = response.Result.ConfigData.OpenID;
                        var vipID = response.Result.ConfigData.VipID;
                        var loggingSessionInfo = CustomerBLL.Instance.GetBSLoggingSession(customer.Key, "1"); //response.Result.ConfigData.LogSession.JsonDeserialize<LoggingSessionInfo>();
                        var Data = new JIT.CPOS.BS.Entity.WX.CouponsUpcomingExpired
                        {
                            first = new DataInfo { value = response.Result.CouponToBeExpiredData.first.value, color = response.Result.CouponToBeExpiredData.first.color },
                            keyword1 = new DataInfo { value = response.Result.CouponToBeExpiredData.keyword1.value, color = response.Result.CouponToBeExpiredData.keyword1.color },
                            keyword2 = new DataInfo { value = response.Result.CouponToBeExpiredData.keyword2.value, color = response.Result.CouponToBeExpiredData.keyword2.color },
                            keyword3 = new DataInfo { value = response.Result.CouponToBeExpiredData.keyword3.value, color = response.Result.CouponToBeExpiredData.keyword3.color },
                            keyword4 = new DataInfo { value = response.Result.CouponToBeExpiredData.keyword4.value, color = response.Result.CouponToBeExpiredData.keyword4.color },
                            remark = new DataInfo { value = response.Result.CouponToBeExpiredData.remark.value, color = response.Result.CouponToBeExpiredData.remark.color }
                        };




                        commonBLL.SendMatchWXTemplateMessage(templateID, null, null, null, null, null, null, Data, null, "1", openID, null, loggingSessionInfo);

                    }
                }
            }
        }

    }


}
