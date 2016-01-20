/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/6/1 16:12:04
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
    /// ҵ���� ��Ϊ�����˫���� 
    /// </summary>
    public partial class SysRetailRewardRuleBLL
    {
        public SysRetailRewardRuleEntity[] GetSysRetailRewardRule(SysRetailRewardRuleEntity en)
        {
            return DataTableToObject.ConvertToList<SysRetailRewardRuleEntity>(this._currentDAO.GetSysRetailRewardRule(en).Tables[0]).ToArray();
        }

        public void UpdateSysRetailRewardRule(int IsTemplate, string CooperateType, string RewardTypeCode, string RetailTraderID, string CustomerID)
        {
            this._currentDAO.UpdateSysRetailRewardRule(IsTemplate,CooperateType,RewardTypeCode,  RetailTraderID, CustomerID);
        }
        /// <summary>
        /// ��Ʒ�б������۸�
        /// </summary>
        /// <param name="strCustomerID"></param>
        /// <returns></returns>
        public DataSet GetItemListWithSharePrice(string strCustomerID, string strRetailTraderID, int intPageIndex, int intPageSize, string strSort, string strSortName)
        {
            return this._currentDAO.GetItemListWithSharePrice(strCustomerID, strRetailTraderID, intPageIndex, intPageSize, strSort, strSortName);

        }
        /// <summary>
        /// ���������N���ڼ�������
        /// </summary>
        /// <param name="strRetailTraderID">������id</param>
        /// <param name="intDays">����</param>
        /// <returns></returns>
        public DataSet GetRetailTraderVipCountByDays(string strRetailTraderID, int intDays)
        {
        

            return this._currentDAO.GetRetailTraderVipCountByDays(strRetailTraderID,intDays);

        }
        public DataSet GetRetailTraderEarnings(string strRetailTraderID, string strType)
        {


            return this._currentDAO.GetRetailTraderEarnings(strRetailTraderID, strType);

        }
    }
}