using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class GetOrderListByUserIdRP:IAPIRequestParameter
    {      
        #region 属性
        //订单ID
        public string OrderID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 每页记录数，默认15
        /// </summary>
        public int PageSize { get; set; }

        public OrderStatusInfo[] OrderStatus { get; set; }
        public string OrderChannelID { get; set; }
        /// <summary>
        /// 页码，默认0
        /// </summary>
        public int PageIndex { get; set; }
        #endregion
        public void Validate()
        {
        }

    }
    public class OrderStatusInfo
    {
        public int? Status { get; set; }
    }
}
