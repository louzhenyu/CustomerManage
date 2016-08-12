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
    /// ҵ����  
    /// </summary>
    public partial class VipCardProfitRuleBLL : BaseService
    {
        /// <summary>
        /// �����ŵ�ͼ���������Ϣ
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

            #region �����ŵ�ӳ����Ϣ
            if (item.IsApplyAllUnits == 0) //�����ŵ� ����ŵ�ӳ���ϵ
            {
                if (item.RuleUnitInfoList != null)
                {
                    foreach (var unitInfo in item.RuleUnitInfoList) //�ŵ꼯���б�
                    {

                        VipCardProfitRuleUnitMappingEntity UnitMappEntity = new VipCardProfitRuleUnitMappingEntity()
                        {
                            CardBuyToProfitRuleId = CardBuyToProfitRuleId,
                            CustomerID = CurrentUserInfo.ClientID,
                            UnitID = unitInfo.UnitID
                        };
                        if (unitInfo.Id == null) //�ŵ���Ϊ�� �� ����ŵ�
                        {
                            UnitMappService.Create(UnitMappEntity, tran);
                        }
                        else   //�ŵ��Ų�Ϊ��  �� {�޸��ŵ�| ɾ���ŵ� }
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
                //ȫ���ŵ� Ĭ�Ͻ��ù�������� �ŵ�ɾ��
                UnitMappService.UpdateUnitMapping(item.CardBuyToProfitRuleId, tran);
            }
            #endregion

            #region �����ֵ�������
            if (item.ProfitTypeInfoList != null)
            {
                foreach (var ProfitTypeInfo in item.ProfitTypeInfoList) //���ѳ�ֵ�б�
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

                    if (ProfitTypeInfo.ReRechargeProfitRuleId == null) //���ѳ�ֵ��������Ϊ�� --->������ѳ�ֵ����ʽ
                    {
                        if (ProfitTypeInfo.LimitAmount > 0)  //��ֹ ���ɳ�ֵ��ʽ������������
                        {
                            ReRechargeProfitRuleService.Create(VipCardReRechargeProfitRuleInfo, tran);
                        }
                    }
                    else //----->�޸����ѳ�ֵ����ʽ
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
        /// ��ȡ��Ա�����ѳ�ֵ������� ��Ϣ
        /// </summary>
        /// <param name="CustomerId">�̻����</param>
        /// <returns></returns>
        public DataSet GetVipCardReRechargeProfitRuleList(string CustomerId)
        {
            return _currentDAO.GetVipCardReRechargeProfitRuleList(CustomerId);
        }
        /// <summary>
        /// ת��ʵ�巽��
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
        /// ��ȡ ĳ������ ���ѳ�ֵ��ʽ
        /// </summary>
        /// <param name="CustomerId">�̻����</param>
        /// <param name="CardTypeId">�����</param>
        /// <returns></returns>
        public string[] GetRechargeProfitRuleByIsPrepaid(string CustomerId, int ? CardTypeId)
        {
            return this._currentDAO.GetRechargeProfitRuleByIsPrepaid(CustomerId, CardTypeId);
        }
    }
}