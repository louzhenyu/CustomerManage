/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-06-08 20:59:54
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
    public partial class t_unitEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public t_unitEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_name_short { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_city_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_contact { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_tel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_fax { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_postcode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CUSTOMER_LEVEL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status_desc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String bat_id { get; set; }

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
		public String longitude { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String dimension { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String imageURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ftpImagerURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String webserversURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String weiXinId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String dimensionalCodeURL { get; set; }


        #endregion

    }
}