/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/10/21 17:38:06
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
    public partial class CustomerTakeDeliveryEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CustomerTakeDeliveryEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// Id
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// 商户ID
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 备货期
		/// </summary>
		public Int32? StockUpPeriod { get; set; }

		/// <summary>
		/// 门店工作开始时间
		/// </summary>
		public DateTime? BeginWorkTime { get; set; }

		/// <summary>
		/// 门店工作结束时间
		/// </summary>
		public DateTime? EndWorkTime { get; set; }

		/// <summary>
		/// 提货期最长
		/// </summary>
		public Int32? MaxDelivery { get; set; }

		/// <summary>
		/// 状态1启用0停用
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 修改人
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}