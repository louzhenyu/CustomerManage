/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:49
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
    /// 实体： 拜访数据表 
    /// </summary>
    public partial class VisitingTaskDataEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskDataEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public Guid? VisitingTaskDataID { get; set; }

		/// <summary>
		/// 人员编号(关联ClientUser表)
		/// </summary>
		public int? ClientUserID { get; set; }

		/// <summary>
		/// 拜访对象编号(关联门店/经销商等)
		/// </summary>
		public string POPID { get; set; }

		/// <summary>
		/// 拜访任务编号(关联VisitionTask表)
		/// </summary>
		public Guid? VisitingTaskID { get; set; }

		/// <summary>
		/// 进店时间
		/// </summary>
		public DateTime? InTime { get; set; }

		/// <summary>
		/// 进店照片
		/// </summary>
		public string InPic { get; set; }

		/// <summary>
		/// 进店坐标
		/// </summary>
		public string InCoordinate { get; set; }

		/// <summary>
		/// 进店定位类型
		/// </summary>
		public int? InGPSType { get; set; }

		/// <summary>
		/// 出店时间
		/// </summary>
		public DateTime? OutTime { get; set; }

		/// <summary>
		/// 出店照片
		/// </summary>
		public string OutPic { get; set; }

		/// <summary>
		/// 出店定位
		/// </summary>
		public string OutCoordinate { get; set; }

		/// <summary>
		/// 出店定位类型
		/// </summary>
		public int? OutGPSType { get; set; }

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