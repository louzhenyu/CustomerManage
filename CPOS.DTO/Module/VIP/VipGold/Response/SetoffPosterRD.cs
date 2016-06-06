using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class SetoffPosterRD : IAPIResponseData
    {
        public string SetoffPosterID { get; set; }
        public string Name { get; set; }
       
        public string ImageUrl { get; set; }
    }
}
