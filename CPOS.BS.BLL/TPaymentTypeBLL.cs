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
    /// ҵ����  
    /// </summary>
    public partial class TPaymentTypeBLL
    {
        /// <summary>
        /// Ϊ�˼���ԭ���Ĵ���,
        /// ����������Դ�Ƚ���,���Զ����˲���Source��Դ
        /// ���� android,iphone�Ǵ�Plat��ȡ����,
        /// ΢�Ŷ��Ǵ�dataFromId��ȡ����,������ֵ��3��ôpSource="IsJSPay"
        /// </summary>
        /// <param name="pCustomerID">�ͻ���</param>
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

        #region ���ݿͻ���ȡ��Ӧ��֧����ʽ����

        /// <summary>
        /// ���ݿͻ���ȡ��Ӧ��֧����ʽ����
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <returns></returns>
        public DataSet GetPaymentListByCustomerId(string customerId,string pAppChanelId)
        {
            return _currentDAO.GetPaymentListByCustomerId(customerId, pAppChanelId);
        }

        #endregion

        #region ���ݿͻ�ID��֧������ID��ȡ��Ӧ��֧����ʽ

        /// <summary>
        /// ���ݿͻ�ID��֧������ID��ȡ��Ӧ��֧����ʽ
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <param name="paymentTypeId">֧������ID</param>
        /// <returns></returns>
        public DataSet GetPaymentByCustomerIdAndPaymentID(string customerId, string paymentTypeId)
        {
            return this._currentDAO.GetPaymentByCustomerIdAndPaymentID(customerId, paymentTypeId);
        }

        #endregion

        #region ��ȡ�տ��¼����

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