using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request
{
    public class DelSysVipCardTypeRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public int VipCardTypeID { get; set; }
        public void Validate()
        {

        }
    }
}
