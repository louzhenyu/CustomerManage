using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetRTTopListRD : IAPIResponseData
    {
        public List<RSRTRTTopInfo> rsrtrttopinfo { get; set; }
    }

    public class RSRTRTTopInfo
    {
        /// <summary>
        /// 排名
        /// </summary>
        public int? Idx { get; set; }
        /// <summary>
        /// 分销商名称
        /// </summary>
        public string SuperRetailTraderName { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? SalesAmount { get; set; }
        /// <summary>
        /// 主标识
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string dataTime { get; set; }
        /// <summary>
        /// 分销商Id
        /// </summary>
        public Guid? SuperRetailTraderID { get; set; }
        /// <summary>
        /// 新增下线人数
        /// </summary>
        public int? AddRTCount { get; set; }
    }
}
