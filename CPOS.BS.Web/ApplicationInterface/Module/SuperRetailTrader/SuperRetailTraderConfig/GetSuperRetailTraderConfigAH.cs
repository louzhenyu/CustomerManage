using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.SuperRetailTraderConfig
{
    /// <summary>
    /// 获取 分润 佣金 规则 信息 {方法思路：按照商户编号获取最新一条配置信息}
    /// </summary>
    public class GetTSuperRetailTraderConfigAH : BaseActionHandler<GetTSuperRetailTraderConfigRP, GetTSuperRetailTraderConfigRD>
    {
        protected override GetTSuperRetailTraderConfigRD ProcessRequest(DTO.Base.APIRequest<GetTSuperRetailTraderConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetTSuperRetailTraderConfigRD();
            T_SuperRetailTraderConfigBLL bll = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
            //按照时间获取最新一条有效记录
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>()
            {
                new EqualsCondition(){FieldName = "CustomerId", Value =loggingSessionInfo.ClientID},
                new EqualsCondition(){ FieldName="IsDelete", Value=0},
            };

            List<OrderBy> orderCondition = new List<OrderBy>() { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } };
            var model = bll.Query(lstWhereCondition.ToArray(), orderCondition.ToArray());

            if (model != null && model.Length > 0)
            {
                rd.Agreement = model[0].Agreement;
                rd.AgreementName = model[0].AgreementName;
                rd.CustomerProfit = model[0].CustomerProfit.ToString();
                rd.DistributionProfit = model[0].DistributionProfit.ToString();
                rd.Id = model[0].Id.ToString();
                rd.SkuCommission = model[0].SkuCommission.ToString();
                rd.Cost = model[0].Cost.ToString();
            }
            return rd;
        }
    }
}