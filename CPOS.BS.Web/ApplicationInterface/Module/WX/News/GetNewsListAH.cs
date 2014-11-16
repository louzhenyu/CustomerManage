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
    public class GetNewsListAH : BaseActionHandler<GetNewsListRP, GetNewsListRD>
    {
        protected override GetNewsListRD ProcessRequest(DTO.Base.APIRequest<GetNewsListRP> pRequest)
        {
            var rd = new GetNewsListRD();

            int? pageSize = pRequest.Parameters.PageSize;
            int? pageIndex = pRequest.Parameters.PageIndex;

            string newsTypeId = pRequest.Parameters.NewsTypeId;
            string newsName = pRequest.Parameters.NewsName;

            var bll = new LNewsBLL(CurrentUserInfo);

            var ds = bll.GetNewsList(CurrentUserInfo.ClientID, newsTypeId, newsName, pageIndex ?? 0, pageSize ?? 15);

            if (ds.Tables[0].Rows.Count == 0)
            {
                rd.NewsList = null;
                rd.TotalPages = 0;
            }
            else
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new NewsInfo
                {
                    NewsId = t["NewsId"].ToString(),
                    NewsTypeName = t["NewsTypeName"].ToString(),
                    NewsName = t["NewsName"].ToString(),
                    PublishTime = t["PublishTime"].ToString(),
                });
                rd.NewsList = temp.ToArray();
                int totalCount = bll.GetNewsListCount(CurrentUserInfo.ClientID, newsTypeId, newsName);


                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount * 1.00 / (pageSize ?? 15) * 1.00)));
            }
            return rd;

        }
    }
}