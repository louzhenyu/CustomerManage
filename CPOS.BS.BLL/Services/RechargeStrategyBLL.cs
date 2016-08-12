/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-8-25 11:44:14
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
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class RechargeStrategyBLL
    {
        /// <summary>
        /// 获取订单、出入库和商品信息(支付回调使用)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetInoutOrderItems(string orderId)
        {
            return this._currentDAO.GetInoutOrderItems(orderId);
        }
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// 根据当前卡类型ID 获取充值活动列表
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="vipCardTypeID"></param>
        /// <returns></returns>
        public DataSet GetRechargeActivityList(string CustomerID, string vipCardTypeID, int ActType)
        {
            return this._currentDAO.GetRechargeActivityList(CustomerID, vipCardTypeID,ActType);
        }
    }
}