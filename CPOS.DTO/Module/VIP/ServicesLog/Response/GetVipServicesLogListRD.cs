using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.ServicesLog.Response
{
    public class GetVipServicesLogListRD:IAPIResponseData
    {

        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        public VipServicesLogInfo[] VipServicesLogList { get; set; }
    }
    public class VipServicesLogInfo
    {
        public string ServicesLogID { get; set; }
        public string ServicesTime { get; set; }
        public string ServicesMode { get; set; }
   
        public string UnitName { get; set; }

        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
