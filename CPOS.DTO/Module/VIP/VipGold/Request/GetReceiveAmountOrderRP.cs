using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetReceiveAmountOrderRP : IAPIRequestParameter
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 类型 1 - 收款订单 2 - 充值订单 3 - 售卡订单
        /// </summary>
        public int Type { get; set; }
        public void Validate() { }
    }
}
