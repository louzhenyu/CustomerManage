using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.WEvent.Bargain
{
    public class DeleteKJEventJoinAH : BaseActionHandler<DeleteKJEventJoinRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<DeleteKJEventJoinRP> pRequest)
        {
            var rp = pRequest.Parameters;
            var rd = new EmptyResponseData();
            var Bll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);
            var Result = Bll.GetByID(rp.KJEventJoinId);
            if (Result == null)
                throw new APIException("未找到相关砍价参与主表信息，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

            Bll.Delete(Result);
            return rd;
        }
    }
}