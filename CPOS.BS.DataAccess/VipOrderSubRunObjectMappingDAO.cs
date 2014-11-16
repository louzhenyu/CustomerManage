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
    /// 表VipOrderSubRunObjectMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipOrderSubRunObjectMappingDAO : Base.BaseCPOSDAO, ICRUDable<VipOrderSubRunObjectMappingEntity>, IQueryable<VipOrderSubRunObjectMappingEntity>
    {
        /// <summary>
        /// 处理会员与分润方关系存储过程
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="vipId"></param>
        /// <param name="subRunObjectId"></param>
        /// <param name="subRunObjectValue"></param>
        /// <returns></returns>
        public dynamic SetVipOrderSubRun(string customerId, string vipId,
            int subRunObjectId, string subRunObjectValue)
        {
            SqlParameter[] ps =  {
                new SqlParameter("@CustomerId",SqlDbType.NVarChar),
                new SqlParameter("@VipId",SqlDbType.NVarChar),
                new SqlParameter("@SubRunObjectId",SqlDbType.Int),
                new SqlParameter("@SubRunObjectValue",SqlDbType.NVarChar),
                new SqlParameter("@IsSuccess",SqlDbType.Int,4),
                new SqlParameter("@FailureDesc",SqlDbType.NVarChar,800)
            };
            ps[0].Value = customerId;
            ps[1].Value = vipId;
            ps[2].Value = subRunObjectId;
            ps[3].Value = subRunObjectValue;
            ps[4].Direction = ParameterDirection.Output;
            ps[5].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ProcSetVipOrderSubRunObject", ps);
            dynamic result = new { IsSuccess = ps[4].Value.ToString(), Desc = ps[5].Value.ToString() };
            return result;
        }
    }
}
