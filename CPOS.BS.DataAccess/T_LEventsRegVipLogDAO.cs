/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/14 16:14:12
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
    /// 表T_LEventsRegVipLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_LEventsRegVipLogDAO : Base.BaseCPOSDAO, ICRUDable<T_LEventsRegVipLogEntity>, IQueryable<T_LEventsRegVipLogEntity>
    {
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <param name="strVipId"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public int IsExistsLog(string strCTWEventId,string strVipId,string strType,string strCustomerId)
        {

            string strSql = string.Empty;
            if (strType=="Reg")
            {
                strSql = string.Format(@"SELECT COUNT(1) LogCount  
                                        FROM T_LEventsRegVipLog With(nolock) 
                                        WHERE CustomerId='{0}' and ObjectId='{1}' and RegVipId='{2}'", strCustomerId, strCTWEventId, strVipId);
            }
            if (strType == "Focus")
            {
                strSql = string.Format(@"SELECT COUNT(1) LogCount  
                                        FROM T_LEventsRegVipLog With(nolock) 
                                        WHERE CustomerId='{0}' and ObjectId='{1}' and FocusVipId='{2}'", strCustomerId, strCTWEventId, strVipId);
            }

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql));
        }
    }
}
