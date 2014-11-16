/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:15
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
    public partial class T_Inout_DetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Inout_DetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String order_detail_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String order_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ref_order_detail_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String sku_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? order_qty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? enter_qty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? enter_price { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? enter_amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? std_price { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? discount_rate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? retail_price { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? retail_amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? plan_price { get; set; }

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
		public String remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String pos_order_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String order_detail_status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? display_index { get; set; }

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
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ref_order_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? if_flag { get; set; }

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