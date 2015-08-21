using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Logistics.Response
{
    public class GetLogisticsCompanyRD : IAPIResponseData
    {
        public List<LogisticsCompanyInfo> LogisticsCompanyList { get; set; }
    }
    public class LogisticsCompanyInfo
    {
        public Guid? LogisticsID { get; set; }
        public string LogisticsName { get; set; }
        public string LogisticsShortName { get; set; }

    }
}
