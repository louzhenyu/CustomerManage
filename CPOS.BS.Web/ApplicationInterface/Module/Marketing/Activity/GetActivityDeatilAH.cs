using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Marketing.Request;
using JIT.CPOS.DTO.Module.Marketing.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class GetActivityDeatilAH : BaseActionHandler<GetActivityDeatilRP, GetActivityDeatilRD>
    {
        protected override GetActivityDeatilRD ProcessRequest(DTO.Base.APIRequest<GetActivityDeatilRP> pRequest)
        {
            var rd = new GetActivityDeatilRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var activityBll = new C_ActivityBLL(loggingSessionInfo);
            var prizesBll = new C_PrizesBLL(loggingSessionInfo);
            var prizesDetailBll = new C_PrizesDetailBLL(loggingSessionInfo);
            var activityMessageBll = new C_ActivityMessageBLL(loggingSessionInfo);
            var targetGroupBll = new C_TargetGroupBLL(loggingSessionInfo);
            var rechargeStrategyBll = new RechargeStrategyBLL(loggingSessionInfo);
            if (!string.IsNullOrWhiteSpace(para.ActivityID))
            {
                #region 基础信息
                C_ActivityEntity activityData = activityBll.GetByID(para.ActivityID);
                if (activityData != null)
                {
                    rd.ActivityID = activityData.ActivityID.ToString();
                    rd.ActivityType = activityData.ActivityType ?? 2;
                    rd.ActivityName = activityData.ActivityName;
                    rd.StartTime = activityData.StartTime == null ? "" : activityData.StartTime.Value.ToString("yyyy-MM-dd");
                    rd.EndTime = activityData.EndTime == null ? "" : activityData.EndTime.Value.ToString("yyyy-MM-dd");
                    rd.IsLongTime = activityData.IsLongTime == null ? "0" : activityData.IsLongTime.Value.ToString();
                    rd.IsAllCardType = activityData.IsAllVipCardType ?? 0;
                    rd.VipCardTypeID = activityBll.GetTargetGroupId(rd.IsAllCardType, rd.ActivityID);
                    rd.HolderCardCount = activityBll.GetTargetCount(rd.VipCardTypeID, rd.ActivityType, rd.StartTime, rd.EndTime, activityData.IsLongTime.Value);
                    rd.Status = activityData.Status.Value;
                }
                #endregion
                #region 奖品
                var PrizesList = prizesBll.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "ActivityID", Value = para.ActivityID }, new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID } }, null).ToList();
                if (PrizesList.Count > 0)
                {
                    //奖品集合赋值
                    rd.PrizesInfoList = (from u in PrizesList
                                         select new PrizesInfo()
                                         {
                                             PrizesID = Convert.ToString(u.PrizesID),
                                             PrizesType = u.PrizesType.Value
                                         }).ToList();

                    foreach (var item in rd.PrizesInfoList)
                    {
                        item.PrizesDetailList = new List<PrizesDetailInfo>();
                        //奖品明细
                        var PrizesDetailList = prizesDetailBll.GetPrizesDetailList(item.PrizesID);
                        if (PrizesDetailList.Count > 0)
                        {
                            CouponTypeBLL ctbll = new CouponTypeBLL(CurrentUserInfo);
                            foreach (var itemes in PrizesDetailList)
                            {
                                PrizesDetailInfo m = new PrizesDetailInfo();
                                m.PrizesDetailID = Convert.ToString(itemes.PrizesDetailID);
                                m.CouponTypeID = Convert.ToString(itemes.CouponTypeID);
                                m.CouponTypeName = itemes.CouponTypeName;
                                m.NumLimit = itemes.NumLimit.Value;
                                m.CouponTypeDesc = itemes.CouponTypeDesc;
                                //ValidityPeriod = t.BeginTime == null ? ("领取后" + (t.ServiceLife == 0 ? "1天内有效" : t.ServiceLife.ToString() + "天内有效")) : (t.BeginTime.Value.ToString("yyyy-MM-dd") + "至" + t.EndTime.Value.ToString("yyyy-MM-dd")),
                                var t = ctbll.GetByID(m.CouponTypeID);
                                m.ValidityPeriod = t.BeginTime == null ? ("领取后" + (t.ServiceLife == 0 ? "1天内有效" : t.ServiceLife.ToString() + "天内有效")) : (t.BeginTime.Value.ToString("yyyy-MM-dd") + "至" + t.EndTime.Value.ToString("yyyy-MM-dd"));
                                //奖品明细集合额赋值
                                item.PrizesDetailList.Add(m);
                            }
                        }
                    }
                }
                #endregion
                #region 消息
                var ActivityMessageList = activityMessageBll.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "ActivityID", Value = para.ActivityID }, new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID } }, null).ToList();
                if (ActivityMessageList.Count > 0)
                {
                    //消息赋值
                    rd.ActivityMessageInfoList = (from u in ActivityMessageList
                                                  select new ActivityMessageInfo()
                                                  {
                                                      MessageID = u.MessageID.Value.ToString(),
                                                      MessageType = u.MessageType.Trim(),
                                                      Content = u.Content,
                                                      SendTime = u.SendTime == null ? "" : u.SendTime.Value.ToString(),
                                                      AdvanceDays = u.AdvanceDays,
                                                      SendAtHour = u.SendAtHour
                                                  }).ToList();
                }
                #endregion
                #region 充值策略

                var rechargeStrategyInfoList = rechargeStrategyBll.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "ActivityID", Value = para.ActivityID }, new EqualsCondition() { FieldName = "CustomerId", Value = loggingSessionInfo.ClientID } }, new[]{
                             new OrderBy(){ FieldName="RechargeAmount", Direction= OrderByDirections.Asc}
                            }).ToList();
                if (rechargeStrategyInfoList.Count > 0)
                {
                    //消息赋值
                    rd.RechargeStrategyInfoList = (from u in rechargeStrategyInfoList
                                                  select new RechargeStrategyInfo()
                                                  {
                                                      RechargeStrategyId = u.RechargeStrategyId.ToString(),
                                                      RuleType= u.RuleType,
                                                      RechargeAmount = u.RechargeAmount,
                                                      GiftAmount = u.GiftAmount.Value
                                                  }).ToList();
                }
                #endregion
            }
            return rd;
        }
    }
}