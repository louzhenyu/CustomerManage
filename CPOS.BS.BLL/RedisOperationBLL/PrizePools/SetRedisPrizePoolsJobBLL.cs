using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.PrizePools
{
    public class SetRedisPrizePoolsJobBLL
    {
        public void SetRedisPrizePools()
        {
            var CustomerIDList = CustomerBLL.Instance.GetCustomerList();//这里的Instance使用了单例的模式
            

            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            LoggingManager CurrentLoggingManager = new LoggingManager();
            foreach (var customer in CustomerIDList)
            {
                loggingSessionInfo.ClientID = customer.Key;
                CurrentLoggingManager.Connection_String = customer.Value;
                loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                //入奖品池队列
                LEventsBLL bllEvent = new LEventsBLL(loggingSessionInfo);
                LPrizePoolsBLL bllPools = new LPrizePoolsBLL(loggingSessionInfo);

                var eventList=bllEvent.QueryByEntity(new LEventsEntity() { CustomerId = customer.Key, IsDelete = 0,EventStatus=20 },null).ToList();
                foreach (var levent in eventList)
                {


                    DataSet dsPools = bllPools.GetPrizePoolsByEvent(loggingSessionInfo.ClientID, levent.EventID);
                    if (dsPools != null && dsPools.Tables.Count > 0 && dsPools.Tables[0].Rows.Count > 0)
                    {
                        var poolTemp = DataTableToObject.ConvertToList<CC_PrizePool>(dsPools.Tables[0]);
                        var poolsList = Utils.GetRandomList<CC_PrizePool>(poolTemp);//随机列表
                        if (poolsList != null && poolsList.Count > 0)
                        {

                            var redisPrizePoolsBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.PrizePools.RedisPrizePoolsBLL();
                            CC_PrizePool prizePool = new CC_PrizePool();
                            prizePool.CustomerId = customer.Key;
                            prizePool.EventId = levent.EventID;

                            redisPrizePoolsBLL.DeletePrizePoolsList(prizePool);
                            redisPrizePoolsBLL.SetPrizePoolsToRedis(poolsList);
                        }
                    }
                }
            }
        }
    }
}
