/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:19
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// </summary>
    public partial class PanicbuyingEventBLL
    {  
        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public string AddPanicbuyingEvent(PanicbuyingEventEntity pEntity, IDbTransaction pTran)
        {
            return this._currentDAO.AddPanicbuyingEvent(pEntity, pTran);
        }
         /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEvent(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetPanicbuyingEvent(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
        public PagedQueryResult<PanicbuyingEventEntity> GetPanicbuyingEventList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetPanicbuyingEventList(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
        public DataSet GetPanicbuyingEvent(string pEvenid)
        {
            return this._currentDAO.GetPanicbuyingEvent(pEvenid);

        }
        /// <summary>
        /// 获取活动详情
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public PanicbuyingEventEntity GetPanicbuyingEventDetails(object pID)
        {
            return this._currentDAO.GetPanicbuyingEventDetails(pID);
        }
        
        public DataSet GetKJEventList(int pageIndex, int pageSize, string strEventName, int intEventStatus, string strBeginTime, string strEndTime)
        {
            return this._currentDAO.GetKJEventList(pageIndex, pageSize, strEventName, intEventStatus, strBeginTime, strEndTime);
        }
        /// <summary>
        /// 根据主题id结束促销活动
        /// </summary>
        /// <param name="strCTWEventId"></param>
        public void EndOfEvent(string strCTWEventId)
        {
            this._currentDAO.EndOfEvent(strCTWEventId);
        }
        /// <summary>
        /// 根据主题id推迟促销活动
        /// </summary>
        /// <param name="strCTWEventId"></param>
        public void DelayEvent(string strCTWEventId,string strEndDate)
        {
            this._currentDAO.DelayEvent(strCTWEventId, strEndDate);
        }
    }
}