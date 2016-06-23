using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class CreateQRCodeForSalesRP:IAPIRequestParameter
    {
        public string SkuId { get; set; }
        public string SkuPrice { get; set; }
        public int SkuQty { get; set; }
        public string SuperRetailTraderId { get; set; }
        public void Validate()
        {

        }
    }
}
