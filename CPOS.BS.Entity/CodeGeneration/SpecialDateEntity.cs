/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    public partial class SpecialDateEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SpecialDateEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? SpecialId { get; set; }

		/// <summary>
		/// 卡类型标识
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// 假日标识
		/// </summary>
		public String HolidayID { get; set; }

		/// <summary>
		/// 不可用积分
		/// </summary>
		public Int32? NoAvailablePoints { get; set; }

		/// <summary>
		/// 不可用折扣
		/// </summary>
		public Int32? NoAvailableDiscount { get; set; }

		/// <summary>
		/// 不可回馈积分
		/// </summary>
		public Int32? NoRewardPoints { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public String Desciption { get; set; }

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