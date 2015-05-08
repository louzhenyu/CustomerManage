/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-4-11 11:18:23
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
    public partial class T_CityEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CityEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String city_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city1_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city2_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city3_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city_code { get; set; }


        #endregion

    }
}