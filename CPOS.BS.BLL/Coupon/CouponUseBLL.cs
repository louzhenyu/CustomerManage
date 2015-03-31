/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-1-15 20:06:20
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
    public partial class CouponUseBLL
    {
        #region 订单使用的优惠券列表

        /// <summary>
        /// 订单使用的优惠券列表
        /// </summary>
        /// <param name="vipId">用户ID</param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public DataSet GetOrderCouponUseList(string vipId, string orderId)
        {
            return this._currentDAO.GetOrderCouponUseList(vipId, orderId);
        }

        /// <summary>
        /// 获取用户使用的优惠券列表
        /// </summary>
        /// <param name="vipID">用户ID</param>
        /// <param name="CouponID">优惠券ID</param>
        /// <returns></returns>
        public DataSet GetCouponUseListByCouponID(string vipID, string CouponID)
        {
            return this._currentDAO.GetCouponUseListByCouponID(vipID, CouponID);
        }

        #endregion
    }
}