using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Item.QRCode.Response
{
    public class GetQRCodeRD : IAPIResponseData
    {
        public string QRCodeURL { get; set; }
    }
}
