using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.Reward.Response;
using JIT.CPOS.DTO.Module.VIP.Reward.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System.Data;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Reward
{
    public class GetTopRewardListAH : BaseActionHandler<GetTopRewardListRP, GetTopRewardListRD>
    {
        protected override GetTopRewardListRD ProcessRequest(DTO.Base.APIRequest<GetTopRewardListRP> pRequest)
        {
            var rd = new GetTopRewardListRD();
            var customerId = CurrentUserInfo.ClientID;

            var trrBll = new T_RewardRecordBLL(CurrentUserInfo);
            var userBll = new T_UserBLL(CurrentUserInfo);
            //var cbsBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            //var trcBll = new T_RewardConfigBLL(CurrentUserInfo);
            //var cbsEntity = cbsBll.QueryByEntity(new CustomerBasicSettingEntity() { CustomerID = customerId },null).FirstOrDefault();
            //var trcEntitys = trcBll.QueryByEntity(new T_RewardConfigEntity() { CustomerId = customerId }, null);
            

            //获取员工列表 (TOP10就好)
            var userList = userBll.QueryByEntity(new T_UserEntity() { customer_id = customerId }, null);
            //pRequest.UserID;
            var trrList = trrBll.QueryByEntity(new T_RewardRecordEntity() { PayStatus = 2,CustomerId = customerId }, null);

            var oeBll = new ObjectEvaluationBLL(CurrentUserInfo);
            var oeList = oeBll.QueryByEntity(new ObjectEvaluationEntity() { Type = 4, CustomerID = customerId }, null);
            var allOE = (from p in oeList.AsEnumerable()
                              group p by p.ObjectID into g
                              select new
                              {
                                  g.Key,
                                  SumValue = g.Average(p => p.StarLevel)
                              });

            var allRewards = (from p in trrList.AsEnumerable()
                         group p by p.RewardedOP into g
                         select new
                         {
                             g.Key,
                             SumValue = g.Sum(p => p.RewardAmount)
                         }).Where(g => g.SumValue > 0).OrderByDescending(g => g.SumValue);

            var rewardCount = allRewards.ToList().Count;
            var top10Rewards = allRewards.Take(10);

            //var result2 = trrList.Sum();

            rd.RewardList = new List<RewardInfo>();
            rd.MyReward = new RewardInfo();
            
            var index = 1;
            foreach (var item in top10Rewards)
            {
                var tmpRewardInfo = new RewardInfo();
                var userinfo = userList.Where(t => t.user_id == item.Key).ToArray().FirstOrDefault();
                var oeinfo = allOE.Where(t => t.Key == item.Key).ToArray().FirstOrDefault();
                tmpRewardInfo = new RewardInfo()
                {
                    UserID = userinfo.user_id,
                    UserName = userinfo.user_name,
                    UserPhoto = userinfo.HighImageUrl,
                    StarLevel = oeinfo != null ? Convert.ToInt32(oeinfo.SumValue) : 0,
                    Rank = index,
                    RewardIncome = item.SumValue
                };
                rd.RewardList.Add(tmpRewardInfo);
                if(userinfo.user_id == pRequest.UserID)
                {
                    rd.MyReward = tmpRewardInfo;
                }
                
                index++;
            }
            if (string.IsNullOrEmpty(rd.MyReward.UserID))//Top10之外
            {
                var userinfo = userList.Where(t => t.user_id == pRequest.UserID).ToArray().FirstOrDefault();
                var oeinfo = allOE.Where(t => t.Key == pRequest.UserID).ToArray().FirstOrDefault();
                var myReward = allRewards.Where(t => t.Key == pRequest.UserID).FirstOrDefault();
                decimal? myRewardIncome = 0;
                if (myReward != null)
                {
                    myRewardIncome = myReward.SumValue != null ? myReward.SumValue : 0;
                    rewardCount = allRewards.Where(g => g.SumValue > myReward.SumValue).ToList().Count;
                }
                //var myRewardIncome = myReward != null ? myReward.SumValue : 0;
                
                rd.MyReward = new RewardInfo()
                {
                    UserID = userinfo.user_id,
                    UserName = userinfo.user_name,
                    UserPhoto = userinfo.HighImageUrl,
                    StarLevel = oeinfo != null ? Convert.ToInt32(oeinfo.SumValue) : 0,
                    Rank = rewardCount + 1,
                    RewardIncome = myRewardIncome
                };
            }

            return rd;
        }
    }
}