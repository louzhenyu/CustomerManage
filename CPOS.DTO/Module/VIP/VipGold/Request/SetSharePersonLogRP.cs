using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class SetSharePersonLogRP : IAPIRequestParameter
    {
        public string ApplicationType { get; set; }//1=微信用户;  2=APP员工; 3=超级分销

        public string ShareVipType { get; set; } //1=员工 2=客服 3=会员 4=超级分销商

        public string ObjectID { get; set; }
        public string SetOffToolID { get; set; }//工具ID
        public string ToolType { get; set; }//工具类型
        public void Validate()
        {

        } 
    }
}
