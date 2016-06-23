using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class GetLoginInfoRP : IAPIRequestParameter
    {
        /// <summary>
        /// 商户编码
        /// </summary>
        public string SuperRetailTraderCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        public void Validate()
        {
      
        }
    }
}