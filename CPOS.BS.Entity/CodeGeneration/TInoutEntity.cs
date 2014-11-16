/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 14:14:45
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
    public partial class TInoutEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInoutEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String OrderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderReasonID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RedFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RefOrderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RefOrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WarehouseID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RequestDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CompleteDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateUnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RelatedUnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RelatedUnitCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PosID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ShiftID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesUser { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DiscountRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ActualAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReceivePoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PayPoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PrintTimes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CarrierID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StatusDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalRetail { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? KeepTheChange { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WipingZero { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ApproveTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ApproveUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SendTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SendUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AccpectTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AccpectUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModifyTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModifyUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DataFromID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesUnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PurchaseUnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IfFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BatID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesWarehouseID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PurchaseWarehouseID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field7 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field9 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field10 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field11 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field12 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field13 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field14 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field15 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field16 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field17 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field18 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field19 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Field20 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PaymentcenterID { get; set; }


        #endregion

    }
}