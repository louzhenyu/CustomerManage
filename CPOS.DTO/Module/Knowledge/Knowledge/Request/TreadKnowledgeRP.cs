using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request
{
    public class TreadKnowledgeRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员

        public void Validate()
        {
            
        }
        public Guid? ID { get; set; }
        #endregion
    }
}
