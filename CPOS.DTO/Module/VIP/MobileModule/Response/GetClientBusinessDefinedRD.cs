using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Response
{
    public class GetClientBusinessDefinedRD : IAPIResponseData
    {
        public ClientBunessDefinedSubInfo[] BasicItems { get; set; }
        public ClientBunessDefinedSubInfo[] ExtendItems { get; set; }
        //public ClientBunessDefinedSubInfo[] SeniorItems { get; set; }
        public int TotalRow { get; set; }
    }

    public class ClientBunessDefinedSubInfo
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public int ControlType { get; set; }

        public string ColumnDesc { get; set; }

        public string CorrelationValue { get; set; }

        public int ListOrder { get; set; }

        public int AttributeType { get; set; }

    }
}
