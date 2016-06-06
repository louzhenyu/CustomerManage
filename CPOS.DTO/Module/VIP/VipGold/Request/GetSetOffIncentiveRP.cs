using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetSetOffIncentiveRP : IAPIRequestParameter
    {

        public string PlatformType { get; set; } //应用类型 1=微信；2= APP会员服务；3=APP客服列表
        public void Validate()
        {

        } 
    }
}
