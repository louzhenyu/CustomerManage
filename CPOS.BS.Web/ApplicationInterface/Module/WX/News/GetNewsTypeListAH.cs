using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.News.Request;
using JIT.CPOS.DTO.Module.WeiXin.News.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.News
{
    public class GetNewsTypeListAH : BaseActionHandler<GetNewsTypeListRP, GetNewsTypeListRD>
    {
        protected override GetNewsTypeListRD ProcessRequest(DTO.Base.APIRequest<GetNewsTypeListRP> pRequest)
        {
            var rd = new GetNewsTypeListRD();

            int? pageSize = pRequest.Parameters.PageSize;
            int? pageIndex = pRequest.Parameters.PageIndex;
            
            var bll = new LNewsTypeBLL(CurrentUserInfo);

            var ds = bll.GetNewsTypeList(CurrentUserInfo.ClientID, pageIndex ?? 0, pageSize ?? 15);

            if (ds.Tables[0].Rows.Count == 0)
            {
                rd.NewsTypeList = null;
            }
            else
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new NewsTypeInfo
                {
                    NewsTypeId = t["NewsTypeId"].ToString(),
                    NewsTypeName = t["NewsTypeName"].ToString()
                });
                rd.NewsTypeList = temp.ToArray();
            }
            return rd;
        }
    }
}