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
    public partial class Agg_UnitDaily_VipCardTypeBLL
    {
        /// <summary>
        /// 查询所有会员等级，非会员排除
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="dateCode"></param>
        /// <returns></returns>
        public Agg_UnitDaily_VipCardTypeEntity[] GetEntitiesForVip(string customerId, string unitId, DateTime dateCode)
        {
            //查询参数
            List<IWhereCondition> lstWhere_DayVipCardType = new List<IWhereCondition> { };
            lstWhere_DayVipCardType.Add(new DirectCondition("DateCode <= '" + dateCode.ToString("yyyy-MM-dd") + "'"));
            lstWhere_DayVipCardType.Add(new EqualsCondition() { FieldName = "UnitId", Value = unitId });
            lstWhere_DayVipCardType.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            lstWhere_DayVipCardType.Add(new DirectCondition("VipCardTypeID >= 0"));
            //排序
            List<OrderBy> lstOrder_DayVipCardType = new List<OrderBy> { };
            lstOrder_DayVipCardType.Add(new OrderBy() { FieldName = "DateCode", Direction = OrderByDirections.Desc });
            lstOrder_DayVipCardType.Add(new OrderBy() { FieldName = "VipCardLevel", Direction = OrderByDirections.Desc });
            //查询
            return _currentDAO.Query(lstWhere_DayVipCardType.ToArray(), lstOrder_DayVipCardType.ToArray(), 1);
        }
    }
}