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
    public class EndOfEventAH : BaseActionHandler<EndOfEventRP, EndOfEventRD>
    {
        protected override EndOfEventRD ProcessRequest(APIRequest<EndOfEventRP> pRequest)
        {
            var rd = new EndOfEventRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventBLL bllCTWEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            bllCTWEvent.Update(new T_CTW_LEventEntity() { Status = 40,CTWEventId=new Guid(para.CTWEventId) }, null);

            if(para.EventType=="Game")
            {
                T_CTW_LEventInteractionBLL bllEventInteraction = new T_CTW_LEventInteractionBLL(loggingSessionInfo);
                string strEventId=bllEventInteraction.QueryByEntity(new T_CTW_LEventInteractionEntity() { CTWEventId = new Guid(para.CTWEventId) }, null).FirstOrDefault().LeventId;
                LEventsBLL bllEvent = new LEventsBLL(loggingSessionInfo);
                var entityEvent = bllEvent.GetByID(strEventId);
                entityEvent.EventStatus = 40;
                bllEvent.Update(entityEvent);
            }
            if (para.EventType == "Sales")
            {
                PanicbuyingEventBLL bllEvent = new PanicbuyingEventBLL(loggingSessionInfo);

                bllEvent.EndOfEvent(para.CTWEventId);
            }
            rd.CTWEventId = para.CTWEventId;
            return rd;
        }
    }
}