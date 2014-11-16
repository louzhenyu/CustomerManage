/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/9 14:50:33
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
    /// 实体： TaskStepSumViewEntity 
    /// </summary>
    public class TaskStepSumViewEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TaskStepSumViewEntity()
        {
        }
        #endregion
        #region 属性集
        /// <summary>
        /// VisitingTaskID
        /// </summary>
        public Guid? VisitingTaskID { get; set; }

        /// <summary>
        /// OutPic
        /// </summary>
        public string OutPic { get; set; }

        /// <summary>
        /// IsInOutPic
        /// </summary>
        public int IsInOutPic { get; set; }

        /// <summary>
        /// PicSum
        /// </summary>
        public int PicSum { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// StepName
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// VisitingTaskName
        /// </summary>
        public string VisitingTaskName { get; set; }

        /// <summary>
        /// InPic
        /// </summary>
        public string InPic { get; set; }

        /// <summary>
        /// StepPriority
        /// </summary>
        public int? StepPriority { get; set; }

        /// <summary>
        /// TaskCreateTime
        /// </summary>
        public DateTime? TaskCreateTime { get; set; }

        /// <summary>
        /// StepCreateTime
        /// </summary>
        public DateTime? StepCreateTime { get; set; }

        /// <summary>
        /// VisitingTaskStepID
        /// </summary>
        public Guid? VisitingTaskStepID { get; set; }


        #endregion

    }
}