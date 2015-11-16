using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.MessageTemplate.Request;
using JIT.CPOS.DTO.Module.Marketing.Activity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.MessageTemplate
{
    /// <summary>
    /// 获取消息模板详情
    /// </summary>
    public class GetMessageTemplateDeatileAH:BaseActionHandler<SetMessageTemplateRP, GetMessageTemplateDeatileRD>
    {
        protected override GetMessageTemplateDeatileRD ProcessRequest(DTO.Base.APIRequest<SetMessageTemplateRP> pRequest)
        {
            var rd = new GetMessageTemplateDeatileRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var MessageTemplateBLL = new C_MessageTemplateBLL(loggingSessionInfo);

            if (!string.IsNullOrWhiteSpace(para.TemplateID))
            {
                C_MessageTemplateEntity Data = MessageTemplateBLL.GetByID(para.TemplateID);
                if (Data != null)
                {
                    rd.TemplateID = Data.TemplateID.ToString();
                    rd.Title = Data.Title;
                    rd.Content = Data.Content;
                }
            }

            return rd;
        }
    }
}