using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;


namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetSuperRetailTraderIncomeAndUnderlingInfoRD : IAPIResponseData
    {
        public IncomeAndUnderlingInfo DayInfo { get; set; }
        public IncomeAndUnderlingInfo MonthInfo { get; set; }
        public IncomeAndUnderlingInfo AllInfo { get; set; }
    }
    public class IncomeAndUnderlingInfo
    {
        public decimal EarningMoney { get; set; }
        public int UnderlingCount { get; set; }
        public decimal Bonus { get; set; }
        public int ContributeUnderling { get; set; } 
    }
}
