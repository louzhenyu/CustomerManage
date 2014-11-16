/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/11 9:54:08
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
    public class CallDayPlanningViewEntity_POP : BaseEntity
    {
        
        /// <summary>
        /// 自动编号
        /// </summary>
        public Guid? MappingID { get; set; }

        /// <summary>
        /// 拜访日期
        /// </summary>
        public DateTime? CallDate { get; set; }

        /// <summary>
        /// 人员编号(关联ClientUser表)
        /// </summary>
        public int? ClientUserID { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public Guid? StoreID { get; set; }

        /// <summary>
        /// 渠道ID
        /// </summary>
        public int? DistributorID { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public int? IsDelete { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sequence { get; set; }
    }
}