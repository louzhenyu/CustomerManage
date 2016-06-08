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
using RedisOpenAPIClient.Models.CC;


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
            CouponTypeItemMappingBLL bllCouponTypeItemMapping = new CouponTypeItemMappingBLL(loggingSessionInfo);
            CouponTypeItemMappingEntity entityCouponTypeItemMapping = null;
            var redisCouponBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon.RedisCouponBLL();

            if (couponTypeBLL.ExistsCouponTypeName(para.CouponTypeName, loggingSessionInfo.ClientID) > 0)
            {
                throw new APIException(-1,"优惠券名称不可重复！");
            }
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
                        couponTypeEntity.CouponCategory = para.CouponCategory;
                        couponTypeEntity.ParValue = para.ParValue;
                        couponTypeEntity.IssuedQty = para.IssuedQty;
                        couponTypeEntity.IsVoucher = 0;
                        couponTypeEntity.UsableRange = para.UsableRange;
                        if (para.BeginTime != DateTime.MinValue)
                            couponTypeEntity.BeginTime = para.BeginTime;
                        if (para.EndTime != DateTime.MinValue)
                        {
                            para.EndTime = para.EndTime.AddHours(23).AddMinutes(59).AddSeconds(59);
                            couponTypeEntity.EndTime = para.EndTime;
                        }
                        couponTypeEntity.ServiceLife = para.ServiceLife;
                        couponTypeEntity.ConditionValue = para.ConditionValue;
                        couponTypeEntity.SuitableForStore = para.SuitableForStore;
                        couponTypeEntity.CustomerId = loggingSessionInfo.ClientID;

                        couponTypeEntity.IsRepeatable = 0;
                        couponTypeEntity.IsMixable = 0;
                        couponTypeEntity.ValidPeriod = 0;

                        couponTypeEntity.CouponTypeID = couponTypeBLL.CreateReturnID(couponTypeEntity, pTran);

                        redisCouponBLL.RedisSetSingleCoupon(new CC_Coupon()
                        {
                            CustomerId = couponTypeEntity.CustomerId,
                            CouponTypeId = couponTypeEntity.CouponTypeID.ToString(),
                            CouponTypeDesc = couponTypeEntity.CouponTypeDesc,
                            CouponTypeName=couponTypeEntity.CouponTypeName,
                            BeginTime = couponTypeEntity.BeginTime.ToString(),
                            EndTime = couponTypeEntity.EndTime.ToString(),
                            ServiceLife=couponTypeEntity.ServiceLife??0,
                            CouponLenth = couponTypeEntity.IssuedQty??0,
                            CouponCategory = couponTypeEntity.CouponCategory
                        });
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
                        if (!string.IsNullOrEmpty(para.BindType) && para.BindTypeIdList != null)
                        {
                            foreach (var objId in para.BindTypeIdList)
                            {
                                entityCouponTypeItemMapping = new CouponTypeItemMappingEntity()
                                {
                                    CouponTypeID = couponTypeEntity.CouponTypeID,
                                    BindType = para.BindType,
                                    ObjectId = objId.ObjectID
                                };
                                bllCouponTypeItemMapping.Create(entityCouponTypeItemMapping);
                            }
                        }
                    }
                    else//追加优惠券
                    {
                        couponTypeEntity = couponTypeBLL.GetByID(para.CouponTypeID);
                        couponTypeEntity.IssuedQty += para.IssuedQty;
                        couponTypeBLL.Update(couponTypeEntity);


                        redisCouponBLL.RedisSetSingleCoupon(new CC_Coupon()
                        {
                            CustomerId = couponTypeEntity.CustomerId,
                            CouponTypeId = couponTypeEntity.CouponTypeID.ToString(),
                            CouponTypeDesc = couponTypeEntity.CouponTypeDesc,
                            CouponTypeName = couponTypeEntity.CouponTypeName,
                            BeginTime = couponTypeEntity.BeginTime.ToString(),
                            EndTime = couponTypeEntity.EndTime.ToString(),
                            ServiceLife = couponTypeEntity.ServiceLife ?? 0,
                            CouponLenth = para.IssuedQty,
                            CouponCategory = couponTypeEntity.CouponCategory
                        });
                    }
         
                    pTran.Commit();//提交事物

                    //批量生成券
                    //if (couponTypeEntity != null)
                    //{
                    //    Hashtable htCouponInfo = new Hashtable();
                    //    htCouponInfo["CouponTypeID"] = couponTypeEntity.CouponTypeID;
                    //    htCouponInfo["CouponName"] = couponTypeEntity.CouponTypeName;
                    //    htCouponInfo["CouponDesc"] = couponTypeEntity.CouponTypeDesc;
                    //    if (couponTypeEntity.BeginTime != DateTime.MinValue)
                    //        htCouponInfo["BeginTime"] = couponTypeEntity.BeginTime;
                    //    else
                    //        htCouponInfo["BeginTime"] = null;
                    //    if (couponTypeEntity.EndTime != DateTime.MinValue)
                    //        htCouponInfo["EndTime"] = couponTypeEntity.EndTime;
                    //    else
                    //        htCouponInfo["EndTime"] = null;
                    //    htCouponInfo["IssuedQty"] = para.IssuedQty;
                    //    couponBLL.GenerateCoupon(htCouponInfo);
                    //}
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