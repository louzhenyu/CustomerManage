using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    /// <summary>
    /// 会员余额请求参数
    /// </summary>
    public class GetVipAmountListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
        public void Validate()
        {

        }
    }
}

