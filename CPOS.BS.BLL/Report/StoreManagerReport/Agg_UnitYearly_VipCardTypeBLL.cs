/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 14:10:42
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
    /// ҵ����  
    /// </summary>
    public partial class Agg_UnitYearly_VipCardTypeBLL
    {
        /// <summary>
        /// �õ�һ�������
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="dateCode"></param>
        /// <returns></returns>
        public Agg_UnitYearly_VipCardTypeEntity[] GetEntitiesForVip(string customerId, string unitId, DateTime dateCode)
        {
            //��ѯ����
            List<IWhereCondition> lstWhere_YearVipCardType = new List<IWhereCondition> { };
            lstWhere_YearVipCardType.Add(new EqualsCondition() { FieldName = "DateCode", Value = new DateTime(dateCode.Year, 1, 1) });
            lstWhere_YearVipCardType.Add(new EqualsCondition() { FieldName = "UnitId", Value = unitId });
            lstWhere_YearVipCardType.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            lstWhere_YearVipCardType.Add(new DirectCondition("VipCardTypeID >= 0"));
            //����
            List<OrderBy> lstOrder_YearVipCardType = new List<OrderBy> { };
            lstOrder_YearVipCardType.Add(new OrderBy() { FieldName = "VipCardLevel", Direction = OrderByDirections.Desc });
            //��ѯ
            return Query(lstWhere_YearVipCardType.ToArray(), lstOrder_YearVipCardType.ToArray());
        }
    }  
}