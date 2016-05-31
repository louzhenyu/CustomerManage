using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Report.MailReport.Request;
using JIT.CPOS.DTO.Module.Report.MailReport.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.MailReport
{
    public class GetLast7DaysOperationDataAH : BaseActionHandler<GetLast7DaysOperationDataRP, GetLast7DaysOperationDataRD>
    {
        protected override GetLast7DaysOperationDataRD ProcessRequest(DTO.Base.APIRequest<GetLast7DaysOperationDataRP> pRequest)
        {
            R_WxO2OPanel_7DaysBLL bll = new R_WxO2OPanel_7DaysBLL(CurrentUserInfo);
            var dbEntity = bll.GetEntityByDate(DateTime.Parse(pRequest.Parameters.DateCode));
            return new GetLast7DaysOperationDataRD(dbEntity) { };
        }
    }
}