/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/17 15:39:53
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
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 泸州老窖
    /// </summary>
    public partial class LEventsBLL
    {
        #region 获取首页活动列表
        /// <summary>
        /// 获取首页活动列表
        /// </summary>
        /// <returns></returns>
        public LEventsEntity[] GetHomePageActivityList(int pPageIndex,int pPageSize)
        {
            return this._currentDAO.GetHomePageActivityList(pPageIndex,pPageSize);
        }
        #endregion


        public DataSet GetEventList(string customerId, string eventTypeId, string eventName,int drawMethodId, bool? beginFlag,
            bool? endFlag,int eventStatus, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.GetEventList(customerId, eventTypeId, eventName, drawMethodId,beginFlag, endFlag, 
                eventStatus,pPageIndex, pPageSize);
        }

        public int GetEventListCount(string customerId, string eventTypeId, string eventName, int drawMethodId,
            bool? beginFlag,
            bool? endFlag, int eventStatus)
        {
            return this._currentDAO.GetEventListCount(customerId, eventTypeId, eventName, drawMethodId, beginFlag, endFlag,
                eventStatus);
        }

        public string GetRecommendId(string customerId)
        {
            return this._currentDAO.GetRecommendId(customerId);
        }

        public int GetIsShareByEventId(string eventId)
        {
            return this._currentDAO.GetIsShareByEventId(eventId);
        }

        public string GetEnableFlagByEventId(string eventId)
        {
            return this._currentDAO.GetEnableFlagByEventId(eventId);
        }
    }
}
