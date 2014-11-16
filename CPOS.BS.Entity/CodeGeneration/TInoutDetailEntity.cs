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
    public partial class TInoutDetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInoutDetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
        public String OrderDetailID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String order_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RefOrderDetailID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SkuID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OrderQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? EnterQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? EnterPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? EnterAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? StdPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DiscountRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RetailPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RetailAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PlanPrice { get; set; }

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
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PosOrderCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDetailStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

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
		public String ModifyTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModifyUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RefOrderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IfFlag { get; set; }

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


        #endregion

    }
}