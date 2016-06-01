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
using RedisOpenAPIClient.Models.CC.OrderReward;
using System.Data;
using JIT.CPOS.BS.DataAccess.Base;
using System.Data.SqlClient;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderReward
{
    public class ProcessOrderRewardMsgJobBLL
    {

        /// <summary>
        /// APP/后台订单发货-发送微信模板消息
        /// </summary>
        public void ProcessOrderRewardMsg()
        {
            //
            var numCount = 100;
            var commonBLL = new CommonBLL();

            //
            var customerIDs = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in customerIDs)
            {
                //
                var count = RedisOpenAPI.Instance.CCOrderReward().GetOrderRewardLength(new CC_OrderReward
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

                    var response = RedisOpenAPI.Instance.CCOrderReward().GetOrderReward(new CC_OrderReward
                    {
                        CustomerID = customer.Key
                    });
                    if (response.Code == ResponseCode.Success)
                    {
                        var loggingSessionInfo = response.Result.LogSession.JsonDeserialize<LoggingSessionInfo>();
                        var orderInfo = response.Result.OrderInfo.JsonDeserialize<T_InoutEntity>();

                        IDbTransaction tran = new TransactionHelper(loggingSessionInfo).CreateTransaction();
                        using (tran.Connection)
                        {
                            try
                            {
                                new VipIntegralBLL(loggingSessionInfo).OrderReward(orderInfo, (SqlTransaction)tran);//还是把原来的事务传进去                      
                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                            }
                        }
                    }
                }
            }

        }

    }
}
