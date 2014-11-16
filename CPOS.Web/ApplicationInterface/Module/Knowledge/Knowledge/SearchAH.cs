using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Knowledge.Knowledge
{
    public class SearchAH : BaseActionHandler<SearchRP, SearchRD>
    {
        #region 错误码
        #endregion

        protected override SearchRD ProcessRequest(DTO.Base.APIRequest<SearchRP> pRequest)
        {
            SearchRD rd = new SearchRD();
            var para = pRequest.Parameters;

            #region 获取数据,并更新了标签查询次数
            var bll = new KnowledgeBLL(CurrentUserInfo);
            var entitys = bll.GetByParaAndUpdateTagSearchTimes(HttpUtility.UrlDecode(para.Key), para.PageIndex, para.PageSize, para.Type);
            var list = entitys.OrderBy(t => t.DisplayIndex).Select(t => new KnowledgeListItemInfo
            {
                Author = t.Author,
                ClickCount = t.ClickCount,
                EvaluateCount = t.EvaluateCount,
                ID = t.KnowIedgeId,
                KeepCount = t.KeepCount,
                PraiseCount = t.PraiseCount,
                ShareCount = t.ShareCount,
                Title = t.Title,
                TreadCount = t.TreadCount
            });
            #endregion

            #region 增加查询记录
            var logbll = new KnowledgeTagLogBLL(CurrentUserInfo);
            var logEntity = new KnowledgeTagLogEntity()
            {
                Keyword = para.Key,
                VipId = CurrentUserInfo.UserID,
                TagLogId = Guid.NewGuid()
            };
            logbll.Create(logEntity);
            #endregion

            rd.Knowledges = list.ToArray();
            return rd;
        }
    }
}
