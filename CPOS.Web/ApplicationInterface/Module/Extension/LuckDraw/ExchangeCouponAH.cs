using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Extension.LuckDraw.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.LuckDraw
{
    public class ExchangeCouponAH : BaseActionHandler<ExchangeCouponRP, EmptyResponseData>
    {

        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<ExchangeCouponRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var pointMarkBLL = new X_VipPointMarkBLL(CurrentUserInfo);
            var pointMarkDetailBLL = new X_VipPointMarkDetailBLL(CurrentUserInfo);

            var activityPrizesBLL = new X_ActivityPrizesBLL(CurrentUserInfo);
            var activityJoinBLL = new X_ActivityJoinBLL(CurrentUserInfo);

            var couponBLL = new CouponBLL(CurrentUserInfo);
            var vipCouponMappingBLL = new VipCouponMappingBLL(CurrentUserInfo);
            var couponTypeBLL = new CouponTypeBLL(CurrentUserInfo);


            var activityPrizeInfo = activityPrizesBLL.GetByID(para.PrizesID);
            if (activityPrizeInfo != null)
            {
                //扣减积点
                var pointMarkInfo = pointMarkBLL.QueryByEntity(new X_VipPointMarkEntity() { VipID = CurrentUserInfo.UserID }, null).FirstOrDefault();
                if (pointMarkInfo != null)
                {
                    pointMarkInfo.Count = pointMarkInfo.Count - activityPrizeInfo.UsePoint;
                    pointMarkBLL.Update(pointMarkInfo);

                    X_VipPointMarkDetailEntity detail = new X_VipPointMarkDetailEntity()
                    {
                        VipID = CurrentUserInfo.UserID,
                        Count = -activityPrizeInfo.UsePoint,
                        Source = 2,//抽奖兑换
                        CustomerID = CurrentUserInfo.ClientID
                    };
                    pointMarkDetailBLL.Create(detail);
                }
                //更改参与信息
                var activityJoinInfo = activityJoinBLL.QueryByEntity(new X_ActivityJoinEntity() { VipID = CurrentUserInfo.UserID }, new OrderBy[] { new OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
                if (activityJoinInfo != null)
                {
                    activityJoinInfo.IsExchange = 1;
                    activityJoinBLL.Update(activityJoinInfo);
                }
                //兑换优惠券
                string couponID = couponBLL.GetUsableCouponID(activityPrizeInfo.CouponTypeID.ToString());
                VipCouponMappingEntity vipCouponMappingInfo = new VipCouponMappingEntity()
                {
                    VIPID=CurrentUserInfo.UserID,
                    CouponID=couponID
                };
                vipCouponMappingBLL.Create(vipCouponMappingInfo);
                //修改券产品个数
                var couponTypeInfo = couponTypeBLL.GetByID(activityPrizeInfo.CouponTypeID);
                if (couponTypeInfo != null)
                {
                    couponTypeInfo.IsVoucher = couponTypeInfo.IsVoucher + 1;
                    couponTypeBLL.Update(couponTypeInfo);
                }
            }
            return rd;
        }
    }
}