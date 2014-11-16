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
    /// ҵ����  
    /// </summary>
    public partial class t_customerBLL
    {
        public t_customerEntity GetCustomer(string customerCode)
        {
            return this._currentDAO.GetCustomer(customerCode);
        }
        //public t_customerBLL() { }

        /// <summary>
        /// ����CustomerId��ȡ�̻���Ϣ
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public t_customerEntity GetByCustomerID(object pID)
        {
            return this._currentDAO.GetByCustomerID(pID);
        }
    }
}