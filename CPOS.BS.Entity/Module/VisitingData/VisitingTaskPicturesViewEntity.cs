/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:18:56
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
    /// 实体： 拜访执行明细查看 (某人某天在某个店的产品检查明细)
    /// </summary>
    public class VisitingTaskPicturesViewEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskPicturesViewEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 部门标识
        /// </summary>
        public string ClientStructureID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string StructureName { get; set; }

        /// <summary>
        /// 职位标识
        /// </summary>
        public string ClientPositionID { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 门店/经销商标识
        /// </summary>
        public string POPID { get; set; }

        /// <summary>
        /// 门店/经销商名称
        /// </summary>
        public string POPName { get; set; }

        /// <summary>
        /// 人员标识
        /// </summary>
        public int? ClientUserID { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 进店时间
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// 进店坐标
        /// </summary>
        public string InCoordinate { get; set; }

        /// <summary>
        /// 出店坐标
        /// </summary>
        public string OutCoordinate { get; set; }

        /// <summary>
        /// 拜访步骤标识
        /// </summary>
        public string VisitingTaskStepID { get; set; }

        /// <summary>
        /// 拜访步骤名称
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 拜访任务标识
        /// </summary>
        public string VisitingTaskID { get; set; }

        /// <summary>
        /// 拜访任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 照片名称
        /// </summary>
        public string PhotoName { get; set; }

        ///// <summary>
        ///// 照片名称(英文)
        ///// </summary>
        //public string PhotoNameEn { get; set; }

        /// <summary>
        /// 照片存储文件名
        /// </summary>
        public string Value { get; set; }

        #endregion

    }
}