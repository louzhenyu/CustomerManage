using JIT.CPOS.BS.BLL.RedisOperationBLL.Connection;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.BasicSetting
{
    public class BasicSettingBLL
    {
        /// <summary>
        /// 种植/更新BasicSetting缓存
        /// </summary>
        /// <param name="strCustomerId"></param>
        /// <param name="basicSettingList"></param>
        public void SetBasicSetting(string strCustomerId, List<CustomerBasicSettingEntity> basicSettingList)
        {
            try
            {

                LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
                LoggingManager CurrentLoggingManager = new LoggingManager();
                var connection = GetCustomerConn(strCustomerId);

                _loggingSessionInfo.ClientID = strCustomerId;
                CurrentLoggingManager.Connection_String = connection;
                _loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;

                CustomerBasicSettingBLL bllBasicSetting = new CustomerBasicSettingBLL(_loggingSessionInfo);
                List<CustomerBasicSettingEntity> listBasicSetting = bllBasicSetting.QueryByEntity(new CustomerBasicSettingEntity(){CustomerID=strCustomerId,IsDelete=0},null).ToList();

                RedisOpenAPI.Instance.CCBasicSetting().SetBasicSetting(new CC_BasicSetting()
                {
                    CustomerId = strCustomerId,
                    SettingList = listBasicSetting.Select(b => new Setting
                    {
                        SettingCode = b.SettingCode,
                        SettingDesc = b.SettingDesc,
                        SettingValue = b.SettingValue
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                throw new Exception("设置缓存失败!" + ex.Message);
            }

        }
        /// <summary>
        /// 获取BasicSetting缓存
        /// </summary>
        /// <param name="strCustomerId"></param>
        /// <returns></returns>
        public List<CustomerBasicSettingEntity> GetBasicSetting(string strCustomerId)
        {
            try
            {
                var response = RedisOpenAPI.Instance.CCBasicSetting().GetBasicSetting(new CC_BasicSetting
                {
                    CustomerId = strCustomerId
                });
                if (response.Code == ResponseCode.Success)
                {
                    return response.Result.SettingList.Select(it => new CustomerBasicSettingEntity
                    {
                        SettingCode = it.SettingCode,
                        SettingValue = it.SettingValue,
                        SettingDesc = it.SettingDesc
                    }).ToList();
                }
                else
                {
                    return new List<CustomerBasicSettingEntity>();
                }
            }
            catch
            {
                return new List<CustomerBasicSettingEntity>();
            }
        }
        /// <summary>
        /// 删除BasicSetting缓存
        /// </summary>
        /// <param name="strCustomerId"></param>
        public void DelBasicSetting(string strCustomerId)
        {
            try
            {
                RedisOpenAPI.Instance.CCBasicSetting().DelBasicSetting(new CC_BasicSetting
                {
                    CustomerId = strCustomerId
                });
            }
            catch (Exception ex)
            {
                throw new Exception("删除缓存失败!" + ex.Message);
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
