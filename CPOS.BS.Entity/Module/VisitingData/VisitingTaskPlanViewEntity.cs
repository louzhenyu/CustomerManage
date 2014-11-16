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
    /// 实体： 拜访执行明细查看 (一个人一天所有的走店信息明细)
    /// </summary>
    public class VisitingTaskPlanViewEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskPlanViewEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 任务标识
        /// </summary>
        public string VisitingTaskID { get; set; }
        
        /// <summary>
        /// 任务名称
        /// </summary>
        public string VisitingTaskName { get; set; }

        /// <summary>
        /// 拜访对象编号(关联门店/经销商等)
        /// </summary>
        public string POPID { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public int POPType { get; set; }
        /// <summary>
        /// 门店名称(拜访对象名称)
        /// </summary>
        public string POPName { get; set; }

        /// <summary>
        /// 门店坐标
        /// </summary>
        public string Coordinate { get; set; }

        #endregion

    }
}