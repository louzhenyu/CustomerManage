using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class SetBargainAH : BaseActionHandler<SetBargainRP, SetBargainRD>
    {
        protected override SetBargainRD ProcessRequest(DTO.Base.APIRequest<SetBargainRP> pRequest)
        {
            var rd = new SetBargainRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllPanicbuyingEvent = new PanicbuyingEventBLL(loggingSessionInfo);
            var entityPanicbuyingEvent = new PanicbuyingEventEntity();

            entityPanicbuyingEvent.EventName = para.EventName;
            entityPanicbuyingEvent.BeginTime = para.BeginTime;
            entityPanicbuyingEvent.EndTime = para.EndTime;
            entityPanicbuyingEvent.EventTypeId = 4;
            entityPanicbuyingEvent.EventStatus = 10;

            if (string.IsNullOrEmpty(para.EventId))
            {
                bllPanicbuyingEvent.Create(entityPanicbuyingEvent);

            }
            else
            {
                entityPanicbuyingEvent.EventId =new Guid(para.EventId);
                bllPanicbuyingEvent.Update(entityPanicbuyingEvent,false);

            }
            rd.EventId = entityPanicbuyingEvent.EventId.ToString();

            return rd;
        }
    }
}