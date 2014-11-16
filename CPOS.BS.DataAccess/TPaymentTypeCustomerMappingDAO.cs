/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/14 10:15:25
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表TPaymentTypeCustomerMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TPaymentTypeCustomerMappingDAO : Base.BaseCPOSDAO, ICRUDable<TPaymentTypeCustomerMappingEntity>, IQueryable<TPaymentTypeCustomerMappingEntity>
    {
        public void UpdatePaymentMap(string updateSql, int channelId,string paymentTypeId,string customerId)
        {
            var sql = "update TPaymentTypeCustomerMapping set " + updateSql + ",channelId = '" + channelId.ToString()
                + "' where paymentTypeId ='" + paymentTypeId
                + "' and customerId = " + "'" + customerId + "' ";
            this.SQLHelper.ExecuteNonQuery(sql);
        }

        public string GetChannelIdByPaymentTypeAndCustomer(string paymentTypeId, string customerId)
        {
            var sql = new StringBuilder();
            sql.Append("select ChannelId From cpos_ap..PaymentDefault a,cpos_ap..TCustomerDataDeployMapping b");
            sql.Append(" where a.IsALD = b.IsALD and b.CustomerId = @pCustomerId");
            sql.Append(" and a.CorrespondApplyId = @pPaymentTypeId");
            sql.Append(" and a.isdelete = 0 and b.isdelete = 0");

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });
            para.Add(new SqlParameter(){ParameterName = "@pPaymentTypeId",Value = paymentTypeId});

            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), para.ToArray());

            if (result == null || result.ToString() == "")
            {
                return "-1";
            }
            else
            {
                return result.ToString();
            }
            
        }

        public string GetChannelIdByCustomerId(string customerId)
        {
            var sql =
                string.Format(
                    "select ChannelId from TPaymentTypeCustomerMapping where CustomerID = '{0}' and IsDelete = 0 and PaymentTypeID = 'DFD3E26D5C784BBC86B075090617F44B'",
                    customerId);
            var result = this.SQLHelper.ExecuteScalar(sql.ToString());

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return "";
            }
            else
            {
                return result.ToString();
            }
        }
    }
}
