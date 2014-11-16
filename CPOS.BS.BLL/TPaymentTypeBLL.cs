/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/29 16:01:06
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
    public partial class TPaymentTypeBLL
    {
        /// <summary>
        /// 为了兼容原来的代码,
        /// 其中数据来源比较乱,所以定义了参数Source来源
        /// 其中 android,iphone是从Plat中取到的,
        /// 微信端是从dataFromId中取到的,如果这个值是3那么pSource="IsJSPay"
        /// </summary>
        /// <param name="pCustomerID">客户号</param>
        /// <param name="pSource"></param>
        /// <returns></returns>
        public TPaymentTypeEntity[] GetByCustomerBySource(string pCustomerID,string pSource)
        {
            return this._currentDAO.GetByCustomerBySource(pCustomerID, pSource); 
        }


        public TPaymentTypeEntity[] GetByCustomerByChanel(string pCustomerID, string pAPPChannelID)
        {
            return this._currentDAO.GetByCustomer(pCustomerID, pAPPChannelID);
        }

        #region 根据客户获取对应的支付方式集合

        /// <summary>
        /// 根据客户获取对应的支付方式集合
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetPaymentListByCustomerId(string customerId,string pAppChanelId)
        {
            return _currentDAO.GetPaymentListByCustomerId(customerId, pAppChanelId);
        }

        #endregion

        #region 根据客户ID和支付类型ID获取对应的支付方式

        /// <summary>
        /// 根据客户ID和支付类型ID获取对应的支付方式
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="paymentTypeId">支付类型ID</param>
        /// <returns></returns>
        public DataSet GetPaymentByCustomerIdAndPaymentID(string customerId, string paymentTypeId)
        {
            return this._currentDAO.GetPaymentByCustomerIdAndPaymentID(customerId, paymentTypeId);
        }

        #endregion

        #region 获取收款记录集合

        public DataSet GetCollectionRecord(string customerId, int page, int pageSize)
        {
            return _currentDAO.GetCollectionRecord(customerId, page, pageSize);
        }

        #endregion

        #region GetPaymentTypePage
        public PagedQueryResult<TPaymentTypeEntity> GetPaymentTypePage(IWhereCondition[] pWhereConditions, int pageIndex,
            int pageSize)
        {
            return _currentDAO.GetPaymentTypePage(pWhereConditions, pageIndex, pageSize)
   ;
        }
        #endregion

        public void DisablePayment(string PaymentTypeID)
        {
            this._currentDAO.DisablePayment(PaymentTypeID);
        }
    }
}