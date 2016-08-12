using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    public class GetVipIntegralListRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string VipCardCode { get; set; }
        /// <summary>
        /// 0=查看全部{积分}、1=收入{积分}、2=支出{积分}
        /// </summary>
        public int IntegralType { get; set; }
        public void Validate()
        {
        }
    }
}
