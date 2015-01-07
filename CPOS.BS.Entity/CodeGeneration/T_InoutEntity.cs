/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:14
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
    public partial class T_InoutEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_InoutEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String order_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String order_no { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String order_type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String order_reason_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String red_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ref_order_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ref_order_no { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String warehouse_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String order_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String request_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String complete_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String related_unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String related_unit_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String pos_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String shift_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String sales_user { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? total_amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? discount_rate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? actual_amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? receive_points { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? pay_points { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String pay_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? print_times { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String carrier_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status_desc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? total_qty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? total_retail { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? keep_the_change { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? wiping_zero { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String vip_no { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String approve_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String approve_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String send_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String send_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String accpect_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String accpect_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String data_from_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String sales_unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String purchase_unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String if_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String bat_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String sales_warehouse_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String purchase_warehouse_id { get; set; }

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
		public String paymentcenter_id { get; set; }

        /// <summary>
        /// 支付方式号码
        /// Payment_Type_Code
        /// </summary>
        public String Payment_Type_Code { get; set; }

        /// <summary>
        /// 支付方式名称
        /// Payment_Type_Name
        /// </summary>
        public String Payment_Type_Name { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal? ReturnCash { get; set; }

        #endregion

    }
}