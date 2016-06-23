/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/28 10:12:43
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
    public partial class T_SuperRetailTraderSkuMappingEntity : BaseEntity 
    {
        #region 属性集
        public string ItemName { get; set; }
        public string PropName1 { get; set; }

        public string PropName2 { get; set; }

        public decimal SalesPrice { get; set; }

        public int IsSelected { get; set; }
        #endregion
    }
}