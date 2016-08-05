using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class UpdateVipCardUpgradeRewardRD : IAPIResponseData
    {
        public List<VipCardUpgradeRewardEntity> VipCardUpgradeRewardList { get; set; }
    }
    
}
