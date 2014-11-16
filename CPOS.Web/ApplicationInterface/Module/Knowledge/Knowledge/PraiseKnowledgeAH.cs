using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Request;
using JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Response;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Knowledge.Knowledge
{
    /// <summary>
    /// 4.5.4 赞文章
    /// </summary>
    public class PraiseKnowledgeAH : BaseActionHandler<PraiseKnowledgeRP, PraiseKnowledgeRD>
    {
        protected override PraiseKnowledgeRD ProcessRequest(DTO.Base.APIRequest<PraiseKnowledgeRP> pRequest)
        {
            PraiseKnowledgeRD rd = new PraiseKnowledgeRD();
            var para = pRequest.Parameters;
            #region 更新文章 赞的数量
            var bll = new KnowledgeBLL(CurrentUserInfo);
            var entity=  bll.GetByID(para.ID);
            entity.PraiseCount++;
            bll.Update(entity);
            #endregion

            #region 往文章赞表中插入一条记录
            var praisebll = new KnowledgePraiseBLL(CurrentUserInfo);
            var praiseEntity = new KnowledgePraiseEntity()
            {
                PraiseId = Guid.NewGuid(),
                VipId = CurrentUserInfo.UserID,
                KnowIedgeId = para.ID
            };
            praisebll.Create(praiseEntity);
            #endregion
            return rd;
        }
    }
}
