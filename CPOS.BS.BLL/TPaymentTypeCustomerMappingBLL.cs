/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/14 10:15:25
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class TPaymentTypeCustomerMappingBLL
    {
        public void UpdatePaymentMap(string updateSql, int channelId, string paymentTypeId, string customerId)
        {
            this._currentDAO.UpdatePaymentMap(updateSql, channelId, paymentTypeId, customerId);
        }
        public string GetChannelIdByPaymentTypeAndCustomer(string paymentTypeId, string customerId)
        {
            return this._currentDAO.GetChannelIdByPaymentTypeAndCustomer(paymentTypeId, customerId);
        }

        public string GetChannelIdByCustomerId(string customerId)
        {
            return this._currentDAO.GetChannelIdByCustomerId(customerId);
        }
    }
}