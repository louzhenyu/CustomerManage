using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.ServicesLog.Request
{
    public class SetApiVipServicesLogRP : IAPIRequestParameter
    {
        public string ServicesLogID { get; set; }
        public string VipID { get; set; }
        public string ServicesTime { get; set; }
        public string ServicesMode { get; set; }
        public int ServicesType { get; set; }
        public string Duration { get; set; }
        public string Content { get; set; }

        public void Validate()
        {

        }
    }
}
