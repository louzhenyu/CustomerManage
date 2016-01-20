/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-3 10:30:21
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
    public partial class T_RefundOrderBLL
    {  
         /// <summary>
        /// ����״̬��ȡ�˿
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DataSet GetWhereRefundOrder(int Status, string CustomerID)
        {
            return this._currentDAO.GetWhereRefundOrder(null,null,null,Status, CustomerID);
        }
        public DataSet GetWhereRefundOrder(string RefundNo, string paymentcenterId, string payId, int Status, string CustomerID)
        {
            return this._currentDAO.GetWhereRefundOrder(RefundNo,paymentcenterId,payId,Status, CustomerID);
        }
    }
}