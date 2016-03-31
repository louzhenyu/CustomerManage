using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Delivery.Response
{
    public class GetPickingDateRD : IAPIResponseData
    {
        public string BeginDate { get; set; }

        public string EndDate { get; set; }


        //是否显示时间段：0-都不现实 1-显示日期  2-都显示
        public int IsDisplay { get; set; }  
    }
}
