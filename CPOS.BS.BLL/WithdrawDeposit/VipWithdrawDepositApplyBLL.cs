/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipWithdrawDepositApplyBLL
    {  
        /// <summary>
        /// 获取今日提款情况
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public VipWithdrawDepositApplyEntity[] GetVipWDApplyByToday(string vipId)
        {
            return this._currentDAO.GetVipWDApplyByToday(vipId);
        }
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// 根据会员/店员名称执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<VipWithdrawDepositApplyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex,int isVip)
        {
            if (isVip == 1)//会员
                return this._currentDAO.PagedQueryByVipName(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
            else if (isVip == 2)//店员
                return this._currentDAO.PagedQueryByUserName(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
            else // (isVip ==3)//分销商
                return this._currentDAO.PagedQueryByRetailName(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
    }
}