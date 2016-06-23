using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.RTExtend
{
    /// <summary>
    ///  图文素材列表
    /// </summary>
    public class GetMaterialTextListAH : BaseActionHandler<EmptyRequestParameter, GetNewsListRD>
    {
        protected override GetNewsListRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
            {
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_RESPONSE };
            }
            WMaterialTextBLL bll = new WMaterialTextBLL(loggingSessionInfo);
            var r = bll.GetAllByCustomId();
            GetNewsListRD result = TransModel(r);
            return result;
        }
        /// <summary>
        /// 模型转换
        /// </summary>
        /// <param name="dbList"></param>
        /// <returns></returns>
        GetNewsListRD TransModel(List<WMaterialTextEntity> dbList)
        {
            GetNewsListRD rd = new GetNewsListRD();
            if (dbList.Count() == 0)
                return null;
            rd.List = dbList.Select(x =>
                new NewsInfo()
                {
                    TextId = x.TextId,
                    Title = x.Title,
                    CoverImageUrl = x.CoverImageUrl,
                    Text = x.Author == null ? "" : x.Author.Length > 50 ? x.Author.Substring(0, 50) : x.Author
                }).ToList();
            return rd;
        }
    }
}