/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:36
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
    public partial class T_QN_QuestionnaireBLL
    {

        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }


        /// <summary>
        /// 根据活动id符获取实例
        /// </summary>
        /// <param name="AID">活动id</param>
        /// <returns></returns>
        public T_QN_QuestionnaireEntity GetByAID(object AID)
        {
            return _currentDAO.GetByAID(AID);
        }

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="QuestionnaireName">问卷名称</param>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<T_QN_QuestionnaireEntity> PagedQuery(string QuestionnaireName, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQuery(QuestionnaireName, pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
    }
}