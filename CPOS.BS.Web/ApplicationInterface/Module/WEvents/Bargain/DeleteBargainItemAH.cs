using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class DeleteBargainItemAH : BaseActionHandler<SetBargainDetailsRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetBargainDetailsRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var Bll = new PanicbuyingKJEventItemMappingBLL(loggingSessionInfo);
            var PanicbuyingEventBLL = new PanicbuyingEventBLL(loggingSessionInfo);
            var pTran = Bll.GetTran();
            using (pTran.Connection)
            {
                var DeleteItemData = Bll.GetByID(para.EventItemMappingID);
                if (DeleteItemData == null)
                    throw new APIException("未找到相关砍价活动商品，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                //
                Bll.Delete(DeleteItemData, pTran);
                //更新活动商品数量
                var UpdateEventData = PanicbuyingEventBLL.GetByID(para.EventId);
                if (UpdateEventData == null)
                    throw new APIException("未找到相关砍价活动，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                UpdateEventData.ItemQty -= 1;
                PanicbuyingEventBLL.Update(UpdateEventData, pTran);
                //提交
                pTran.Commit();
            }

            return rd;
        }
    }
}