
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.Connection
{
    public class SetConnectionJobBLL
    {
        /// <summary>
        /// 所有商户
        /// </summary>
        private Dictionary<string, string> _CustomerIDList
        { get; set; }
      
   

        /// <summary>
        /// 种植 商户数据库链接 缓存
        /// </summary>
        public void AutoSetConnectionCache()
        {
            _CustomerIDList = CustomerBLL.Instance.GetCustomerList();//这里的Instance使用了单例的模式
            foreach (var customer in _CustomerIDList)
            {
                string name = GetCustomerName(customer.Key);
                string code = GetCustomerCode(customer.Key);
                //string conn = customer.Value;//取链接字符串             
                new RedisConnectionBLL().SetConnection(customer.Key, customer.Value, name, code);//key和value分别代表商户的id和链接字符串
              
            }
        }

        #region GetCustomerConn
        /// <summary>
        /// 获取商户编码
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static string GetCustomerCode(string customerId)
        {
            string sql = string.Format(
                "select a.customer_code from t_customer a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        public static string GetCustomerName(string customerId)
        {
            string sql = string.Format(
                "select a.customer_name from t_customer a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion


    }
}
