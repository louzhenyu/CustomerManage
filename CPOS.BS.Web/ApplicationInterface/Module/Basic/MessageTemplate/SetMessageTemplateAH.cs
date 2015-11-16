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
    public class SetMessageTemplateAH : BaseActionHandler<SetMessageTemplateRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetMessageTemplateRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var MessageTemplateBLL = new C_MessageTemplateBLL(loggingSessionInfo);

            try
            {
                if (string.IsNullOrWhiteSpace(para.TemplateID))
                {
                    //添加
                    C_MessageTemplateEntity AddData = new C_MessageTemplateEntity();
                    AddData.TemplateID = System.Guid.NewGuid();
                    AddData.Type = "未定义";
                    AddData.Title = para.Title;
                    AddData.Content = para.Content;
                    AddData.CustomerID = loggingSessionInfo.ClientID;
                    MessageTemplateBLL.Create(AddData);
                }
                else
                {
                    //编辑
                    C_MessageTemplateEntity UpData = MessageTemplateBLL.GetByID(para.TemplateID);
                    if (UpData==null)
                        throw new APIException("假日对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                    UpData.Title = para.Title;
                    UpData.Content = para.Content;
                    UpData.CustomerID = loggingSessionInfo.ClientID;
                    MessageTemplateBLL.Update(UpData);

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