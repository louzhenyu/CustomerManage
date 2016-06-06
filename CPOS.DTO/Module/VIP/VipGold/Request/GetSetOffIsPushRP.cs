using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetSetOffIsPushRP : IAPIRequestParameter
    {

        public string ShareVipType { get; set; } //1=员工;2=客服;3=会员

        public string BeShareVipID { get; set; }//被分享或被推送会员ID
        public void Validate()
        {

        } 
    }
}
