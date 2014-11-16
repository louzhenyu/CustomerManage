/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/27 11:34:46
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
    /// 实体： 拜访路线定义 
    /// </summary>
    public class RouteViewEntity :RouteEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 部门ID（作查询用）
        /// </summary>
        public Guid? ClientStructureID { get; set; }

        /// <summary>
        /// 操作人ID（作查询用）
        /// </summary>
        public int? ClientUserID { get; set; }
    }
}