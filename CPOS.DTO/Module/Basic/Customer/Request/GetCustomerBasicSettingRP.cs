using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Customer.Request
{
    public  class GetCustomerBasicSettingRP : IAPIRequestParameter
    {
        /// <summary>
        /// 商户配置编号
        /// </summary>
        public string SettingCode { get; set; }


        public void Validate()
        {

        }
    }
}
