using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.MaterialText
{

    public class GetMaterialTextListAH : BaseActionHandler<GetMaterialTextListRP, GetMaterialTextListRD>
    {
        protected override GetMaterialTextListRD ProcessRequest(DTO.Base.APIRequest<GetMaterialTextListRP> pRequest)
        {
            var rd = new GetMaterialTextListRD();

            string materialTextId = pRequest.Parameters.MaterialTextId;
            string name = pRequest.Parameters.Name;
            string typeId = pRequest.Parameters.TypeId;

            int? pageSize = pRequest.Parameters.PageSize;
            int? pageIndex = pRequest.Parameters.PageIndex;

            var bll = new WMaterialTextBLL(CurrentUserInfo);
            var entitys = bll.GetWMaterialTextList(this.CurrentUserInfo.ClientID, name, materialTextId,typeId, pageSize, pageIndex);

            var list = entitys.Select(t => new MaterialTextListInfo

            {
                TestId = t.TextId,
                ApplicationId = t.ApplicationId,
                Abstract = t.Author,//摘要使用原来的字段
                DisplayIndex = Convert.ToInt32(t.DisplayIndex),
                ImageUrl = t.CoverImageUrl,
                OriginalUrl = t.OriginalUrl,
                PageId = t.PageId.ToString(),
                PageParamJson = t.PageParamJson,
                Text = t.Text,
                Title = t.Title,
                TypeId = t.ModelId,
                UnionTypeId = t.TypeId
            });
            rd.MaterialTextList = list.ToArray();
            int totalCount = bll.GetWMaterialTextListCount(CurrentUserInfo.ClientID, name, materialTextId,typeId);


            rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount * 1.00 / (pageSize ?? 15) * 1.00)));
            return rd;
        }
    }
}