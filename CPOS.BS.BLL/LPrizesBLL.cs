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
using CPOS.Common;
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
            string strCouponSourceId = "";
            try
            {
                ContactEventEntity contactEvent = null;
                if (strEventId != "" || strEventId.Length > 0)
                {
                    contactEvent = bllContactEvent.QueryByEntity(new ContactEventEntity() { ShareEventId = strEventId, IsDelete = 0, Status = 2, CustomerID = this.CurrentUserInfo.ClientID }, null).FirstOrDefault();
                    strCouponSourceId = "5F671057-E6B5-4B5E-B2D4-AC3A64F6710F";
                }
                else
                {
                    contactEvent = bllContactEvent.QueryByEntity(new ContactEventEntity() { ContactTypeCode = strType, IsDelete = 0, Status = 2, CustomerID = this.CurrentUserInfo.ClientID }, null).FirstOrDefault();
                    switch(strType)
                    {
                        case "Reg":
                            strCouponSourceId = "FD977FBA-03F9-4AC0-805F-75A56BD6A429";
                            break;
                        case "Comment":
                            strCouponSourceId = "80EF450A-5CEF-4DB6-B28D-420BDDA59894";
                            break;
                        case "Focus":
                            strCouponSourceId = "945C3C2D88DF4AFAA260A1CED81C6870";
                            break;

                    }
                        


                        
                }

                if (contactEvent != null)
                {
                   

                    
                    
                    var entityPrize = bllPrize.GetPrizesByEventId(contactEvent.ContactEventId.ToString()).FirstOrDefault();

                    var bllPrizePool = new LPrizePoolsBLL(CurrentUserInfo);
                    var entityPrizePool = new LPrizePoolsEntity();

                    LPrizeWinnerBLL bllPrizeWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
                    LPrizeWinnerEntity entityPrizeWinner = null;


                    List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                    complexCondition.Add(new EqualsCondition() { FieldName = " PrizeID", Value = strEventId });
                    complexCondition.Add(new EqualsCondition() { FieldName = " VipID", Value = strVipId });

                    List<OrderBy> lstOrder = new List<OrderBy> { };
                    lstOrder.Add(new OrderBy() { FieldName = " CreateTime", Direction = OrderByDirections.Desc });

                    entityPrizeWinner = bllPrizeWinner.Query(complexCondition.ToArray(), lstOrder.ToArray()).FirstOrDefault();
                    if(entityPrizeWinner!=null)
                    {
                        if(contactEvent.RewardNumber=="OnlyOne")
                        {
                            rd.PrizeName = "仅有一次奖励！";
                            return rd;
                        }
                        if (contactEvent.RewardNumber == "OnceADay" && Convert.ToDateTime(entityPrizeWinner.CreateTime).Date==DateTime.Now.Date)
                        {
                            rd.PrizeName = "每天一次奖励！";
                            return rd;
                        }
                    }




                    if (entityPrize.PrizeTypeId == "Point")
                    {
                        #region 调用积分统一接口
                        var salesReturnBLL = new T_SalesReturnBLL(this.CurrentUserInfo);
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

                        #endregion
                    }
                    else if (entityPrize.PrizeTypeId == "Coupon")
                    {

                        entityPrizePool = bllPrizePool.QueryByEntity(new LPrizePoolsEntity() { EventId = contactEvent.ContactEventId.ToString(), PrizeID = entityPrize.PrizesID, Status = 1 }, null).FirstOrDefault();
                        if (entityPrizePool == null)
                        {
                            rd.PrizeName = "奖品已发完！";
                            return rd;
                        }
                        ///改变奖品池状态
                        entityPrizePool.Status = 2;
                        bllPrizePool.Update(entityPrizePool);

                        CouponBLL bllCoupon = new CouponBLL(this.CurrentUserInfo);
                        int intResult = bllCoupon.CouponBindVip(strVipId, entityPrize.CouponTypeID);


                        //CouponEntity entityCoupon = null;
                        
                        //entityCoupon = DataTableToObject.ConvertToList<CouponEntity>(bllCoupon.GetCouponIdByCouponTypeID(entityPrize.CouponTypeID).Tables[0]).FirstOrDefault();

                        //VipCouponMappingEntity entityVipCouponMapping = null;
                        //VipCouponMappingBLL bllVipCouponMapping = new VipCouponMappingBLL(this.CurrentUserInfo);

                        //entityVipCouponMapping = new VipCouponMappingEntity()
                        //{
                        //    VipCouponMapping = Guid.NewGuid().ToString(),
                        //    VIPID = strVipId,
                        //    CouponID = entityCoupon.CouponID,
                        //    CouponSourceId = strCouponSourceId
                        //};
                        //bllVipCouponMapping.Create(entityVipCouponMapping);
                        ////更新CouponType的IsVoucher(被领用数量)
                        //CouponTypeBLL bllCouponType = new CouponTypeBLL(this.CurrentUserInfo);
                        //CouponTypeEntity entityCouponType = bllCouponType.GetByID(entityPrize.CouponTypeID);
                        //entityCouponType.IsVoucher = entityCouponType.IsVoucher + 1;
                        //bllCouponType.Update(entityCouponType);
                        /////更新优惠券有效期
                        //if (entityCouponType.ServiceLife != null && entityCouponType.ServiceLife > 0)
                        //{
                        //    entityCoupon.BeginDate = DateTime.Now.Date;
                        //    entityCoupon.EndDate = Convert.ToDateTime(DateTime.Now.Date.AddDays((int)entityCouponType.ServiceLife - 1).ToShortDateString() + " 23:59:59.998");
                            

                        //}
                        //entityCoupon.Status = 2;
                        //bllCoupon.Update(entityCoupon);
                    }
                    else if (entityPrize.PrizeTypeId == "Chance" && !string.IsNullOrEmpty(strEventId) && contactEvent.ChanceCount>0)
                    {
                        LEventsVipObjectEntity entityEventsVipObject = new LEventsVipObjectEntity();
                        LEventsVipObjectBLL bllEventsVipObject = new LEventsVipObjectBLL(this.CurrentUserInfo);

                        for (int i = 0; i < contactEvent.ChanceCount;i++ )
                        {
                            entityEventsVipObject = new LEventsVipObjectEntity()
                            {
                                MappingId = Guid.NewGuid().ToString(),
                                EventId = strEventId,
                                VipId = strVipId,
                                ObjectId = contactEvent.ContactEventId.ToString(),
                                IsCheck = 0,
                                IsLottery = 0

                            };
                            bllEventsVipObject.Create(entityEventsVipObject, null);
                        }
                    }
                    LLotteryLogBLL bllLottery = new LLotteryLogBLL(this.CurrentUserInfo);
                    LLotteryLogEntity lotteryEntity = bllLottery.GetByID(contactEvent.ContactEventId);
                    if (lotteryEntity != null)
                    {
                        lotteryEntity.LotteryCount = lotteryEntity.LotteryCount + 1;
                        bllLottery.Update(lotteryEntity, false);
                    }
                    else
                    {
                        lotteryEntity = new LLotteryLogEntity()
                        {
                            LogId = Guid.NewGuid().ToString(),
                            VipId = strVipId,
                            EventId = contactEvent.ContactEventId.ToString(),
                            LotteryCount = 1,
                            IsDelete = 0

                        };
                        bllLottery.Create(lotteryEntity);
                    }

                    entityPrizeWinner = new LPrizeWinnerEntity()
                    {
                        PrizeWinnerID = Guid.NewGuid().ToString(),
                        VipID = strVipId,
                        PrizeID = entityPrize.PrizesID,
                        PrizeName = entityPrize.PrizeName,
                        PrizePoolID =entityPrizePool==null?"": entityPrizePool.PrizePoolsID,
                        CreateBy = this.CurrentUserInfo.UserID,
                        CreateTime = DateTime.Now,
                        IsDelete = 0
                    };

                    bllPrizeWinner.Create(entityPrizeWinner);

                    rd.PrizeId = entityPrize.PrizesID;
                    rd.PrizeName = entityPrize.PrizeName;
                    rd.ResultMsg = "已奖励";
                }
            }
            catch (Exception ex)
            {

                rd.ResultMsg = ex.Message.ToString();
            }
            return rd;
        }

        #endregion
        #region 红包 大转盘
        public LotteryRD RedPacket(string strVipId, string strEventId,string strCustomerID)
        {
            var rd = new LotteryRD();//返回值

            LLotteryLogBLL bllLottery = new LLotteryLogBLL(this.CurrentUserInfo);
            LEventsBLL bll = new LEventsBLL(this.CurrentUserInfo);
            t_award_poolBLL bllAward = new t_award_poolBLL(this.CurrentUserInfo);
            LPrizesBLL bllPrize = new LPrizesBLL(this.CurrentUserInfo);

            LLotteryLogEntity lotteryEntityOld = null;
            LLotteryLogEntity lotteryEntityNew = null;



            LEventsEntity eventEntity = bll.QueryByEntity(new LEventsEntity() { EventID = strEventId, CustomerId = strCustomerID }, null).FirstOrDefault();// bll.GetByID(strEventId);
            if (eventEntity == null || eventEntity.EventStatus == 40)
            {
                rd.PrizeName = "抱歉 来晚一步 活动已经结束啦";
                return rd;
            }
            if (eventEntity.EventStatus == 10 || eventEntity.EventStatus == 30)
            {
                rd.PrizeName = "活动尚未开始 请耐心等待哦";
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
                rd.PrizeName = "很遗憾 您的当前积分不够 无法参与本次活动";
                return rd;
            }


            t_award_poolEntity awardEntity = null;
            DataSet dsAwardEntity = new DataSet();

            LEventsVipObjectEntity entityEventsVipObject = new LEventsVipObjectEntity();
            LEventsVipObjectBLL bllEventsVipObject = new LEventsVipObjectBLL(this.CurrentUserInfo);

            int intChange = 0;//是否有抽奖机会默认没有
            entityEventsVipObject = bllEventsVipObject.QueryByEntity(new LEventsVipObjectEntity() { EventId = strEventId, VipId = strVipId, IsLottery = 0 }, null).FirstOrDefault();
            if (entityEventsVipObject != null)
                intChange = 1;
            bool boolHaveChange = true;


            lotteryEntityOld = bllLottery.QueryByEntity(new LLotteryLogEntity() { EventId = strEventId, VipId = strVipId }, null).FirstOrDefault();
            switch (eventEntity.PersonCount)
            {
                case 1://仅能参加一次抽奖
                    if (lotteryEntityOld != null)
                    {
                        boolHaveChange = false;
                    }
                    if (!boolHaveChange && intChange == 0)
                    {
                        rd.PrizeName = "你已经参与过活动啦~请继续关注品牌其它活动哦";
                        return rd;
                    }
                    else
                    {
                        dsAwardEntity = bllAward.GetPrizeByEventId(strEventId);
                    }
                    break;
                case 2://每天一次
                    if ((lotteryEntityOld != null && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date == DateTime.Now.Date))
                    {
                        boolHaveChange = false;
                    }
                    if (!boolHaveChange && intChange == 0)
                    {
                        rd.PrizeName = "今天的抽奖机会已经用光啦~明天再来哦";
                        return rd;
                    }
                    else
                    {
                        dsAwardEntity = bllAward.GetPrizeByEventId(strEventId);
                    }
                    break;
                case 3://每周一次
                    DateTime startWeek = DateTimeHelper.GetMondayDate(DateTime.Now).Date;  //本周周一
                    DateTime endWeek = DateTimeHelper.GetSundayDate(DateTime.Now).AddDays(1).Date;   //本周周日

                    if ((lotteryEntityOld != null && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date >= startWeek && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date <= endWeek))
                    {
                        boolHaveChange = false;

                    }
                    if (!boolHaveChange && intChange == 0)
                    {
                        rd.PrizeName = "本周的抽奖机会已经用光啦，欢迎下周再来哦";
                        return rd;
                    }
                    break;
                default:
                    dsAwardEntity = bllAward.GetPrizeByEventId(strEventId);
                    break;

            }
            #endregion
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
            //更新抽奖机会
            if (!boolHaveChange && intChange == 1)
            {
                entityEventsVipObject.IsLottery = 1;
                bllEventsVipObject.Update(entityEventsVipObject, false, null);
            }
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
            }

            //获取奖品
            if (dsAwardEntity.Tables[0].Rows.Count > 0)
            {
                awardEntity = DataTableToObject.ConvertToObject<t_award_poolEntity>(dsAwardEntity.Tables[0].Rows[0]);
            }
            else
            {
                int intLocation = bllPrize.GetLocationByEventID(strEventId);
                rd.Location = intLocation;
                rd.PrizeId = "0";
                rd.PrizeName = "啊呜  手气不是时时有 下次再接再厉哦";
                return rd;
            }
            if (awardEntity != null)
            {

                //var prize = bllPrize.GetCouponTypeIDByPrizeId(awardEntity.PrizesID);
                var prize = DataTableToObject.ConvertToList<LPrizesEntity>(bllPrize.GetCouponTypeIDByPrizeId(awardEntity.PrizesID).Tables[0]).FirstOrDefault();
               
               
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
                    int intResult = bllCoupon.CouponBindVip(strVipId, prize.CouponTypeID);
                    if (intResult == 0)
                    {
                        int intLocation = bllPrize.GetLocationByEventID(strEventId);
                        rd.Location = intLocation;
                        rd.PrizeId = "0";
                        rd.PrizeName = "啊呜  手气不是时时有 下次再接再厉哦";
                        return rd;
                    }
                    //entityCoupon = DataTableToObject.ConvertToList<CouponEntity>(bllCoupon.GetCouponIdByCouponTypeID(prize.CouponTypeID).Tables[0]).FirstOrDefault();
                    //if (entityCoupon == null)
                    //{
                    //    int intLocation = bllPrize.GetLocationByEventID(strEventId);
                    //    rd.Location = intLocation;
                    //    rd.PrizeId = "0";
                    //    rd.PrizeName = "啊呜  手气不是时时有 下次再接再厉哦";
                    //    return rd;
                    //}
                    //VipCouponMappingEntity entityVipCouponMapping = null;
                    //VipCouponMappingBLL bllVipCouponMapping = new VipCouponMappingBLL(this.CurrentUserInfo);

                    //entityVipCouponMapping = new VipCouponMappingEntity()
                    //{
                    //    VipCouponMapping = Guid.NewGuid().ToString(),
                    //    VIPID = CurrentUserInfo.UserID,
                    //    CouponID = entityCoupon.CouponID,
                    //    CouponSourceId = "7D87E7E1-66AC-403B-9BB4-80AE4278F6A4"
                    //};

                    //bllVipCouponMapping.Create(entityVipCouponMapping);
                    ////更新CouponType的IsVoucher(被领用数量)
                    //CouponTypeBLL bllCouponType = new CouponTypeBLL(this.CurrentUserInfo);
                    //CouponTypeEntity entityCouponType = bllCouponType.GetByID(prize.CouponTypeID);
                    //entityCouponType.IsVoucher = entityCouponType.IsVoucher + 1;
                    //bllCouponType.Update(entityCouponType);
                    /////更新优惠券有效期
                    //if (entityCouponType.ServiceLife != null && entityCouponType.ServiceLife > 0)
                    //{
                    //    entityCoupon.BeginDate = DateTime.Now.Date;
                    //    entityCoupon.EndDate = Convert.ToDateTime(DateTime.Now.Date.AddDays((int)entityCouponType.ServiceLife - 1).ToShortDateString() + " 23:59:59.998");

                    //    bllCoupon.Update(entityCoupon);

                    //}
                }


                rd.PrizeId = prize.PrizesID;
                rd.PrizeName = prize.PrizeName;
                rd.Location = prize.Location;
                rd.ResultMsg = "哇塞 人品爆棚的你中奖啦";
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
            }
            return rd;
        }
        #endregion
        #region  红包中奖(必中)
        public LotteryRD RedPacket2(string strVipId, string strEventId, string strCustomerID)
        {
            var rd = new LotteryRD();//返回值

            LLotteryLogBLL bllLottery = new LLotteryLogBLL(this.CurrentUserInfo);
            LEventsBLL bll = new LEventsBLL(this.CurrentUserInfo);
            t_award_poolBLL bllAward = new t_award_poolBLL(this.CurrentUserInfo);
            LPrizesBLL bllPrize = new LPrizesBLL(this.CurrentUserInfo);

            LLotteryLogEntity lotteryEntityOld = null;
            LLotteryLogEntity lotteryEntityNew = null;

            LPrizePoolsEntity entityPrizePools = new LPrizePoolsEntity();
            LPrizePoolsBLL bllPrizePools = new LPrizePoolsBLL(this.CurrentUserInfo);

            LEventsVipObjectEntity entityEventsVipObject = new LEventsVipObjectEntity();
            LEventsVipObjectBLL bllEventsVipObject = new LEventsVipObjectBLL(this.CurrentUserInfo);

            CouponEntity entityCoupon = null;
            CouponBLL bllCoupon = new CouponBLL(this.CurrentUserInfo);

            LEventsEntity eventEntity = new LEventsEntity();
            eventEntity = bll.QueryByEntity(new LEventsEntity() { EventID = strEventId, CustomerId = strCustomerID }, null).FirstOrDefault();// bll.GetByID(strEventId);
            if (eventEntity == null||eventEntity.EventStatus == 40)
            {
                rd.PrizeName = "抱歉 来晚一步 活动已经结束啦";
                return rd;
            }
            if (eventEntity.EventStatus == 10 || eventEntity.EventStatus == 30)
            {
                rd.PrizeName = "活动尚未开始 请耐心等待哦";
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
                rd.PrizeName = "很遗憾 您的当前积分不够 无法参与本次活动";
                return rd;
            }



            int intChange = 0;//是否有抽奖机会默认没有
            entityEventsVipObject = bllEventsVipObject.QueryByEntity(new LEventsVipObjectEntity() { EventId = strEventId, VipId = strVipId, IsLottery = 0 }, null).FirstOrDefault();
            if (entityEventsVipObject != null)
                intChange = 1;
            bool boolHaveChange = true;

            lotteryEntityOld = bllLottery.QueryByEntity(new LLotteryLogEntity() { EventId = strEventId, VipId = strVipId }, null).FirstOrDefault();
            switch (eventEntity.PersonCount)
            {
                case 1://活动期间参与N次 
                    if (lotteryEntityOld != null)
                    {
                        boolHaveChange = false;
                    }
                    else
                    {
                        if (eventEntity.LotteryNum!=null && eventEntity.LotteryNum > 0)
                        {
                            for (int i = 1; i < eventEntity.LotteryNum; i++)
                            {
                                entityEventsVipObject = new LEventsVipObjectEntity()
                                {
                                    MappingId=Guid.NewGuid().ToString(),
                                    IsLottery = 0,
                                    EventId = strEventId,
                                    VipId = strVipId
                                };
                                bllEventsVipObject.Create(entityEventsVipObject);
                            }
                        }
                    }
                    if (!boolHaveChange && intChange == 0)
                    {
                        rd.PrizeName = "你已经参与过活动啦~请继续关注品牌其它活动哦";
                        return rd;
                    }
                    break;
                case 2://每天一次
                    if ((lotteryEntityOld != null && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date == DateTime.Now.Date))
                    {
                        boolHaveChange = false;
                    }
                    if (!boolHaveChange && intChange == 0)
                    {
                        rd.PrizeName = "今天的抽奖机会已经用光啦~明天再来哦";
                        return rd;
                    }
                    break;
                case 3://每周一次
                    DateTime startWeek = DateTimeHelper.GetMondayDate(DateTime.Now).Date;  //本周周一
                    DateTime endWeek = DateTimeHelper.GetSundayDate(DateTime.Now).AddDays(1).Date;   //本周周日
                    if ((lotteryEntityOld != null && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date >= startWeek && Convert.ToDateTime(lotteryEntityOld.LastUpdateTime.ToString()).Date <= endWeek))
                    {
                        boolHaveChange = false;

                    }
                    if (!boolHaveChange && intChange == 0)
                    {
                        rd.PrizeName = "本周的抽奖机会已经用光啦，欢迎下周再来哦";
                        return rd;
                    }           
                    break;
                default:
                    
                    break;

            }
            #endregion
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
            //更新抽奖机会
            if (!boolHaveChange && intChange == 1)
            {
                entityEventsVipObject.IsLottery = 1;
                bllEventsVipObject.Update(entityEventsVipObject, false, null);
                
            }
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
            }
            //获取奖品
            if (bllPrizePools.GetRandomPrizeByEventId(strEventId).Tables[0].Rows.Count > 0)
            {
                entityPrizePools = DataTableToObject.ConvertToObject<LPrizePoolsEntity>(bllPrizePools.GetRandomPrizeByEventId(strEventId).Tables[0].Rows[0]);
            }
            else
            {
                int intLocation = bllPrize.GetLocationByEventID(strEventId);
                rd.Location = intLocation;
                rd.PrizeId = "0";
                rd.PrizeName = "啊呜  手气不是时时有 下次再接再厉哦";
                return rd;
            }
            var prize = DataTableToObject.ConvertToList<LPrizesEntity>(bllPrize.GetCouponTypeIDByPrizeId(entityPrizePools.PrizeID).Tables[0]).FirstOrDefault();
                if(prize==null)
                {
                    rd.Location = 0;
                    rd.PrizeId = "0";
                    rd.PrizeName = "啊呜  手气不是时时有 下次再接再厉哦";
                    return rd;
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

                    int intResult=bllCoupon.CouponBindVip(strVipId, prize.CouponTypeID);
                    if(intResult==0)
                    {
                        int intLocation = bllPrize.GetLocationByEventID(strEventId);
                        rd.Location = intLocation;
                        rd.PrizeId = "0";
                        rd.PrizeName = "啊呜  手气不是时时有 下次再接再厉哦";
                        return rd;
                    }
                    //entityCoupon = DataTableToObject.ConvertToList<CouponEntity>(bllCoupon.GetCouponIdByCouponTypeID(prize.CouponTypeID).Tables[0]).FirstOrDefault();
                    //if (entityCoupon == null)
                    //{
                    //    int intLocation = bllPrize.GetLocationByEventID(strEventId);
                    //    rd.Location = intLocation;
                    //    rd.PrizeId = "0";
                    //    rd.PrizeName = "啊呜  手气不是时时有 下次再接再厉哦";
                    //    return rd;
                    //}
                    //VipCouponMappingEntity entityVipCouponMapping = null;
                    //VipCouponMappingBLL bllVipCouponMapping = new VipCouponMappingBLL(this.CurrentUserInfo);

                    //entityVipCouponMapping = new VipCouponMappingEntity()
                    //{
                    //    VipCouponMapping = Guid.NewGuid().ToString(),
                    //    VIPID = CurrentUserInfo.UserID,
                    //    CouponID = entityCoupon.CouponID,
                    //    CouponSourceId = "7D87E7E1-66AC-403B-9BB4-80AE4278F6A4"
                    //};

                    //bllVipCouponMapping.Create(entityVipCouponMapping);
                    ////更新CouponType的IsVoucher(被领用数量)
                    //CouponTypeBLL bllCouponType = new CouponTypeBLL(this.CurrentUserInfo);
                    //CouponTypeEntity entityCouponType = bllCouponType.GetByID(prize.CouponTypeID);
                    //entityCouponType.IsVoucher = entityCouponType.IsVoucher + 1;
                    //bllCouponType.Update(entityCouponType);
                    /////更新优惠券有效期
                    //if (entityCouponType.ServiceLife != null && entityCouponType.ServiceLife > 0)
                    //{
                    //    entityCoupon.BeginDate = DateTime.Now.Date;
                    //    entityCoupon.EndDate = Convert.ToDateTime(DateTime.Now.Date.AddDays((int)entityCouponType.ServiceLife - 1).ToShortDateString() + " 23:59:59.998");

                    //    bllCoupon.Update(entityCoupon);

                    //}
                }
                if (entityCoupon!=null)
                {
                    rd.ParValue = entityCoupon.ParValue;
                    rd.CouponTypeName = entityCoupon.CoupontypeName;
                }
                rd.PrizeId = prize.PrizesID;
                rd.PrizeName = prize.PrizeName;
                rd.Location = prize.Location;
                rd.ResultMsg = "哇塞 人品爆棚的你中奖啦";
                //中奖记录
                LPrizeWinnerBLL bllPrizeWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
                LPrizeWinnerEntity entityPrizeWinner = null;

                entityPrizeWinner = new LPrizeWinnerEntity()
                {
                    PrizeWinnerID = Guid.NewGuid().ToString(),
                    VipID = strVipId,
                    PrizeID = entityPrizePools.PrizeID,
                    PrizeName = prize.PrizeName,
                    PrizePoolID = entityPrizePools.PrizePoolsID,
                    CreateBy = this.CurrentUserInfo.UserID,
                    CreateTime = DateTime.Now,
                    IsDelete = 0
                };
                bllPrizeWinner.Create(entityPrizeWinner);
                //更新中奖人数
                LEventRountPrizesMappingBLL bllEventRountPrizesMapping = new LEventRountPrizesMappingBLL(this.CurrentUserInfo);
                bllEventRountPrizesMapping.UpdateWinnerCount(prize.PrizesID);
                //PrizePools状态更新
                entityPrizePools.PrizePoolsID = entityPrizePools.PrizePoolsID;
                entityPrizePools.Status = 0;
                bllPrizePools.Update(entityPrizePools, false);
                return rd;
        }
        #endregion
    }
}