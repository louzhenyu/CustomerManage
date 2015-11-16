/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:27
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
    public partial class VipCardGradeChangeLogEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardGradeChangeLogEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ChangeLogID { get; set; }

		/// <summary>
		/// 会员卡标识
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// 变动前等级
		/// </summary>
		public Int32? ChangeBeforeGradeID { get; set; }

		/// <summary>
		/// 现等级
		/// </summary>
		public Int32? NowGradeID { get; set; }

		/// <summary>
		/// 变动原因
		/// </summary>
		public String ChangeReason { get; set; }

		/// <summary>
		/// 操作类型(1=自动,2=手动）
		/// </summary>
		public Int32? OperationType { get; set; }

		/// <summary>
		/// 变动时间
		/// </summary>
		public DateTime? ChangeTime { get; set; }

		/// <summary>
		/// 操作门店
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 操作员工
		/// </summary>
		public String OperationUserID { get; set; }

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
		/// 客户标识
		/// </summary>
		public String CustomerID { get; set; }


        #endregion

    }
}