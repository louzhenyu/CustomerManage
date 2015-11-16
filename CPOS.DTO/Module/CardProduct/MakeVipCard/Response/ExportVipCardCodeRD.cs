using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Response
{
    /// <summary>
    /// 导出卡号响应对象
    /// </summary>
    public class ExportVipCardCodeRD : IAPIResponseData
    {
        /// <summary>
        /// 卡号集合
        /// </summary>
        public List<string> VipCardCodeList { get; set; }
    }
}
