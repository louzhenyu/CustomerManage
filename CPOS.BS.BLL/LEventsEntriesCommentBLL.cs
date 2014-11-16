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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class LEventsEntriesCommentBLL
    {
        #region 获取评论列表
        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="entriesId">作品ID</param>
        /// <param name="date">查询日期</param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public GetResponseParams<LEventsEntriesCommentEntity> GetCommentList(string entriesId, string date, string IsCrowdDaren, int Page, int PageSize)
        {
            GetResponseParams<LEventsEntriesCommentEntity> response = new GetResponseParams<LEventsEntriesCommentEntity>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "成功";

            try
            {
                #region 业务处理

                var commentEntity = new LEventsEntriesCommentEntity();
                commentEntity.ICount = this._currentDAO.GetCommentCount(entriesId, date, IsCrowdDaren);

                var commentList = new List<LEventsEntriesCommentEntity>();
                if (commentEntity.ICount > 0)
                {
                    var ds = _currentDAO.GetCommentList(entriesId, date, IsCrowdDaren, Page, PageSize);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        commentList = DataTableToObject.ConvertToList<LEventsEntriesCommentEntity>(ds.Tables[0]);
                    }
                }

                commentEntity.CommentList = commentList;

                #endregion

                response.Params = commentEntity;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "错误:" + ex.ToString();
                return response;
            }
        }
        #endregion
    }
}