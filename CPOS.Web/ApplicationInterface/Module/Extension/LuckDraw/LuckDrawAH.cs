using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Extension.LuckDraw.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.LuckDraw
{
    public class LuckDrawAH : BaseActionHandler<EmptyRequestParameter, LuckDrawRD>
    {

        protected override LuckDrawRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new LuckDrawRD();
            var activityBLL = new X_ActivityBLL(CurrentUserInfo);
            var para = pRequest.Parameters;
            var activityPrizesBLL = new X_ActivityPrizesBLL(CurrentUserInfo);
            var vipPointBLL = new X_VipPointMarkBLL(CurrentUserInfo);
            var activityJoinBLL = new X_ActivityJoinBLL(CurrentUserInfo);
            DateTime dtNow = DateTime.Now;      //当前时间
            DateTime startWeekTime = DateTimeHelper.GetMondayDate(dtNow).Date;  //本周周一
            DateTime endWeekTime = DateTimeHelper.GetSundayDate(dtNow).AddDays(1).Date;   //本周周日

            int point = 0;      //用户当前积点
            int joinLimit = 0;  //每周参与次数限制
            int LowestPointLimit = 0;   //可参与的最小积点限制

            //判断活动是否已结束
            var activityInfo = activityBLL.QueryByEntity(new X_ActivityEntity() { CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
            if (activityInfo != null)
            {
                if (dtNow < activityInfo.BeginTime || dtNow > activityInfo.EndTime)
                {
                    rd.Flag = 5;//活动已结束
                    return rd;
                }
                joinLimit = activityInfo.JoinLimit.Value;
                LowestPointLimit = activityInfo.LowestPointLimit.Value;
            }
            //判断用户当前积点是否够

            var vipPointInfo = vipPointBLL.QueryByEntity(new X_VipPointMarkEntity() { VipID = pRequest.UserID }, null).FirstOrDefault();
            if (vipPointInfo != null)
                point = vipPointInfo.Count.Value;

            if (point < LowestPointLimit)
            {
                rd.Flag = 4;//积点不足
                return rd;
            }
            //判断用户本周是否已抽奖
            var activityJoinInfo = activityJoinBLL.GetActivityJoinByWeek(pRequest.UserID, startWeekTime, endWeekTime);
            if (activityJoinInfo != null)
            {
                rd.Flag = 3;//本周已抽奖
                return rd;
            }

            //读取所有奖品
            var activityPrizesList = activityPrizesBLL.QueryByEntity(new X_ActivityPrizesEntity() { CustomerID = this.CurrentUserInfo.ClientID }
                , new OrderBy[] { new OrderBy() { FieldName = "CAST(ImageUrl AS int ) ", Direction = OrderByDirections.Asc } });

            #region 判断是否中奖
            Random Random1 = new Random();
            int i = Random1.Next(0, 100);
            string prizesId = string.Empty;
            string prizesName = string.Empty;

            foreach (var item in activityPrizesList)
            {
                if (int.Parse(item.ImageUrl) >= i)//ImageUrl字段临时作为中奖概率范围
                {
                    prizesId = item.PrizesID.ToString();
                    break;
                }
            }
            var activityPrizesInfo = activityPrizesBLL.GetByID(prizesId);
            if (activityPrizesInfo != null)
            {
                //判断该奖品是否已兑完
                if (activityPrizesInfo.RemainingQty <= 0)
                {
                    rd.Flag = 2;    //未中奖
                    rd.DisplayIndex = 4;
                }
                //判断本周奖品是否已抽完
                int weekCount = activityJoinBLL.GetPrizesCountByWeek(activityPrizesInfo.PrizesID.Value, startWeekTime, endWeekTime);
                if (activityPrizesInfo.WeekCount <= weekCount)
                {
                    rd.Flag = 2;    //未中奖
                    rd.DisplayIndex = 4;
                }
                //判断积点是否足够兑奖，不够则为不中
                if (point < activityPrizesInfo.UsePoint)
                {
                    rd.Flag = 2;    //未中奖
                    rd.DisplayIndex = 4;
                }
                if (rd.Flag == 0)   //中奖
                {
                    rd.Flag = 1;    //中奖
                    rd.PrizesID = activityPrizesInfo.PrizesID;
                    rd.PrizesName = activityPrizesInfo.PrizesName;
                    rd.DisplayIndex = activityPrizesInfo.DisplayIndex;  //显示序号
                }

                var activityJoinInfoNew = new X_ActivityJoinEntity()
                {
                    VipID = pRequest.UserID,
                    ActivityID = activityInfo.ActivityID,
                    IsWinPrice = rd.Flag == 1 ? 1 : 2,
                    PrizesID = activityPrizesInfo.PrizesID,
                    IsExchange = 2,
                    CustomerID = this.CurrentUserInfo.ClientID
                };
                activityJoinBLL.Create(activityJoinInfoNew);
            }
            #endregion


            return rd;
        }
    }
}