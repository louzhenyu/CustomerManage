using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.SuperRetailTraderConfig
{
    public class SetSuperRetailTraderAH : BaseActionHandler<GetTSuperRetailTraderConfigRP, GetTSuperRetailTraderConfigRD>
    {
        /// <summary>
        /// 超级分销App 同意协议
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetTSuperRetailTraderConfigRD ProcessRequest(DTO.Base.APIRequest<GetTSuperRetailTraderConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;

            var rd = new GetTSuperRetailTraderConfigRD();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            T_SuperRetailTraderBLL bll = new T_SuperRetailTraderBLL(loggingSessionInfo);
            var _model = bll.GetByID(parameter.SuperRetailTraderID);  //获取单个实体
            if (_model == null)
            {
                throw new APIException("系统繁忙,请稍后重试！");
            }
            else
            {
                _model.Status = "10";
            }
            _model.JoinTime = DateTime.Now; //修改加盟时间
            bll.Update(_model);
            return rd;
        }
    }
}