/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 11:41:01
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
    public partial class HotelRoomEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public HotelRoomEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String RoomId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RoomName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? StandardPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxIntegralToCurrency { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DonateIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReturnCurrency { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MinBookPeriod { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxBookPeriod { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CommitPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CommitType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ValidBeginDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ValidEndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? RoomQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDisplay { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}