using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Event.EventPrizes.Request;
using JIT.CPOS.DTO.Module.Event.EventPrizes.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Event.EventPrizes
{
    public class GetEventPrizesAH : BaseActionHandler<GetEventPrizesRP, GetEventPrizesRD>
    {
        protected override GetEventPrizesRD ProcessRequest(DTO.Base.APIRequest<GetEventPrizesRP> pRequest)
        {
            GetEventPrizesRD rd = new GetEventPrizesRD();

            string vipID = pRequest.UserID;

            //string vipID = "f3d925e364e34bf69dfda34fcedc58f8";
            string vipName = string.Empty;
            string reCommandId = pRequest.Parameters.RecommandId;
            string eventId = pRequest.Parameters.EventId;
            float longitude = pRequest.Parameters.Longitude;
            float latitude = pRequest.Parameters.Latitude;
            string customerId = this.CurrentUserInfo.ClientID;
            int pointsLotteryFlag = pRequest.Parameters.PointsLotteryFlag;

            if (string.IsNullOrEmpty(customerId))
            {
                customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

            #region 是否在活动现场
            //respData.content.isSite = "1";
            rd.content = new EventPrizesInfo {IsSite = 1};

            #endregion

            #region 判断活动是否需要注册

            var leventsBll = new LEventsBLL(this.CurrentUserInfo);

            var enableFlag = leventsBll.GetEnableFlagByEventId(eventId);

            var vipService = new VipBLL(loggingSessionInfo);
            rd.SignFlag = 1;
            //如果需要注册，则判断该会员有没有注册，没有返回
            if (enableFlag.Substring(0, 1) == "1")
            {
               
                var vipEntity = vipService.GetByID(vipID);
                if (vipEntity == null)
                {
                    rd.SignFlag = 0;
                    return rd;
                }
                else
                {
                    rd.SignFlag = 1;
                    vipID = vipEntity.VIPID;
                    vipName = vipEntity.VipName;
                }
            }



            #endregion

            #region 获取VIPID

            #endregion



            #region

            Loggers.Debug(new DebugLogInfo()
            {
                Message = "vipName = " + vipName
                + ",vipID =" + vipID
                + ",eventId=" + eventId
                + ",longitude" + longitude
                + ",latitude" + latitude
                + ",customerId" + customerId
                + ",reCommandId" + reCommandId
            });
            LPrizePoolsBLL poolsServer = new LPrizePoolsBLL(loggingSessionInfo);
            var ds = poolsServer.GetEventWinningInfo(vipName, vipID, eventId, longitude, latitude, customerId, reCommandId, pointsLotteryFlag);

            int isLottery = Convert.ToInt32(ds.Tables[0].Rows[0]["IsLottery"]);
            int winnerFlag = Convert.ToInt32(ds.Tables[0].Rows[0]["WinnerFlag"]);//是否中奖
            string prizeName = ds.Tables[0].Rows[0]["prizeName"].ToString();
            string prizesId = ds.Tables[0].Rows[0]["prizesId"].ToString();            

            
            rd.content.IsLottery = isLottery;
            rd.content.IsWinning = winnerFlag;
            rd.content.WinningDesc = prizeName;
            rd.content.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
            rd.content.WinningExplan = ds.Tables[0].Rows[0]["PrizeDesc"].ToString();
            rd.content.EventRound = ds.Tables[0].Rows[0]["EventRoundId"].ToString();
            rd.content.Status = Convert.ToInt32(ds.Tables[0].Rows[0]["step"].ToString());
            rd.content.PrizesTypeName = ds.Tables[0].Rows[0]["prizeTypeName"].ToString();
            rd.content.PrizesName = ds.Tables[0].Rows[0]["prizesName"].ToString();
            rd.content.EventPoints = Convert.ToInt32(ds.Tables[0].Rows[0]["EventPoints"].ToString());

            #region update by wzq 2014/07/01 积分兑换标识（活动，个人）
            rd.content.PersonPointsLotteryFlag = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonPointsLotteryFlag"].ToString());
            rd.content.EventPointsLotteryFlag = Convert.ToInt32(ds.Tables[0].Rows[0]["EventPointsLotteryFlag"].ToString());
            #endregion

            #endregion


            #region 推荐人
            //活动是否分享
          

            var isShare = leventsBll.GetIsShareByEventId(eventId);

            rd.IsShare = isShare;
            Loggers.Debug(new DebugLogInfo() { Message = "isShare = " + isShare });

            CouponBLL couponService = new CouponBLL(loggingSessionInfo);

            //Updated by Willie Yan on 2014-05-30   必须是新推荐的会员
            if (isShare == 1 && !couponService.IfRecordedRecommendTrace(vipID, reCommandId))
            {
                Loggers.Debug(new DebugLogInfo() { Message = "reCommandId = " + reCommandId + ", VipID = " + vipID });
                if (reCommandId!= "" && !string.IsNullOrEmpty(reCommandId))
                {
                    if (reCommandId != vipID)
                    {
                        var vipEntity = vipService.GetByID(vipID);
                        vipEntity.HigherVipID = reCommandId;
                        vipService.Update(vipEntity);

                        Loggers.Debug(new DebugLogInfo() { Message = "Update HigherVipID = " + reCommandId + "for vipid=" + vipID });

                        //查看推荐人成功推荐人数，满足条件给奖励
                        
                        //TODO:added by zhangwei20141009，保存上下线记录
                        couponService.UpdateVipRecommandTrace(vipID, reCommandId);
                        Loggers.Debug(new DebugLogInfo() { Message = "UpdateVipRecommandTrace vipid=" + vipID + ",reCommandId= " + reCommandId });

                        couponService.RecommenderPrize(reCommandId, eventId);
                        Loggers.Debug(new DebugLogInfo() { Message = "RecommenderPrize reCommandId= " + reCommandId });
                    }
                }
            }

            //推荐人是否为空

            //推荐人跟vipId是否相同
            #endregion

            #region 奖品集合

            LPrizesBLL prizesService = new LPrizesBLL(loggingSessionInfo);

            var orderby = new List<OrderBy> { 
            new OrderBy(){ FieldName = "DisplayIndex",Direction = OrderByDirections.Asc}
            };
            

            var prizesList = prizesService.QueryByEntity(new LPrizesEntity()
            {
                EventId = eventId
            }, orderby.ToArray());

            rd.content.PrizesList = new List<PrizesEntity>();
            if (prizesList != null && prizesList.Length > 0)
            {
                foreach (var item in prizesList)
                {
                    var entity = new PrizesEntity()
                    {
                        PrizesID = item.PrizesID,
                        PrizeName = item.PrizeName,
                        PrizeDesc = item.PrizeDesc,
                        DisplayIndex = item.DisplayIndex.ToString(),
                        CountTotal = item.CountTotal.ToString(),
                        ImageUrl = item.ImageUrl,
                        Sponsor = item.ContentText
                    };

                    if (prizesId == item.PrizesID)
                    {
                        rd.content.PrizeIndex = item.DisplayIndex;
                    }
                    rd.content.PrizesList.Add(entity);
                }
            }

            #endregion


            #region 中奖集合
            var winDs = poolsServer.GetPersonWinnerList(vipID, eventId);
            rd.content.WinnerList = new List<WinnerList>();
            if (winDs != null && winDs.Tables.Count > 0 && winDs.Tables[0].Rows.Count > 0)
            {
                rd.content.WinnerList = DataTableToObject.ConvertToList<WinnerList>(winDs.Tables[0]);
            }

            #endregion


            #region  新增字段

           

            var leventsInfoDs = leventsBll.GetLeventsInfoDataSet(eventId);

            if (leventsInfoDs.Tables[0].Rows.Count > 0)
            {
                rd.BootUrl = leventsInfoDs.Tables[0].Rows[0]["BootUrl"].ToString();
                rd.OverRemark = leventsInfoDs.Tables[0].Rows[0]["OverRemark"].ToString();
                rd.PosterImageUrl = leventsInfoDs.Tables[0].Rows[0]["PosterImageUrl"].ToString();
                rd.ShareRemark = leventsInfoDs.Tables[0].Rows[0]["ShareRemark"].ToString();
                rd.ShareLogoUrl = leventsInfoDs.Tables[0].Rows[0]["ShareLogoUrl"].ToString();

            }

            #endregion

            #region 添加引导页URL

            //var bootDs = leventsBll.GetBootUrlByEventId(eventId);

            //if (bootDs.Tables[0].Rows.Count > 0)
            //{
            //    rd.ShareRemark = bootDs.Tables[0].Rows[0]["ShareRemark"].ToString();
            //    rd.OverRemark = bootDs.Tables[0].Rows[0]["OverRemark"].ToString();
            //    rd.PosterImageUrl = bootDs.Tables[0].Rows[0]["PosterImageUrl"].ToString();
            //    rd.BootUrl = bootDs.Tables[0].Rows[0]["BootUrl"].ToString();
            //}
            
            #endregion
            return rd;
        }

        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
    

    }
}