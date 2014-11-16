using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request
{
    public class GetMaterialTextDetailRP : IAPIRequestParameter
    {
        public string InterfaceHost { get; set; }
        public string CustomerId { get; set; }
        public string TextId { get; set; }
        public void Validate()
        {
           
        }
    }
}
