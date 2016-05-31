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
    public class GetAllRankAH : BaseActionHandler<GetGoodsRankListRP, GetAllRankRD>
    {
        protected override GetAllRankRD ProcessRequest(DTO.Base.APIRequest<GetGoodsRankListRP> pRequest)
        {
            pRequest.Parameters.DateCode = DateTime.Now.ToShortDateString();
            R_WxO2OPanel_ItemTopTenBLL bll = new R_WxO2OPanel_ItemTopTenBLL(CurrentUserInfo);
            var dbEntity1 = bll.GetListByDate(DateTime.Parse(pRequest.Parameters.DateCode));
            if (dbEntity1.Count() == 0)
            {
                dbEntity1 = bll.GetListByDate(DateTime.Parse(pRequest.Parameters.DateCode).AddDays(-1));
            }

            R_WxO2OPanel_30DaysBLL bll2 = new R_WxO2OPanel_30DaysBLL(CurrentUserInfo);
            var dbEntity2 = bll2.GetEntityByDate(DateTime.Parse(pRequest.Parameters.DateCode));
            if (dbEntity2 == null)
            {
                dbEntity2 = bll2.GetEntityByDate(DateTime.Parse(pRequest.Parameters.DateCode).AddDays(-1));
            }

            R_WxO2OPanel_7DaysBLL bll3 = new R_WxO2OPanel_7DaysBLL(CurrentUserInfo);
            var dbEntity3 = bll3.GetEntityByDate(DateTime.Parse(pRequest.Parameters.DateCode));
            if (dbEntity3 == null)
            {
                dbEntity3 = bll3.GetEntityByDate(DateTime.Parse(pRequest.Parameters.DateCode).AddDays(-1));
            }

            return new GetAllRankRD { GoodsRankList = new GetGoodsRankListRD(dbEntity1) { }, Last30DaysTransform = new GetLast30DaysTransformRD(dbEntity2) { }, Last7DaysOperationData = new GetLast7DaysOperationDataRD(dbEntity3) { } };
        }
    }
}