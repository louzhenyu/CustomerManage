using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.SysVipCardType
{
    /// <summary>
    /// 卡类型列表
    /// </summary>
    public class GetSysVipCardTypeListAH : BaseActionHandler<GetSysVipCardTypeListRP, GetSysVipCardTypeListsRD>
    {
        protected override GetSysVipCardTypeListsRD ProcessRequest(DTO.Base.APIRequest<GetSysVipCardTypeListRP> pRequest)
        {
            var rd = new GetSysVipCardTypeListsRD();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var parameter = pRequest.Parameters;

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //商户条件
            complexCondition.Add(new EqualsCondition() { FieldName = "vct.CustomerID", Value = loggingSessionInfo.ClientID });
            complexCondition.Add(new EqualsCondition() { FieldName = "vct.Category", Value = parameter.Category });

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "vct.VipCardLevel", Direction = OrderByDirections.Desc });
            lstOrder.Add(new OrderBy() { FieldName = "vct.CreateTime", Direction = OrderByDirections.Desc });
           

            var tempList = sysVipCardTypeBLL.GetVipCardList(complexCondition.ToArray(), lstOrder.ToArray());

            rd.SysVipCardTypeList = tempList.Select(t => new SysVipCardTypeInfos()
            {
                VipCardTypeID = t.VipCardTypeID.Value,
                VipCardTypeCode = t.VipCardTypeCode,
                VipCardTypeName = t.VipCardTypeName,
                CardDiscount = t.CardDiscount,
                PointsMultiple = t.PointsMultiple,
                ChargeFull = t.ChargeFull,
                ChargeGive = t.ChargeGive,
                Price = t.Prices
            }).ToList();

            return rd;
        }
    }
}