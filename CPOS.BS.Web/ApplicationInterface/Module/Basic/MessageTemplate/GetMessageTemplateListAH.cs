using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.MessageTemplate.Request;
using JIT.CPOS.DTO.Module.Basic.MessageTemplate.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.MessageTemplate
{
    public class GetMessageTemplateListAH : BaseActionHandler<SetMessageTemplateRP, GetMessageTemplateListRD>
    {
        protected override GetMessageTemplateListRD ProcessRequest(DTO.Base.APIRequest<SetMessageTemplateRP> pRequest)
        {
            var rd = new GetMessageTemplateListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var MessageTemplateBLL = new C_MessageTemplateBLL(loggingSessionInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //商户条件
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "LastUpdateTime", Direction = OrderByDirections.Desc });

            List<C_MessageTemplateEntity> ResultList = MessageTemplateBLL.Query(complexCondition.ToArray(), lstOrder.ToArray()).ToList();
            if (ResultList.Count > 0)
            {
                rd.MessageTemplateInfoList = (from u in ResultList
                                              select new MessageTemplateInfo()
                                              {
                                                  TemplateID = u.TemplateID.Value.ToString(),
                                                  Title = u.Title,
                                                  Content = u.Content,
                                                  LastUpdateTime = u.LastUpdateTime == null ? "" : u.LastUpdateTime.Value.ToString("yyyy-MM-dd")
                                              }).ToList();
            }


            return rd;
        }
    }
}