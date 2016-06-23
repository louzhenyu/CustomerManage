/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 9:08:39
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
    /// 表T_SuperRetailTraderProfitConfig的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SuperRetailTraderProfitConfigDAO : Base.BaseCPOSDAO, ICRUDable<T_SuperRetailTraderProfitConfigEntity>, IQueryable<T_SuperRetailTraderProfitConfigEntity>
    {

        /// <summary>
        /// 当 可分润比例改变时 将 三级分销提成体系 全部置为失效状态
        /// </summary>
        /// <param name="CustomerId"></param>
        public void UpdateByCustomerId(string CustomerId)
        {
            string sql = "UPDATE T_SuperRetailTraderProfitConfig SET STATUS=90 WHERE CustomerId=@CustomerId";
            SqlParameter[] parameter = new SqlParameter[]{
                 new SqlParameter("@CustomerId",CustomerId)
            };
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }
    }
}
