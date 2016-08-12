using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 生成收款订单
    /// </summary>
    public class SetReceiveAmountOrderAH : Base.BaseActionHandler<SetReceiveAmountOrderRP,SetReceiveAmountOrderRD>
    {
        protected override SetReceiveAmountOrderRD ProcessRequest(APIRequest<SetReceiveAmountOrderRP> pRequest)
        {
            SetReceiveAmountOrderRP rp = pRequest.Parameters;
            SetReceiveAmountOrderRD rd = new SetReceiveAmountOrderRD();

            var receiveAmountOrderBll = new ReceiveAmountOrderBLL(CurrentUserInfo);
            var sysVipCardGradeBLL = new SysVipCardGradeBLL(CurrentUserInfo);    //获取折扣表
            var vipBLL = new VipBLL(CurrentUserInfo);
            var unitBLL = new t_unitBLL(CurrentUserInfo);
            var vipInfo = vipBLL.GetByID(pRequest.UserID); //获取会员信息
            if (vipInfo == null)
            {
                throw new APIException("没有会员信息") { ErrorCode = 101 };
            }
            var unitInfo = unitBLL.GetByID(rp.UnitId);
            if (unitInfo == null)
            {
                throw new APIException("没有服务门店信息") { ErrorCode = 101 };
            }
            decimal discountAmount = 0;         //抵扣金额汇总
            decimal couponAmount = 0; //优惠券抵用金额
            decimal endAmount = rp.VipEndAmount; //余额
            decimal integralAmount = rp.Integral;
            //获取订单号
            TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(CurrentUserInfo);
            string orderNo = serviceUnitExpand.GetUnitOrderNo();
            //折扣
            decimal discount = 1; 
            if(rp.VipDiscount > 0)
            discount = sysVipCardGradeBLL.GetVipDiscount() / 10;//会员折扣

            decimal tempAmount = Math.Round((discount == 0 ? 1 : discount) * rp.TotalAmount, 2 ,MidpointRounding.AwayFromZero);
            Guid orderId = Guid.NewGuid();

            //积分处理
            if (rp.IntegralFlag == 1)
            {
                //加入折扣金额
                discountAmount = discountAmount + rp.IntegralAmount;   
            }
            else
            {
                integralAmount = 0;
            }

            //使用优惠券
            if (rp.CouponFlag == 1)
            {
                #region 判断优惠券是否是该会员的

                var vipcouponMappingBll = new VipCouponMappingBLL(CurrentUserInfo);

                var vipcouponmappingList = vipcouponMappingBll.QueryByEntity(new VipCouponMappingEntity()
                {
                    VIPID = vipInfo.VIPID,
                    CouponID = rp.CouponId
                }, null);

                if (vipcouponmappingList == null || vipcouponmappingList.Length == 0)
                {
                    throw new APIException("此张优惠券不是该会员的") { ErrorCode = 103 };
                }

                #endregion

                #region 判断优惠券是否有效

                var couponBll = new CouponBLL(CurrentUserInfo);

                var couponEntity = couponBll.GetByID(rp.CouponId);

                if (couponEntity == null)
                {
                    throw new APIException("无效的优惠券") { ErrorCode = 103 };
                }

                if (couponEntity.Status == 1)
                {
                    throw new APIException("优惠券已使用") { ErrorCode = 103 };
                }

                if (couponEntity.EndDate < DateTime.Now)
                {
                    throw new APIException("优惠券已过期") { ErrorCode = 103 };
                }
                var couponTypeBll = new CouponTypeBLL(CurrentUserInfo);
                var couponTypeEntity = couponTypeBll.GetByID(couponEntity.CouponTypeID);

                if (couponTypeEntity == null)
                {
                    throw new APIException("无效的优惠券类型") { ErrorCode = 103 };
                }

                #endregion

                discountAmount = discountAmount + couponTypeEntity.ParValue ?? 0;
                couponAmount = couponTypeEntity.ParValue ?? 0;

                //更新使用记录
                var couponUseBll = new CouponUseBLL(CurrentUserInfo);
                var couponUseEntity = new CouponUseEntity()
                {
                    CouponUseID = Guid.NewGuid(),
                    CouponID = rp.CouponId,
                    VipID = vipInfo.VIPID,
                    UnitID = rp.UnitId,
                    OrderID = orderId.ToString(),
                    Comment = "商城使用电子券",
                    CustomerID = CurrentUserInfo.ClientID,
                    CreateBy = CurrentUserInfo.UserID,
                    CreateTime = DateTime.Now,
                    LastUpdateBy = CurrentUserInfo.UserID,
                    LastUpdateTime = DateTime.Now,
                    IsDelete = 0
                };
                couponUseBll.Create(couponUseEntity);

                //更新CouponType数量
                var conponTypeBll = new CouponTypeBLL(CurrentUserInfo);
                var conponTypeEntity = conponTypeBll.QueryByEntity(new CouponTypeEntity() { CouponTypeID = new Guid(couponEntity.CouponTypeID), CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                conponTypeEntity.IsVoucher += 1;
                conponTypeBll.Update(conponTypeEntity);

                //停用该优惠券
                couponEntity.Status = 1;
                couponBll.Update(couponEntity);
            }

            //使用余额
            if (rp.VipEndAmountFlag == 1)
            {
                var vipAmountBll = new VipAmountBLL(CurrentUserInfo);
                var vipAmountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);

                var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                if (vipAmountEntity != null)
                {
                    //判断该会员账户是否被冻结
                    if (vipAmountEntity.IsLocking == 1)
                        throw new APIException("账户已被冻结，请先解冻") { ErrorCode = 103 };

                    //判断该会员的账户余额是否大于本次使用的余额
                    if (vipAmountEntity.EndAmount < rp.VipEndAmount)
                        throw new APIException(string.Format("账户余额不足，当前余额为【{0}】", vipAmountEntity.EndAmount)) { ErrorCode = 103 };

                    //所剩余额大于商品价格，扣除余额的数量为商品价格
                    if (tempAmount < rp.VipEndAmount)
                    {
                        rp.VipEndAmount = endAmount = Convert.ToDecimal(tempAmount);
                    }
                }
            }
            //不使用余额，余额为0
            else
            {
                endAmount = 0;
            }
            //实付金额
            decimal transAmount = tempAmount - discountAmount;
            //支付状态
            string payStatus = "0";
            //支付时间
            DateTime? PayDatetTime = null;

            //实付金额全是由余额支付，支付状态和支付时间全部更新，积分扣减、余额扣减、优惠券使用
            if (transAmount == endAmount || transAmount == 0)
            {
                payStatus = "10";
                PayDatetTime = DateTime.Now;
                //处理积分抵扣
                if(rp.IntegralFlag == 1)
                {
                    var vipIntegralBll = new VipIntegralBLL(CurrentUserInfo);
                    string sourceId = "20"; //积分抵扣
                    var IntegralDetail = new VipIntegralDetailEntity()
                    {
                        Integral = -Convert.ToInt32(rp.Integral),
                        IntegralSourceID = sourceId,
                        ObjectId = orderId.ToString()
                    };
                    if (IntegralDetail.Integral != 0)
                    {
                        //变动前积分
                        string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                        //变动积分
                        string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                        var vipIntegralDetailId = vipIntegralBll.AddIntegral(ref vipInfo, unitInfo, IntegralDetail, CurrentUserInfo);
                        //发送微信积分变动通知模板消息
                        if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                        {
                            var CommonBLL = new CommonBLL();
                            CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, CurrentUserInfo);
                        }
                    }
                }
                //处理余额抵扣
                if (rp.VipEndAmountFlag == 1)
                {
                    var vipAmountBll = new VipAmountBLL(CurrentUserInfo);
                    var vipAmountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);

                    var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                    if (vipAmountEntity != null)
                    {
                        var detailInfo = new VipAmountDetailEntity()
                        {
                            Amount = -rp.VipEndAmount,
                            AmountSourceId = "1",
                            ObjectId = orderId.ToString()
                        };
                        var vipAmountDetailId = vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipAmountEntity, detailInfo, CurrentUserInfo);
                        if (!string.IsNullOrWhiteSpace(vipAmountDetailId))
                        {//发送微信账户余额变动模板消息
                            var CommonBLL = new CommonBLL();
                            CommonBLL.BalanceChangedMessage(orderNo, vipAmountEntity, detailInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, CurrentUserInfo);
                        }
                    }
                }
                
            }
            //收款订单
            ReceiveAmountOrderEntity receiveAmountOrderEntity = new ReceiveAmountOrderEntity() 
            { 
                OrderId = orderId,
                OrderNo = orderNo,
                VipId = vipInfo.VIPID,
                ServiceUnitId = unitInfo.unit_id,
                ServiceUserId = rp.EmployeeID,
                TotalAmount = rp.TotalAmount,
                VipDiscount = discount * 100,
                TransAmount = transAmount,
                PayPoints = integralAmount,
                AmountFromPayPoints = rp.IntegralAmount,
                CouponUsePay = couponAmount,
                AmountAcctPay = endAmount,
                PayStatus = payStatus,
                TimeStamp = rp.TimeStamp,
                PayDatetTime = PayDatetTime,
                CustomerId = CurrentUserInfo.ClientID
            };
            receiveAmountOrderBll.Create(receiveAmountOrderEntity);
            rd.orderId = orderId.ToString();
            return rd;
        }
    }
}