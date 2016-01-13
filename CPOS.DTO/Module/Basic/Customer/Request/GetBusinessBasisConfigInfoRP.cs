using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Customer.Request
{
    public class GetBusinessBasisConfigInfoRP : IAPIRequestParameter
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerId { get; set; }


        public void Validate()
        {

        }
    }
}
