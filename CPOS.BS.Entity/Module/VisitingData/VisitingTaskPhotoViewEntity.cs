/*
 * Author		:tiansheng.zhu@jitmarketing.cn
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/29 10:51:59
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
    /// 实体： VisitingTaskPhotoViewEntity 
    /// </summary>
    public class VisitingTaskPhotoViewEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskPhotoViewEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// ClientPositionID
        /// </summary>
        public int? ClientPositionID { get; set; }

        /// <summary>
        /// InCoordinate
        /// </summary>
        public string InCoordinate { get; set; }

        /// <summary>
        /// InPic
        /// </summary>
        public string InPic { get; set; }

        /// <summary>
        /// ClientUserName
        /// </summary>
        public string ClientUserName { get; set; }

        /// <summary>
        /// ObjectName
        /// </summary>
        public int? ObjectName { get; set; }

        /// <summary>
        /// VisitingTaskStepID
        /// </summary>
        public int? VisitingTaskStepID { get; set; }

        /// <summary>
        /// OutPic
        /// </summary>
        public string OutPic { get; set; }

        /// <summary>
        /// WithinPic
        /// </summary>
        public int? WithinPic { get; set; }

        /// <summary>
        /// StepName
        /// </summary>
        public int? StepName { get; set; }

        /// <summary>
        /// POPID
        /// </summary>
        public string POPID { get; set; }

        /// <summary>
        /// POPName
        /// </summary>
        public string POPName { get; set; }

        /// <summary>
        /// VisitingTaskDataID
        /// </summary>
        public Guid? VisitingTaskDataID { get; set; }

        /// <summary>
        /// VisitingTaskID
        /// </summary>
        public Guid? VisitingTaskID { get; set; }

        /// <summary>
        /// InTime
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// PhotoName
        /// </summary>
        public int? PhotoName { get; set; }

        /// <summary>
        /// ClientUserID
        /// </summary>
        public int? ClientUserID { get; set; }

        /// <summary>
        /// OutCoordinate
        /// </summary>
        public string OutCoordinate { get; set; }

        /// <summary>
        /// ClientStructureID
        /// </summary>
        public Guid? ClientStructureID { get; set; }

        /// <summary>
        /// TaskName
        /// </summary>
        public string TaskName { get; set; }   
         
        #endregion
    }
}