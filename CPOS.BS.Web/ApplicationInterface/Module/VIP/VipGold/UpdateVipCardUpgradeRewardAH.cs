using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    /// <summary>
    /// 编辑开卡礼信息
    /// </summary>
    public class UpdateVipCardUpgradeRewardAH : BaseActionHandler<UpdateVipCardUpgradeRewardRP, UpdateVipCardUpgradeRewardRD>
    {
        protected override UpdateVipCardUpgradeRewardRD ProcessRequest(DTO.Base.APIRequest<UpdateVipCardUpgradeRewardRP> pRequest)
        {
            var rd = new UpdateVipCardUpgradeRewardRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            
            var bllVipCardUpgradeReward = new VipCardUpgradeRewardBLL(loggingSessionInfo);
            List<VipCardUpgradeRewardEntity> ListVipCardUpgradeReward = new List<VipCardUpgradeRewardEntity>();
            foreach (var RewardItem in para.OpeCouponTypeList)
            {
                var entityVipCardUpgradeReward = new VipCardUpgradeRewardEntity();
                //当CardUpgradeRewardId不为空时可能有两种操作（删除或修改） 为空时新增
                if(!string.IsNullOrEmpty(RewardItem.CardUpgradeRewardId))
                {
                    var VipCardUpgradeRewardInfo = bllVipCardUpgradeReward.QueryByEntity(new VipCardUpgradeRewardEntity() { CustomerID = loggingSessionInfo.ClientID, CardUpgradeRewardId = new Guid(RewardItem.CardUpgradeRewardId), VipCardTypeID = RewardItem.VipCardTypeID }, null).FirstOrDefault();
                    if (VipCardUpgradeRewardInfo != null)
                    {
                        entityVipCardUpgradeReward.CardUpgradeRewardId = new Guid(RewardItem.CardUpgradeRewardId);
                    }
                }                
                else
                {
                    entityVipCardUpgradeReward.CardUpgradeRewardId = Guid.NewGuid();
                }
                entityVipCardUpgradeReward.VipCardTypeID = RewardItem.VipCardTypeID;
                entityVipCardUpgradeReward.CouponTypeId = new Guid(RewardItem.CouponTypeID);
                entityVipCardUpgradeReward.CouponNum = RewardItem.CouponNum;
                entityVipCardUpgradeReward.CustomerID = loggingSessionInfo.ClientID;
                ListVipCardUpgradeReward.Add(entityVipCardUpgradeReward);
                switch (RewardItem.OperateType)//判断此处为何种操作(0=删除券;1=新增;2=修改;)
                {
                    case 0://删除券
                        bllVipCardUpgradeReward.Delete(entityVipCardUpgradeReward);
                        break;
                    case 1://新增券
                        bllVipCardUpgradeReward.Create(entityVipCardUpgradeReward);
                        break;
                    case 2://修改
                        bllVipCardUpgradeReward.Update(entityVipCardUpgradeReward);
                        break;
                }      
            }
            //如果有操作就给当前编辑的数据，如果没有就不给
            if (ListVipCardUpgradeReward.Count > 0)
            {
                rd.VipCardUpgradeRewardList = ListVipCardUpgradeReward;
            }
            return rd;
        }
    }
}