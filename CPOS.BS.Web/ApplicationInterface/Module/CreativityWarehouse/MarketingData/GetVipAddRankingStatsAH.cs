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
    public class GetVipAddRankingStatsAH : BaseActionHandler<GetVipAddRankingStatsRP, GetVipAddRankingStatsRD>
    {
        /// <summary>
        /// 获取游戏与促销会员增长排行
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetVipAddRankingStatsRD ProcessRequest(APIRequest<GetVipAddRankingStatsRP> pRequest)
        {
            var rd = new GetVipAddRankingStatsRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            T_CTW_LEventBLL bllLEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            //获取游戏与促销会员增长排行
            DataSet dsVipAddRankingStats = bllLEvent.GetVipAddRankingStats(para.CTWEventId);
            if (dsVipAddRankingStats != null && dsVipAddRankingStats.Tables.Count > 0)
            {
                if (dsVipAddRankingStats.Tables[0].Rows.Count > 0)
                    rd.GameVipAddRankingList = DataTableToObject.ConvertToList<GameVipAddRankingInfo>(dsVipAddRankingStats.Tables[0]);
                if (dsVipAddRankingStats.Tables[1].Rows.Count > 0)
                    rd.PromotionVipAddRankingList = DataTableToObject.ConvertToList<PromotionVipAddRankingInfo>(dsVipAddRankingStats.Tables[1]);

            }
            return rd;
        }
    }
}