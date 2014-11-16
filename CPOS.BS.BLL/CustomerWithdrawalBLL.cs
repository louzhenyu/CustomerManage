/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:21:46
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
    public partial class CustomerWithdrawalBLL
    {

        public System.Data.SqlClient.SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        public DataSet GetCustomerWithdrawalList(string customerId, string serialNo, string beginDate, string endDate, int status, int pageIndex, int pageSize)
        {
            return this._currentDAO.GetCustomerWithdrawalList(customerId, serialNo, beginDate, endDate, status, pageIndex, pageSize);
        }
        public DataSet GetCustomerOrderPayStatus()
        {
            return this._currentDAO.GetCustomerOrderPayStatus();
        }
        /// <summary>
        /// 获取客户提现基本信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerWithrawalInfo(string pCustomerId)
        {
            return this._currentDAO.GetCustomerWithrawalInfo(pCustomerId);
        }
        ///// <summary>
        ///// 获取提现主标识
        ///// 客户ID。提现金额
        ///// </summary>
        ///// <returns></returns>
        //public string GetWithdrawalID(string pCustomerId,  int pWithdrawalStatus)
        //{
        //    return this._currentDAO.GetWithdrawalID(pCustomerId, pWithdrawalStatus);
        //}
        ///// <summary>
        ///// 根据提现主标识更新表的状态 =30  已提现。
        ///// </summary>
        ///// <param name="pWithdrawalld"></param>
        ///// <param name="pWithdrawalStatus"></param>
        ///// <returns></returns>
        //public int UpdateWithdrawalStatus(string pCustomerId,string pWithdrawalld,int pWithdrawalStatus,string pUserId)
        //{ 
        //    return this._currentDAO.UpdateWithdrawalStatus(pCustomerId,pWithdrawalld,pWithdrawalStatus,pUserId);
        //}
        /// <summary>
        /// 判断客户提现时间
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <returns></returns>
        public bool GetWithdrawalDayByMaxPeriod(string pCustomerId)
        {
            return this._currentDAO.GetWithdrawalDayByMaxPeriod(pCustomerId);
        }
        /// <summary>
        /// 申请提现
        /// 客户ID。登录用户ID。提现金额
        /// </summary>
        /// <returns></returns>
        public int getApplyForWithdrawal(string pCustomerId, string pUserId, decimal pWithdrawalAmount)
        {
            return this._currentDAO.getApplyForWithdrawal(pCustomerId, pUserId, pWithdrawalAmount);
        }
        /// <summary>
        ///  查询可提现
        /// </summary>
        /// <returns></returns>
        public DataSet GetTradeCenterPay(string pWithdrawalId)
        {
            return this._currentDAO.GetTradeCenterPay(pWithdrawalId);
        }

        public void SetTradeCenterPay(string SerialNo)
        {
            this._currentDAO.SetTradeCenterPay(SerialNo);
        }

        public void NotityTradeCenterPay(string SeriaNo, string WithdrawalStatus)
        {
            this._currentDAO.NotityTradeCenterPay(SeriaNo, WithdrawalStatus);
        }
    }
}