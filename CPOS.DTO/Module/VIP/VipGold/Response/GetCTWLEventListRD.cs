using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetCTWLEventListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }

        public List<CTWLEventInfo> CTWLEventInfoList { get; set; }
    }
    public class CTWLEventInfo {
        public string CTWEventId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        //线下二维码ImageID
        public string OnfflineQRCodeId { get; set; }
        public string OnfflineQRCodeUrl { get; set; }
    }
}
