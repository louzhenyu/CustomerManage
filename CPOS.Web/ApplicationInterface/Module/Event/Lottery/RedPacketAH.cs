using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Event.Lottery.Request;
using JIT.CPOS.DTO.Module.Event.Lottery.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Event.Lottery
{
    public class RedPacketAH : BaseActionHandler<LotteryRP, LotteryRD>
    {

        protected override LotteryRD ProcessRequest(DTO.Base.APIRequest<LotteryRP> pRequest)
        {
            var rd = new LotteryRD();//返回值

            LLotteryLogBLL bllLottery = new LLotteryLogBLL(this.CurrentUserInfo);
            LEventsBLL bll = new LEventsBLL(this.CurrentUserInfo);
            t_award_poolBLL bllAward = new t_award_poolBLL(this.CurrentUserInfo);
            LPrizesBLL bllPrize = new LPrizesBLL(this.CurrentUserInfo);

            LLotteryLogEntity lotteryEntityOld = null;
            LLotteryLogEntity lotteryEntityNew = null;



            LEventsEntity eventEntity = bll.QueryByEntity(new LEventsEntity() { EventID = pRequest.Parameters.EventId,EventStatus=20 }, null).FirstOrDefault();// bll.GetByID(pRequest.Parameters.EventId);
            if(eventEntity==null)
            {
                
                rd.ResultMsg = "活动不存在或活动已经结束";
                return rd;
            }
            var entityPrize=bllPrize.QueryByEntity(new LPrizesEntity() { EventId = eventEntity.EventID }, null);
            if(entityPrize==null)
            {
                rd.ResultMsg = "红包活动未设置奖品";
                return rd;
            }
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrEmpty(pRequest.Parameters.EventId))
                complexCondition.Add(new EqualsCondition() { FieldName = " EventId", Value = pRequest.Parameters.EventId });
            complexCondition.Add(new DirectCondition("releasetime<='" + DateTime.Now + "' and balance=1 "));

            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = " releasetime", Direction = OrderByDirections.Desc });

            t_award_poolEntity awardEntity = null;

            awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();
            
            lotteryEntityOld = bllLottery.QueryByEntity(new LLotteryLogEntity() { EventId = pRequest.Parameters.EventId, VipId = pRequest.UserID }, null).FirstOrDefault();

            #region 判断是否有资格参与抽奖
            switch (eventEntity.PersonCount)
            {
                case 1://仅能参加一次抽奖
                    if (lotteryEntityOld == null)
                    {
                        awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();
                    }
                    else
                    {
                        rd.ResultMsg = "今天的抽奖机会已使用";
                        return rd;
                    }
                    break;
                case 2://每天一次
                    if ((lotteryEntityOld != null && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date < DateTime.Now.Date) || lotteryEntityOld == null)
                    {
                        awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();

                    }
                    else
                    {
                        rd.ResultMsg = "今天的抽奖机会已使用";
                        return rd;
                    }
                    break;
                case 3://每周一次
                    if ((lotteryEntityOld != null && DateTime.Now.Date < Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date) || lotteryEntityOld == null)
                    {
                        awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();

                    }
                    else
                    {
                        rd.ResultMsg = "本周的抽奖机会已使用";
                        return rd;
                    }
                    break;
                case 4://无限
                    awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();
                    break;

            }
            #endregion

            //抽奖记录
            lotteryEntityNew = new LLotteryLogEntity()
            {
                LogId = Guid.NewGuid().ToString(),
                VipId = pRequest.UserID,
                EventId = pRequest.Parameters.EventId,
                LotteryCount = (lotteryEntityOld == null ? 0 + 1 : lotteryEntityOld.LotteryCount + 1),
                CreateBy = this.CurrentUserInfo.UserID,
                CreateTime = DateTime.Now,
                IsDelete = 0

            };
            if (lotteryEntityOld == null)
                bllLottery.Create(lotteryEntityNew);
            else
            {
                bllLottery.Update(lotteryEntityNew);
            }


            if (awardEntity != null)
            {

                //var prize = bllPrize.GetCouponTypeIDByPrizeId(awardEntity.PrizesID);
                var prize = DataTableToObject.ConvertToList<LPrizesEntity>(bllPrize.GetCouponTypeIDByPrizeId(awardEntity.PrizesID).Tables[0]).FirstOrDefault();
                rd.PrizeId = prize.PrizesID;
                rd.PrizeName = prize.PrizeName;
                rd.ResultMsg = "中奖";
                //中奖记录
                LPrizeWinnerBLL bllPrizeWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
                LPrizeWinnerEntity entityPrizeWinner = null;

                entityPrizeWinner = new LPrizeWinnerEntity()
                {
                    PrizeWinnerID = Guid.NewGuid().ToString(),
                    VipID = pRequest.UserID,
                    PrizeID = awardEntity.PrizesID,
                    PrizeName = prize.PrizeName,
                    PrizePoolID = awardEntity.PrizePoolsID,
                    CreateBy = this.CurrentUserInfo.UserID,
                    CreateTime = DateTime.Now,
                    IsDelete = 0
                };

                bllPrizeWinner.Create(entityPrizeWinner);


                //PrizePools状态更新
                LPrizePoolsEntity entityPrizePools = new LPrizePoolsEntity();

                entityPrizePools.PrizePoolsID = awardEntity.PrizePoolsID;
                entityPrizePools.Status = 0;

                LPrizePoolsBLL bllPrizePools = new LPrizePoolsBLL(this.CurrentUserInfo);
                bllPrizePools.Update(entityPrizePools, false);

                //更新t_award_pool奖品池的状态
                awardEntity = new t_award_poolEntity()
                {
                    Balance = 0,
                    id = awardEntity.id
                };
                bllAward.Update(awardEntity, false);
                //扣除参与积分
                VipIntegralBLL bllVipIntegral = new VipIntegralBLL(this.CurrentUserInfo);
                var vipBLL = new VipBLL(this.CurrentUserInfo);
                var vipInfo = vipBLL.GetByID(pRequest.UserID);

                VipIntegralDetailEntity IntegralDetail = new VipIntegralDetailEntity()
                {
                    Integral = -eventEntity.PointsLottery,
                    IntegralSourceID = "22",
                    ObjectId = ""
                };
                bllVipIntegral.AddIntegral(vipInfo, null, IntegralDetail, null, this.CurrentUserInfo);

                if (prize.PrizeTypeId == "Point")
                {
                    #region 调用积分统一接口
                    var salesReturnBLL = new T_SalesReturnBLL(this.CurrentUserInfo);
                    var pTran = salesReturnBLL.GetTran();//事务
                    using (pTran.Connection)
                    {
                         IntegralDetail = new VipIntegralDetailEntity()
                             {
                                 Integral = prize.Point,
                                 IntegralSourceID = "22",
                                 ObjectId = ""
                             };
                         bllVipIntegral.AddIntegral(vipInfo, null, IntegralDetail, null, this.CurrentUserInfo);
                    }
                    #endregion
            

                }
                if (prize.PrizeTypeId == "Coupon")
                {
                    CouponEntity entityCoupon = null;
                    CouponBLL bllCoupon = new CouponBLL(this.CurrentUserInfo);
                    //lstOrder = new List<OrderBy> { };
                    //lstOrder.Add(new OrderBy() { FieldName = " createtime", Direction = OrderByDirections.Desc });
                    //entityCoupon = bllCoupon.QueryByEntity(new CouponEntity() { CouponTypeID = prize.CouponTypeID, Status = 0 }, lstOrder.ToArray()).FirstOrDefault();
                    //entityCoupon.Status = 0;
                    //bllCoupon.Update(entityCoupon, null);

                    entityCoupon = DataTableToObject.ConvertToList<CouponEntity>(bllCoupon.GetCouponIdByCouponTypeID(prize.CouponTypeID).Tables[0]).FirstOrDefault();

                    VipCouponMappingEntity entityVipCouponMapping = null;
                    VipCouponMappingBLL bllVipCouponMapping = new VipCouponMappingBLL(this.CurrentUserInfo);

                    entityVipCouponMapping = new VipCouponMappingEntity()
                    {
                        VipCouponMapping = Guid.NewGuid().ToString(),
                        VIPID = CurrentUserInfo.UserID,
                        CouponID = entityCoupon.CouponID
                    };
                    bllVipCouponMapping.Create(entityVipCouponMapping);



                }
            }
            else
            {
                rd.ResultMsg = "未中奖";
            }

            return rd;

        }

    }

}