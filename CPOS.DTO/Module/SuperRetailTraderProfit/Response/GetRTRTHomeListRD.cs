using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetRTRTHomeListRD : IAPIResponseData
    {

        /// <summary>
        /// 近30天活跃分销商数量
        /// </summary>
        public int? Day30ActiveRTCount { get; set; }

        /// <summary>
        /// 近30天非活跃分销商数量
        /// </summary>
        public int? Day30NoActiveRTCount { get; set; }
        /// <summary>
        /// 成交分销商数量
        /// </summary>

        public int? Day30SalesRTCount { get; set; }
        /// <summary>
        /// 近30天成交新增下线
        /// </summary>
        public int? Day30SalesExpandRTCount { get; set; }
        /// <summary>
        /// 成交未新增下线
        /// </summary>
        public int? Day30SalesNoExpandRTCount { get; set; }
        /// <summary>
        /// 拓展下线的分销商
        /// </summary>
        public int? Day30ExpandRTCount { get; set; }
        /// <summary>
        /// 拓展下线的分销商成交数量
        /// </summary>
        public int? Day30JoinSalesRTCount { get; set; }
        /// <summary>
        /// 拓展下线的分销商未成交数量
        /// </summary>
        public int? Day30JoinNoSalesRTCount { get; set; }
    }
}
