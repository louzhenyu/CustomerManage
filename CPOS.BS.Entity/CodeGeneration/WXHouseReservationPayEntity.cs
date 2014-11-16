/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 10:16:11
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
    public partial class WXHouseReservationPayEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXHouseReservationPayEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ReservationPayID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? PrePaymentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SeqNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Fundtype { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? FundState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Merchantdate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Hatradedt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HaorderNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Retcode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Retmsg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}