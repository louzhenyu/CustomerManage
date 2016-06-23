using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetSuperRetailTraderOrderInfoRP : IAPIRequestParameter
    {
        public string SuperRetailTraderId { get; set; }
        /// <summary>
        /// 页码  从1开始
        /// </summary>
        public int Page { get; set; }
        public int Size { get; set; }
        public void Validate() { }

    }
}
