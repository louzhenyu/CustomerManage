/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/7/6 16:12:43
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
    public partial class T_Item_Price_TypeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Item_Price_TypeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String item_price_type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_Price_type_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_price_type_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

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


        #endregion

    }
}