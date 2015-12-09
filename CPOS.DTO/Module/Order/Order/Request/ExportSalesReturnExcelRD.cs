using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class ExportSalesReturnExcelRD : IAPIRequestParameter
    {

        /// <summary>
        /// 状态(待退款：1，已完成：2)
        /// </summary>
        public int Status { get; set; }
       public void Validate()
       {

       }
    }
}
