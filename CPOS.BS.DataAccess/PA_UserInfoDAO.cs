/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/17 11:49:55
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
using CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表PA_UserInfo的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PA_UserInfoDAO : BaseCPOSDAO, ICRUDable<PA_UserInfoEntity>, IQueryable<PA_UserInfoEntity>
    {

        /// <summary>
        /// 获取当前最大ID
        /// </summary>
        /// <returns></returns>
        public string GetMaxVipId()
        {
            string result = "920000000";
            string sql = @"        
                        SELECT TOP 1 u.Field1 FROM dbo.PA_UserInfo u WITH(NOLOCK)
                        ORDER BY u.Field1 DESC";
            object obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj != null)
            {
                result = obj.ToString();
            }
            // 编码规则：客户标识符 9 + 八位流水码
            return result;
        }
    }
}
