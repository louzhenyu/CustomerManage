using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.VipGoldReport
{
    public class GetVipGoldHomeListAH : BaseActionHandler<GetVipGoldHomeListRP, GetVipGoldHomeListRD>
    {
        /// <summary>
        /// 会员金矿主页 图
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetVipGoldHomeListRD ProcessRequest(APIRequest<GetVipGoldHomeListRP> pRequest)
        {

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息

            var rd = new GetVipGoldHomeListRD();
            R_VipGoldHomeBLL vipgoldService = new R_VipGoldHomeBLL(loggingSessionInfo);
            //按照时间排序 查找最新一条记录
            var ds = vipgoldService.GetReceiveRecodsByCustomerId(loggingSessionInfo.ClientID);
            if (ds != null && ds.Tables.Count > 0)
            {
                var result = DataTableToObject.ConvertToList<R_VipGoldHomeEntity>(ds.Tables[0]).FirstOrDefault();
                if (result != null)
                {
                    rd.OnlineFansCount = result.OnlineFansCount;
                    rd.OnlineOnlyFansCount = result.OnlineOnlyFansCount;
                    rd.OnlineVipCount = result.OnlineVipCount;
                    rd.VipCount = result.VipCount;
                    rd.OfflineVipCount = result.OfflineVipCount;
                    rd.OnlineVipCountFor30DayOrder = result.OnlineVipCountFor30DayOrder;
                    rd.OnlineVipCountPerFor30DayOrder = result.OnlineVipCountPerFor30DayOrder;
                    rd.OnlineVipCountFor30DayOrderM2M = result.OnlineVipCountFor30DayOrderM2M;
                    rd.OnlineVipCountPerFor30DayOrderM2M = result.OnlineVipCountPerFor30DayOrderM2M;
                    rd.OnlineVipSalesFor30Day = result.OnlineVipSalesFor30Day;
                    rd.OnlineVipSalesFor30DayM2M = result.OnlineVipSalesFor30DayM2M;
                    rd.OnlineVipSalesPerFor30Day = result.OnlineVipSalesPerFor30Day;
                    rd.OnlineVipSalesPerFor30DayM2M = result.OnlineVipSalesPerFor30DayM2M;
                }
            }
            return rd;
        }
    }
}