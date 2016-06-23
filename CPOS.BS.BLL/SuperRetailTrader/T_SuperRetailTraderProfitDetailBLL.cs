/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/3 15:11:55
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
    /// ҵ����  
    /// </summary>
    public partial class T_SuperRetailTraderProfitDetailBLL
    {
        /// <summary>
        /// ���س��������̷���������Ϣ
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderOrderInfo(string strSuperRetailTraderID, string strCustomerId, int intPage, int intSize)
        {
            return this._currentDAO.GetSuperRetailTraderOrderInfo(strSuperRetailTraderID, strCustomerId, intPage, intSize);
        }
        /// <summary>
        /// ����������ֱ���¼������(����)
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <param name="strCustomerId"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderUnderlingInfo(string strSuperRetailTraderID, string strCustomerId, int intPage, int intSize, string strDomin)
        {
            return this._currentDAO.GetSuperRetailTraderUnderlingInfo(strSuperRetailTraderID, strCustomerId, intPage, intSize, strDomin);

        }
        /// <summary>
        /// ���������̷�����������߻�����Ϣ
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderIncomeAndUnderlingInfo(string strSuperRetailTraderID,string strDate)
        {
            return this._currentDAO.GetSuperRetailTraderIncomeAndUnderlingInfo(strSuperRetailTraderID,strDate);
        }
    }
}