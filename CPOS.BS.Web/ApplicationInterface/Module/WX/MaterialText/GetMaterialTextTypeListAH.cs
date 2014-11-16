using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.MaterialText
{
    public class GetMaterialTextTypeListAH : BaseActionHandler<GetMaterialTextTypeListRP, GetMaterialTextTypeListRD>
    {
        protected override GetMaterialTextTypeListRD ProcessRequest(DTO.Base.APIRequest<GetMaterialTextTypeListRP> pRequest)
        {
            var rd = new GetMaterialTextTypeListRD();

            int? pageIndex = pRequest.Parameters.PageIndex;
            int? pageSize = pRequest.Parameters.PageSize;

            string applicationId = pRequest.Parameters.ApplicationId;
         

            var bll = new WModelBLL(CurrentUserInfo);

            var ds = bll.GetWModelList(this.CurrentUserInfo.ClientID,applicationId, pageIndex ?? 0, pageSize ?? 15);

            if (ds.Tables[0].Rows.Count == 0)
            {
                rd.MaterialTextTypeList = null;
            }
            else
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new MaterialTextTypeInfo
                {
                    TypeId = t["ModelId"].ToString(),
                    TypeName = t["ModelName"].ToString()
                });

                rd.MaterialTextTypeList = tmp.ToArray();
            }


            return rd;

        }
    }
}
