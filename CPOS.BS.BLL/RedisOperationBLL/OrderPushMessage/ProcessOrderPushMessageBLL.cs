using JIT.CPOS.BS.BLL.WX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedisOpenAPIClient.Models.CC.OrderPushMessage;
using System.Data;
using JIT.CPOS.BS.DataAccess.Base;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPushMessage
{
    /// <summary>
    /// 执行发送消息的方法
    /// </summary>
    public class ProcessOrderPushMessageBLL
    {
        public void ProcessOrderPushMessage()
        {
            //
            var numCount = 100;
            var commonBLL = new CommonBLL();

            //获取所有商户信息
            var customerIDs = CustomerBLL.Instance.GetCustomerList();


            foreach (var customer in customerIDs)
            {
                //通过Key来调用Redis里面的获取长度的方法
                var count = RedisOpenAPI.Instance.CCOrderPushMessage().GetOrderPushMessageLength(new CC_OrderPushMessage()
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
                    var response = RedisOpenAPI.Instance.CCOrderPushMessage().GetOrderPushMessage(new CC_OrderPushMessage()
                    {
                        CustomerID = customer.Key,
                    });
                    if (response.Code == ResponseCode.Success)
                    {
                        var loggingSessionInfo = CustomerBLL.Instance.GetBSLoggingSession(customer.Key, "1");// response.Result.LogSession.JsonDeserialize<LoggingSessionInfo>();

                        IDbTransaction tran = new TransactionHelper(loggingSessionInfo).CreateTransaction();
                        using (tran.Connection)
                        {
                            try
                            {
                                //根据订单状态推送不同的消息信息
                                new InoutService(loggingSessionInfo).OrderPushMessage(response.Result.OrderID, response.Result.OrderStauts); // 根据不同订单状态发送不同的模板消息 还是把原来的事务传进去                      
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