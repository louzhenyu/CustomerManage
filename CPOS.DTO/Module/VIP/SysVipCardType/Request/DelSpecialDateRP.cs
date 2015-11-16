using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request
{
    public class DelSpecialDateRP : IAPIRequestParameter
    {
        /// <summary>
        /// 特殊日期删除
        /// </summary>
        public Guid SpecialID { get; set; }
        public void Validate()
        {

        }
    }
}
