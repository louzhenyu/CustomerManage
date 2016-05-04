/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/7/6 16:12:44
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
    public partial class T_SkuEntity : BaseEntity 
    {
        #region 属性集

        #endregion
    }
    /// <summary>
    /// 活动商品 规格1信息
    /// </summary>
    public class  prop1Info 
    {
        public string skuId { get; set; }

        public string prop1DetailId { get; set; }

        public string prop1DetailName { get; set; }

        public int stock { get; set; }

        public int salesCount { get; set; }
    }
}