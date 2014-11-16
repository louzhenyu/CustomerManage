/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:18:56
 * Description	:
 * 1st Modified On	:2013/6/7
 * 1st Modified By	:tianjun
 * 1st Modified Desc:删除字段GPSFailureTime，新增字段 有效拜访、订单达成率
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
    /// 实体： 拜访计划执行查看 (一个人一天所有的店的信息合计)
    /// </summary>
    public class VisitingTaskDataSummaryViewEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskDataSummaryViewEntity()
        {
        }
        #endregion     

        #region 属性集
        ///// <summary>
        ///// 自动编号
        ///// </summary>
        //public string VisitingTaskDataID { get; set; }

        /// <summary>
        /// 人员标识
        /// </summary>
        public string ClientUserID { get; set; }
        /// <summary>
        /// 计划拜访日期
        /// </summary>
        public DateTime? CallDate { get; set; }

		/// <summary>
        /// 人员名称((VisitingTaskData.ClientUserID->ClientUser.Name))
		/// </summary>
		public string ClientUserName { get; set; }

        /// <summary>
        /// 人员职位标识
        /// </summary>
        public string ClientPositionID { get; set; }

		/// <summary>
        /// 人员职位
		/// </summary>
		public string UserPositionName { get; set; }

        /// <summary>
        /// 人员职位英文
        /// </summary>
        public string UserPositionNameEn { get; set; }

        /// <summary>
        /// 部门标识
        /// </summary>
        public string ClientStructureID { get; set; }

		/// <summary>
        /// 所属部门(VisitingTaskData.ClientUserID->ClientStructureUserMapping.ClientStructureID->ClientStructure.StructureName)
		/// </summary>
        public string DepartmentName{ get; set; }

		/// <summary>
        /// 首次进店时间(Min(VisitingTaskData.InTime))
		/// </summary>
		public DateTime? FirstInTime { get; set; }           

		/// <summary>
        /// 最后出店时间(Max(VisitingTaskData.OutTime))
		/// </summary>
		public DateTime? LastOutTime { get; set; }

		/// <summary>
        /// 当天工作时间长度(分钟)
		/// </summary>
        public double? WorkingHoursTotal { get; set; }

		/// <summary>
        /// 店内工作时间长度(分钟)
		/// </summary>
        public double? WorkingHoursIndoor { get; set; }

		/// <summary>
        /// 路途工作时间长度(分钟)
		/// </summary>
        public double? WorkingHoursJourneyTime { get; set; }

		/// <summary>
        /// 有效百分比(店内总工作时间/当天工作时间长度)
		/// </summary>
        public string WorkingHoursEfficiency { get; set; }

		/// <summary>
        /// 计划拜访任务数
		/// </summary>
        public int? VisitingTaskPlanCount { get; set; }

		/// <summary>
        /// 实际拜访数
		/// </summary>
        public int? VisitingTaskExecutionCount { get; set; }

		/// <summary>
        /// 拜访任务完成百分比(实际拜访数/计划拜访任务数)（50%=》0.5）
		/// </summary>
        public string VisitingTaskExecutionEfficiency { get; set; }

        /// <summary>
        /// 定位失败次数
        /// </summary>
        //public int? GPSFailureTime { get; set; }

        /// <summary>
        /// 有效拜访(订单数)
        /// </summary>
        public int? EffectiveVisit { get; set; }

        /// <summary>
        /// 订单达成率(订单数/拜访数*100%)
        /// </summary>
        public string OrderSuccessRate { get; set; }

        #endregion

    }
}