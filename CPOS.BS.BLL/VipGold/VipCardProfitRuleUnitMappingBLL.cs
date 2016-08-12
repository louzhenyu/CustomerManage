/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 14:47:15
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
    public partial class VipCardProfitRuleUnitMappingBLL
    {

        /// <summary>
        /// 根据卡类型 判断 门店是否 重复
        /// </summary>
        /// <param name="VipCardTypeId">卡类型编号</param>
        /// <param name="CustomerId">商户编号</param>
        /// <param name="UnitId">门店编号</param>
        /// <param name="ProfitOwner">分润拥有者</param>
        /// <returns></returns>
        public DataSet CheckUnitIsExists(int VipCardTypeId,string ProfitOwner, string CustomerId, string UnitId)
        {
            return _currentDAO.CheckUnitIsExists(VipCardTypeId,ProfitOwner, CustomerId, UnitId);
        }

        public DataSet GetAllUnitTypeList(string CustomerId, Guid? CardBuyToProfitRuleId)
        {
            return _currentDAO.GetAllUnitTypeList(CustomerId, CardBuyToProfitRuleId);
        }

        /// <summary>
        /// 批量 删除 门店信息
        /// </summary>
        /// <param name="CardBuyToProfitRuleId"></param>
        public void UpdateUnitMapping(Guid? CardBuyToProfitRuleId,IDbTransaction pTran)
        {
            _currentDAO.UpdateUnitMapping(CardBuyToProfitRuleId, pTran);
        }
    }
}