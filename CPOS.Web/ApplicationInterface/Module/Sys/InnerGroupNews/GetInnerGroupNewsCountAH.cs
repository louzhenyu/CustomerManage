using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Request;
using JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Response;
using JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Sys.InnerGroupNews
{
    public class GetInnerGroupNewsCountAH : BaseActionHandler<GetInnerGroupNewsCountRP, GetInnerGroupNewsCountRD>
    {
        protected override GetInnerGroupNewsCountRD ProcessRequest(DTO.Base.APIRequest<GetInnerGroupNewsCountRP> pRequest)
        {
            var rd = new GetInnerGroupNewsCountRD();
            string userId = pRequest.UserID;
            string customerId = pRequest.CustomerID;
            int NoticePlatformTypeId = pRequest.Parameters.NoticePlatformTypeId;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);
            InnerGroupNewsBLL InnerGroupNewsService = new InnerGroupNewsBLL(loggingSessionInfo);
            SetoffToolsBLL setofftoolService = new SetoffToolsBLL(loggingSessionInfo);
            var innergroupnewsdsCount = InnerGroupNewsService.GetVipInnerGroupNewsUnReadCount(userId, customerId, NoticePlatformTypeId,null);
            rd.InnerGroupNewsCount = innergroupnewsdsCount;

            //获取集客工具列表
            var setofftooldsCount = setofftoolService.GetUnReadSetoffToolsCount(userId, pRequest.CustomerID, NoticePlatformTypeId,2);
            rd.SetoffToolsCount = setofftooldsCount;
            return rd;
        }
    }
}