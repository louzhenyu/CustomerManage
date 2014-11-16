/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/7 11:51:59
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
    /// 实体： 拜访路线定义 
    /// </summary>
    public partial class RouteEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public RouteEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自增编号
		/// </summary>
		public Guid? RouteID { get; set; }

		/// <summary>
		/// 路线编号
		/// </summary>
		public string RouteNo { get; set; }

		/// <summary>
		/// 路线名称
		/// </summary>
		public string RouteName { get; set; }

		/// <summary>
		/// 路线状态(1-启用,2-停止)
		/// </summary>
		public int? Status { get; set; }

		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 终端类型(1-门店,2-经销商)
		/// </summary>
		public int? POPType { get; set; }

		/// <summary>
		/// 距离
		/// </summary>
		public decimal? Distance { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? TripMode { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete { get; set; }


        #endregion

    }
}