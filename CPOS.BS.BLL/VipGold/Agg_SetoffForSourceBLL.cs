/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 15:25:10
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
    public partial class Agg_SetoffForSourceBLL
    {

        /// <summary>
        /// 分页获取 集客行动来源分析
        /// </summary>
        /// <param name="CustomerId">商户编号</param>
        /// <param name="SetoffRoleId">角色来源 1=员工、2=客服、3=会员</param>
        /// <param name="PageSize">页码</param>
        /// <param name="PageIndex">当前页索引</param>
        /// <returns></returns>
        public DataSet GetSetofSourcesListByCustomerId(string CustomerId, int? SetoffRoleId, string beginTime, string endTime)
        {
            return _currentDAO.GetSetofSourcesListByCustomerId(CustomerId, SetoffRoleId, beginTime, endTime);
        }

        /// <summary>
        /// 分页获取 集客来源分析 列表
        /// </summary>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="PageSize">每页分多少条</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForSourceEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {
            return _currentDAO.FindAllByPage(pWhereConditions, pOrderBys, PageSize, PageIndex);

        }

        /// <summary>
        /// 通过集客角色来源类型Id 获取名称
        /// </summary>
        /// <param name="type">类型编号</param>
        /// <returns>集客角色来源名称</returns>
        public string GetSetoffRoleNameBySetoffRoleId(int type)
        {
            if (type == 1)
            {
                return "员工";
            }
            else if (type == 2)
            {
                return "客服";
            }
            else
            {
                return "会员";
            }
        }
    }
}