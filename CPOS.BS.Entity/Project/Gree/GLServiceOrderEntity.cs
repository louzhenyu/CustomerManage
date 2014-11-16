/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/3 18:46:08
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
    public partial class GLServiceOrderEntity : BaseEntity
    {
        #region 属性集

        #endregion
    }

    public class GLServiceOrderModel : GLServiceOrderEntity
    {
        public string CustomerName { set; get; }
        public string CustomerPhone { set; get; }
        public int? CustomerGender { set; get; }
        public string ProductOrderSN { set; get; }
    }
}