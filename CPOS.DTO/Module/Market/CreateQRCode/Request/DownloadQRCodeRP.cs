using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Market.CreateQRCode.Request
{
    public class DownloadQRCodeRP : IAPIRequestParameter
    {
        
        public int VipDCode { get; set; }
        public string unitId { get; set; }
        public void Validate()
        {
        }
    }
}
