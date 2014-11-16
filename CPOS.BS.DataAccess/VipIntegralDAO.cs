/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 11:13:49
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
    /// 表VipIntegral的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipIntegralDAO : Base.BaseCPOSDAO, ICRUDable<VipIntegralEntity>, IQueryable<VipIntegralEntity>
    {
        #region 积分处理    by Willie Yan
        public string ProcessPoint(int sourceId, string customerId, string vipId, string objectId, SqlTransaction tran, string fromVipId, decimal point, string remark, string updateBy)
        {
            string result = "0";
            string sql = "PointProcessor";

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter() { ParameterName = "@SourceId", Value = sourceId });
            parameter.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = customerId });
            parameter.Add(new SqlParameter() { ParameterName = "@VipId", Value = vipId });
            parameter.Add(new SqlParameter() { ParameterName = "@ObjectId", Value = objectId });
            parameter.Add(new SqlParameter() { ParameterName = "@FromVipId", Value = fromVipId });
            parameter.Add(new SqlParameter() { ParameterName = "@Point", Value = point });
            parameter.Add(new SqlParameter() { ParameterName = "@Remark", Value = remark });
            parameter.Add(new SqlParameter() { ParameterName = "@UpdateBy", Value = updateBy });
            parameter.Add(new SqlParameter() { ParameterName = "@IsReturn", Value = 1 });
            if (tran == null)
                result = this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, sql, parameter.ToArray()).ToString();
            else
                result = this.SQLHelper.ExecuteScalar(tran, CommandType.StoredProcedure, sql, parameter.ToArray()).ToString();
            return result;
        }
        #endregion
    }
}
