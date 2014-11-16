using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
   public class GetOrderActionsRP:IAPIRequestParameter
    {
        #region 属性
       /// <summary>
       /// 订单ID
       /// </summary>
        public string OrderID { get; set; }
        #endregion
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.OrderID))
            {
                throw new APIException(string.Format("订单ID不能为空"));
            }
        }
    }
}
