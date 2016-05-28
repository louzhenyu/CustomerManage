using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
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
using System.Configuration;
namespace JIT.CPOS.BS.BLL.RedisOperationBLL.Contact
{
    public class RedisContactBLL
    {
        public void SetRedisContact(CC_Contact contact)
        {
            RedisOpenAPI.Instance.CCContact().SetContact(new CC_Contact()
            {
                CustomerId = contact.CustomerId,
                ContactType = contact.ContactType,
                VipId=contact.VipId
 
            });
        }
        public int GetContactLength(CC_Contact contact)
        {
           var count= RedisOpenAPI.Instance.CCContact().GetContactLength(new CC_Contact()
            {
                CustomerId = contact.CustomerId,
                ContactType = contact.ContactType,
                VipId = contact.VipId

            });
           if (count.Code != ResponseCode.Success)
           {
               return 0;
           }
           else
           {
               return Convert.ToInt32(count.Result);
           }

        }
        public void GetContact()
        {
            try
            {


                var numCount = 50;
                var customerIDs = CustomerBLL.Instance.GetCustomerList();
                CC_Connection connection = new CC_Connection();
                LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
                LoggingManager CurrentLoggingManager = new LoggingManager();
                foreach (var customer in customerIDs)
                {
                    
                    var count = RedisOpenAPI.Instance.CCContact().GetContactLength(new CC_Contact
                    {
                        CustomerId = customer.Key
                    });
                    if (count.Code != ResponseCode.Success)
                    {
                        continue;
                    }
                    if (count.Result <= 0)
                    {
                        continue;
                    }
                    
                    
                    _loggingSessionInfo.ClientID = customer.Key;
                    CurrentLoggingManager.Connection_String = customer.Value;
                    _loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                    var lPrizeBll = new LPrizesBLL(_loggingSessionInfo);

                    if (count.Result < numCount)
                    {
                        numCount = Convert.ToInt32(count.Result);
                    }
                    BaseService.WriteLog("触点:" + customer.Key + "_" + customer.Value + ",数量" + numCount.ToString());
                    for (var i = 0; i < numCount; i++)
                    {
                        var response = RedisOpenAPI.Instance.CCContact().GetContact(new CC_Contact
                         {
                             CustomerId = customer.Key
                         });
                        if (response.Code == ResponseCode.Success)
                        {
                            lPrizeBll.CheckIsWinnerForShareForRedis(response.Result.VipId, response.Result.EventId, response.Result.ContactType, _loggingSessionInfo);
                        }

                    }
                    BaseService.WriteLog("触点结束");

                }
            }
            catch (Exception ex)
            {

                BaseService.WriteLog("触点异常" + ex.Message.ToString());
            }
        }

        public static string GetCustomerConn(string customerId)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn " +
                " from t_customer_connect a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
    }
}
