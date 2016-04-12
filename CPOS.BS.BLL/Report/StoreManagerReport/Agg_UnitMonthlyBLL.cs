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
    public partial class Agg_UnitMonthlyBLL
    {
        /// <summary>
        /// 得到时间范围内
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="startDateCode"></param>
        /// <param name="endDateCode"></param>
        /// <returns></returns>
        public Agg_UnitMonthlyEntity[] Get1YearEntities(string customerId, string unitId, DateTime dateCode)
        {
            //查询参数
            List<IWhereCondition> lstWhere_Month = new List<IWhereCondition> { };
            lstWhere_Month.Add(new DirectCondition("DateCode >'" + new DateTime(dateCode.Year, dateCode.Month, 1).AddYears(-1).ToString("yyyy-MM-dd") + "'"));
            lstWhere_Month.Add(new DirectCondition("DateCode <='" + new DateTime(dateCode.Year, dateCode.Month, 1).ToString("yyyy-MM-dd") + "'"));
            lstWhere_Month.Add(new EqualsCondition() { FieldName = "UnitId", Value = unitId });
            lstWhere_Month.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            //排序
            List<OrderBy> lstOrder_Month = new List<OrderBy> { };
            lstOrder_Month.Add(new OrderBy() { FieldName = "DateCode", Direction = OrderByDirections.Asc });
            //查询
            return Query(lstWhere_Month.ToArray(), lstOrder_Month.ToArray());
        }
    }
}