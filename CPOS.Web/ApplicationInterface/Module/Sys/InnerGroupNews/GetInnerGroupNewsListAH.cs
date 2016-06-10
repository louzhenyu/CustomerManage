using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Request;
using JIT.CPOS.DTO.Module.Sys.InnerGroupNews.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Sys.InnerGroupNews
{
    public class GetInnerGroupNewsListAH : BaseActionHandler<GetInnerGroupNewsListRP, GetInnerGroupNewsListRD>
    {
        protected override GetInnerGroupNewsListRD ProcessRequest(DTO.Base.APIRequest<GetInnerGroupNewsListRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);

            InnerGroupNewsBLL bll = new InnerGroupNewsBLL(loggingSessionInfo);

            var rd = new GetInnerGroupNewsListRD();

            //分页查找消息列表
            PagedQueryResult<InnerGroupNewsEntity> lst = bll.GetVipInnerGroupNewsList(parameter.PageIndex, parameter.PageSize, pRequest.UserID, pRequest.CustomerID, parameter.NoticePlatformTypeId, parameter.BusType);
            rd.InnerGroupNewsList = lst.Entities.Select(m => new InnerGroupNewsInfo()
                                                        {
                                                            Title = m.Title,
                                                            IsRead = m.IsRead,
                                                            BusType = m.BusType,
                                                            GroupNewsId = m.GroupNewsId,
                                                            MsgTime = m.CreateTime + "",
                                                            Text = m.Text
                                                        }).ToList();

            rd.TotalCount = lst.RowCount;
            rd.TotalPages = lst.PageCount;
            return rd;
        }
    }
}