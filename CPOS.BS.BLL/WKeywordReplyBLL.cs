/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
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
    public partial class WKeywordReplyBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<WKeywordReplyEntity> GetWebWKeywordReply(WKeywordReplyEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WKeywordReplyEntity> list = new List<WKeywordReplyEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebWKeywordReply(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WKeywordReplyEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebWKeywordReplyCount(WKeywordReplyEntity entity)
        {
            return _currentDAO.GetWebWKeywordReplyCount(entity);
        }
        public bool IsExistKeyword(string ApplicationId, string Keyword, string ReplyId)
        {
            return _currentDAO.IsExistKeyword(ApplicationId, Keyword, ReplyId);
        }
        #endregion

        public DataSet GetKeyWordList(string applicationId, string keyword, int pageSize, int pageIndex)
        {
            return this._currentDAO.GetKeyWordList(applicationId, keyword, pageSize, pageIndex);
        }

        public DataSet GetKeyWordListByReplyId(string replyId)
        {
            return this._currentDAO.GetKeyWordListByReplyId(replyId);
        }

        public DataSet GetWMaterialTextByReplyId(string replyId)
        {
            return this._currentDAO.GetWMaterialTextByReplyId(replyId);
        }

        public void UpdateWkeywordReplyByReplyId(string replyId, int beLinkType, int keywordType, int displayIndex)
        {
            this._currentDAO.UpdateWkeywordReplyByReplyId(replyId,beLinkType,keywordType,displayIndex);
        }

        public DataSet GetDefaultKeyword(string applicationId, int keywordType)
        {
            return this._currentDAO.GetDefaultKeyword(applicationId, keywordType);
        }

        //public string GetReplyIdByQrcode(string qrCode, string customerId)
        //{
        //    return this._currentDAO.GetReplyIdByQrcode(qrCode, customerId);
        //}
    }
}