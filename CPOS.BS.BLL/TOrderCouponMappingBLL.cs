/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/13 10:54:59
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class TOrderCouponMappingBLL
    {
        #region 删除跟订单相关的表
        public bool DeleteOrderCouponMapping(string OrderId)
        {
            _currentDAO.DeleteOrderCouponMapping(OrderId);
            return true;
        }
        #endregion

        public decimal GetCouponParValue(string orderId)
        {
            return this._currentDAO.GetCouponParValue(orderId);
        }


    }
}