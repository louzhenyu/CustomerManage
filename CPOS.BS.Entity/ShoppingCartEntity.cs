/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/23 17:47:45
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
    public partial class ShoppingCartEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// ItemId
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// ItemDetail
        /// </summary>
        public VwItemDetailEntity ItemDetail { get; set; }
        /// <summary>
        /// DisplayIndex
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// GG
        /// </summary>
        public string GG { get; set; }

        public int DayCount { get; set; }

        public decimal SalesPrice { get; set; }
        #endregion
        public decimal EveryoneSalesPrice { get; set; } // 人人销售价 add by donal 2014-9-23 11:06:07
    }
}