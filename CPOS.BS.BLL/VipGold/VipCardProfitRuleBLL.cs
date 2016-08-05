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
using JIT.CPOS.DTO;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipCardProfitRuleBLL : BaseService
    {
        /// <summary>
        /// 处理门店和激励规则信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="CardBuyToProfitRuleId"></param>
        /// <param name="tran"></param>
        public void SetUnitAndProfitRule(CardBuyToProfitRuleInfoInfo item, Guid? CardBuyToProfitRuleId, LoggingSessionInfo loggingSessionInfo, IDbTransaction tran)
        {
            VipCardProfitRuleBLL RuleService = new VipCardProfitRuleBLL(loggingSessionInfo);
            SysVipCardTypeBLL SysCardTypeService = new SysVipCardTypeBLL(loggingSessionInfo);
            VipCardProfitRuleUnitMappingBLL UnitMappService = new VipCardProfitRuleUnitMappingBLL(loggingSessionInfo);
            VipCardReRechargeProfitRuleBLL ReRechargeProfitRuleService = new VipCardReRechargeProfitRuleBLL(loggingSessionInfo);
            UnitBLL UnitService = new UnitBLL(loggingSessionInfo);

            #region 处理门店映射信息
            if (item.IsApplyAllUnits == 0) //部分门店 添加门店映射关系
            {
                if (item.RuleUnitInfoList != null)
                {
                    foreach (var unitInfo in item.RuleUnitInfoList) //门店集合列表
                    {

                        VipCardProfitRuleUnitMappingEntity UnitMappEntity = new VipCardProfitRuleUnitMappingEntity()
                        {
                            CardBuyToProfitRuleId = CardBuyToProfitRuleId,
                            CustomerID = CurrentUserInfo.ClientID,
                            UnitID = unitInfo.UnitID
                        };
                        if (unitInfo.Id == null) //门店编号为空 既 添加门店
                        {
                            UnitMappService.Create(UnitMappEntity, tran);
                        }
                        else   //门店编号不为空  既 {修改门店| 删除门店 }
                        {
                            UnitMappEntity.CardBuyToProfitRuleId = item.CardBuyToProfitRuleId;
                            UnitMappEntity.Id = unitInfo.Id;
                            UnitMappEntity.IsDelete = unitInfo.IsDelete;
                            UnitMappService.Update(UnitMappEntity, tran);
                        }
                    }
                }
            }
            else
            {
                //全部门店 默认将该规则下面的 门店删除
                UnitMappService.UpdateUnitMapping(item.CardBuyToProfitRuleId, tran);
            }
            #endregion

            #region 处理充值分润规则
            if (item.ProfitTypeInfoList != null)
            {
                foreach (var ProfitTypeInfo in item.ProfitTypeInfoList) //续费充值列表
                {

                    VipCardReRechargeProfitRuleEntity VipCardReRechargeProfitRuleInfo = new VipCardReRechargeProfitRuleEntity()
                    {
                        CardBuyToProfitRuleId = CardBuyToProfitRuleId,
                        CustomerID = loggingSessionInfo.ClientID,
                        LimitAmount = ProfitTypeInfo.LimitAmount,
                        ProfitPct = ProfitTypeInfo.ProfitPct,
                        ProfitType = ProfitTypeInfo.ProfitType,
                        VipCardTypeID = item.VipCardTypeID
                    };

                    if (ProfitTypeInfo.ReRechargeProfitRuleId == null) //续费充值分润主键为空 --->添加续费充值分润方式
                    {
                        if (ProfitTypeInfo.LimitAmount > 0)  //防止 不可充值方式参数传递问题
                        {
                            ReRechargeProfitRuleService.Create(VipCardReRechargeProfitRuleInfo, tran);
                        }
                    }
                    else //----->修改续费充值分润方式
                    {
                        VipCardReRechargeProfitRuleInfo.ReRechargeProfitRuleId = ProfitTypeInfo.ReRechargeProfitRuleId;
                        VipCardReRechargeProfitRuleInfo.IsDelete = ProfitTypeInfo.IsDelete;
                        ReRechargeProfitRuleService.Update(VipCardReRechargeProfitRuleInfo, tran);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 获取会员卡续费充值分润规则 信息
        /// </summary>
        /// <param name="CustomerId">商户编号</param>
        /// <returns></returns>
        public DataSet GetVipCardReRechargeProfitRuleList(string CustomerId)
        {
            return _currentDAO.GetVipCardReRechargeProfitRuleList(CustomerId);
        }
        /// <summary>
        /// 转换实体方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="CardBuyToProfitRuleId"></param>
        /// <returns></returns>
        public VipCardProfitRuleEntity GetEntity(CardBuyToProfitRuleInfoInfo item)
        {
            VipCardProfitRuleEntity VipCardProfitRule = new VipCardProfitRuleEntity()
            {
                CardSalesProfitPct = item.CardSalesProfitPct,
                FirstCardSalesProfitPct = item.FirstCardSalesProfitPct,
                FirstRechargeProfitPct = item.FirstRechargeProfitPct,
                IsApplyAllUnits = item.IsApplyAllUnits,
                IsConsumeRule = 0,
                ProfitOwner = item.ProfitOwner,
                RechargeProfitPct = item.RechargeProfitPct,
                IsDelete = item.IsDelete,
                VipCardTypeID = item.VipCardTypeID,
                UnitCostRebateProfitPct = 0,
                RefId = item.CardBuyToProfitRuleId
            };
            return VipCardProfitRule;
        }
        /// <summary>
        /// 获取 某个卡的 续费充值方式
        /// </summary>
        /// <param name="CustomerId">商户编号</param>
        /// <param name="CardTypeId">卡编号</param>
        /// <returns></returns>
        public string[] GetRechargeProfitRuleByIsPrepaid(string CustomerId, int ? CardTypeId)
        {
            return this._currentDAO.GetRechargeProfitRuleByIsPrepaid(CustomerId, CardTypeId);
        }
    }
}