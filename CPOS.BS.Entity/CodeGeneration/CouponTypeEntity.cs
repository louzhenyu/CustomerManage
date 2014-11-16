/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
    public partial class CouponTypeEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CouponTypeEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public string CouponTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CouponTypeName { get; set; }

        /// <summary>
        /// 面值
        /// </summary>
        public Decimal? ParValue { get; set; }

        /// <summary>
        /// 折扣，大于0小于1的小数
        /// </summary>
        public Decimal? Discount { get; set; }

        /// <summary>
        /// 条件值，订单总额满足条件值才能使用
        /// </summary>
        public Decimal? ConditionValue { get; set; }

        /// <summary>
        /// 是否可以叠加使用
        /// </summary>
        public Int32? IsRepeatable { get; set; }

        /// <summary>
        /// 是否可以与其它优惠券混合使用
        /// </summary>
        public Int32? IsMixable { get; set; }

        /// <summary>
        /// 优惠券来源
        /// </summary>
        public String CouponSourceID { get; set; }

        /// <summary>
        /// 优惠券有效期，单位：分钟，为0时永久有效
        /// </summary>
        public Int32? ValidPeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }


        #endregion

    }
}