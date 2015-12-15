/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:46
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.Event.Lottery.Response;
using JIT.CPOS.BS.BLL.WX;
namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class LPrizesBLL
    {

        #region 活动奖品列表
        /// <summary>
        /// 活动奖品列表
        /// </summary>
        /// <param name="EventID">活动ID</param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public GetResponseParams<LPrizesEntity> GetEventPrizes(string EventID, int Page, int PageSize)
        {
            #region 判断对象不能为空
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<LPrizesEntity>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "活动ID不能为空",
                };
            }
            if (PageSize == 0) PageSize = 15;
            #endregion

            GetResponseParams<LPrizesEntity> response = new GetResponseParams<LPrizesEntity>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "成功";
            try
            {
                #region 业务处理
                LPrizesEntity usersInfo = new LPrizesEntity();

                usersInfo.ICount = _currentDAO.GetEventPrizesCount(EventID);

                IList<LPrizesEntity> usersInfoList = new List<LPrizesEntity>();
                if (usersInfo.ICount > 0)
                {
                    DataSet ds = new DataSet();
                    ds = _currentDAO.GetEventPrizesList(EventID, Page, PageSize);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        usersInfoList = DataTableToObject.ConvertToList<LPrizesEntity>(ds.Tables[0]);
                    }
                }

                usersInfo.EntityList = usersInfoList;
                #endregion
                response.Params = usersInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "错误:" + ex.ToString();
                return response;
            }
        }
        #endregion

        #region Jermyn20131107 奖品品牌集合
        /// <summary>
        /// 根据品牌分组，获取信息
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public IList<LPrizesEntity> GetLPrizesGroupBrand(string EventId, string RoundId)
        {
            IList<LPrizesEntity> usersInfoList = new List<LPrizesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetLPrizesGroupBrand(EventId, RoundId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                usersInfoList = DataTableToObject.ConvertToList<LPrizesEntity>(ds.Tables[0]);
            }
            return usersInfoList;
        }
        #endregion


        #region 我的中奖名单
        /// <summary>
        /// 我的中奖名单
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public IList<LPrizesEntity> GetEventPrizesByVipId(string EventId, string VipId)
        {
            IList<LPrizesEntity> usersInfoList = new List<LPrizesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetEventPrizesByVipId(EventId, VipId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                usersInfoList = DataTableToObject.ConvertToList<LPrizesEntity>(ds.Tables[0]);
            }
            return usersInfoList;
        }
        #endregion

        #region 获取奖品列表
        /// <summary>
        /// 获取奖品列表
        /// </summary>
        public IList<LPrizesEntity> GetPrizesByEventId(string EventId)
        {
            IList<LPrizesEntity> usersInfoList = new List<LPrizesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetPrizesByEventId(EventId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                usersInfoList = DataTableToObject.ConvertToList<LPrizesEntity>(ds.Tables[0]);
            }
            return usersInfoList;
        }
        #endregion

        #region 获取奖品人员列表
        /// <summary>
        /// 获取奖品人员列表
        /// </summary>
        public IList<LPrizeWinnerEntity> GetPrizeWinnerByPrizeId(string PrizeId)
        {
            IList<LPrizeWinnerEntity> usersInfoList = new List<LPrizeWinnerEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetPrizeWinnerByPrizeId(PrizeId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                usersInfoList = DataTableToObject.ConvertToList<LPrizeWinnerEntity>(ds.Tables[0]);
            }
            return usersInfoList;
        }
        #endregion

        #region 获取轮次奖品列表
        /// <summary>
        /// 获取轮次奖品列表
        /// </summary>
        public IList<LPrizesEntity> GetEventRoundPrizesList(string EventId, string RoundId, int page, int pageSize)
        {
            IList<LPrizesEntity> usersInfoList = new List<LPrizesEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetEventRoundPrizesList(EventId, RoundId, page, pageSize);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                usersInfoList = DataTableToObject.ConvertToList<LPrizesEntity>(ds.Tables[0]);
            }
            return usersInfoList;
        }

        public int GetEventRoundPrizesCount(string EventId, string RoundId)
        {
            return _currentDAO.GetEventRoundPrizesCount(EventId, RoundId);
        }
        #endregion

        #region 保存奖品
        public int SavePrize(LPrizesEntity pEntity)
        {
            return this._currentDAO.SavePrize(pEntity);
        }
        #endregion
        #region 删除奖品

        public int DeletePrize(LPrizesEntity pEntity)
        {
            return this._currentDAO.DeletePrize(pEntity);
        }
        #endregion
        #region 追加奖品

        public int AppendPrize(LPrizesEntity pEntity)
        {
            return this._currentDAO.AppendPrize(pEntity);
        }
        #endregion
        public DataSet GetPirzeList(string strEventId)
        {
            return this._currentDAO.GetPirzeList(strEventId);
        }
        public DataSet GetCouponTypeIDByPrizeId(string strPrizesID)
        {
            return this._currentDAO.GetCouponTypeIDByPrizeId(strPrizesID);

        }
        /// <summary>
        /// 根据活动id返回未中奖的位置
        /// </summary>
        /// <param name="strEventID"></param>
        /// <returns></returns>
        public int GetLocationByEventID(string strPrizesID)
        {
            return this._currentDAO.GetLocationByEventID(strPrizesID);

        }
        #region 分享设置是否中奖
        /// <summary>
        /// 是否中奖
        /// </summary>
        /// <param name="strVipId"></param>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        public LotteryRD CheckIsWinnerForShare(string strVipId, string strEventId, string strType)
        {
            var rd = new LotteryRD();//返回值


            var bllShare = new LEventsShareBLL(this.CurrentUserInfo);
            var bllContactEvent = new ContactEventBLL(this.CurrentUserInfo);
            var bllPrize = new LPrizesBLL(this.CurrentUserInfo);
            var entityShare = new LEventsShareEntity();

            try
            {
                ContactEventEntity contactEvent = null;
                if (strEventId != "" || strEventId.Length>0)
                    contactEvent = bllContactEvent.QueryByEntity(new ContactEventEntity() { ShareEventId = strEventId, IsDelete = 0, Status = 2 }, null).FirstOrDefault();
                else
                    contactEvent = bllContactEvent.QueryByEntity(new ContactEventEntity() { ContactTypeCode=strType,IsDelete = 0, Status = 2 }, null).FirstOrDefault();

                if (contactEvent != null)
                {

                    var entityPrize = bllPrize.GetPrizesByEventId(contactEvent.ContactEventId.ToString()).FirstOrDefault();

                    var bllPrizePool = new LPrizePoolsBLL(CurrentUserInfo);
                    var entityPrizePool = new LPrizePoolsEntity();
                     entityPrizePool = bllPrizePool.QueryByEntity(new LPrizePoolsEntity() { EventId = contactEvent.ContactEventId.ToString(), PrizeID = entityPrize.PrizesID, Status = 1 }, null).FirstOrDefault();
                    if (entityPrizePool == null)
                    {
                        rd.ResultMsg = "奖品已发完！";
                        return rd;
                    }
                    ///改变奖品池状态
                    entityPrizePool.Status = 2;
                    bllPrizePool.Update(entityPrizePool);


                    if (entityPrize.PrizeTypeId == "Point")
                    {
                        #region 调用积分统一接口
                        var salesReturnBLL = new T_SalesReturnBLL(this.CurrentUserInfo);
                        //var pTran = salesReturnBLL.GetTran();//事务
                        //using (pTran.Connection)
                        //{
                            VipIntegralBLL bllVipIntegral = new VipIntegralBLL(this.CurrentUserInfo);
                            var vipBLL = new VipBLL(this.CurrentUserInfo);

                            var vipInfo = vipBLL.GetByID(strVipId);
                            var IntegralDetail = new VipIntegralDetailEntity()
                            {
                                Integral = entityPrize.Point,
                                IntegralSourceID = "22",
                                ObjectId = ""
                            };
                            //变动前积分
                            string OldIntegral=(vipInfo.Integration??0).ToString();
                            //变动积分
                            string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                            var vipIntegralDetailId = bllVipIntegral.AddIntegral(ref vipInfo, null,IntegralDetail, null, this.CurrentUserInfo);
                            //发送微信积分变动通知模板消息
                            if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                            {
                                var CommonBLL = new CommonBLL();
                                CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, this.CurrentUserInfo);
                            }

                        //}


                        #endregion
                    }
                    else if (entityPrize.PrizeTypeId == "Coupon")
                    {
                        List<OrderBy> lstOrder = new List<OrderBy> { };
                        CouponEntity entityCoupon = null;
                        CouponBLL bllCoupon = new CouponBLL(this.CurrentUserInfo);

                        //lstOrder = new List<OrderBy> { };
                        //lstOrder.Add(new OrderBy() { FieldName = " createtime", Direction = OrderByDirections.Desc });
                        ////entityCoupon = bllCoupon.QueryByEntity(new CouponEntity() { CouponTypeID = entityPrize.CouponTypeID, Status = 0 }, lstOrder.ToArray()).FirstOrDefault();
                        //entityCoupon.Status = 1;
                        //bllCoupon.Update(entityCoupon, null);
                        entityCoupon = DataTableToObject.ConvertToList<CouponEntity>(bllCoupon.GetCouponIdByCouponTypeID(entityPrize.CouponTypeID).Tables[0]).FirstOrDefault();

                        VipCouponMappingEntity entityVipCouponMapping = null;
                        VipCouponMappingBLL bllVipCouponMapping = new VipCouponMappingBLL(this.CurrentUserInfo);

                        entityVipCouponMapping = new VipCouponMappingEntity()
                        {
                            VipCouponMapping = Guid.NewGuid().ToString(),
                            VIPID = strVipId,
                            CouponID = entityCoupon.CouponID
                        };
                        bllVipCouponMapping.Create(entityVipCouponMapping);
                        //更新CouponType的IsVoucher(被领用数量)
                        CouponTypeBLL bllCouponType = new CouponTypeBLL(this.CurrentUserInfo);
                        CouponTypeEntity entityCouponType = bllCouponType.GetByID(entityPrize.CouponTypeID);
                        entityCouponType.IsVoucher = entityCouponType.IsVoucher + 1;
                        bllCouponType.Update(entityCouponType);
                        ///更新优惠券有效期
                        if (entityCouponType.ServiceLife!=null && entityCouponType.ServiceLife > 0)
                        {
                            entityCoupon.BeginDate = DateTime.Now.Date;
                            entityCoupon.EndDate = DateTime.Now.Date.AddDays((int)entityCouponType.ServiceLife).Date;

                            bllCoupon.Update(entityCoupon);

                        }
                    }
                    else if (entityPrize.PrizeTypeId == "Chance")
                    {
                        string s = "";
                    }

                    LPrizeWinnerBLL bllPrizeWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
                    LPrizeWinnerEntity entityPrizeWinner = null;

                    entityPrizeWinner = new LPrizeWinnerEntity()
                    {
                        PrizeWinnerID = Guid.NewGuid().ToString(),
                        VipID = strVipId,
                        PrizeID = entityPrize.PrizesID,
                        PrizeName = entityPrize.PrizeName,
                        PrizePoolID = entityPrizePool.PrizePoolsID,
                        CreateBy = this.CurrentUserInfo.UserID,
                        CreateTime = DateTime.Now,
                        IsDelete = 0
                    };

                    bllPrizeWinner.Create(entityPrizeWinner);

                    rd.PrizeId = entityPrize.PrizesID;
                    rd.PrizeName = entityPrize.PrizeName;
                    rd.ResultMsg = "中奖";
                }




                //if (share.ShareEventId !="")
                //{

                //}
            }
            catch (Exception ex)
            {

                rd.ResultMsg = ex.Message.ToString();
            }
            return rd;
        }

        #endregion
        #region 红包 大转盘
        public LotteryRD RedPacket(string strVipId, string strEventId)
        {
            var rd = new LotteryRD();//返回值

            LLotteryLogBLL bllLottery = new LLotteryLogBLL(this.CurrentUserInfo);
            LEventsBLL bll = new LEventsBLL(this.CurrentUserInfo);
            t_award_poolBLL bllAward = new t_award_poolBLL(this.CurrentUserInfo);
            LPrizesBLL bllPrize = new LPrizesBLL(this.CurrentUserInfo);

            LLotteryLogEntity lotteryEntityOld = null;
            LLotteryLogEntity lotteryEntityNew = null;



            LEventsEntity eventEntity = bll.QueryByEntity(new LEventsEntity() { EventID = strEventId }, null).FirstOrDefault();// bll.GetByID(strEventId);
            if (eventEntity.EventStatus == 40 || eventEntity == null)
            {
                rd.PrizeName = "活动已结束";
                return rd;
            }
            if (eventEntity.EventStatus == 10 || eventEntity.EventStatus == 30)
            {
                rd.PrizeName = "活动未开始";
                return rd;
            }
            var entityPrize = bllPrize.QueryByEntity(new LPrizesEntity() { EventId = eventEntity.EventID }, null);
            if (entityPrize == null)
            {
                rd.PrizeName = "活动无奖品";
                return rd;
            }
            #region 判断是否有资格参与抽奖
            var vipBLL = new VipBLL(this.CurrentUserInfo);
            var vipInfo = vipBLL.GetByID(strVipId);

            if (vipInfo == null)
            {
                rd.PrizeName = "用户不存在";
                return rd;
            }

            if (eventEntity.PointsLottery > 0 && (vipInfo.Integration == null ? 0 : vipInfo.Integration) < eventEntity.PointsLottery)
            {
                rd.PrizeName = "积分不足";
                return rd;
            }

            
            t_award_poolEntity awardEntity = null;
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrEmpty(strEventId))
                complexCondition.Add(new EqualsCondition() { FieldName = " EventId", Value = strEventId });
            complexCondition.Add(new DirectCondition("releasetime<='" + DateTime.Now + "' and balance=0 "));
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = " releasetime", Direction = OrderByDirections.Desc });
            //awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();
            //if (awardEntity == null)
            //{
            //    rd.PrizeName = "未中奖";
            //    return rd;
            //}
            lotteryEntityOld = bllLottery.QueryByEntity(new LLotteryLogEntity() { EventId = strEventId, VipId = strVipId }, null).FirstOrDefault();
            switch (eventEntity.PersonCount)
            {
                case 1://仅能参加一次抽奖
                    if (lotteryEntityOld == null)
                    {
                        awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();
                    }
                    else
                    {
                        rd.PrizeName = "抽奖机会用完了";
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
                        rd.PrizeName = "今天抽奖机会用完了";
                        return rd;
                    }
                    break;
                case 3://每周一次
                    if ((lotteryEntityOld != null && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date < DateTime.Now.Date.AddDays(-7)) || lotteryEntityOld == null)
                    {
                        awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();

                    }
                    else
                    {
                        rd.PrizeName = "本周抽奖机会用完了";
                        return rd;
                    }
                    break;
                case 4://无限
                    awardEntity = bllAward.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();
                    break;

            }
            #endregion
            if (awardEntity == null)
            {
                rd.PrizeName = "未中奖";
                return rd;
            }

            
            //抽奖记录
            lotteryEntityNew = new LLotteryLogEntity()
            {
                LogId = Guid.NewGuid().ToString(),
                VipId = strVipId,
                EventId = strEventId,
                LotteryCount = 1,
                IsDelete = 0

            };
            if (lotteryEntityOld == null)
                bllLottery.Create(lotteryEntityNew);
            else
            {
                lotteryEntityOld.LotteryCount = (lotteryEntityOld == null ? 0 + 1 : lotteryEntityOld.LotteryCount + 1);
                bllLottery.Update(lotteryEntityOld);
            }


            if (awardEntity != null)
            {

                //var prize = bllPrize.GetCouponTypeIDByPrizeId(awardEntity.PrizesID);
                var prize = DataTableToObject.ConvertToList<LPrizesEntity>(bllPrize.GetCouponTypeIDByPrizeId(awardEntity.PrizesID).Tables[0]).FirstOrDefault();
                rd.PrizeId = prize.PrizesID;
                rd.PrizeName = prize.PrizeName;
                rd.Location = prize.Location;
                rd.ResultMsg = "中奖";
                //中奖记录
                LPrizeWinnerBLL bllPrizeWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
                LPrizeWinnerEntity entityPrizeWinner = null;

                entityPrizeWinner = new LPrizeWinnerEntity()
                {
                    PrizeWinnerID = Guid.NewGuid().ToString(),
                    VipID = strVipId,
                    PrizeID = awardEntity.PrizesID,
                    PrizeName = prize.PrizeName,
                    PrizePoolID = awardEntity.PrizePoolsID,
                    CreateBy = this.CurrentUserInfo.UserID,
                    CreateTime = DateTime.Now,
                    IsDelete = 0
                };
                bllPrizeWinner.Create(entityPrizeWinner);
                //更新中奖人数
                LEventRountPrizesMappingBLL bllEventRountPrizesMapping = new LEventRountPrizesMappingBLL(this.CurrentUserInfo);
                bllEventRountPrizesMapping.UpdateWinnerCount(prize.PrizesID);
                //PrizePools状态更新
                LPrizePoolsEntity entityPrizePools = new LPrizePoolsEntity();

                entityPrizePools.PrizePoolsID = awardEntity.PrizePoolsID;
                entityPrizePools.Status = 0;

                LPrizePoolsBLL bllPrizePools = new LPrizePoolsBLL(this.CurrentUserInfo);
                bllPrizePools.Update(entityPrizePools, false);
                //更新t_award_pool奖品池的状态
                awardEntity = new t_award_poolEntity()
                {
                    Balance = 1,
                    id = awardEntity.id
                };
                bllAward.Update(awardEntity, false);
                VipIntegralBLL bllVipIntegral = new VipIntegralBLL(this.CurrentUserInfo);
                VipIntegralDetailEntity IntegralDetail = new VipIntegralDetailEntity();
                if (eventEntity.PointsLottery > 0)
                {
                    //扣除参与积分
                    IntegralDetail = new VipIntegralDetailEntity()
                    {
                        Integral = -eventEntity.PointsLottery,
                        IntegralSourceID = "24",
                        ObjectId = "",
                        UnitID=vipInfo.CouponInfo
                    };
                    //变动前积分
                    string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                    //变动积分
                    string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                    var vipIntegralDetailId = bllVipIntegral.AddIntegral(ref vipInfo, null, IntegralDetail, null, this.CurrentUserInfo);
                    //发送微信积分变动通知模板消息
                    if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                    {
                        var CommonBLL = new CommonBLL();
                        CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, this.CurrentUserInfo);
                    }
                }
                if (prize.PrizeTypeId == "Point")
                {
                    #region 调用积分统一接口
                    //var salesReturnBLL = new T_SalesReturnBLL(this.CurrentUserInfo);
                    //var pTran = salesReturnBLL.GetTran();//事务
                    //using (pTran.Connection)
                    //{
                    IntegralDetail = new VipIntegralDetailEntity()
                    {
                        Integral = prize.Point,
                        IntegralSourceID = "22",
                        ObjectId = "",
                        UnitID = vipInfo.CouponInfo
                    };
                    //变动前积分
                    string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                    //变动积分
                    string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                    var vipIntegralDetailId = bllVipIntegral.AddIntegral(ref vipInfo, null, IntegralDetail, null, this.CurrentUserInfo);
                    //发送微信积分变动通知模板消息
                    if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                    {
                        var CommonBLL = new CommonBLL();
                        CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, this.CurrentUserInfo);
                    }
                    //}
                    #endregion


                }
                if (prize.PrizeTypeId == "Coupon")
                {
                    CouponEntity entityCoupon = null;
                    CouponBLL bllCoupon = new CouponBLL(this.CurrentUserInfo);
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
                    //更新CouponType的IsVoucher(被领用数量)
                    CouponTypeBLL bllCouponType = new CouponTypeBLL(this.CurrentUserInfo);
                    CouponTypeEntity entityCouponType = bllCouponType.GetByID(prize.CouponTypeID);
                    entityCouponType.IsVoucher = entityCouponType.IsVoucher + 1;
                    bllCouponType.Update(entityCouponType);
                    ///更新优惠券有效期
                    if (entityCouponType.ServiceLife != null && entityCouponType.ServiceLife > 0)
                    {
                        entityCoupon.BeginDate = DateTime.Now.Date;
                        entityCoupon.EndDate = DateTime.Now.Date.AddDays((int)entityCouponType.ServiceLife).Date;

                        bllCoupon.Update(entityCoupon);

                    }
                }
            }
            else
            {
                int intLocation = bllPrize.GetLocationByEventID(strEventId);
                rd.Location = intLocation;
                rd.PrizeId = "0";
                rd.PrizeName = "未中奖";
            }

            return rd;
        }
        #endregion
    }
}