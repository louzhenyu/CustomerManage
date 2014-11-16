/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/29 11:10:51
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
    /// 表EclubVipQueryLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubVipQueryLogDAO : Base.BaseCPOSDAO, ICRUDable<EclubVipQueryLogEntity>, IQueryable<EclubVipQueryLogEntity>
    {
        /// <summary>
        /// 获取用户登录次数及允许登录的次数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code">配置值</param>
        /// <returns></returns>
        public DataSet GetUserSearchCountInfo(string userId,string code)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select Name,Code,Value,Sequence from EclubSetUp ");
            sbSQL.AppendFormat("where Code='{0}' and CustomerId='{1}' and IsDelete=0 ;", code, CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            sbSQL.Append("select COUNT(1) as SearchCount from EclubVipQueryLog ");
            sbSQL.AppendFormat("where VipID='{0}' and DATEDIFF(DAY,createtime,getdate()) = 0 and CustomerId='{1}' and IsDelete=0 ;", userId, CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            //Access DB
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
    }
}
