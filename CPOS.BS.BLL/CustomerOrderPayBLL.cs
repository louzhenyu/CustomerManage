/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:10:21
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
    public partial class CustomerOrderPayBLL
    {
        public DataSet GetCustomerOrderPayPage(CustomerOrderPayEntity entity, int pageIndex, int pageSize)
        {
            return this._currentDAO.GetCustomerOrderPayPage(entity, pageIndex, pageSize);
        }

        /// <summary>
        /// ����������ϸ���е�֧����ϸID ����֧����ϸ����
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrderPayList(string pCustomerID,string pOrderPayId)
        {
            return this._currentDAO.GetOrderPayList(pCustomerID,pOrderPayId);
        }

        /// <summary>
        /// ����������ϸ���е�֧����ϸID ���¶���֧����ϸ
        /// </summary>
        /// <returns></returns>
        public int UpdateOrderPayList(string pWithdrawalld,string pCustomerID, int pOrderPayStatus,string pUserId)
        {
            return this._currentDAO.UpdateOrderPayList(pWithdrawalld, pCustomerID,pOrderPayStatus,pUserId);
        }
    }
}