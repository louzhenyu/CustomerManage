/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:20
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
    /// 实体：  
    /// </summary>
    public partial class ESalesEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 所属客户
        /// </summary>
        public string EnterpriseCustomerName { get; set; }
        /// <summary>
        /// 销售产品
        /// </summary>
        public string SalesProductName { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string ECSourceName { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public string StageName { get; set; }
        /// <summary>
        /// 销售负责人
        /// </summary>
        public string SalesVipName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        public IList<ESalesVisitVipMappingEntity> ESalesVisitVipMappingList { get; set; }
        public IList<string> ESalesVisitVipMappingIds { get; set; }
        #endregion
    }
}