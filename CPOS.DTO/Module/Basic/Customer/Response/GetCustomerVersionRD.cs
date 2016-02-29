using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Customer.Response
{
    public class GetCustomerVersionRD : IAPIResponseData
    {
        public string VersionValue { get; set; }
    }
}
