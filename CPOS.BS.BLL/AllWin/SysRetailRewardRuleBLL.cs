/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/6/1 16:12:04
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
    /// 业务处理： 分为单向和双向奖励 
    /// </summary>
    public partial class SysRetailRewardRuleBLL
    {  

        public void UpdateSysRetailRewardRule(int IsTemplate, string CooperateType, string RetailTraderID, string CustomerID )
        {
            this._currentDAO.UpdateSysRetailRewardRule(IsTemplate, CooperateType, RetailTraderID, CustomerID);
        }
    }
}