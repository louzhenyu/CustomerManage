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
    public partial class Agg_UnitDailyBLL
    {
        /// <summary>
        /// 得到一条记录
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="dateCode"></param>
        /// <returns></returns>
        public Agg_UnitDailyEntity GetEntity(string customerId, string unitId, DateTime dateCode)
        {
            return QueryByEntity(new Agg_UnitDailyEntity()
            {
                CustomerId = customerId,
                UnitId = unitId,
                DateCode = Convert.ToDateTime(dateCode)
            }, null).FirstOrDefault();
        }

        /// <summary>
        /// 得到按时间范围内的多条记录
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="startDateCode"></param>
        /// <param name="endDateCode"></param>
        /// <returns></returns>
        public Agg_UnitDailyEntity[] GetEntities(string customerId, string unitId, DateTime startDateCode,DateTime endDateCode)
        {            
            //查询参数
            List<IWhereCondition> lstWhere = new List<IWhereCondition> { };
            lstWhere.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            lstWhere.Add(new EqualsCondition() { FieldName = "UnitId", Value = unitId });
            lstWhere.Add(new DirectCondition("DateCode>='" + startDateCode.ToString("yyyy-MM-dd") + "' "));
            lstWhere.Add(new DirectCondition("DateCode<='" + endDateCode.ToString("yyyy-MM-dd") + "' "));

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "DateCode", Direction = OrderByDirections.Asc });

            //查询
            return Query(lstWhere.ToArray(), lstOrder.ToArray());
        }
    }
}