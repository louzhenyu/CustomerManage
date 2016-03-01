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
            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);
            var PrizesBLL = new C_PrizesBLL(loggingSessionInfo);
            var PrizesDetailBLL = new C_PrizesDetailBLL(loggingSessionInfo);
            var ActivityMessageBLL = new C_ActivityMessageBLL(loggingSessionInfo);
            var TargetGroupBLL = new C_TargetGroupBLL(loggingSessionInfo);
            if (!string.IsNullOrWhiteSpace(para.ActivityID))
            {
                #region 基础信息
                C_ActivityEntity ActivityData = ActivityBLL.GetByID(para.ActivityID);
                if (ActivityData != null)
                {

                    rd.ActivityID = ActivityData.ActivityID.ToString();
                    rd.ActivityName = ActivityData.ActivityName;
                    rd.StartTime = ActivityData.StartTime == null ? "" : ActivityData.StartTime.Value.ToString("yyyy-MM-dd");
                    rd.EndTime = ActivityData.EndTime == null ? "" : ActivityData.EndTime.Value.ToString("yyyy-MM-dd");
                    rd.IsLongTime = ActivityData.IsLongTime == null ? "0" : ActivityData.IsLongTime.Value.ToString();
                    rd.PointsMultiple = ActivityData.PointsMultiple == null ? 0 : ActivityData.PointsMultiple.Value;

                    int m_IsAllVipCardType = ActivityData.IsAllVipCardType == null ? 0 : ActivityData.IsAllVipCardType.Value;
                    if (m_IsAllVipCardType == 1)
                    {
                        C_TargetGroupEntity VipCardTargetGroupData = TargetGroupBLL.QueryByEntity(new C_TargetGroupEntity() { ActivityID = ActivityData.ActivityID, GroupType = 1 }, null).FirstOrDefault();
                        rd.VipCardTypeID = VipCardTargetGroupData == null ? "" : VipCardTargetGroupData.ObjectID;
                    }
                    int m_IsVipGrouping = ActivityData.IsVipGrouping == null ? 0 : ActivityData.IsVipGrouping.Value;
                    if (m_IsAllVipCardType == 1)
                    {
                        C_TargetGroupEntity VipCardTargetGroupData = TargetGroupBLL.QueryByEntity(new C_TargetGroupEntity() { ActivityID = ActivityData.ActivityID, GroupType = 2 }, null).FirstOrDefault();
                        rd.VipGroupingID = VipCardTargetGroupData == null ? "" : VipCardTargetGroupData.ObjectID;
                    }
                    //持卡人数
                    //rd.holderCardCount = ActivityBLL.GetholderCardCount(null,para.ActivityID);
                }
                #endregion
                #region 奖品
                var PrizesList = PrizesBLL.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "ActivityID", Value = para.ActivityID }, new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID } }, null).ToList();
                if (PrizesList.Count > 0)
                {
                    //奖品集合赋值
                    rd.PrizesInfoList = (from u in PrizesList
                                         select new PrizesInfo()
                                         {
                                             PrizesID = Convert.ToString(u.PrizesID),
                                             PrizesType = u.PrizesType.Value,
                                             AmountLimit = u.AmountLimit.Value,
                                             IsCirculation = u.IsCirculation == null ? 0 : u.IsCirculation.Value
                                         }).ToList();

                    foreach (var item in rd.PrizesInfoList)
                    {
                        item.PrizesDetailList = new List<PrizesDetailInfo>();
                        //奖品明细
                        var PrizesDetailList = PrizesDetailBLL.GetPrizesDetailList(item.PrizesID);
                        if (PrizesDetailList.Count > 0)
                        {
                            foreach (var itemes in PrizesDetailList)
                            {
                                PrizesDetailInfo m = new PrizesDetailInfo();
                                m.PrizesDetailID = Convert.ToString(itemes.PrizesDetailID);
                                m.CouponTypeID = Convert.ToString(itemes.CouponTypeID);
                                m.CouponTypeName = itemes.CouponTypeName;
                                m.EndTime = itemes.EndTime == null ? "" : itemes.EndTime.Value.ToString("yyyy-MM-dd");
                                m.AvailableQty = (itemes.IssuedQty == null ? 0 : itemes.IssuedQty.Value) - (itemes.IsVoucher == null ? 0 : itemes.IsVoucher.Value);
                                m.CouponTypeDesc = itemes.CouponTypeDesc == null ? "" : itemes.CouponTypeDesc;

                                //奖品明细集合额赋值
                                item.PrizesDetailList.Add(m);
                            }
                        }
                    }
                }
                #endregion
                #region 消息
                var ActivityMessageList = ActivityMessageBLL.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "ActivityID", Value = para.ActivityID },new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID } }, null).ToList();
                if (ActivityMessageList.Count > 0)
                {
                    //消息赋值
                    rd.ActivityMessageInfoList = (from u in ActivityMessageList
                                                  select new ActivityMessageInfo()
                                                  {
                                                      MessageID = u.MessageID.Value.ToString(),
                                                      MessageType = u.MessageType.Trim(),
                                                      TemplateID = u.TemplateID.Value.ToString(),
                                                      Content = u.Content,
                                                      SendTime = u.SendTime == null ? "" : u.SendTime.Value.ToString()
                                                  }).ToList();
                }
                #endregion
            }
            return rd;
        }
    }
}