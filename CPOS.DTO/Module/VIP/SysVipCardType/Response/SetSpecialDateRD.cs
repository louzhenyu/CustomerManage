using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.SysVipCardType.Response
{
    public class SetSpecialDateRD : IAPIResponseData
    {
        /// <summary>
        /// 卡类别ID
        /// </summary>
        public int? VipCardTypeID { get; set; }
    }
}
