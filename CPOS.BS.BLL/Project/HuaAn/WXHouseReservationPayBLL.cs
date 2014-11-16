/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 13:46:22
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
    /// 业务处理： 过户（支付） 
    /// </summary>
    public partial class WXHouseReservationPayBLL
    {
        /// <summary>
        /// 根据prePaymentID 获取过户信息
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WXHouseReservationPayEntity GetByPrePaymentID(string customerID, string prePaymentID)
        {
            return _currentDAO.GetByPrePaymentID(customerID, prePaymentID);
        }

        /// <summary>
        /// 获取过户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fundType"></param>
        /// <param name="assignbuyer"></param>
        /// <param name="orderNO"></param>
        /// <param name="seqNO"></param>
        /// <returns></returns>
        public WXHouseReservationPayEntity GetBy(string customerID, string assignbuyer, string thirdOrderNO)
        {
            return _currentDAO.GetBy(customerID, assignbuyer, thirdOrderNO);
        }

        /// <summary>
        /// 根据customerID得到用WXHouseReservationPayEntity
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<WXHouseReservationPayEntity> Get(string customerID, int fundState)
        {
            DataSet ds = _currentDAO.Get(customerID, fundState);
            List<WXHouseReservationPayEntity> list = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WXHouseReservationPayEntity>(ds.Tables[0]);
            }

            return list;
        }
    }
}