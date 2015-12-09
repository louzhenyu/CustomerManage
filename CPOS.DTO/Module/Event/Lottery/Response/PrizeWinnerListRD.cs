using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.Event.Lottery.Response
{
    public class PrizeWinnerListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public List<WinnerInfo> WinnerList { get; set; }
    }
    public class WinnerInfo
    {
        /// <summary>
        /// 中奖人员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 奖品等级
        /// </summary>
        public string PrizeLevel { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string PrizeName { get; set; }
    }
}
