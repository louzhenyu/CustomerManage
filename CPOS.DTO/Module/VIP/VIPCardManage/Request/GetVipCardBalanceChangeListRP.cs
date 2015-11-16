using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request
{
    public class GetVipCardBalanceChangeListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 页码。为空则默认为0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数。为空则为15
        /// </summary>
        public int PageSize { get; set; }

        public void Validate() { }
    }
}
