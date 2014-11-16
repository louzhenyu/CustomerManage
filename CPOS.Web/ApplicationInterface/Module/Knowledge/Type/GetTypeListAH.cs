using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Knowledge.Type.Request;
using JIT.CPOS.DTO.Module.Knowledge.Type.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Knowledge.Type
{
    public class GetTypeListAH : BaseActionHandler<GetTypeListRP, GetTypeListRD>
    {
        #region 错误码
        #endregion

        protected override GetTypeListRD ProcessRequest(DTO.Base.APIRequest<GetTypeListRP> pRequest)
        {
            GetTypeListRD rd = new GetTypeListRD();
            var bll = new KnowledgeTypeBLL(CurrentUserInfo);
            var enititys = bll.GetAll();
            var list = enititys.OrderBy(t => t.DisplayIndex).Select(t => new CategoryInfo
            {
                ID = t.KnowledgeTypeId,
                Description = t.TypeRemark,
                Name = t.TypeName
            });
            rd.CategoryList = list.ToArray();
            return rd;
        }
    }
}