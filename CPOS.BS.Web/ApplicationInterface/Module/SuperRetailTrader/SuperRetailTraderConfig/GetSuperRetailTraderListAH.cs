using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.RetailTrader.Response;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.SuperRetailTraderConfig
{
    /// <summary>
    /// 分销商列表 {查看下线信息，订单总数，提现次数，提现总金额} 默认要过滤掉成为分销商之前的信息过滤掉
    /// </summary>
    public class GetSuperRetailTraderListAH : BaseActionHandler<GetTSuperRetailTraderConfigRP, GetSuperRetailTraderListRD>
    {
        protected override GetSuperRetailTraderListRD ProcessRequest(DTO.Base.APIRequest<GetTSuperRetailTraderConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetSuperRetailTraderListRD();
            T_SuperRetailTraderBLL bll = new T_SuperRetailTraderBLL(loggingSessionInfo);
            List<IWhereCondition> complexCondition = new List<IWhereCondition>();

            complexCondition.Add(new EqualsCondition() { FieldName = "a.Status", Value = "10" });
            complexCondition.Add(new EqualsCondition() { FieldName = "a.IsDelete", Value = 0 });

            if (!String.IsNullOrEmpty(parameter.SuperRetailTraderFrom))
                complexCondition.Add(new EqualsCondition() { FieldName = "a.SuperRetailTraderFrom", Value = parameter.SuperRetailTraderFrom });

            if (!String.IsNullOrEmpty(parameter.SuperRetailTraderName))
                complexCondition.Add(new LikeCondition() { FieldName = "a.SuperRetailTraderName", Value = "%" + parameter.SuperRetailTraderName + "%" });

            if (!String.IsNullOrEmpty(parameter.JoinSatrtTime))
                complexCondition.Add(new DirectCondition("a.JoinTime>= '" + parameter.JoinSatrtTime + "' "));

            if (!String.IsNullOrEmpty(parameter.JoinEndTime))
                complexCondition.Add(new DirectCondition("a.JoinTime<= '" + parameter.JoinEndTime + "' "));

            if (!String.IsNullOrEmpty(parameter.SuperRetailTraderID.ToString()))
            {
                if (parameter.IsFlag == 1)  //特殊处理方式
                {
                    complexCondition.Add(new DirectCondition("a.JoinTime< '" + DateTime.Now + "' AND  a.JoinTime>='" + DateTime.Now.AddDays(-31) + "'"));
                }
                complexCondition.Add(new EqualsCondition() { FieldName = "a.HigheSuperRetailTraderID", Value = parameter.SuperRetailTraderID });
            }
            else
            {
                complexCondition.Add(new EqualsCondition() { FieldName = "a.CustomerId", Value = loggingSessionInfo.ClientID });
            }

            PagedQueryResult<T_SuperRetailTraderEntity> models = bll.FindListByCustomerId(complexCondition.ToArray(), null, parameter.PageIndex, parameter.PageSize, loggingSessionInfo.ClientID);
            rd.SuperRetailTraderList = models.Entities.Select(m => new SuperRetailTraderListInfo()
             {
                 JoinTime = Convert.ToDateTime(m.JoinTime).ToString("yyyy-MM-dd HH:mm:ss"),
                 NumberOffline = m.NumberOffline,
                 OrderCount = m.OrderCount,
                 SuperRetailTraderFrom = m.SuperRetailTraderFrom,
                 SuperRetailTraderName = m.SuperRetailTraderName,
                 SuperRetailTraderPhone = m.SuperRetailTraderPhone,
                 WithdrawCount = m.WithdrawCount,
                 WithdrawTotalMoney = m.WithdrawTotalMoney,
                 SuperRetailTraderID = m.SuperRetailTraderID
             }).ToList();

            rd.TotalCount = models.RowCount;
            rd.TotalPages = models.PageCount;
            return rd;
        }
    }
}