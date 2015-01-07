using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Response
{
    public class SetSignInRD : IAPIResponseData
    {
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public RoleCodeInfo[] RoleCodeList { get; set; }
        //用户汇集店ID
        public string UnitId { get; set; }
    }

    public class RoleCodeInfo
    {
        public string RoleCode { get; set; }
 
    }
}
