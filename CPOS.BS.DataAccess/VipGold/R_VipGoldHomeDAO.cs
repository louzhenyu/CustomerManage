/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/18 14:40:25
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
    /// 表R_VipGoldHome的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_VipGoldHomeDAO : Base.BaseCPOSDAO, ICRUDable<R_VipGoldHomeEntity>, IQueryable<R_VipGoldHomeEntity>
    {
        /// <summary>
        /// 获取 商户的最新一条信息作为 会员主页的报表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetReceiveRecodsByCustomerId(string customerId)
        {
            string sql = string.Format("SELECT TOP 1 * FROM R_VipGoldHome WHERE CustomerId='{0}' ORDER BY DateCode DESC", customerId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
    }
}
