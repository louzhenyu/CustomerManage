using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class UpdateSetoffToolsStatusRP : IAPIRequestParameter
    {

        public List<string> SetoffToolIDList { get; set; }
        public void Validate()
        {

        } 
    }
}
