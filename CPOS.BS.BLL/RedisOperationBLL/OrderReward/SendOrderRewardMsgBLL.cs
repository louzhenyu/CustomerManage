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

using RedisOpenAPIClient.Models.CC.OrderReward;
using System.Data.SqlClient;
using JIT.CPOS.BS.BLL.Utility;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderReward
{
    /// <summary>
    /// 确认收货时处理积分、返现、佣金
    /// </summary>
    public class SendOrderRewardMsgBLL
    {

        public void OrderReward(T_InoutEntity orderInfo, LoggingSessionInfo loggingSessionInfo, SqlTransaction tran)
        {
            //下面往redis里存入数据
            var response = RedisOpenAPI.Instance.CCOrderReward().SeOrderReward(new CC_OrderReward
            {
                CustomerID = loggingSessionInfo.ClientID,
                LogSession = loggingSessionInfo.JsonSerialize(),
                OrderInfo = orderInfo.JsonSerialize(),
                OrderID = orderInfo.order_id
            });
            //如果往缓存redis里写入不成功，还是按照原来的老方法往数据库里写
            if (response.Code != ResponseCode.Success)
            {
                new VipIntegralBLL(loggingSessionInfo).OrderReward(orderInfo, tran);//还是把原来的事务传进去
                new RedisXML().RedisReadDBCount("OrderReward", "确认收货/完成订单 - 处理奖励", 2);
            }
            else {
                new RedisXML().RedisReadDBCount("OrderReward", "确认收货/完成订单 - 处理奖励", 1);
            
            }
          
        }
    }
}
