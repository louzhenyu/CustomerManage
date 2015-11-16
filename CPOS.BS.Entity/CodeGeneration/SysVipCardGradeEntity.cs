/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:25
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
    public partial class SysVipCardGradeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysVipCardGradeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardGradeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardGradeName { get; set; }

		/// <summary>
		/// 会员卡等级(按数值大小来确定等级高低)
		/// </summary>
		public Int32? VipCardGrade { get; set; }

		/// <summary>
		/// 累计充值
		/// </summary>
		public Decimal? AddUpAmount { get; set; }

		/// <summary>
		/// 是否扩展到会员
		/// </summary>
		public Int32? IsExpandVip { get; set; }

		/// <summary>
		/// 充值优惠
		/// </summary>
		public Decimal? PreferentialAmount { get; set; }

		/// <summary>
		/// 消费优惠
		/// </summary>
		public Decimal? SalesPreferentiaAmount { get; set; }

		/// <summary>
		/// 积分倍数
		/// </summary>
		public Decimal? IntegralMultiples { get; set; }

		/// <summary>
		/// 成为会员
		/// </summary>
		public String BeVip { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 期初积分
		/// </summary>
		public Int32? BeginIntegral { get; set; }

		/// <summary>
		/// 期末积分
		/// </summary>
		public Int32? EndIntegral { get; set; }

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
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }


        #endregion

    }
}