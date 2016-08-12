using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL.WX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using RedisOpenAPIClient.Models;

using RedisOpenAPIClient.Models.CC.OrderPushMessage;
using System.Data.SqlClient;
using JIT.CPOS.BS.BLL.Utility;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPushMessage
{
    /// <summary>
    /// 根据订单状态，做出不同的推送消息 
    /// </summary>
    public class SendOrderPushMessageBLL
    {
        public void OrderPushMessage(string orderId, string orderStatus, LoggingSessionInfo loggingSessionInfo, SqlTransaction tran)
        {
            //下面往redis里存入数据
            var response = RedisOpenAPI.Instance.CCOrderPushMessage().SetOrderPushMessage(new CC_OrderPushMessage()
            {
                CustomerID = loggingSessionInfo.ClientID,
                LogSession = loggingSessionInfo.JsonSerialize(),
                OrderID = orderId,
                OrderStauts = orderStatus
            });
            //如果往缓存redis里写入不成功，还是按照原来的老方法往数据库里写
            if (response.Code != ResponseCode.Success)
            {
                new InoutService(loggingSessionInfo).OrderPushMessage(orderId, orderStatus);
                new RedisXML().RedisReadDBCount("OrderPushMessage", "根据订单状态，做出不同的推送消息", 2);
            }
            else
            {
                new RedisXML().RedisReadDBCount("OrderPushMessage", "根据订单状态，做出不同的推送消息", 1);
            }
        }
    }
}
