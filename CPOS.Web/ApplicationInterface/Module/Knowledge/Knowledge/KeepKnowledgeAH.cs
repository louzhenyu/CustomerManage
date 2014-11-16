using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Knowledge.Knowledge
{
    public class KeepKnowledgeAH : BaseActionHandler<KeepKnowledgeRP, KeepKnowledgeRD>
    {
        protected override KeepKnowledgeRD ProcessRequest(DTO.Base.APIRequest<KeepKnowledgeRP> pRequest)
        {
            KeepKnowledgeRD rd = new KeepKnowledgeRD();
            var para = pRequest.Parameters;

            #region 文章的收藏数据+1
            var bll = new KnowledgeBLL(CurrentUserInfo);
            var entity = bll.GetByID(para.ID);
            entity.KeepCount++;
            bll.Update(entity);
            #endregion

            #region 往文章的收藏表插入一条记录。
            var keepbll = new KnowledgeKeepBLL(CurrentUserInfo);
            var keepEntity = new KnowledgeKeepEntity()
            {
                VipId = CurrentUserInfo.UserID,
                KnowIedgeId = para.ID
            };
            var temp = keepbll.QueryByEntity(keepEntity, null);
            if (temp.Length == 0)
            {
                keepEntity.KnowledgeKeepId = Guid.NewGuid();
                keepbll.Create(keepEntity);
            }
            #endregion
            return rd;
        }
    }
}
