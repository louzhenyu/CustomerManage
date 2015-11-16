using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Report.DayReport.Request;
using JIT.CPOS.DTO.Module.Report.DayReport.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.StoresReport
{
    public class GetDayVendingCountAH : BaseActionHandler<DayVendingRP, DayVendingRD>
    {
        protected override DayVendingRD ProcessRequest(DTO.Base.APIRequest<DayVendingRP> pRequest)
        {
            var rd = new DayVendingRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBLL = new VipCardBLL(loggingSessionInfo);
            try
            {
                if (string.IsNullOrWhiteSpace(para.StareDate) || string.IsNullOrWhiteSpace(para.EndDate))
                    throw new APIException("日期区间参数为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }


            rd = VipCardBLL.GetDayVendingCount(para.StareDate, para.EndDate);
            if (rd == null)
            {
                rd = new DayVendingRD();
            }
            return rd;
        }
    }
}