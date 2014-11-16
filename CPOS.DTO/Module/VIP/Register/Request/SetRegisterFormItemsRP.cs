using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Register.Request
{
    public class SetRegisterFormItemsRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
        public CommitFormItemInfo[] ItemList { get; set; }
        public string ObjectId { get; set; }

        //public int ValidFlag { get; set; }
        
    }
    public class CommitFormItemInfo
    {
        public string ID { get; set; }
        public bool? IsMustDo { get; set; }
        public string Value { get; set; }
    }
}
