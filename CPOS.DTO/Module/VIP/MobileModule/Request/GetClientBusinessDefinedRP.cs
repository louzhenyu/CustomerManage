using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Request
{
    public class GetClientBusinessDefinedRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        //public string CustomerID { get; set; }

        public int Type { get; set; }

        public void Validate()
        {
            if (this.PageIndex < 0)
            {
                PageIndex = 0;
            }
            if (this.PageSize < 15)
            {
                PageSize = 15;
            }
        }
    }
}
