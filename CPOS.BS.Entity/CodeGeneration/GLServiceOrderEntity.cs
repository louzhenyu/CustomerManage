/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/6 16:00:15
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
    public partial class GLServiceOrderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GLServiceOrderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ServiceOrderID { get; set; }

		/// <summary>
		/// 服务类型
		/// </summary>
		public Int32? ServiceType { get; set; }

		/// <summary>
		/// 服务时间
		/// </summary>
		public DateTime? ServiceDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ServiceDateEnd { get; set; }

		/// <summary>
		/// 服务地址
		/// </summary>
		public String ServiceAddress { get; set; }

		/// <summary>
		/// 经度
		/// </summary>
		public Decimal? Latitude { get; set; }

		/// <summary>
		/// 纬度
		/// </summary>
		public Decimal? Longitude { get; set; }

		/// <summary>
		/// 客户留言
		/// </summary>
		public String CustomerMessage { get; set; }

		/// <summary>
		/// 关联订单(外键)
		/// </summary>
		public String ProductOrderID { get; set; }

		/// <summary>
		/// CustomerID
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 最后更新人
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 删除标识
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}