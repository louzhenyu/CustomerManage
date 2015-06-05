using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.ServicesLog.Request
{
   public class GetVipServicesLogListRP : IAPIRequestParameter
    {
       public string VipID { get; set; }
       public int PageIndex	{get;set;}
       public int PageSize { get; set; }
 
        public void Validate()
        {

        }
    }
}
