using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Request
{
    public class GetBargainDetailsRP : IAPIRequestParameter
    {
        public string EventItemMappingID { get; set; }
        public string ItemId { get; set; }
        public void Validate()
        {

        }
    }
}
