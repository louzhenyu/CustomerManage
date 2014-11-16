/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 13:31:25
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
    public partial class OrderIntegralEntity : BaseEntity
    {
        #region 属性集

        /// <summary>
        /// 商品名称
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string VipCode { get; set; }
        /// <summary>
        /// 下单时间格式化
        /// </summary>
        public string CreateTimeFormat
        {
            get
            {
                return CreateTime == null ? "" : DateTime.Parse(CreateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        #endregion
    }
}