/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/4/15 11:46:36
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
    public partial class T_ItemSkuPropEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_ItemSkuPropEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ItemSkuPropID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Item_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public DateTime modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }



        public String ItemSku_prop_1_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ItemSku_prop_2_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ItemSku_prop_3_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ItemSku_prop_4_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ItemSku_prop_5_id { get; set; }
        #endregion

    }
}