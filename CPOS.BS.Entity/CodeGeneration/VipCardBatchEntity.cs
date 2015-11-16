/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 20:05:31
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
    /// 实体： 批量制卡 
    /// </summary>
    public partial class VipCardBatchEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardBatchEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? BatchID { get; set; }

		/// <summary>
		/// 批次
		/// </summary>
		public Int32? BatchNo { get; set; }

		/// <summary>
		/// 卡介质（IC/ID）
		/// </summary>
		public String CardMedium { get; set; }

		/// <summary>
		/// 地区编号
		/// </summary>
		public String RegionNumber { get; set; }

		/// <summary>
		/// 卡前缀
		/// </summary>
		public String CardPrefix { get; set; }

		/// <summary>
		/// 卡类型
		/// </summary>
		public String VipCardTypeCode { get; set; }

		/// <summary>
		/// 开始卡号
		/// </summary>
		public String StartCardNo { get; set; }

		/// <summary>
		/// 结束卡号
		/// </summary>
		public String EndCardNo { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public Int32? Qty { get; set; }

		/// <summary>
		/// 异常数量
		/// </summary>
		public Int32? OutliersQty { get; set; }

		/// <summary>
		/// 成本价
		/// </summary>
		public Decimal? CostPrice { get; set; }

		/// <summary>
		/// 导出计数
		/// </summary>
		public Int32? ExportCount { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String ImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ImportQty { get; set; }


        #endregion

    }
}