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
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class DeleteBargainAH : BaseActionHandler<ChangeStatusRP, EmptyRD>
    {
        protected override EmptyRD ProcessRequest(DTO.Base.APIRequest<ChangeStatusRP> pRequest)
        {
            var rd = new EmptyRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllPanicbuyingEvent = new PanicbuyingEventBLL(loggingSessionInfo);
            var entityPanicbuyingEvent = bllPanicbuyingEvent.GetByID(para.EventId);
            if (entityPanicbuyingEvent == null)
            {
                throw new APIException("未找到相关砍价活动，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
            else
            {
                entityPanicbuyingEvent.IsDelete = 1;
                bllPanicbuyingEvent.Update(entityPanicbuyingEvent, false);
            }


            return rd;
        }
    }
}