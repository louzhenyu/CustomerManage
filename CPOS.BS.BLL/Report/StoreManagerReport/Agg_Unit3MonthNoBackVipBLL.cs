/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:40
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
    public partial class Agg_Unit3MonthNoBackVipBLL
    {
        /// <summary>
        /// 门店三个月内新增未复购会员列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="startDateCode"></param>
        /// <param name="endDateCode"></param>
        /// <returns></returns>
        public PagedQueryResult<Agg_Unit3MonthNoBackVipEntity> PagedQueryForVip(string customerId, string unitId, DateTime dateCode, int pageIndex, int pageSize)
        {
            //查询参数
            List<IWhereCondition> lstWhere = new List<IWhereCondition> { };
            lstWhere.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            lstWhere.Add(new EqualsCondition() { FieldName = "UnitId", Value = unitId });
            lstWhere.Add(new DirectCondition("DateCode='" + dateCode.ToString("yyyy-MM-dd") + "' "));
            lstWhere.Add(new DirectCondition("VipCardTypeID >= 0"));

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "VipName", Direction = OrderByDirections.Asc });
            lstOrder.Add(new OrderBy() { FieldName = "Phone", Direction = OrderByDirections.Asc });

            //查询
            return PagedQuery(lstWhere.ToArray(), lstOrder.ToArray(), pageSize, pageIndex+1);
        }
    }
}