using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Request
{
   public class GetMobileBusinessDefinedRP : IAPIRequestParameter
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string MobileModuleID { get; set; }

        public void Validate()
        {
            if (this.Page < 1)
            {
                Page = 1;
            }
            if (this.PageSize < 15)
            {
                PageSize = 15;
            }
        }
    }
}
