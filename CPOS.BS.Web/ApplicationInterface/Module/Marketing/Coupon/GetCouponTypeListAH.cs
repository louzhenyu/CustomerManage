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
            bool IsEffective = false;
            if (para.IsEffective != null)
                IsEffective = para.IsEffective.Value;
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            if (!string.IsNullOrEmpty(para.CouponTypeName))
                complexCondition.Add(new LikeCondition() { FieldName = "CouponTypeName", Value = "%" + para.CouponTypeName + "%" });
            if (!string.IsNullOrEmpty(para.ParValue))
                complexCondition.Add(new LikeCondition() { FieldName = "ParValue", Value = para.ParValue.ToString() });
            //过滤用完的 #bug 2838 (不改了 改从前端改)
            //complexCondition.Add(new DirectCondition(" ((IssuedQty-IsVoucher)>0) "));

            if (para.SurplusCount != null && para.SurplusCount == 1)
            {
                complexCondition.Add(new DirectCondition(" IssuedQty-IsVoucher>0 ")); //剩余张数大于o
                complexCondition.Add(new DirectCondition(" (EndTime>='" + DateTime.Now + "' OR (BeginTime IS NULL AND EndTime IS NULL AND ServiceLife>0))")); //剩余张数大于o  结束时间小于当前时间也要过滤掉
            }



            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });



            var tempList = couponTypeBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.CouponTypeList = tempList.Entities.Select(t => new CouponTypeInfo()
            {
                CouponTypeID = t.CouponTypeID,
                CouponTypeName = t.CouponTypeName,
                ParValue = t.ParValue,
                IssuedQty = t.IssuedQty,
                SurplusQty = t.IssuedQty - (t.IsVoucher ?? 0),
                ValidityPeriod = t.BeginTime == null ? ("领取后" + (t.ServiceLife == 0 ? "1天内有效" : t.ServiceLife.ToString() + "天内有效")) : (t.BeginTime.Value.ToString("yyyy-MM-dd") + "至" + t.EndTime.Value.ToString("yyyy-MM-dd")),
                BeginTime = t.BeginTime,
                EndTime = t.EndTime,
                BeginTimeDate = t.BeginTime == null ? "" : t.BeginTime.Value.ToString("yyyy年MM月dd日"),
                EndTimeDate = t.EndTime == null ? "" : t.EndTime.Value.ToString("yyyy年MM月dd日"),
                ServiceLife = t.ServiceLife
            }).ToArray();
            return rd;
        }
    }
}