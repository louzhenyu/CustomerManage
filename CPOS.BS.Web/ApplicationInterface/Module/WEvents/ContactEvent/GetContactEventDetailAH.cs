using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.ContactEvent.Request;
using JIT.CPOS.DTO.Module.Event.ContactEvent.Response;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.ContactEvent
{
    public class GetContactEventDetailAH : BaseActionHandler<GetContactEventDetailRP,GetContactEventDetailRD>
    {
        protected override GetContactEventDetailRD ProcessRequest(DTO.Base.APIRequest<GetContactEventDetailRP> pRequest)
        {
            var rd = new GetContactEventDetailRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllContactEvent = new ContactEventBLL(loggingSessionInfo);

            try
            {
                ContactEventEntity entityContactEvent = new ContactEventEntity();
                entityContactEvent = bllContactEvent.GetByID(para.ContactEventId);
                if (entityContactEvent != null)
                {
                    throw new APIException("未找到相关触点活动，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
                else
                {
                    rd.ContactEventId = entityContactEvent.ContactEventId;
                    rd.ContactEventName = entityContactEvent.ContactEventName;
                    rd.ContactTypeCode = entityContactEvent.ContactTypeCode;
                    rd.PrizeType = entityContactEvent.PrizeType;
                    rd.BeginDate = entityContactEvent.BeginDate.ToString();
                    rd.EndDate = entityContactEvent.EndDate.ToString();
                    rd.CouponTypeID = entityContactEvent.CouponTypeID;
                    rd.Integral = entityContactEvent.Integral;
                    rd.ChanceCount = entityContactEvent.ChanceCount;
                    rd.EventId = entityContactEvent.EventId;
                    rd.ShareEventId = entityContactEvent.ShareEventId;
                    rd.ShareEventName = entityContactEvent.ShareEventName;
                    rd.EventName = entityContactEvent.EventName;
                    rd.CouponTypeName = entityContactEvent.CouponTypeName;
                    
                }
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }
            return rd;
        }
    }
}