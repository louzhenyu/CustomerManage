/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-6-18 14:44:14
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
    /// ʵ�壺  
    /// </summary>
    public partial class X_ActivityPrizesEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public X_ActivityPrizesEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? PrizesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PrizesName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageUrl { get; set; }

		/// <summary>
		/// Ϊ�մ���Ϊ���н�
		/// </summary>
		public Guid? CouponTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TotalCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WeekCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? RemainingQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LowestPointLimit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UsePoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Probability { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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


        #endregion

    }
}