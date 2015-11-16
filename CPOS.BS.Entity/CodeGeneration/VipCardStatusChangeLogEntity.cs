/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 19:35:02
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
    public partial class VipCardStatusChangeLogEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardStatusChangeLogEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String LogID { get; set; }

		/// <summary>
		/// 会员卡标识
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// 会员卡状态标识
		/// </summary>
		public Int32? VipCardStatusID { get; set; }

		/// <summary>
		/// 门店标识
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 操作动作
		/// </summary>
		public String Action { get; set; }

		/// <summary>
		/// 原因
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// 图片URL
		/// </summary>
		public String PicUrl { get; set; }

		/// <summary>
		/// 原状态标识
		/// </summary>
		public Int32? OldStatusID { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }


        #endregion

    }
}