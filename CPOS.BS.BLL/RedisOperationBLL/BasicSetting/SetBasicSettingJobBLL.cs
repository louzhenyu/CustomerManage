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

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.BasicSetting
{
    public class SetBasicSettingJobBLL
    {
        /// <summary>
        /// 定时全量种植
        /// </summary>
        public void TimeSetBasicSetting()
        {
            CC_Connection connection = new CC_Connection();

            LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
            LoggingManager CurrentLoggingManager = new LoggingManager();

            var customerIDs = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in customerIDs)
            {
                connection = new RedisConnectionBLL().GetConnection(customer.Key);

                BasicSettingBLL redisBasicSettingBll = new BasicSettingBLL();
                redisBasicSettingBll.DelBasicSetting(customer.Key);
                redisBasicSettingBll.SetBasicSetting(customer.Key);
            }
        }
    }
}
