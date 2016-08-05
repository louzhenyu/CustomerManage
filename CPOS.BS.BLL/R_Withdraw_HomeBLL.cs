/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 17:09:07
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class R_Withdraw_HomeBLL
    {
        /// <summary>
        /// 根据商户编码和 会员类型 获取最近一条统计信息
        /// </summary>
        /// <param name="CustomerId">商户编码</param>
        /// <param name="VipTypeId">1=会员 2=员工 3=旧分销商 4=超级分销商</param>
        /// <returns></returns>
        public R_Withdraw_HomeEntity GetTopListByCustomer(string CustomerId, int VipTypeId)
        {
            return _currentDAO.GetTopListByCustomer(CustomerId, VipTypeId);
        }
    }
}