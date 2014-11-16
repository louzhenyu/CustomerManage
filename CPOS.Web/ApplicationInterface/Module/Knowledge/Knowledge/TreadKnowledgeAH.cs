using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Knowledge.Knowledge
{
    /// <summary>
    /// 4.5.5	踩文章
    /// </summary>
    public class TreadKnowledgeAH : BaseActionHandler<TreadKnowledgeRP, TreadKnowledgeRD>
    {
        protected override TreadKnowledgeRD ProcessRequest(DTO.Base.APIRequest<TreadKnowledgeRP> pRequest)
        {
            TreadKnowledgeRD rd = new TreadKnowledgeRD();
            var para = pRequest.Parameters;

            #region 文章的踩数+1

            var bll = new KnowledgeBLL(CurrentUserInfo);
            var entity = bll.GetByID(para.ID);
            entity.TreadCount++;
            bll.Update(entity);

            #endregion

            #region 往文章的踩表插入一条记录
            var treadbll = new KnowledgeTreadBLL(CurrentUserInfo);
            var treadEntity = new KnowledgeTreadEntity()
            {
                VipId = CurrentUserInfo.UserID,
                KnowIedgeId = para.ID
            };
            var temp = treadbll.QueryByEntity(treadEntity, null);
            if (temp.Length == 0)
            {
                treadEntity.TreadId = Guid.NewGuid();
                treadbll.Create(treadEntity);
            }

            #endregion
            return rd;
        }
    }
}
