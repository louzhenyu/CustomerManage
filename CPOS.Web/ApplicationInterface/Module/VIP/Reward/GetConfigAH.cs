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
    public class GetConfigAH : BaseActionHandler<GetConfigRP, GetConfigRD>
    {
        protected override GetConfigRD ProcessRequest(DTO.Base.APIRequest<GetConfigRP> pRequest)
        {
            var rd = new GetConfigRD();
            var customerId = CurrentUserInfo.ClientID;
            var trrBll = new T_RewardRecordBLL(CurrentUserInfo);//打赏记录
            var userBll = new T_UserBLL(CurrentUserInfo);
            var cbsBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            var trcBll = new T_RewardConfigBLL(CurrentUserInfo);
            var cbsEntity = cbsBll.QueryByEntity(new CustomerBasicSettingEntity() { CustomerID = customerId },null).FirstOrDefault();
            var trcEntitys = trcBll.QueryByEntity(new T_RewardConfigEntity() { CustomerId = customerId }, null);
            //获取员工
            var userInfo = new T_UserEntity();
            var employeeId = pRequest.Parameters.EmployeeID;
            if (!string.IsNullOrEmpty(employeeId))
            {
                userInfo = userBll.QueryByEntity(new T_UserEntity() { user_id = employeeId, customer_id = customerId }, null).FirstOrDefault();

                //员工头像
                var _ObjectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
                var userImg = _ObjectImagesBLL.QueryByEntity(new ObjectImagesEntity() {ObjectId = employeeId, CustomerId = customerId }, null).OrderByDescending(p => p.CreateTime).FirstOrDefault();

                //星级
                var oeBll = new ObjectEvaluationBLL(CurrentUserInfo);
                var oeList = oeBll.QueryByEntity(new ObjectEvaluationEntity() { ObjectID = employeeId, Type = 4, CustomerID = customerId }, null);
                var oeinfo = (from p in oeList.AsEnumerable()
                             group p by p.ObjectID into g
                             select new
                             {
                                 g.Key,
                                 SumValue = g.Average(p => p.StarLevel)
                             }).ToArray().FirstOrDefault();
                
                //打赏数据
                var rewardCount = trrBll.QueryByEntity(new T_RewardRecordEntity() { RewardedOP = employeeId, PayStatus = 2, CustomerId = customerId }, null).Length;

                //员工信息
                rd.UserInfo = new List<UserInfo>();
                rd.UserInfo.Add(new UserInfo()
                {
                    UserName = userInfo.user_name,
                    UserPhoto = userImg != null ? userImg.ImageURL : string.Empty,
                    StarLevel = oeinfo != null ? Convert.ToInt32(oeinfo.SumValue) : 0,
                    RewardCount = rewardCount
                });

            }
            
            //打赏类型
            if (cbsEntity != null)
            {
                switch (cbsEntity.SettingValue)
                {
                    case "1":
                        rd.Type = 1;
                        break;
                    case "2":
                        rd.Type = 2;
                        break;
                    default:
                        rd.Type = 0;//0或空(null)为两者
                        break;
                }
            }
            else
            {
                rd.Type = 0;//两者
            }

            //打赏金额列表
            rd.AmountList = new List<RewardAmountInfo>();
            foreach (var item in trcEntitys)
            {
                rd.AmountList.Add(new RewardAmountInfo()
                {
                    Amount = item.RewardAmount
                });
            }

            return rd;
        }
    }
}