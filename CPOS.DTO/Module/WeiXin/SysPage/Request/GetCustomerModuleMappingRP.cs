using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
   public class GetCustomerModuleMappingRP:IAPIRequestParameter
    {

       public string CustomerId { get; set; }  //客户ID


        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.CustomerId))
            {
                throw new APIException(string.Format("客户ID不能为空")) {ErrorCode=301 };
            }
        }
    }
}
