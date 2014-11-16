using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Order.Request
{
    /// <summary>
    /// 4.4.4	获取会员个人中心订单列表请求内容
    /// </summary>
    public class GetOrdersRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 分组方式。1=待付款;2=待收货/提货;3=已完成
        /// </summary>
        public int GroupingType { get; set; }
        /// <summary>
        /// 页码。为空则默认为0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数。为空则为15
        /// </summary>
        public int PageSize { get; set; }

        #endregion

        public void Validate()
        {

        }
    }
}
