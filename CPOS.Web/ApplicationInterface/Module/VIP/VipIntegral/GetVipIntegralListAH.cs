using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipIntegral
{
    /// <summary>
    /// 微信端 积分列表 {总积分|收入积分|支出积分}，已经和产品确认 列表 不需要区分积分来源
    /// </summary>
    public class GetVipIntegralListAH : BaseActionHandler<GetVipIntegralListRP, GetVipIntegralListRD>
    {
        protected override GetVipIntegralListRD ProcessRequest(APIRequest<GetVipIntegralListRP> pRequest)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            var rd = new GetVipIntegralListRD();
            var vipService = new VipBLL(loggingSessionInfo);
            var unitService = new UnitBLL(loggingSessionInfo);
            var parameter = pRequest.Parameters;

            List<IWhereCondition> complexCondition = new List<IWhereCondition>();
            complexCondition.Add(new EqualsCondition() { FieldName = "L.CustomerId", Value = loggingSessionInfo.ClientID });
            complexCondition.Add(new EqualsCondition() { FieldName = " L.VIPID", Value = loggingSessionInfo.UserID });

            if (String.IsNullOrEmpty(parameter.VipCardCode))  //默认当前会员卡编号
            {
                var entity = vipService.GetByID(pRequest.UserID);
                if (entity != null && !String.IsNullOrEmpty(entity.VipCode))
                {
                    parameter.VipCardCode = entity.VipCode;
                    complexCondition.Add(new EqualsCondition() { FieldName = " L.VipCardCode", Value = entity.VipCode });
                }
            }
            else
            {
                complexCondition.Add(new EqualsCondition() { FieldName = " L.VipCardCode", Value = parameter.VipCardCode });
            }

            if (parameter.IntegralType == 1)  //收入积分
            {
                complexCondition.Add(new DirectCondition() { Expression = "Integral > 0" });
            }
            else if (parameter.IntegralType == 2)  //支出积分
            {
                complexCondition.Add(new DirectCondition() { Expression = "Integral < 0" });
            }
            //分页获取个人积分列表
            PagedQueryResult<VipIntegralDetailEntity> IntegralList = unitService.GetMyIntegral(complexCondition.ToArray(), parameter.PageIndex, parameter.PageSize);
            if (IntegralList.Entities != null)
            {
                rd.IntegralList = IntegralList.Entities.Select(m => new IntegralInfo() { UpdateCount = m.UpdateCount, UpdateReason = m.UpdateReason, UpdateTime = m.UpdateTime.ToShortDateString() }).ToList();
            }

            #region 统计 总积分|收入积分|支出积分
            /// lst[0]=总积分
            /// lst[1]=收入积分
            /// lst[2]=支出积分
            decimal[] lst = unitService.GetMyTotalIntegral(loggingSessionInfo.ClientID, pRequest.UserID, parameter.VipCardCode);
            if (lst != null)
            {
                rd.ExpenditureAmount = -lst[2];    //转换为正数
                rd.IncomeAmount = lst[1];
            }
            #endregion

            rd.TotalPageCount = IntegralList.PageCount;
            rd.TotalCount = IntegralList.RowCount;
            return rd;
        }
    }
}