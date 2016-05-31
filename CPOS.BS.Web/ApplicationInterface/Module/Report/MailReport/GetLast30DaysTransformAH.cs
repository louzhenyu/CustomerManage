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
    public class GetLast30DaysTransformAH : BaseActionHandler<GetLast30DaysTransformRP, GetLast30DaysTransformRD>
    {
        protected override GetLast30DaysTransformRD ProcessRequest(DTO.Base.APIRequest<GetLast30DaysTransformRP> pRequest)
        {
            R_WxO2OPanel_30DaysBLL bll = new R_WxO2OPanel_30DaysBLL(CurrentUserInfo);
            var dbEntity = bll.GetEntityByDate(DateTime.Parse(pRequest.Parameters.DateCode));
            return new GetLast30DaysTransformRD(dbEntity) { };
        }
    }
}