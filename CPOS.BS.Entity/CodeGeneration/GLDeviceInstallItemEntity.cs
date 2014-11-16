/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/4 11:56:05
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
    public partial class GLDeviceInstallItemEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GLDeviceInstallItemEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String DeviceInstallID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeviceItemID { get; set; }

		/// <summary>
		/// 设备全名
		/// </summary>
		public String DeviceFullName { get; set; }

		/// <summary>
		/// 安装位置
		/// </summary>
		public Int32? InstallPosition { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ServiceOrderID { get; set; }

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