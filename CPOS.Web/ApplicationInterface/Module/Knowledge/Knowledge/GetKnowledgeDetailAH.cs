using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Knowledge.Knowledge
{
    /// <summary>
    /// 4.5.3	获取文章详细
    /// </summary>
    public class GetKnowledgeDetailAH : BaseActionHandler<GetKnowledgeDetailRP, GetKnowledgeDetailRD>
    {
        #region 错误码
        const int ERROR_KNOWLEDGE_NOTEXISTS = 340;
        #endregion

        protected override GetKnowledgeDetailRD ProcessRequest(DTO.Base.APIRequest<GetKnowledgeDetailRP> pRequest)
        {
            GetKnowledgeDetailRD rd = new GetKnowledgeDetailRD();
            var bll = new KnowledgeBLL(CurrentUserInfo);
            var entity = bll.GetByID(pRequest.Parameters.ID);

            if (entity == null)
            {
                throw new APIException(string.Format("未找到ID：{0}的文章", pRequest.Parameters.ID)) { ErrorCode = 340 };
            }
            var info = new KnowledgeInfo
            {
                Author = entity.Author,
                ClickCount = entity.ClickCount,
                Content = entity.Content,
                Description = entity.Remark,
                EvaluateCount = entity.EvaluateCount,
                ID = entity.KnowIedgeId,
                ImageUrl = entity.ImageUrl,
                KeepCount = entity.KeepCount,
                PraiseCount = entity.PraiseCount,
                ShareCount = entity.ShareCount,
                Title = entity.Title,
                TreadCount = entity.TreadCount
            };
            rd.KnowledgeInfo = info;
            return rd;
        }
    }
}
