using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request
{
    /// <summary>
    /// 导出卡号请求对象
    /// </summary>
    public class ExportVipCardCodeRP : IAPIRequestParameter
    {
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo { get; set; }


        public void Validate()
        {

        }
    }
}
