using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.MailReport.Response
{
    public class GetAllRankRD : IAPIResponseData
    {
        public GetGoodsRankListRD GoodsRankList { get; set; }
        public GetLast30DaysTransformRD Last30DaysTransform { get; set; }
        public GetLast7DaysOperationDataRD Last7DaysOperationData { get; set; }
    }
}
