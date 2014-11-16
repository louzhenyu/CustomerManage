using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
    public class GetCustomerPageSettingRP:IAPIRequestParameter
    {
        public string PageKey { get; set; }
        public string ApplicationId { get; set; }
        public void Validate()
        {
           
        }
    }
}
