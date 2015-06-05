using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Request;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Coupon
{
    public class GetCouponTypeListAH : BaseActionHandler<GetCouponTypeListRP, GetCouponTypeListRD>
    {

        protected override GetCouponTypeListRD ProcessRequest(DTO.Base.APIRequest<GetCouponTypeListRP> pRequest)
        {
            var rd = new GetCouponTypeListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponTypeBLL = new CouponTypeBLL(loggingSessionInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            if (!string.IsNullOrEmpty(para.CouponTypeName))
                complexCondition.Add(new LikeCondition() { FieldName = "CouponTypeName", Value = para.CouponTypeName + "%" });
            if (!string.IsNullOrEmpty(para.ParValue))
                complexCondition.Add(new LikeCondition() { FieldName = "ParValue", Value = para.ParValue.ToString() });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            var tempList = couponTypeBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize,para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.CouponTypeList = tempList.Entities.Select(t => new CouponTypeInfo()
            {
               CouponTypeID=t.CouponTypeID,
               CouponTypeName=t.CouponTypeName,
               ParValue=t.ParValue,
               IssuedQty=t.IssuedQty,
               SurplusQty=t.IssuedQty-t.IsVoucher,
               ValidityPeriod = t.BeginTime ==null? (t.ServiceLife + "天") : (t.BeginTime.Value.ToString("yyyy-MM-dd") + "至" + t.EndTime.Value.ToString("yyyy-MM-dd")),
               BeginTime=t.BeginTime,
               EndTime=t.EndTime,
               ServiceLife=t.ServiceLife
            }).ToArray();
            return rd;
        }
    }
}