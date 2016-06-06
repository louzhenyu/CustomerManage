using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    public class GetCTWLEventListAH : BaseActionHandler<GetCTWLEventListRP, GetCTWLEventListRD>
    {
        protected override GetCTWLEventListRD ProcessRequest(DTO.Base.APIRequest<GetCTWLEventListRP> pRequest)
        {
            var rd = new GetCTWLEventListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var Bll = new T_CTW_LEventBLL(this.CurrentUserInfo);

            //排序
            var pOrderBys = new List<OrderBy>();
            pOrderBys.Add(new OrderBy() { FieldName = "LastUpdateTime", Direction = OrderByDirections.Desc });

            var Result = Bll.PagedQueryByEntity(new T_CTW_LEventEntity() { Status=20,CustomerId = this.CurrentUserInfo.ClientID }, pOrderBys.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalCount = Result.RowCount;
            rd.TotalPage = Result.PageCount;

            rd.CTWLEventInfoList = Result.Entities.Select(m => new CTWLEventInfo()
            {
                CTWEventId = m.CTWEventId.ToString(),
                Name = m.Name,
                StartDate = m.StartDate.Value.ToString("yyyy年MM月dd日"),
                EndDate = m.EndDate.Value.ToString("yyyy年MM月dd日"),
                OnfflineQRCodeId = m.OnlineQRCodeId
            }).ToList();

            //获取线下二维码URL
            foreach (var item in rd.CTWLEventInfoList)
            {
                item.OnfflineQRCodeUrl = Bll.GetOnlineQRCodeUrl(item.OnfflineQRCodeId);
            }

            return rd;
        }
    }
}