/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 11:40:35
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
using System.Data.SqlClient;
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
    public partial class VipAmountDetailBLL
    {
        /// <summary>
        /// 获取订单使用的余额支付/阿拉币抵扣金额 update by Henry 2014-10-13
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <param name="amountSourceId"></param>
        /// <returns></returns>
        public decimal GetVipAmountByOrderId(string orderId, string userId, int amountSourceId)
        {
            return this._currentDAO.GetVipAmountByOrderId(orderId, userId, amountSourceId);
        }
        
        /// <summary>
        /// 获取余额/返现变更明细
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<VipAmountDetailEntity> GetVipAmountDetailList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetVipAmountDetailList(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
    }
}