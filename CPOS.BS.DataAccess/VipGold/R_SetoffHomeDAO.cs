/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 16:23:34
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
    /// 表R_SetoffHome的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_SetoffHomeDAO : BaseCPOSDAO, ICRUDable<R_SetoffHomeEntity>, IQueryable<R_SetoffHomeEntity>
    {
        /// <summary>
        /// 根据工具类型、会员员工类型获取最新一条数据作为饼状图 显示 记住表有最开始的R_SetoffHome变为R_SetoffHome2
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="SetoffToolTypeId"></param>
        /// <param name="SetoffTypeId"></param>
        /// <returns></returns>
        public DataSet GetReceiveSetoffHomeByCustomerId(string customerId, string SetoffToolTypeId, int SetoffTypeId)
        {
            string sql = "SELECT TOP 1 * FROM R_SetoffHome2 WHERE CustomerId=@CustomerId  AND SetoffToolType=@SetoffToolType AND SetoffType=@SetoffTypeId ORDER BY DateCode DESC ";
            SqlParameter[] parameter = new SqlParameter[]{
                 new SqlParameter("@CustomerId",customerId),
                 new SqlParameter("@SetoffToolType",SetoffToolTypeId),
                 new SqlParameter("@SetoffTypeId",SetoffTypeId)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }
    }
}
