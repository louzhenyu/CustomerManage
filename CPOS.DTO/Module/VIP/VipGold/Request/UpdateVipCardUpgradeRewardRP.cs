using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class UpdateVipCardUpgradeRewardRP : IAPIRequestParameter
    {
        
        /// <summary>
        /// 开卡礼列表
        /// </summary>
        public List<VipCardUpgradeRewardRPInfo> OpeCouponTypeList { get; set; }
        public void Validate()
        {
            
        } 
    }
}
