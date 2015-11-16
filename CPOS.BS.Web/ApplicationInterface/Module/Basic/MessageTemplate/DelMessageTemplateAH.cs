using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.MessageTemplate.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.MessageTemplate
{
    public class DelMessageTemplateAH : BaseActionHandler<SetMessageTemplateRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetMessageTemplateRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var MessageTemplateBLL = new C_MessageTemplateBLL(loggingSessionInfo);
            try
            {
                C_MessageTemplateEntity DelData = MessageTemplateBLL.GetByID(para.TemplateID);
                if (DelData == null)
                    throw new APIException("消息模板对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                //执行
                MessageTemplateBLL.Delete(DelData);
            }
            catch (APIException apiEx)
            {

                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }


            return rd;
        }
    }
}