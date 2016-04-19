using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingActivity
{
    public class DelayEventAH : BaseActionHandler<DelayEventRP, DelayEventRD>
    {
        protected override DelayEventRD ProcessRequest(APIRequest<DelayEventRP> pRequest)
        {
            var rd = new DelayEventRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventBLL bllCTWEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            T_CTW_LEventEntity entityCTWEvent = new T_CTW_LEventEntity();
            entityCTWEvent = bllCTWEvent.GetByID(para.CTWEventId);
            entityCTWEvent.EndDate = Convert.ToDateTime(para.EndDate);
            bllCTWEvent.Update(entityCTWEvent, null);
            if (para.EventType == "Game")
            {
                T_CTW_LEventInteractionBLL bllEventInteraction = new T_CTW_LEventInteractionBLL(loggingSessionInfo);
                string strEventId = bllEventInteraction.QueryByEntity(new T_CTW_LEventInteractionEntity() { CTWEventId = new Guid(para.CTWEventId) }, null).FirstOrDefault().LeventId;
                LEventsBLL bllEvent = new LEventsBLL(loggingSessionInfo);
                bllEvent.Update(new LEventsEntity() { EndTime = para.EndDate, EventID = strEventId }, false);

            }
            if (para.EventType == "Sales")
            {
                PanicbuyingEventBLL bllEvent = new PanicbuyingEventBLL(loggingSessionInfo);

                bllEvent.DelayEvent(para.CTWEventId,para.EndDate);
            }
            rd.EventId = para.CTWEventId;
            return rd;
        }
    }
}