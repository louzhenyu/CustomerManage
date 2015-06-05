using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Coupon
{
    public class SetCouponAH : BaseActionHandler<SetCouponRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetCouponRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponTypeBLL = new CouponTypeBLL(loggingSessionInfo);
            var couponBLL = new CouponBLL(loggingSessionInfo);
            var couponTypeUnitMappingBLL = new CouponTypeUnitMappingBLL(loggingSessionInfo);
            var pTran = couponTypeBLL.GetTran();//事务 
            using (pTran.Connection)
            {
                try
                {
                    CouponTypeEntity couponTypeEntity = null;
                    //保存券产品表
                    if (string.IsNullOrEmpty(para.CouponTypeID))//生成优惠券
                    {
                        couponTypeEntity = new CouponTypeEntity();

                        couponTypeEntity.CouponTypeName = para.CouponTypeName;
                        couponTypeEntity.CouponTypeDesc = para.CouponTypeDesc;
                        couponTypeEntity.ParValue = para.ParValue;
                        couponTypeEntity.IssuedQty = para.IssuedQty;
                        couponTypeEntity.UsableRange = para.UsableRange;
                        if (para.BeginTime != DateTime.MinValue)
                            couponTypeEntity.BeginTime = para.BeginTime;
                        if (para.EndTime != DateTime.MinValue)
                            couponTypeEntity.EndTime = para.EndTime;
                        couponTypeEntity.ServiceLife = para.ServiceLife;
                        couponTypeEntity.ConditionValue = para.ConditionValue;
                        couponTypeEntity.SuitableForStore = para.SuitableForStore;
                        couponTypeEntity.CustomerId = loggingSessionInfo.ClientID;

                        couponTypeEntity.IsRepeatable = 0;
                        couponTypeEntity.IsMixable = 0;
                        couponTypeEntity.ValidPeriod = 0;

                        couponTypeEntity.CouponTypeID = couponTypeBLL.CreateReturnID(couponTypeEntity, pTran);
                        //保存适用门店
                        if (para.SuitableForStore == 2)
                        {
                            CouponTypeUnitMappingEntity mapping = null;
                            foreach (var obj in para.ObjectIDList)
                            {
                                mapping = new CouponTypeUnitMappingEntity()
                                {
                                    CouponTypeID = couponTypeEntity.CouponTypeID,
                                    ObjectID = obj.ObjectID,
                                    CustomerID = loggingSessionInfo.ClientID
                                };
                                couponTypeUnitMappingBLL.Create(mapping, pTran);
                            }
                        }
                    }
                    else//追加优惠券
                    {
                        couponTypeEntity = couponTypeBLL.GetByID(para.CouponTypeID);
                        couponTypeEntity.IssuedQty += para.IssuedQty;
                        couponTypeBLL.Update(couponTypeEntity);
                    }
                    pTran.Commit();//提交事物

                    //批量生成券
                    if (couponTypeEntity != null)
                    {
                        Hashtable htCouponInfo = new Hashtable();
                        htCouponInfo["CouponTypeID"] = couponTypeEntity.CouponTypeID;
                        htCouponInfo["CouponName"] = couponTypeEntity.CouponTypeName;
                        htCouponInfo["CouponDesc"] = couponTypeEntity.CouponTypeDesc;
                        if (couponTypeEntity.BeginTime != DateTime.MinValue)
                            htCouponInfo["BeginTime"] = couponTypeEntity.BeginTime;
                        else
                            htCouponInfo["BeginTime"] = null;
                        if (couponTypeEntity.EndTime != DateTime.MinValue)
                            htCouponInfo["EndTime"] = couponTypeEntity.EndTime;
                        else
                            htCouponInfo["EndTime"] = null;
                        htCouponInfo["IssuedQty"] = para.IssuedQty;
                        couponBLL.GenerateCoupon(htCouponInfo);
                    }
                }
                catch (Exception ex)
                {
                    pTran.Rollback();
                    throw new APIException(ex.Message);
                }
            }
            return rd;
        }
    }
}