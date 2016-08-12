using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetVipIntegralListRD : IAPIResponseData
    {
        public PagedQueryResult<VipIntegralDetailEntity> entity { get; set; }
        public decimal? Total { get; set; }
        public decimal? IncomeAmount { get; set; }
        public decimal? ExpenditureAmount { get; set; }
    }
}
