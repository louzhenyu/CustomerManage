using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request
{
    public class KeepKnowledgeRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员
        public Guid? ID { get; set; }  
        public void Validate()
        {
           
        }

        #endregion
    }
}
