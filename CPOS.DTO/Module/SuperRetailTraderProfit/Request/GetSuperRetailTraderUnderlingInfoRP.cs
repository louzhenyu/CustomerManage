using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetSuperRetailTraderUnderlingInfoRP : IAPIRequestParameter
    {
        /// <summary>
        /// 页码  从1开始
        /// </summary>
        public int Page { get; set; }
        public int Size { get; set; }
        public string SuperRetailTraderId { get; set; }
        public void Validate() { }
    }
}
