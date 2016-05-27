using JIT.CPOS.BS.Entity;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.Connection
{
    /// <summary>
    /// 商户数据库链接
    /// </summary>
    public class RedisConnectionOP
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        public void SetConnection(string _customerID, string _ConnectionStr, string _Customer_Name, string _Customer_Code)
        {

            try
            {
                RedisOpenAPI.Instance.CCConnection().SetConnection(new CC_Connection
                {
                    CustomerID = _customerID,
                    ConnectionStr = _ConnectionStr,
                    Customer_Name = _Customer_Name,
                    Customer_Code = _Customer_Code
                });

            }
            catch (Exception ex)
            {
                throw new Exception("设置缓存失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 获取缓存(商户数据库链接)
        /// </summary>
        public CC_Connection GetConnection(string customerID)
        {
            try
            {
                var response = RedisOpenAPI.Instance.CCConnection().GetConnection(new CC_Connection
                {
                    CustomerID = customerID,
                });
                if (response.Code == ResponseCode.Success)
                {
                    return response.Result;
                }
                else
                {
                    return new CC_Connection();
                }
            }
            catch
            {
                return new CC_Connection();
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        public void DelConnection(string customerID)
        {
            try
            {
                RedisOpenAPI.Instance.CCConnection().DelConnection(new CC_Connection
                {
                    CustomerID = customerID
                });
            }
            catch (Exception ex)
            {
                throw new Exception("删除缓存失败!" + ex.Message);
            }
        }

    }
}
