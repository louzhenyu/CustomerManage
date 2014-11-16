using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Response
{
     public class GetOrderActionsRD:IAPIResponseData
     {
         public OrderActionInfo[] Actions { get; set; }
     }
     public class OrderActionInfo 
     {
         /// <summary>
         ///操作码 当前使用订单状态码作为订单操作码，后台以此作为业务逻辑判断分支的条件
         /// </summary>
         public string ActionCode { get; set; }
         /// <summary>
         /// 操作文本
         /// </summary>
         public String Text { get; set; }
     }
}
