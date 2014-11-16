/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 13:46:23
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
    /// ҵ���� ������� 
    /// </summary>
    public partial class WXHouseReservationRedeemBLL
    {
        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseReservationRedeemEntity GetByPrePaymentID(string customerID, string prePaymentID)
        {
            return _currentDAO.GetByPrePaymentID(customerID, prePaymentID);
        }



        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fundType"></param>
        /// <param name="assignbuyer"></param>
        /// <param name="orderNO"></param>
        /// <param name="seqNO"></param>
        /// <returns></returns>
        public WXHouseReservationRedeemEntity GetBy(string customerID, string assignbuyer, string thirdOrderNO)
        {
            return _currentDAO.GetBy(customerID, assignbuyer, thirdOrderNO);
        }

        /// <summary>
        /// ����customerID�õ���WXHouseReservationRedeemEntity
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<WXHouseReservationRedeemEntity> Get(string customerID, int fundState)
        {
            DataSet ds = _currentDAO.Get(customerID, fundState);
            List<WXHouseReservationRedeemEntity> list = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WXHouseReservationRedeemEntity>(ds.Tables[0]);
            }

            return list;
        }
    }
}