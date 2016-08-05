using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 收款订单详情
    /// </summary>
    public class GetReceiveAmountOrderAH : Base.BaseActionHandler<GetReceiveAmountOrderRP,GetReceiveAmountOrderRD>
    {
        protected override GetReceiveAmountOrderRD ProcessRequest(APIRequest<GetReceiveAmountOrderRP> pRequest)
        {
            GetReceiveAmountOrderRP rp = pRequest.Parameters;
            GetReceiveAmountOrderRD rd = new GetReceiveAmountOrderRD();
            ReceiveAmountOrderBLL receiveAmountOrderBll = new ReceiveAmountOrderBLL(CurrentUserInfo);
            var couponUseBll = new CouponUseBLL(CurrentUserInfo);
            var couponBll = new CouponBLL(CurrentUserInfo);
            var paymentBll = new T_Payment_TypeBLL(CurrentUserInfo);


            //收款业务
            if (rp.Type == 1)
            {
                if (!string.IsNullOrEmpty(rp.TimeStamp))
                {
                    var receiveAmountOrderEntity = receiveAmountOrderBll.QueryByEntity(new ReceiveAmountOrderEntity() { TimeStamp = rp.TimeStamp }, null).FirstOrDefault();
                    if (receiveAmountOrderEntity != null)
                    {
                        rd.TotalAmount = receiveAmountOrderEntity.TotalAmount ?? 0;
                        rd.PayPoints = receiveAmountOrderEntity.PayPoints ?? 0;
                        rd.AmountAcctPay = receiveAmountOrderEntity.AmountAcctPay ?? 0;
                        rd.PayStatus = receiveAmountOrderEntity.PayStatus;
                        rd.VipId = receiveAmountOrderEntity.VipId;
                        rd.TransAmount = (receiveAmountOrderEntity.TransAmount ?? 0) - (receiveAmountOrderEntity.AmountAcctPay ?? 0);
                        rd.OrderNo = receiveAmountOrderEntity.OrderNo;
                        rd.vipDiscount = receiveAmountOrderEntity.VipDiscount ?? 100;
                        //优惠券使用
                        var couponUseEntity = couponUseBll.QueryByEntity(new CouponUseEntity() { OrderID = receiveAmountOrderEntity.OrderId.ToString() }, null).FirstOrDefault();
                        if (couponUseEntity != null)
                        {
                            var couponEntity = couponBll.GetByID(couponUseEntity.CouponID);
                            if (couponEntity != null)
                            {
                                rd.CouponName = couponEntity.CoupnName;
                                var couponTypeBll = new CouponTypeBLL(CurrentUserInfo);
                                var couponTypeEntity = couponTypeBll.GetByID(couponEntity.CouponTypeID);
                                rd.CouponAmount = couponTypeEntity.ParValue ?? 0;
                            }
                        }
                        //支付方式
                        var paymentEntity = paymentBll.GetByID(receiveAmountOrderEntity.PayTypeId);
                        if (paymentEntity != null)
                        {
                            rd.PayTypeName = paymentEntity.Payment_Type_Name;
                        }
                    }
                    else
                    {
                        throw new APIException("没有找到此订单") { ErrorCode = 200 };
                    }
                }
                else
                {
                    //收款订单
                    if (!string.IsNullOrEmpty(rp.OrderId))
                    {
                        var receiveAmountOrderEntity = receiveAmountOrderBll.GetByID(rp.OrderId);
                        rd.TotalAmount = receiveAmountOrderEntity.TotalAmount ?? 0;
                        rd.PayPoints = receiveAmountOrderEntity.PayPoints ?? 0;
                        rd.AmountAcctPay = receiveAmountOrderEntity.AmountAcctPay ?? 0;
                        rd.PayStatus = receiveAmountOrderEntity.PayStatus;
                        rd.TransAmount = (receiveAmountOrderEntity.TransAmount ?? 0) - (receiveAmountOrderEntity.AmountAcctPay ?? 0);
                        rd.OrderNo = receiveAmountOrderEntity.OrderNo;
                        rd.vipDiscount = receiveAmountOrderEntity.VipDiscount ?? 100;
                        //优惠券使用
                        var couponUseEntity = couponUseBll.QueryByEntity(new CouponUseEntity() { OrderID = rp.OrderId }, null).FirstOrDefault();
                        if (couponUseEntity != null)
                        {
                            var couponEntity = couponBll.GetByID(couponUseEntity.CouponID);
                            if (couponEntity != null)
                            {
                                rd.CouponName = couponEntity.CoupnName;
                                var couponTypeBll = new CouponTypeBLL(CurrentUserInfo);
                                var couponTypeEntity = couponTypeBll.GetByID(couponEntity.CouponTypeID);
                                rd.CouponAmount = couponTypeEntity.ParValue ?? 0;
                            }
                        }
                        //支付方式
                        var paymentEntity = paymentBll.GetByID(receiveAmountOrderEntity.PayTypeId);
                        if (paymentEntity != null)
                        {
                            rd.PayTypeName = paymentEntity.Payment_Type_Name;
                        }
                    }
                }
            }

            //充值业务
            if (rp.Type == 2)
            {
                var rechargeOrderBll = new RechargeOrderBLL(CurrentUserInfo);
                var rechargeOrderEntity = rechargeOrderBll.GetByID(rp.OrderId);
                if (rechargeOrderEntity != null)
                {
                    rd.PayStatus = rechargeOrderEntity.Status.ToString();
                    rd.VipId = rechargeOrderEntity.VipID;
                    rd.ReturnAmount = rechargeOrderEntity.ReturnAmount ?? 0;
                }
                else
                {
                    throw new APIException("没有找到此订单") { ErrorCode = 200 };
                }
            }
            //订单业务
            if (rp.Type == 3)
            {
                var inoutBll = new T_InoutBLL(CurrentUserInfo);
                var inoutEntity = inoutBll.GetByID(rp.OrderId);
                if (inoutEntity != null)
                {
                    rd.PayStatus = inoutEntity.Field1;
                    rd.VipId = inoutEntity.vip_no;
                }
                else
                {
                    throw new APIException("没有找到此订单") { ErrorCode = 200 };
                }
                
            }

            return rd;
        }
    }
}