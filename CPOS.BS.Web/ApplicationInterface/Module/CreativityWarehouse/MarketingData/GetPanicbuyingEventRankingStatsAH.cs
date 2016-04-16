using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;
using System.Data;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingData
{
    public class GetPanicbuyingEventRankingStatsAH : BaseActionHandler<CTWEventRP, GetPanicbuyingEventRankingStatsRD>
    {
        protected override GetPanicbuyingEventRankingStatsRD ProcessRequest(APIRequest<CTWEventRP> pRequest)
        {
            var rd = new GetPanicbuyingEventRankingStatsRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            T_CTW_LEventBLL bllLEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            DataSet ds = bllLEvent.GetCTW_PanicbuyingEventRankingStats(para.CTWEventId);
            if (ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    rd.OrderMoneyRankList = DataTableToObject.ConvertToList<OrderMoneyRank>(ds.Tables[0]);

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    rd.OrderCountRankList = DataTableToObject.ConvertToList<OrderCountRank>(ds.Tables[1]);
                }
            }
            return rd;
        }
    }
}