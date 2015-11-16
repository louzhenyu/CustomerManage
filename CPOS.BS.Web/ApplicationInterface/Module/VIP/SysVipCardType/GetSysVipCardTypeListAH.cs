using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.SysVipCardType
{
    public class GetSysVipCardTypeListAH : BaseActionHandler<EmptyRequestParameter, GetSysVipCardTypeListRD>
    {
        protected override GetSysVipCardTypeListRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetSysVipCardTypeListRD();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //商户条件
            complexCondition.Add(new EqualsCondition() { FieldName = "vct.CustomerID", Value = loggingSessionInfo.ClientID });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "vct.VipCardLevel", Direction = OrderByDirections.Desc });
            lstOrder.Add(new OrderBy() { FieldName = "vct.CreateTime", Direction = OrderByDirections.Desc });

            var tempList = sysVipCardTypeBLL.GetVipCardList(complexCondition.ToArray(), lstOrder.ToArray());

            rd.SysVipCardTypeList = tempList.Select(t => new SysVipCardTypeInfo()
            {
                VipCardTypeID = t.VipCardTypeID.Value,
                VipCardTypeCode = t.VipCardTypeCode,
                VipCardTypeName = t.VipCardTypeName,
                CardDiscount = t.CardDiscount,
                PointsMultiple = t.PointsMultiple,
                ChargeFull = t.ChargeFull,
                ChargeGive = t.ChargeGive
            }).ToList();

            return rd;
        }
    }
}