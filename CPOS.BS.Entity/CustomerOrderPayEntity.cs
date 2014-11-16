/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:10:21
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
    public partial class CustomerOrderPayEntity : BaseEntity
    {
        #region 属性集
        public string OrderNo { set; get; }
        public DateTime PayTimeBegin { set; get; }
        public DateTime PayTimeEnd { set; get; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        #endregion
    }
}