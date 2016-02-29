using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.ServicesLog.Request
{
    public class GetVipAmountdetailListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 叶大小
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int pageIndex { get; set; }
        public void Validate()
        {

        }
    }
}
