using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Response
{
    public class GetCardholderCountRD : IAPIResponseData
    {
        /// <summary>
        /// 持卡人数
        /// </summary>
        public int Count { get; set; }
    }
}
