using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;
using JIT.Utility.DataAccess.Query;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class GetBargainItemListAH : BaseActionHandler<GetBargainItemRP, GetBargainItemRD>
    {
        protected override GetBargainItemRD ProcessRequest(DTO.Base.APIRequest<GetBargainItemRP> pRequest)
        {
            var rd = new GetBargainItemRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var Bll = new PanicbuyingKJEventItemMappingBLL(loggingSessionInfo);
            var EventBll = new PanicbuyingEventBLL(loggingSessionInfo);

            var complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = para.EventId });
            complexCondition.Add(new EqualsCondition() { FieldName = "customerId", Value = loggingSessionInfo.ClientID });
            var lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "LastUpdateTime", Direction = OrderByDirections.Desc });
            //活动
            var EventData = EventBll.GetByID(para.EventId);
            if (EventData != null)
            {
                rd.EventName = EventData.EventName;
                rd.BeginTime = EventData.BeginTime.ToString("yyyy-MM-dd HH:mm");
                rd.EndTime = EventData.EndTime.ToString("yyyy-MM-dd HH:mm");
            }
            var ResultList = Bll.Query(complexCondition.ToArray(), lstOrder.ToArray()).ToList();
            //列表
            rd.ItemMappingInfoList = (from u in ResultList
                                      select new ItemMappingInfo()
                                      {
                                          EventItemMappingID = u.EventItemMappingID.ToString(),
                                          ItemName = u.ItemName,
                                          SinglePurchaseQty = u.SinglePurchaseQty.Value,
                                          ItemId=u.ItemID
                                      }).ToList();

            return rd;
        }
    }
}