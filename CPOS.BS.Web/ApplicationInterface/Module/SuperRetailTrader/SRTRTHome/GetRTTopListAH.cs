using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.SRTRTHome
{
    public class GetRTTopListAH : BaseActionHandler<GetRTTopListRP, GetRTTopListRD>
    {
        /// <summary>
        /// 获取分销商排名 列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetRTTopListRD ProcessRequest(DTO.Base.APIRequest<GetRTTopListRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetRTTopListRD();
            R_SRT_RTTopBLL bll = new R_SRT_RTTopBLL(loggingSessionInfo);
            string sortorder = string.Empty;


            List<R_SRT_RTTopEntity> models = bll.GetRsrtrtTopList(loggingSessionInfo.ClientID, parameter.BusiType);

            rd.rsrtrttopinfo = models.Select(m => new RSRTRTTopInfo()
            {
                ID = m.ID,
                Idx = m.Idx,
                SalesAmount = m.SalesAmount == null ? 0 : m.SalesAmount,
                SuperRetailTraderName = m.SuperRetailTraderName,
                dataTime = Convert.ToDateTime(m.SuperRetailJoinTime).ToString("yyyy-MM-dd HH:mm:ss"),
                SuperRetailTraderID = m.SuperRetailTraderID,
                AddRTCount = m.AddRTCount == null ? 0 : m.AddRTCount
                 
            }).ToList();
            return rd;
        }
    }
}