using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Register.Request
{
    public class MergeVipInfoRP : IAPIRequestParameter
    {
        #region 属性
        public string Mobile { get; set; }
        public string AuthCode { get; set; }
        #endregion

        public void Validate()
        {
            //throw new NotImplementedException();
        }
    }
}
