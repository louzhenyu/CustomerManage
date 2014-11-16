/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/22 16:57:28
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
    /// 实体： 拜访任务 
    /// </summary>
    public partial class VisitingTaskEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public Guid? VisitingTaskID { get; set; }

		/// <summary>
		/// 拜访任务编号
		/// </summary>
		public string VisitingTaskNo { get; set; }

		/// <summary>
		/// 拜访任务名称
		/// </summary>
		public string VisitingTaskName { get; set; }

		/// <summary>
		/// 拜访任务名称(英文)
		/// </summary>
		public string VisitingTaskNameEn { get; set; }

		/// <summary>
		/// 拜访任务类型(1-日常拜访,2-协访,3-店检)
		/// </summary>
		public int? VisitingTaskType { get; set; }

		/// <summary>
		/// 执行人员职位编号(关联ClientPosition表)
		/// </summary>
		public string ClientPositionID { get; set; }

		/// <summary>
		/// 拜访对象(1-终端,2-经销商)
		/// </summary>
		public int? POPType { get; set; }

		/// <summary>
		/// 终端分组定义编号(关联POPGroup表)
		/// </summary>
		public int? POPGroupID { get; set; }

		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 开始需要执行的定位类型(1-基站定位,2-GPS定位,3-混合定位)
		/// </summary>
		public int? StartGPSType { get; set; }

		/// <summary>
		/// 结束需要执行的定位类型(1-基站定位,2-GPS定位,3-混合定位)
		/// </summary>
		public int? EndGPSType { get; set; }

		/// <summary>
		/// 开始需要拍照(0-否,1-是)
		/// </summary>
		public int? StartPic { get; set; }

		/// <summary>
		/// 结束需要拍照(0-否,1-是)
		/// </summary>
		public int? EndPic { get; set; }

		/// <summary>
		/// 任务优先级
		/// </summary>
		public int? TaskPriority { get; set; }

		/// <summary>
		/// 是否合并操作(0-否,1-是)
		/// </summary>
		public int? IsCombin { get; set; }

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