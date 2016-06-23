/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 9:08:39
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
    public partial class T_SuperRetailTraderBLL
    {
        /// <summary>
        /// 获取分销商信息 {注意成为分销商之前的信息要默认过滤掉}
        /// </summary>
        /// <param name="customerId">商户编号</param>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序数组</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageIndex, int PageSize, string CustomerId)
        {
            return this._currentDAO.FindListByCustomerId(pWhereConditions, pOrderBys, PageIndex, PageSize, CustomerId);
        }


        /// <summary>
        /// 获取分销商信息 {注意成为分销商之前的信息要默认过滤掉} 导出 不需要分页显示
        /// </summary>
        /// <param name="customerId">商户编号</param>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序数组</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, string CustomerId)
        {
            return this._currentDAO.FindListByCustomerId(pWhereConditions, pOrderBys, CustomerId);
        }
        public DataSet GetAllFather(string strSuperRetailTraderId)
        {
            return this._currentDAO.GetAllFather(strSuperRetailTraderId);

        }
    }
}