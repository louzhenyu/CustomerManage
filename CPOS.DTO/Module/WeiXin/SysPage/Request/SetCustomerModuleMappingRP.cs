using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
    public class SetCustomerModuleMappingRP : IAPIRequestParameter
    {
        public string VocaVerMappingID { get; set; }
        public string CustomerID { get; set; }


        public const int NULL_VOCAVERMAPPINGID = 301;
        public const int NULL_CustomerID = 302;
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(VocaVerMappingID.ToString()))
            {
                throw new APIException("VocaVerMappingID不能为空！") { ErrorCode = NULL_VOCAVERMAPPINGID };
            }
            if (string.IsNullOrWhiteSpace(this.CustomerID))
            {
                throw new APIException("CustomerID不能为空！") { ErrorCode = NULL_CustomerID };
            }
        }
    }
}
