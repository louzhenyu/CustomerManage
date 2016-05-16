/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/27 13:54:52
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{

    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class PanicbuyingKJEventSkuMappingEntity : BaseEntity 
    {
        #region 属性集

        #endregion
    }
    public class KJItemSkuInfo
    {
        public string EventSKUMappingId { get; set; }

        public string skuId { get; set; }

        public decimal price { get; set; }

        public decimal BasePrice { get; set; }

        public int SalesCount { get; set; } //销售数量

        public decimal SalesPrice { get; set; } //成交价

        public string skuProp1 { get; set; } //属性1名称

        public string skuProp2 { get; set; } //属性2名称

        public string Stock { get; set; } //库存

        public DateTime CreateTime { get; set; }
    }
}