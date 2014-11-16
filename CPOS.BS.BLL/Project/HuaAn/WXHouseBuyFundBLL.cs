/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/24 12:33:54
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
    public partial class WXHouseBuyFundBLL
    {
        /// <summary>
        /// ��ȡ���������Ϣ
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fundType"></param>
        /// <param name="assignbuyer"></param>
        /// <param name="orderNO"></param>
        /// <param name="seqNO"></param>
        /// <returns></returns>
        public WXHouseBuyFundEntity GetBy(string customerID, string assignbuyer, string thirdOrderNO)
        {
            return _currentDAO.GetBy(customerID, assignbuyer, thirdOrderNO);
        }

        /// <summary>
        /// ����customerID�õ���WXHouseBuyFundEntity
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<WXHouseBuyFundEntity> Get(string customerID,int  fundState)
        {
            DataSet ds = _currentDAO.Get(customerID,fundState);
            List<WXHouseBuyFundEntity> list = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WXHouseBuyFundEntity>(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// ���ݽ���״̬��ȡ������Ϣ��
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<WXHouseBuyFundEntity> Get(string customerID, string userID, int fundState)
        {
            DataSet ds = _currentDAO.Get(customerID, userID, fundState);
            List<WXHouseBuyFundEntity> list = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WXHouseBuyFundEntity>(ds.Tables[0]);
            }

            return list;
        }

        /// <summary>
        /// ��ȡ��ǰ�û�������״̬�µ��ܽ�
        /// </summary>
        /// <returns></returns>
        public string GetTotalMoney(string customerID, string userID, int fundState)
        {
            DataSet ds = _currentDAO.GetTotalMoney(customerID, userID, fundState);
            string totalMoney = string.Empty;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                totalMoney = ds.Tables[0].Rows[0]["TotalRealPay"].ToString();
            }

            return totalMoney;
        }
    }
}