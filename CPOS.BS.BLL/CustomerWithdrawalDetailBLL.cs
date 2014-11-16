/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:21:46
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
    public partial class CustomerWithdrawalDetailBLL
    {
        /// <summary>
        /// 根据提现主标识查询提现明细表中订单支付明细ID
        /// </summary>
        /// <param name="pWithdrawalld"></param>
        /// <param name="pWithdrawalStatus"></param>
        /// <returns></returns>
        public string GetWithdrawalDetailByOrderPayId(string pCustomerId, string pWithdrawalld)
        {
            return this._currentDAO.GetWithdrawalDetailByOrderPayId(pCustomerId, pWithdrawalld);
        }
    }
}