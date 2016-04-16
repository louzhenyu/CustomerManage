/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/18 14:58:43
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
    /// 表T_CTW_PanicbuyingEventKV的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_CTW_PanicbuyingEventKVDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_PanicbuyingEventKVEntity>, IQueryable<T_CTW_PanicbuyingEventKVEntity>
    {
        public DataSet GetPanicbuyingEventKV(string strCTWEventId)
        {
            string strSql = string.Format("SELECT *,b.ImageURL FROM [dbo].[T_CTW_PanicbuyingEventKV] a LEFT JOIN dbo.ObjectImages b ON a.ImageId=b.ImageId  and WHERE CTWEventId='{0}'", strCTWEventId);
            return SQLHelper.ExecuteDataset(strSql);
        }
        public void DeleteByCTWEventID(string strCTWEventId)
        {
            string strSql = string.Format("DELETE T_CTW_PanicbuyingEventKV WHERE CTWEventId='{0}'", strCTWEventId);
            this.SQLHelper.ExecuteNonQuery(strSql);
        }
    }
}
