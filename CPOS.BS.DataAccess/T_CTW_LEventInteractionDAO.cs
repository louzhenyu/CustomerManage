/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 10:45:49
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
    /// 表T_CTW_LEventInteraction的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_CTW_LEventInteractionDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventInteractionEntity>, IQueryable<T_CTW_LEventInteractionEntity>
    {
        public DataSet GetPanicbuyingEventDate(string strCTWEventId)
        {
            string strSql = string.Format("SELECT MIN(BeginTime)BeginTime,MAX(endtime)EndTime FROM T_CTW_LEventInteraction a INNER JOIN dbo.PanicbuyingEvent b ON a.LeventId=b.EventId WHERE CTWEventId='{0}'", strCTWEventId);
            return SQLHelper.ExecuteDataset(strSql);
        }
        public DataSet GetPanicbuyingEventId(string strCTWEventId)
        {
            string strSql = string.Format("SELECT DrawMethodCode,LeventId FROM T_CTW_LEventInteraction a  WHERE CTWEventId='{0}' AND a.isdelete=0", strCTWEventId);
            return SQLHelper.ExecuteDataset(strSql);
        }
        public void DeleteByCTWEventID(string strCTWEventId)
        {
            string strSql = string.Format("DELETE [dbo].[T_CTW_LEventInteraction] WHERE CTWEventId='{0}'", strCTWEventId);
            this.SQLHelper.ExecuteNonQuery(strSql);
        }
        public DataSet GetCTWLEventInteraction(string strObjectId)
        {
            string strSql = string.Format(@"SELECT a.*,b.OnLineRedirectUrl FROM T_CTW_LEventInteraction  a
                                            INNER JOIN dbo.T_CTW_LEvent b ON a.CTWEventId=b.CTWEventId
                                            WHERE a.IsDelete=0 AND CAST(a.CTWEventId AS NVARCHAR(50))='{0}' Or a.LeventId='{0}'", strObjectId);
            return SQLHelper.ExecuteDataset(strSql);
        }
    }
}
