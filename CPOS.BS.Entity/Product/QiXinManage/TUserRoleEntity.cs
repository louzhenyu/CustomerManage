/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 10:37:49
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
    public partial class TUserRoleEntity : BaseEntity 
    {
        #region 属性集
        public string user_role_id { get; set; }
        public string user_id { get; set; }
        public string role_id { get; set; }
        public string unit_id { get; set; }
        public string status { get; set; }
        public DateTime? create_time { get; set; }
        public string create_user_id { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_user_id { get; set; }
        public string default_flag { get; set; }
        #endregion
    }
}