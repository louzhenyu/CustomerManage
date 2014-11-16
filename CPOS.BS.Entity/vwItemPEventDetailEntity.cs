/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 17:25:36
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
    public partial class vwItemPEventDetailEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription{get;set;}
        /// <summary>
        /// 商品业务分类标识(1=团；2=充；3=券)
        /// </summary>
        public int ItemSortId { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int SalesCount { get; set; }
        #endregion
    }
}