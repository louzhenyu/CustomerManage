using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
    public class ChangeCTWEventStatusRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
        public string CTWEventId { get; set; }
        /// <summary>
        /// 10=待发布,20=运行中,30=暂停,40=结束
        /// </summary>
        public int Status { get; set; }

    }
}
