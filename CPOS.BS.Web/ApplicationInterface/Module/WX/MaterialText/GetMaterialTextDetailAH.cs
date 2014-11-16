using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.MaterialText
{
    public class GetMaterialTextDetailAH : BaseActionHandler<GetMaterialTextDetailRP, GetMaterialTextDetailRD>
    {
        protected override GetMaterialTextDetailRD ProcessRequest(DTO.Base.APIRequest<GetMaterialTextDetailRP> pRequest)
        {
            var rd = new GetMaterialTextDetailRD();

            var customerId = pRequest.Parameters.CustomerId;
            var textId = pRequest.Parameters.TextId;

            if (string.IsNullOrEmpty(textId) || textId == "")
            {
                throw new APIException("图文标识不能为空") { ErrorCode = 120 };
            }
            var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

            var bll = new WMaterialTextBLL(currentUserInfo);

            var ds = bll.GetMaterialTextTitleList(textId, customerId);

            var temp = ds.Tables[0].AsEnumerable().Select(t => new MaterialTextTitleInfo()
            {
                Text = t["Text"].ToString(),
                Title = t["Title"].ToString(),
                CoverImageUrl = t["CoverImageUrl"].ToString(),
                Author = t["Author"].ToString(),
                TextId = textId
            }).FirstOrDefault();

            rd.MaterialTextTitleList = temp;

            return rd;

        }
    }
}