using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request
{
    public class GetKnowledgeDetailRP : IAPIRequestParameter
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public Guid? ID { get; set; }

        public void Validate()
        {
            if (ID == null)
                throw new Exception("ID不能为NULL");
        }
    }
}
