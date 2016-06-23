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
    /// 业务处理：  
    /// </summary>
    public partial class T_SuperRetailTraderProfitDetailBLL
    {
        /// <summary>
        /// 返回超级分销商分销订单信息
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderOrderInfo(string strSuperRetailTraderID, string strCustomerId, int intPage, int intSize)
        {
            return this._currentDAO.GetSuperRetailTraderOrderInfo(strSuperRetailTraderID, strCustomerId, intPage, intSize);
        }
        /// <summary>
        /// 超级分销商直线下级的情况(贡献)
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <param name="strCustomerId"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderUnderlingInfo(string strSuperRetailTraderID, string strCustomerId, int intPage, int intSize, string strDomin)
        {
            return this._currentDAO.GetSuperRetailTraderUnderlingInfo(strSuperRetailTraderID, strCustomerId, intPage, intSize, strDomin);

        }
        /// <summary>
        /// 超级分销商分销收入和下线汇总信息
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