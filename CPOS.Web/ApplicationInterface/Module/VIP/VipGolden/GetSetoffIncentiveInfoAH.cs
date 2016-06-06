using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 获取激励信息接口
    /// </summary>
    public class GetSetoffIncentiveInfoAH : BaseActionHandler<GetSetOffIncentiveRP, GetSetOffIncentiveRD>
    {
        protected override GetSetOffIncentiveRD ProcessRequest(APIRequest<GetSetOffIncentiveRP> pRequest)
        {

            GetSetOffIncentiveRD incentiveRuleRD = new GetSetOffIncentiveRD();
            IincentiveRuleBLL incentiveRuleBLL = new IincentiveRuleBLL(CurrentUserInfo);
            var para = pRequest.Parameters;
            //根据平台类型获取激励信息  
            var IncentiveRule = incentiveRuleBLL.GetIncentiveRule(para.PlatformType);

            if (IncentiveRule != null && IncentiveRule.Tables[0].Rows.Count > 0)
            {
                incentiveRuleRD.SetoffRegAwardType = Convert.ToInt32(IncentiveRule.Tables[0].Rows[0]["SetoffRegAwardType"]);//激励类型 积分或现金
                incentiveRuleRD.SetoffRegPrize = IncentiveRule.Tables[0].Rows[0]["SetoffRegPrize"].ToString();//奖励金额或积分数量
                incentiveRuleRD.SetoffOrderPer = Convert.ToDecimal(IncentiveRule.Tables[0].Rows[0]["SetoffOrderPer"]);//订单销售成功提成比例
            }
            return incentiveRuleRD;

        }
    }
}