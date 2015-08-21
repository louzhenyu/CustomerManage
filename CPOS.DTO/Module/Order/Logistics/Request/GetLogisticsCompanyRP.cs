using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Logistics.Request
{
    public class GetLogisticsCompanyRP : IAPIRequestParameter
    {
        /// <summary>
        /// (1=系统默认；2=商户增加)
        /// </summary>
        public int IsSystem{get;set;}
        /// <summary>
        /// 配送商名称
        /// </summary>
        public string LogisticsName { get; set; }

        public void Validate()
        {

        }
    }
}
