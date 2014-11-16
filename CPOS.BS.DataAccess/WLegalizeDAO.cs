/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/24 15:51:18
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
    /// 表WLegalize的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WLegalizeDAO : Base.BaseCPOSDAO, ICRUDable<WLegalizeEntity>, IQueryable<WLegalizeEntity>
    {
        #region 获取当前最大值
        /// <summary>
        /// 获取当前客户的最大值
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public int GetMaxNo(string CustomerId)
        {
            string sql = "SELECT ISNULL(MAX(No),0)+1 FROM WLegalize a WHERE a.customerId = '" + CustomerId + "' ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion
    }
}
