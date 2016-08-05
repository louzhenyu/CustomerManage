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
    public class GetDayReconciliationAH : BaseActionHandler<DayReconciliationRP, DayReconciliationRD>
    {
        protected override DayReconciliationRD ProcessRequest(DTO.Base.APIRequest<DayReconciliationRP> pRequest)
        {
            var rd = new DayReconciliationRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBLL = new VipCardBLL(loggingSessionInfo);
            int Days = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(para.StareDate) || string.IsNullOrWhiteSpace(para.EndDate))
                    throw new APIException("日期区间参数为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                //判断区间小于等于7天，开始日期和结束日期都算一天
                DateTime d1 = Convert.ToDateTime(para.StareDate).Date;
                DateTime d2 = Convert.ToDateTime(para.StareDate).Date;
                Days=(d2-d1).Days;//天数差
                Days++;
                if(Days>7)
                    throw new APIException("查询天数大于7天！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }
            
            DateTime m_StareDate = Convert.ToDateTime(para.StareDate).Date;
            DateTime m_EndDate = Convert.ToDateTime(para.EndDate).Date;
            rd = VipCardBLL.GetDayReconciliation(m_StareDate, m_EndDate, para.UnitID, loggingSessionInfo.ClientID);
            if (rd == null)
            {
                rd = new DayReconciliationRD();
            }
            return rd;
        }
    }
}