/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/17 14:26:19
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
    public partial class t_customerBLL
    {
        public t_customerEntity GetCustomer(string customerCode)
        {
            return this._currentDAO.GetCustomer(customerCode);
        }
        //public t_customerBLL() { }

        /// <summary>
        /// 根据CustomerId获取商户信息
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public t_customerEntity GetByCustomerID(object pID)
        {
            return this._currentDAO.GetByCustomerID(pID);
        }
    }
}