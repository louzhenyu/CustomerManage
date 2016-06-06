/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 14:15:31
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
    /// 表IincentiveRule的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class IincentiveRuleDAO : Base.BaseCPOSDAO, ICRUDable<IincentiveRuleEntity>, IQueryable<IincentiveRuleEntity>
    {
        /// <summary>
        /// 获取集客激励信息
        /// </summary>
        /// <param name="SetoffType"></param>
        /// <returns></returns>
        public DataSet GetIncentiveRule(string SetoffType)
        {
            var parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@SetoffType", SetoffType);
            parm[1] = new SqlParameter("@CustomerId", CurrentUserInfo.CurrentUser.customer_id);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            string sql = string.Format(@"
             select SetoffRegAwardType,SetoffRegPrize,SetoffOrderPer from IincentiveRule a
            inner join SetoffEvent b on a.SetoffEventID=b.SetoffEventID
            where a.Status=10 AND b.Status=10  and a.CustomerId=@CustomerId ");
            if (SetoffType != null && SetoffType != "")
            {
                sql += string.Format(" and a.SetoffType=@SetoffType", SetoffType);
            }
            return this.SQLHelper.ExecuteDataset(CommandType.Text,sql,parm);
        }
        
    }
}
