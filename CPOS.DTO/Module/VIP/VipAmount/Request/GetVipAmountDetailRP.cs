using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipAmount.Request
{
    public class GetVipAmountDetailRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员ID[后台接口使用]
        /// </summary>
        public string VipId { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 类型（1=余额;2=返现;3=红利明细）
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 1=全部;2=收入;3=提现
        /// </summary>
        public int DdividendType { get; set; }
        /// <summary>
        /// 页码。为空则默认为0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数。为空则为10
        /// </summary>
        public int PageSize { get; set; }

        public void Validate() { }
    }
}
