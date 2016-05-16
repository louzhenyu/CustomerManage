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
    public class DeleteBargainAH : BaseActionHandler<GetBargainItemRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<GetBargainItemRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllPanicbuyingEvent = new PanicbuyingEventBLL(loggingSessionInfo);
            var MHCategoryAreaBll = new MHCategoryAreaBLL(loggingSessionInfo);//商城装修业务对象
            var entityPanicbuyingEvent = bllPanicbuyingEvent.GetByID(para.EventId);
            if (entityPanicbuyingEvent == null)
            {
                throw new APIException("未找到相关砍价活动，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
            else
            {
                var Result = MHCategoryAreaBll.QueryByEntity(new MHCategoryAreaEntity() { HomeId =new Guid(para.EventId) }, null).ToList();
                if(Result.Count>0)
                    throw new APIException("商城装修以关联当前砍价活动，请先删除商城装修中的砍价活动再删除！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                bllPanicbuyingEvent.Delete(entityPanicbuyingEvent);
            }


            return rd;
        }
    }
}