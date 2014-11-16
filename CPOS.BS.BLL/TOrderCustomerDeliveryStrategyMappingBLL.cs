/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-9-15 18:44:03
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
    public partial class TOrderCustomerDeliveryStrategyMappingBLL
    {
        /// <summary>
        /// 获取配送费
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public decimal GetDeliverAmount(string orderId)
        {
            decimal deliverAmount = 0;
            TOrderCustomerDeliveryStrategyMappingEntity[] list = Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "OrderId", Value =orderId}
            }, null);
            if (list.Count() > 0)
            {
                deliverAmount = list[0].DeliveryAmount.Value;
            }
            return deliverAmount;
        }
    }
}