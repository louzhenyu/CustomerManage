/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/10 18:14:44
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
    /// 表t_award_pool的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class t_award_poolDAO : Base.BaseCPOSDAO, ICRUDable<t_award_poolEntity>, IQueryable<t_award_poolEntity>
    {
        /// <summary>
        /// 根据EventId 获取PrizeId
        /// </summary>
        /// <param name="strEventId"></param>
        /// <returns></returns>
       public DataSet GetPrizeByEventId(string strEventId)
        {
            string strSql = string.Format("SELECT TOP 1* FROM dbo.t_award_pool with(nolock) WHERE EventId='{0}'	AND GETDATE()>ReleaseTime AND Balance=0 ORDER BY NEWID()", strEventId);
            return SQLHelper.ExecuteDataset(strSql);
        }
    }
}
