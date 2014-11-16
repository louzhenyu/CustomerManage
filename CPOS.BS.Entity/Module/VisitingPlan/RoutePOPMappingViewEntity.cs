/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/7 15:05:22
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
    /// 实体： 路线、终端(经销商)映射 
    /// </summary>
    public class RoutePOPMappingViewEntity :RoutePOPMappingEntity
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public Guid? StoreID { get; set; }

        /// <summary>
        /// 渠道ID
        /// </summary>
        public int? DistributorID { get; set; }
    }
}