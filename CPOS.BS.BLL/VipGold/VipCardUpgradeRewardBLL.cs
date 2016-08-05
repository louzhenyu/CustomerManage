/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 15:10:55
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
    public partial class VipCardUpgradeRewardBLL
    {
        /// <summary>
        /// 获取开卡礼列表
        /// </summary>
        /// <param name="VipCardTypeID">卡类型编号</param>
        /// <param name="CustomerId">商户编号</param>
        /// <returns></returns>
        public DataSet GetVipCardUpgradeRewardList(int ? VipCardTypeID, string CustomerId)
        {
            return _currentDAO.GetVipCardUpgradeRewardList(VipCardTypeID, CustomerId);
        }
    }
}