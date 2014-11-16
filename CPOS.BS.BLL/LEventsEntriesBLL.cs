/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/11 17:00:10
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
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class LEventsEntriesBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<LEventsEntriesEntity> GetWebList(LEventsEntriesEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<LEventsEntriesEntity> list = new List<LEventsEntriesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<LEventsEntriesEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebListCount(LEventsEntriesEntity entity)
        {
            return _currentDAO.GetWebListCount(entity);
        }
        #endregion

        #region 获取当天的参赛作品

        /// <summary>
        /// 获取当天的参赛作品
        /// </summary>
        /// <param name="strDate">查询日期</param>
        /// <returns></returns>
        public DataSet GetEventsEntriesList(string strDate)
        {
            return this._currentDAO.GetEventsEntriesList(strDate);
        }

        #endregion

        #region 获取参赛作品的评论与赞

        /// <summary>
        /// 获取参赛作品的评论与赞
        /// </summary>
        /// <param name="entriesId">作品标识</param>
        /// <returns></returns>
        public DataSet GetEventsEntriesCommentList(string entriesId)
        {
            return this._currentDAO.GetEventsEntriesCommentList(entriesId);
        }

        #endregion

        #region 获取参赛作品的评论集合

        /// <summary>
        /// 获取参赛作品的评论集合
        /// </summary>
        /// <param name="entriesId">作品标识</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetEventsEntriesCommentList(string entriesId, int page, int pageSize)
        {
            return this._currentDAO.GetEventsEntriesCommentList(entriesId, page, pageSize);
        }

        #endregion

        #region 获取获奖名单

        /// <summary>
        /// 获取围观达人数量
        /// </summary>
        /// <param name="strDate">获奖日期</param>
        /// <returns></returns>
        public int GetCrowdDarenCount(string strDate)
        {
            return this._currentDAO.GetCrowdDarenCount(strDate);
        }

        /// <summary>
        /// 获取围观达人集合
        /// </summary>
        /// <param name="strDate">获奖日期</param>
        /// <returns></returns>
        public DataSet GetCrowdDarenList(string strDate)
        {
            return this._currentDAO.GetCrowdDarenList(strDate);
        }

        /// <summary>
        /// 获取爱秀达人数量
        /// </summary>
        /// <param name="strDate">获奖日期</param>
        /// <returns></returns>
        public int GetWorkDarenCount(string strDate)
        {
            return this._currentDAO.GetWorkDarenCount(strDate);
        }

        /// <summary>
        /// 获取爱秀达人集合
        /// </summary>
        /// <param name="strDate">获奖日期</param>
        /// <returns></returns>
        public DataSet GetWorkDarenList(string strDate)
        {
            return this._currentDAO.GetWorkDarenList(strDate);
        }

        #endregion

        #region 获取品味达人作品集

        /// <summary>
        /// 获取品味达人作品集
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventsEntriesList()
        {
            return this._currentDAO.GetEventsEntriesList();
        }

        #endregion
    }
}