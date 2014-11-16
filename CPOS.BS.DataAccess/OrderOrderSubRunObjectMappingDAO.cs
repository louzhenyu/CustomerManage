/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/18 10:28:46
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表OrderOrderSubRunObjectMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class OrderOrderSubRunObjectMappingDAO : Base.BaseCPOSDAO, ICRUDable<OrderOrderSubRunObjectMappingEntity>, IQueryable<OrderOrderSubRunObjectMappingEntity>
    {
        /// <summary>
        /// 处理订单与分润关系
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderId"></param>
        /// <param name="subRunObjectId"></param>
        /// <param name="subRunObjectValue"></param>
        /// <returns></returns>
        public dynamic SetOrderSub(string customerId, string orderId)
        {
            var parameters = new List<SqlParameter>();
            var p = new SqlParameter("@CustomerId", SqlDbType.NVarChar);
            p.Value = customerId;
            parameters.Add(p);

            p = new SqlParameter("@OrderId", SqlDbType.NVarChar);
            p.Value = orderId;
            parameters.Add(p);

            //p= new SqlParameter("@SubRunObjectId",SqlDbType.Int);
            //p.Value = subRunObjectId;
            //parameters.Add(p);

            //p = new SqlParameter("@SubRunObjectValue", SqlDbType.Decimal);
            //p.Value = subRunObjectValue;
            //parameters.Add(p);

            var isSuccess = new SqlParameter("@IsSuccess",SqlDbType.Int,4);
            isSuccess.Direction = ParameterDirection.Output;
            parameters.Add(isSuccess);

            var desc = new SqlParameter("@FailureDesc",SqlDbType.NVarChar,800);
            desc.Direction = ParameterDirection.Output;
            parameters.Add(desc);

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ProcSetOrderOrderSubRunObject", parameters.ToArray());
            dynamic result = new
            {
                IsSuccess = isSuccess.Value.ToString(),
                Desc = desc.Value.ToString()
            };
            return result;
        }
    }
}
