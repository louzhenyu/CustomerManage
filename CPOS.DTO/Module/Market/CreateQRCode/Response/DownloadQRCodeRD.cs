using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Market.CreateQRCode.Response
{
    public class DownloadQRCodeRD : IAPIResponseData
    {
        public string imageUrl { get; set; }
    }
}
