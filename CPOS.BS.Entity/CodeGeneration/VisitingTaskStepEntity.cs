/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:50
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
    /// 实体： 拜访步骤信息明细 
    /// </summary>
    public partial class VisitingTaskStepEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskStepEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public Guid? VisitingTaskStepID { get; set; }

		/// <summary>
		/// 任务编号(关联VisitingTask表)
		/// </summary>
		public Guid? VisitingTaskID { get; set; }

		/// <summary>
		/// 步骤编号
		/// </summary>
		public string StepNo { get; set; }

		/// <summary>
		/// 步骤名称
		/// </summary>
		public string StepName { get; set; }

		/// <summary>
		/// 步骤名称(英文)
		/// </summary>
		public string StepNameEn { get; set; }

		/// <summary>
		/// 步骤类型(对象类型: 1-产品相关,2-品牌相关,3-品类相关)
		/// </summary>
		public int? StepType { get; set; }

		/// <summary>
		/// 是否必做(0-否,1-是)
		/// </summary>
		public int? IsMustDo { get; set; }

		/// <summary>
		/// 步骤优先级
		/// </summary>
		public int? StepPriority { get; set; }

		/// <summary>
		/// 是否一页操作(0-否,1-是)
		/// </summary>
		public int? IsOnePage { get; set; }

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