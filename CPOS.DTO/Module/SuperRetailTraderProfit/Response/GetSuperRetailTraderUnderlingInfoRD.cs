using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetSuperRetailTraderUnderlingInfoRD : IAPIResponseData
    {
        public string SuperRetailTraderId { get; set; }
        public List<UnderlingInfo> UnderlingInfo { get; set; }
    }
    public class UnderlingInfo
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string HeadImgUrl { get; set; }

        public string JoinDate { get; set; }
        /// <summary>
        /// 恭喜金额
        /// </summary>
        public decimal Bonus { get; set; }
        /// <summary>
        /// 下线人数
        /// </summary>
        public int UnderlingCount { get; set; }

    }
}
