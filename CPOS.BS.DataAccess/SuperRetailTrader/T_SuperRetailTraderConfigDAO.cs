/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 10:56:31
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
    /// 表T_SuperRetailTraderConfig的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SuperRetailTraderConfigDAO : BaseCPOSDAO, ICRUDable<T_SuperRetailTraderConfigEntity>, IQueryable<T_SuperRetailTraderConfigEntity>
    {
        /// <summary>
        /// 将状态修改为失效状态
        /// </summary>
        /// <param name="CustomerId">商户编号</param>
        /// <returns></returns>
        public int UpdateByCondition(string CustomerId)
        {
            string sql = "Update T_SuperRetailTraderConfig Set IsDelete=1 WHERE CustomerId=@CustomerId";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",CustomerId)
            };
            return this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }
    }
}
