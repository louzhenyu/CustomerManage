using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Item.QRCode.Request
{
    public class GetQRCodeRP : IAPIRequestParameter
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public void Validate()
        {

        }
    }
}
