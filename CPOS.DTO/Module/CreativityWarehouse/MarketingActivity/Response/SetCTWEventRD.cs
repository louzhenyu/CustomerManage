using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response
{
    public class SetCTWEventRD : IAPIResponseData
    {
        public string CTWEventId { get; set; }
        public string OnlineQRCodeUrl { get; set; }
        /// <summary>
        /// 线上复制地址
        /// </summary>
        public string OnlineRedirectUrl { get; set; }
        public string OfflineQRCodeUrl { get; set; }
        /// <summary>
        /// 线下复制地址
        /// </summary>
        public string OfflineRedirectUrl { get; set; }


    }
}
