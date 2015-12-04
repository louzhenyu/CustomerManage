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

        #region 分享设置是否中奖
        /// <summary>
        /// 是否中奖
        /// </summary>
        /// <param name="strVipId"></param>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        public LotteryRD CheckIsWinnerForShare(string strVipId, string strEventId)
        {
            var rd = new LotteryRD();//返回值


            var bllShare = new LEventsShareBLL(this.CurrentUserInfo);
            var bllContactEvent = new ContactEventBLL(this.CurrentUserInfo);
            var bllPrize = new LPrizesBLL(this.CurrentUserInfo);
            var entityShare = new LEventsShareEntity();

            try
            {


                //var share = bllContactEvent.GetByID(strEventId);
                var contactEvent = bllContactEvent.QueryByEntity(new ContactEventEntity() { ShareEventId = strEventId, IsDelete = 0, Status = 2 }, null).FirstOrDefault();
                if (contactEvent != null)
                {

                    var entityPrize = bllPrize.GetPrizesByEventId(contactEvent.ContactEventId.ToString()).FirstOrDefault();

                    var bllPrizePool = new LPrizePoolsBLL(CurrentUserInfo);
                    var entityPrizePool = new LPrizePoolsEntity();

                    entityPrizePool = bllPrizePool.QueryByEntity(new LPrizePoolsEntity() { EventId = strEventId, PrizeID = entityPrize.PrizesID, Status = 1 }, null).FirstOrDefault();
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
                        var pTran = salesReturnBLL.GetTran();//事务
                        using (pTran.Connection)
                        {
                            VipIntegralBLL bllVipIntegral = new VipIntegralBLL(this.CurrentUserInfo);
                            var vipBLL = new VipBLL(this.CurrentUserInfo);

                            var vipInfo = vipBLL.GetByID(strVipId);
                            var IntegralDetail = new VipIntegralDetailEntity()
                            {
                                Integral = entityPrize.Point,
                                IntegralSourceID = "22",
                                ObjectId = ""
                            };
                            bllVipIntegral.AddIntegral(vipInfo, null, IntegralDetail, pTran, this.CurrentUserInfo);
                        }


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

    }
}