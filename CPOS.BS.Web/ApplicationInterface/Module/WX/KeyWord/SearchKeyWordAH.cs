using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.KeyWord
{
    public class SearchKeyWordAH : BaseActionHandler<SearchKeyWordRP, SearchKeyWordRD>
    {
        protected override SearchKeyWordRD ProcessRequest(DTO.Base.APIRequest<SearchKeyWordRP> pRequest)
        {
            var rd = new SearchKeyWordRD();

            string applicationId = pRequest.Parameters.ApplicationId;
            string keyword = pRequest.Parameters.KeyWord;

            int? pageIndex = pRequest.Parameters.PageIndex;
            int? pageSize = pRequest.Parameters.PageSize;

            var bll = new WKeywordReplyBLL(CurrentUserInfo);

            var ds = bll.GetKeyWordList(applicationId, keyword, pageSize ?? 15, pageIndex ?? 0);

            var dsCount = bll.GetKeyWordList(applicationId, keyword, Int32.MaxValue, 0);
            int totalCount = dsCount.Tables[0].Rows.Count;

            if (ds.Tables[0].Rows.Count == 0)
            {
                rd.SearchKeyList = null;
                rd.TotalPages = 0;
            }
            else
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new SearchKeyWordInfo()
                {
                    DisplayIndex = Convert.ToInt32(t["_row"]),
                    KeyWord = t["keyword"].ToString(),
                    ReplyId = t["replyId"].ToString()
                }).ToArray();
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount * 1.00 / (pageSize ?? 15) * 1.00)));
                rd.SearchKeyList = temp;
            }
            return rd;
        }
    }
}