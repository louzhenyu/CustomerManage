/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-3 10:30:22
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
    public partial class T_SalesReturnHistoryEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SalesReturnHistoryEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? HistoryID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SalesReturnID { get; set; }

		/// <summary>
		/// 1.申请   2.审核通过   3.审核不通过   4.确认收货   5.拒绝收货   6.退款
		/// </summary>
		public Int32? OperationType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OperationDesc { get; set; }

		/// <summary>
		/// 1.保存取件不成功原因   2.保存修改取件时间 “从某时间改至某时间”
		/// </summary>
		public String HisRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OperatorID { get; set; }

		/// <summary>
		/// 0=会员   1=店员
		/// </summary>
		public Int32? OperatorType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OperatorName { get; set; }

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


        #endregion

    }
}