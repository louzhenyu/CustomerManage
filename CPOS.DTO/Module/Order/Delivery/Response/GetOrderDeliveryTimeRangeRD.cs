using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Delivery.Response
{
   public class GetOrderDeliveryTimeRangeRD:IAPIResponseData
    {
       public DateInfo[] DateRange { get; set; }
    }

   public class DateInfo
   {
       public string Date { get; set; }
       public DateTimeRangeInfo[] Ranges { get; set; }
   }
   public class DateTimeRangeInfo 
   {
       /// <summary>
       /// 开始时间
       /// </summary>
       public string BeginTime { get; set; }

       /// <summary>
       /// 结束时间
       /// </summary>
       public string EndTime { get; set; }

       /// <summary>
       /// 时间描述
       /// </summary>
       public string Desc { get; set; }
   }
}
