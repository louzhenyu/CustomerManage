using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.RetailTrader.Request;
using JIT.CPOS.DTO.Module.RetailTrader.Response;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.SuperRetailTrader
{
    public class GetSuperRetailTraderListReport : BaseActionHandler<GetTSuperRetailTraderConfigRP, GetSuperRetailTraderListRD>
    {
        protected override GetSuperRetailTraderListRD ProcessRequest(DTO.Base.APIRequest<GetTSuperRetailTraderConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetSuperRetailTraderListRD();
            T_SuperRetailTraderBLL bll = new T_SuperRetailTraderBLL(loggingSessionInfo);

            List<IWhereCondition> pWhereConditions = new List<IWhereCondition>() {
            new EqualsCondition() { FieldName = "a.CustomerId", Value = loggingSessionInfo .ClientID},
            new EqualsCondition() { FieldName = "a.IsDelete", Value =0},
            new EqualsCondition(){ FieldName="a.Status", Value="10"}
            };

            if (!String.IsNullOrEmpty(parameter.SuperRetailTraderID))
            {
                pWhereConditions.Add(new EqualsCondition() { FieldName = "a.HigheSuperRetailTraderID", Value = parameter.SuperRetailTraderID });
            }


            PagedQueryResult<T_SuperRetailTraderEntity> models = bll.FindListByCustomerId(pWhereConditions.ToArray(), null, parameter.PageIndex, parameter.PageSize, loggingSessionInfo.ClientID);

            rd.lst = models.Entities.Select(m => new SuperRetailTraderListInfo()
            {
                JoinTime = m.JoinTime,
                NumberOffline = 10,
                OrderCount = "20",
                SuperRetailTraderFrom = m.SuperRetailTraderFrom,
                SuperRetailTraderName = m.SuperRetailTraderName,
                SuperRetailTraderPhone = m.SuperRetailTraderPhone,
                WithdrawCount = "30",
                WithdrawTotalMoney = "50"
            }).ToList();

            rd.TotalCount = models.RowCount;
            rd.TotalPages = models.PageCount;
            return rd;
        }
    }
}