using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetRTTopListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 1=30天销售排名 2=30天新增下线排名
        /// </summary>
        public string BusiType { get; set; }
        public void Validate() { }
    }
}
