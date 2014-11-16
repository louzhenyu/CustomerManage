using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class GetOrderListRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 订单状态 
        /// </summary>
        public int[] OrderStatuses { get; set; }

        //订单ID
        public string OrderID {get;set; }
        /// <summary>
        /// 返回的订单商品结果集中是否要包含订单商品明细。如果不填，则默认为false。
        /// </summary>
        public bool IsIncludeOrderDetails { get; set; }

        /// <summary>
        /// 每页记录数，默认15
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码，默认0
        /// </summary>
        public int PageIndex { get; set; }
        #endregion

        public void Validate()
        {
           
        }
    }
}
