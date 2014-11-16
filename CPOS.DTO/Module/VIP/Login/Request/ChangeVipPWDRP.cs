using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Request
{
    public class ChangeVipPWDRP : IAPIRequestParameter
    {
        public string VipID { get; set; }
        public string SourcePWD { get; set; }
        public string NewPWD { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(VipID))
                throw new Exception("参数VipID为空");
        }
    }
}
