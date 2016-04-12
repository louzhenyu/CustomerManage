/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:41
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
    public partial class Agg_UnitMonthly_VipCardTypeBLL
    {
        /// <summary>
        /// 得到1个月的数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="dateCode"></param>
        /// <returns></returns>
        public Agg_UnitMonthly_VipCardTypeEntity[] GetEntitiesForVip(string customerId, string unitId, DateTime dateCode)
        {
            //查询参数
            List<IWhereCondition> lstWhere = new List<IWhereCondition> { };
            lstWhere.Add(new EqualsCondition() { FieldName = "DateCode", Value = new DateTime(dateCode.Year, dateCode.Month, 1) });
            lstWhere.Add(new EqualsCondition() { FieldName = "UnitId", Value = unitId });
            lstWhere.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            lstWhere.Add(new DirectCondition("VipCardTypeID >= 0"));
            lstWhere.Add(new DirectCondition("EXISTS (SELECT 1 FROM SysVipCardType t WHERE Agg_UnitMonthly_VipCardType.VipCardTypeID = t.VipCardTypeID AND Agg_UnitMonthly_VipCardType.CustomerID = t.CustomerID AND t.IsDelete = 0)"));

            //排序
            List<OrderBy> lstOrder_MonthVipCardType = new List<OrderBy> { };
            lstOrder_MonthVipCardType.Add(new OrderBy() { FieldName = "VipCardLevel", Direction = OrderByDirections.Desc });
            //查询
            return Query(lstWhere.ToArray(), lstOrder_MonthVipCardType.ToArray());
        }
    }
}