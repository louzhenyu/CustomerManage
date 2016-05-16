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
    /// ʵ�壺  
    /// </summary>
    public partial class PanicbuyingKJEventSkuMappingEntity : BaseEntity 
    {
        #region ���Լ�

        #endregion
    }
    public class KJItemSkuInfo
    {
        public string EventSKUMappingId { get; set; }

        public string skuId { get; set; }

        public decimal price { get; set; }

        public decimal BasePrice { get; set; }

        public int SalesCount { get; set; } //��������

        public decimal SalesPrice { get; set; } //�ɽ���

        public string skuProp1 { get; set; } //����1����

        public string skuProp2 { get; set; } //����2����

        public string Stock { get; set; } //���

        public DateTime CreateTime { get; set; }
    }
}