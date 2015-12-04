using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Lottery.Response
{
    public class GetPrizeLocationListRD : IAPIResponseData
    {
       
            public string EventID { get; set; }
            public string ErrMsg { get; set; }
            public List<LPrizeLocationEntity> PrizeLocationList { get; set; }
       
    }
}
