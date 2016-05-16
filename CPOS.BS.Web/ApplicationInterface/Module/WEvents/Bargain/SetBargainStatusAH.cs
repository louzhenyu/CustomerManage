using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class SetBargainStatusAH : BaseActionHandler<SetBargainRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetBargainRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllPanicbuyingEvent = new PanicbuyingEventBLL(loggingSessionInfo);

            var UpdateData = bllPanicbuyingEvent.GetByID(para.EventId);
            if(DateTime.Now>UpdateData.EndTime)
                throw new APIException("砍价活动已结束！");
            if (UpdateData == null)
                throw new APIException("未找到砍价活动！");

            UpdateData.EventStatus = 10;//提前结束
            //
            bllPanicbuyingEvent.Update(UpdateData);


            return rd;
        }
    }
}