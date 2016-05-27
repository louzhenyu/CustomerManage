
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System.Data.SqlClient;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC;
using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL.RedisOperationBLL.Connection;
using Newtonsoft.Json;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.PrizePools
{
    public class RedisPrizePoolsBLL
    {
        public void SetPrizePoolsToRedis(List<CC_PrizePool> prizePools)
        {
            RedisOpenAPI.Instance.CCPrizePools().SetPrizePools(prizePools);
        }
        public ResponseModel<CC_PrizePool> GetPrizePoolsFromRedis(CC_PrizePool prizePools)
        {
            return RedisOpenAPI.Instance.CCPrizePools().GetPrizePools(prizePools);

        }
        public long GetPrizePoolsListLength(CC_PrizePool coupon)
        {
            var count = RedisOpenAPI.Instance.CCPrizePools().GetPrizePoolsListLength(coupon);
            return count.Result;
        }
        public void DeletePrizePoolsList(CC_PrizePool coupon)
        {
            var count = RedisOpenAPI.Instance.CCPrizePools().DeletePrizePoolsList(coupon);
            
        }
    }
}
