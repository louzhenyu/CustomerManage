/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 14:06:57
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
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Request;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipWithDrawRuleBLL
    {
        public VipWithDrawRuleEntity GetVipWithDrawRule()
        {
            return _currentDAO.GetVipWithDrawRule();
        }
        public bool SetVipWithDrawRule(SetVipWithDrawRuleRP rp)
        {
            List<VipWithDrawRuleEntity> dbList = _currentDAO.QueryByEntity(new VipWithDrawRuleEntity { CustomerID = CurrentUserInfo.ClientID }, null).ToList();
            //U
            if (dbList.Count == 1)
            {
                var dbEntity = dbList.First();
                dbEntity.BeforeWithDrawDays = rp.BeforeWithDrawDays;
                dbEntity.MinAmountCondition = rp.MinAmountCondition;
                dbEntity.WithDrawMaxAmount = rp.WithDrawMaxAmount;
                dbEntity.WithDrawNum = rp.WithDrawNum;
                dbEntity.WithDrawNumType = 3;
                dbEntity.LastUpdateBy = CurrentUserInfo.UserID;
                dbEntity.LastUpdateTime = DateTime.Now;
                _currentDAO.Update(dbEntity);
                return true;
            }
            //A
            else if (dbList.Count == 0)
            {
                var dbEntity = new VipWithDrawRuleEntity();
                dbEntity.BeforeWithDrawDays = rp.BeforeWithDrawDays;
                dbEntity.MinAmountCondition = rp.MinAmountCondition;
                dbEntity.WithDrawMaxAmount = rp.WithDrawMaxAmount;
                dbEntity.WithDrawNum = rp.WithDrawNum;
                dbEntity.WithDrawNumType = 3;
                dbEntity.CustomerID = CurrentUserInfo.ClientID;
                dbEntity.CreateTime = DateTime.Now;
                dbEntity.CreateBy = CurrentUserInfo.UserID;
                dbEntity.LastUpdateBy = CurrentUserInfo.UserID;
                dbEntity.LastUpdateTime = DateTime.Now;
                _currentDAO.Create(dbEntity);
                return true;
            }
            else
            {
                //数据错误
                throw new APIException(ERROR_CODES.INVALID_BUSINESS, "数据错误,请联系管理员!");
            }
        }
    }
}