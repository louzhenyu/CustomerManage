using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request
{
    public class EndOfEventRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
        /// <summary>
        /// Game(游戏),Sales(促销)
        /// </summary>
        public string EventType { get; set; }
        public string CTWEventId { get; set; }

    }
}
