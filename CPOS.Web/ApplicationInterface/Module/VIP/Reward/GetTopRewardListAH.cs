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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Reward
{
    public class GetTopRewardListAH : BaseActionHandler<GetTopRewardListRP, GetTopRewardListRD>
    {
        /// <summary>
        /// 门店内员工排名
        /// 排名规则：
        /// 1.取出打赏金额大于零的员工
        /// 2.比较打赏金额，（金额高的往前排）
        /// 3.金额相同，比较评价等级 （等级高的往前排）
        /// 4.如果还相同，比较打赏时间（时间靠前，往前排）
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetTopRewardListRD ProcessRequest(DTO.Base.APIRequest<GetTopRewardListRP> pRequest)
        {
            var rd = new GetTopRewardListRD();
            var customerId = CurrentUserInfo.ClientID;

            var trrBll = new T_RewardRecordBLL(CurrentUserInfo);
            var userBll = new T_UserBLL(CurrentUserInfo);

            //获取员工列表(门店内)
            var userList = userBll.QueryByEntity(new T_UserEntity() { customer_id = customerId }, null);
            var userService = new cUserService(CurrentUserInfo);
            CurrentUserInfo.CurrentUserRole.UnitId = string.IsNullOrEmpty(CurrentUserInfo.CurrentUserRole.UnitId) ? "" : CurrentUserInfo.CurrentUserRole.UnitId;
            var para_unit_id = CurrentUserInfo.CurrentUserRole.UnitId;

            var maxRowCount = Utils.GetIntVal(Request("limit"));
            var startRowIndex = Utils.GetIntVal(Request("start"));
            var rowCount = maxRowCount > 0 ? maxRowCount : 999;//每页行数
            var startIndex = startRowIndex > 0 ? startRowIndex : 0;//当前页的起始行数

            var userdata = new JIT.CPOS.BS.Entity.User.UserInfo();
            if (string.IsNullOrEmpty(CurrentUserInfo.CurrentUserRole.UnitId))
            {
                userdata = userService.SearchUserListByUnitID(string.Empty, string.Empty, string.Empty, string.Empty,
                rowCount, startIndex, "", para_unit_id, string.Empty, string.Empty);
            }
            else
            {
                userdata = userService.SearchUserListByUnitID(string.Empty, string.Empty, string.Empty, string.Empty,
                rowCount, startIndex,
                CurrentUserInfo == null ? "" : CurrentUserInfo.CurrentUserRole.UnitId, para_unit_id, string.Empty, string.Empty);
            }

            var orderBys = new OrderBy[1];
            orderBys[0] = new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Asc };
            var trrList = trrBll.QueryByEntity(new T_RewardRecordEntity() { PayStatus = 2,CustomerId = customerId }, orderBys);

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
            //var top10Rewards = allRewards.Take(10);

            //金额相同，比较评价等级 （等级高的往前排）

            rd.RewardList = new List<RewardInfo>();
            rd.MyReward = new RewardInfo();
            
            var index = 1;
            foreach (var item in allRewards)
            {
                var tmpRewardInfo = new RewardInfo();
                //var userinfo = userList.Where(t => t.user_id == item.Key).ToArray().FirstOrDefault();
                var userinfo = userdata.UserInfoList.Where(t => t.User_Id == item.Key).ToArray().FirstOrDefault();
                var oeinfo = allOE.Where(t => t.Key == item.Key).ToArray().FirstOrDefault();
                if(userinfo != null)
                {
                    tmpRewardInfo = new RewardInfo()
                    {
                        UserID = userinfo.User_Id,
                        UserName = userinfo.User_Name,
                        UserPhoto = userinfo.imageUrl,
                        StarLevel = oeinfo != null ? Convert.ToInt32(oeinfo.SumValue) : 0,
                        Rank = index,
                        RewardIncome = item.SumValue
                    };
                    rd.RewardList.Add(tmpRewardInfo);
                    if (userinfo.User_Id == pRequest.UserID)
                    {
                        rd.MyReward = tmpRewardInfo;
                    }

                    index++;
                }
                
            }

            if (string.IsNullOrEmpty(rd.MyReward.UserID))//Top10之外
            {
                //var userinfo = userList.Where(t => t.user_id == pRequest.UserID).ToArray().FirstOrDefault();
                var userinfo = userdata.UserInfoList.Where(t => t.User_Id == pRequest.UserID).ToArray().FirstOrDefault();
                var oeinfo = allOE.Where(t => t.Key == pRequest.UserID).ToArray().FirstOrDefault();
                var myReward = allRewards.Where(t => t.Key == pRequest.UserID).FirstOrDefault();
                decimal? myRewardIncome = 0;
                if (myReward != null)
                {
                    myRewardIncome = myReward.SumValue != null ? myReward.SumValue : 0;
                    rewardCount = allRewards.Where(g => g.SumValue > myReward.SumValue).ToList().Count;
                }
                var myStarLevel = oeinfo != null ? Convert.ToInt32(oeinfo.SumValue) : 0;
                if (userinfo != null)
                {
                    rd.MyReward = new RewardInfo()
                    {
                        UserID = userinfo.User_Id,
                        UserName = userinfo.User_Name,
                        UserPhoto = userinfo.imageUrl,
                        StarLevel = myStarLevel,
                        Rank = myRewardIncome > 0 ? rewardCount + 1 : 0,
                        RewardIncome = myRewardIncome
                    };
                }
                    
            }

            return rd;
        }
        protected string Request(string key)
        {
            if (HttpContext.Current.Request[key] == null) return string.Empty;
            return HttpContext.Current.Request[key];
        }
    }
}