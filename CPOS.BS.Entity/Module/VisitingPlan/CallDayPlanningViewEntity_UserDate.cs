/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/28 10:53:56
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
    /// 实体： 拜访计划表(根据路线,周期等生成) 
    /// </summary>
    public class CallDayPlanningViewEntity_UserDate : BaseEntity
    {
        /// <summary>
        /// 用户自动编号
        /// </summary>
        public int? ClientUserID { get; set; }

        /// <summary>
        /// 拜访时间
        /// </summary>
        public DateTime? CallDate { get; set; }

        /// <summary>
        /// 终端数
        /// </summary>
        public int POPCount { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        public int POPType { get; set; }

        /// <summary>
        /// 部门ID，作查询用
        /// </summary>
        public Guid? ClientStructureID { get; set; }
        /// <summary>
        /// 职位ID，作查询用
        /// </summary>
        public int? ClientPositionID { get; set; }

    }
}