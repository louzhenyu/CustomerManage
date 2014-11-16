/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/12 10:58:24
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
    /// 实体： 拜访对象(门店/经销商) 
    /// </summary>
    public class VisitingPOPMappingViewEntity
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public Guid? MappingID { get; set; }

        /// <summary>
        /// 拜访任务编号(关联VisitingTask表)
        /// </summary>
        public Guid? VisitingTaskID { get; set; }

        /// <summary>
        /// 拜访对象(终端或经销商)
        /// </summary>
        public string POPID { get; set; }

        public string StoreID { get; set; }

        public int? DistributorID { get; set; }
    }
}