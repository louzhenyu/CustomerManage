using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.SuperRetailTraderConfig
{
    public class GetSuperRetailTraderConfigAH : BaseActionHandler<GetTSuperRetailTraderConfigRP, GetTSuperRetailTraderConfigRD>
    {
        /// <summary>
        /// 超级分销App 获取协议信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetTSuperRetailTraderConfigRD ProcessRequest(DTO.Base.APIRequest<GetTSuperRetailTraderConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var rd = new GetTSuperRetailTraderConfigRD();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            T_SuperRetailTraderConfigBLL bll = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
            var _model = bll.QueryByEntity(new T_SuperRetailTraderConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null);

            if (_model.Length > 0)
            {
                rd.Agreement = _model[0].Agreement;
                rd.AgreementName = _model[0].AgreementName;
            }
            return rd;
        }
    }
}