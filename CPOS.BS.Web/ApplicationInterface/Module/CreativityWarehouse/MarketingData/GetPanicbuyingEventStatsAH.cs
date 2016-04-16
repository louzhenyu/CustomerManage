using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingData
{
    public class GetPanicbuyingEventStatsAH : BaseActionHandler<GetPanicbuyingEventStatsRP, GetPanicbuyingEventStatsRD>
    {
        /// <summary>
        /// 获取带促销活动的创意仓库的统计
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetPanicbuyingEventStatsRD ProcessRequest(APIRequest<GetPanicbuyingEventStatsRP> pRequest)
        {
            var rd = new GetPanicbuyingEventStatsRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            T_CTW_LEventBLL bllLEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            //获取带促销活动的创意仓库的统计
            DataSet dsPanicbuyingEventStats = bllLEvent.GetPanicbuyingEventStats(para.CTWEventId);
            if (dsPanicbuyingEventStats != null && dsPanicbuyingEventStats.Tables.Count > 0 && dsPanicbuyingEventStats.Tables[0].Rows.Count > 0)
            {
                rd.PanicbuyingEventStatsInfo = DataTableToObject.ConvertToObject<PanicbuyingEventStatsInfo>(dsPanicbuyingEventStats.Tables[0].Rows[0]);
            }
            return rd;
        }
    }
}