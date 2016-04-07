using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.RetailTrader.Response
{
    public class VipMultiLevelSalerConfigRP : IAPIResponseData
    {
        public VipMultiLevelSalerConfigEntity config{get;set;}
    }

    public class VipMultiLevelSalerConfigEntity
    {
        /// <summary>
        /// 配置主标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 购买金额条件
        /// </summary>
        public string MustBuyAmount { get; set; }
        /// <summary>
        /// 协议文本
        /// </summary>
        public string Agreement { get; set; }
        /// <summary>
        /// 协议URL
        /// </summary>
        // public string col1 { get; set; }

        /// <summary>
        /// 客户标识
        /// </summary>
        public string CustomerId { get; set; }
    }
}
