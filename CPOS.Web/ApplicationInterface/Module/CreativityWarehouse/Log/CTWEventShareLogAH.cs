using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using CPOS.Common;
using JIT.CPOS.BS.BLL.WX;
using RedisOpenAPIClient.Models.CC;

namespace JIT.CPOS.Web.ApplicationInterface.Module.CreativityWarehouse.Log
{
    public class CTWEventShareLogAH : BaseActionHandler<CTWEventShareLogRP, CTWEventShareLogRD>
    {
        protected override CTWEventShareLogRD ProcessRequest(DTO.Base.APIRequest<CTWEventShareLogRP> pRequest)
        {
            var rd = new CTWEventShareLogRD();//返回值
            var para = pRequest.Parameters;
            if (!string.IsNullOrEmpty(para.CTWEventId) && !string.IsNullOrEmpty(para.Sender) && !string.IsNullOrEmpty(para.OpenId))
            {
                var bllLeventShareLog = new T_LEventsSharePersonLogBLL(this.CurrentUserInfo);
                var entityLeventShareLog = new T_LEventsSharePersonLogEntity();

                entityLeventShareLog.ShareVipID = para.Sender;
                entityLeventShareLog.ShareOpenID = para.OpenId;
                entityLeventShareLog.BeShareOpenID = para.BeSharedOpenId;
                entityLeventShareLog.BeShareVipID = para.BEsharedUserId;
                entityLeventShareLog.BusTypeCode = "CTW";
                entityLeventShareLog.ObjectId = para.CTWEventId;
                entityLeventShareLog.ShareURL = para.ShareURL;
                bllLeventShareLog.Create(entityLeventShareLog);
                //是否分享给自己
                if(para.Sender==para.BEsharedUserId)
                {
                    return rd;
                }
                //触点奖励
                ContactEventBLL bllContactEvent = new ContactEventBLL(this.CurrentUserInfo);
                var entityContact = bllContactEvent.QueryByEntity(new ContactEventEntity() { EventId = para.CTWEventId, IsDelete = 0, IsCTW = 1, ContactTypeCode = "Share" }, null).SingleOrDefault();
                if (entityContact != null)
                {
                    LPrizesBLL bllPrize = new LPrizesBLL(this.CurrentUserInfo);
                    LPrizeWinnerBLL bllPrizeWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
                    LLotteryLogBLL bllLottery = new LLotteryLogBLL(this.CurrentUserInfo);
                    ///判断是否已经获得奖励
                    int intLogCount = bllLottery.GetEventLotteryLogByEventId(entityContact.ContactEventId.ToString(),para.Sender);
                    if (intLogCount >0)
                    {
                        return rd;
                    }

                    var prize = DataTableToObject.ConvertToList<LPrizesEntity>(bllPrize.GetCouponTypeIDByEventId(entityContact.ContactEventId.ToString()).Tables[0]).FirstOrDefault();
                    if (prize != null)
                    {
                        CouponBLL bllCoupon = new CouponBLL(this.CurrentUserInfo);

                        if (prize.PrizeTypeId == "Coupon")
                        {
                            bllCoupon.CouponBindVip(para.Sender, prize.CouponTypeID, entityContact.ContactEventId.ToString(), "Share");

                            DataSet ds = bllPrize.GetAllCouponTypeByPrize(prize.PrizesID);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                var redisVipMappingCouponBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon.RedisVipMappingCouponBLL();

                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    redisVipMappingCouponBLL.SetVipMappingCoupon(new CC_Coupon()
                                    {
                                        CustomerId = this.CurrentUserInfo.ClientID,
                                        CouponTypeId = dr["CouponTypeID"].ToString()
                                    }, entityContact.ContactEventId.ToString(), para.Sender, "Share");
                                }
                            }

                        }
                        if (prize.PrizeTypeId == "Point")
                        {
                            #region 调用积分统一接口
                            var salesReturnBLL = new T_SalesReturnBLL(this.CurrentUserInfo);
                            VipIntegralBLL bllVipIntegral = new VipIntegralBLL(this.CurrentUserInfo);
                            var vipBLL = new VipBLL(this.CurrentUserInfo);

                            var vipInfo = vipBLL.GetByID(para.Sender);
                            var IntegralDetail = new VipIntegralDetailEntity()
                            {
                                Integral = prize.Point,
                                IntegralSourceID = "28",
                                ObjectId = entityContact.ContactEventId.ToString()
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

                            #endregion
                        }

                        #region 奖励日志
                        LPrizeWinnerEntity entityPrizeWinner = new LPrizeWinnerEntity()
                        {
                            PrizeWinnerID = Guid.NewGuid().ToString(),
                            VipID = para.Sender,
                            PrizeID = prize.PrizesID,
                            PrizeName = prize.PrizeName,
                            PrizePoolID = "",
                            CreateBy = this.CurrentUserInfo.UserID,
                            CreateTime = DateTime.Now,
                            IsDelete = 0
                        };

                        bllPrizeWinner.Create(entityPrizeWinner);


                        LLotteryLogEntity lotteryEntity = new LLotteryLogEntity()
                        {
                            LogId = Guid.NewGuid().ToString(),
                            VipId = para.Sender,
                            EventId = entityContact.ContactEventId.ToString(),
                            LotteryCount = 1,
                            IsDelete = 0

                        };
                        bllLottery.Create(lotteryEntity);
                        #endregion
                    }

                }
            }
            return rd;
        }
    }
}