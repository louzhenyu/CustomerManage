using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using Microsoft.SqlServer.Server;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Response
{
    public class GetMobileBusinessDefinedRD : IAPIResponseData
    {
        public int TotalRow { get; set; }

        public MobileBunessDefinedSubInfo[] Items { get; set; }

        public string Msg { get; set; }
    }

    public class MobileBunessDefinedSubInfo
    {
        //public string MobileBunessDefinedID { get; set; }

        public string ColumnName { get; set; }

        public int ControlType { get; set; }

        public string ColumnDesc { get; set; }

        public string CorrelationValue { get; set; }

        public int ListOrder { get; set; }

        public int IsMustDo { get; set; }
    }
}
