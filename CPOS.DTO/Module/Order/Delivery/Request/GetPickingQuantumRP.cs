using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Delivery.Request
{
    public class GetPickingQuantumRP : IAPIRequestParameter
    {
        /// <summary>
        /// 允许的开始提货日期
        /// </summary>
        public string BeginDate { get; set; }
        /// <summary>
        /// 选择提货日期
        /// </summary>
        public string PickingDate { get; set; }

        public void Validate() { }
    }
}
