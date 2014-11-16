using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
   public class GetVocationVersionMappingListRP : IAPIRequestParameter
    {

       public int? PageIndex { get; set; }

       public int? PageSize { get; set; }
        public void Validate()
        {
           
        }
    }
}
