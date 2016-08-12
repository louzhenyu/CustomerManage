using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    /// <summary>
    /// 编辑会员体系信息(卡等级、升级条件、基本权益)
    /// </summary>
    public class UpdateVipCardTypeSystemAH : BaseActionHandler<UpdateVipCardTypeSystemRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<UpdateVipCardTypeSystemRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var entitySysVipCardType = new SysVipCardTypeEntity();
            var bllSysVipCardType = new SysVipCardTypeBLL(loggingSessionInfo);
            //卡分润规则
            var bllVipCardProfitRule = new VipCardProfitRuleBLL(loggingSessionInfo);
            //续费充值方式
            var VipCardReRechargeProfitRuleService = new VipCardReRechargeProfitRuleBLL(loggingSessionInfo);
            //编辑会员卡等级
            try
            {
                var SysVipCardTypeInfo = bllSysVipCardType.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardTypeID = para.VipCardTypeID, IsDelete = 0 }, null).FirstOrDefault();
                switch (para.OperateType)
                {
                    //如果为1编辑会员卡等级信息
                    case 1:
                        if (SysVipCardTypeInfo != null)
                        {
                            //获取当前会员信息
                            List<IWhereCondition> wheres = new List<IWhereCondition>();
                            wheres.Add(new EqualsCondition() { FieldName = "Category", Value = 0 });
                            wheres.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
                            wheres.Add(new EqualsCondition() { FieldName = "VipCardTypeName", Value = para.VipCardTypeName });
                            wheres.Add(new DirectCondition("VipCardLevel!='" + SysVipCardTypeInfo.VipCardLevel + "'"));
                            var ExistVipCardTypeResult = bllSysVipCardType.Query(wheres.ToArray(), null).FirstOrDefault();
                            if (ExistVipCardTypeResult != null)
                            {
                                throw new APIException("会员卡名称不能重复！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                            }
                            SysVipCardTypeInfo.VipCardTypeID = para.VipCardTypeID;
                            SysVipCardTypeInfo.Category = 0;
                            SysVipCardTypeInfo.VipCardTypeName = para.VipCardTypeName;
                            SysVipCardTypeInfo.VipCardLevel = SysVipCardTypeInfo.VipCardLevel;
                            SysVipCardTypeInfo.IsPassword = SysVipCardTypeInfo.IsPassword;
                            if (SysVipCardTypeInfo.VipCardLevel == 1)//如果等级为1默认可充值
                            {
                                SysVipCardTypeInfo.Isprepaid = 1;
                            }
                            else
                            {
                                SysVipCardTypeInfo.Isprepaid = para.IsPrepaid;
                            }
                            SysVipCardTypeInfo.IsOnlineSales = para.IsOnlineSales;
                            SysVipCardTypeInfo.PicUrl = para.PicUrl;
                            SysVipCardTypeInfo.CustomerID = loggingSessionInfo.ClientID;
                            if (SysVipCardTypeInfo.VipCardLevel == 1)
                            {
                                SysVipCardTypeInfo.Prices = 0;
                                SysVipCardTypeInfo.ExchangeIntegral = 0;
                                SysVipCardTypeInfo.IsExtraMoney = 2;
                            }
                        }
                        //如果IsOnlineSales为0则需要逻辑删除分润规则数据
                        if (SysVipCardTypeInfo.IsOnlineSales == 0)
                        {
                            var VipCardProfitRuleInfo = bllVipCardProfitRule.QueryByEntity(new VipCardProfitRuleEntity() { VipCardTypeID = SysVipCardTypeInfo.VipCardTypeID, CustomerID = loggingSessionInfo.ClientID }, null);
                            if (VipCardProfitRuleInfo.Length > 0)
                            {
                                bllVipCardProfitRule.Delete(VipCardProfitRuleInfo);
                            }
                        }

                        if (SysVipCardTypeInfo.Isprepaid == 0) //不可充值
                        {
                            var VipCardUpgradeRuleService = new VipCardUpgradeRuleBLL(loggingSessionInfo);
                            //升级规则信息 永远只有一条信息
                            var CardUpgradeRuleEntity = VipCardUpgradeRuleService.QueryByEntity(new VipCardUpgradeRuleEntity() { VipCardTypeID = SysVipCardTypeInfo.VipCardTypeID }, null);
                            if (CardUpgradeRuleEntity != null && CardUpgradeRuleEntity.FirstOrDefault() != null) //如果有设置了升级条件
                            {
                                if (CardUpgradeRuleEntity.FirstOrDefault().IsBuyUpgrade == 1) //消费升级+不可充值==>逻辑删除 会员卡销售激励规则
                                {
                                    var Ids = bllVipCardProfitRule.QueryByEntity(new VipCardProfitRuleEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardTypeID = SysVipCardTypeInfo.VipCardTypeID }, null).Select(m => m.CardBuyToProfitRuleId.Value.ToString()).ToArray();
                                    if (Ids.Length > 0)
                                    {
                                        bllVipCardProfitRule.Delete(Ids);
                                    }
                                }
                            }

                            //将所有梯度信息 逻辑删除 
                            string[] ProfitRuleIds = bllVipCardProfitRule.GetRechargeProfitRuleByIsPrepaid(loggingSessionInfo.ClientID, SysVipCardTypeInfo.VipCardTypeID);
                            if (ProfitRuleIds.Length > 0)
                            {
                                VipCardReRechargeProfitRuleService.Delete(ProfitRuleIds);
                            }
                        }
                        bllSysVipCardType.Update(SysVipCardTypeInfo);
                        //修改虚拟商品
                        ItemService _ItemService = new ItemService(loggingSessionInfo);
                        _ItemService.SaveCardToOffenTItem(loggingSessionInfo, SysVipCardTypeInfo);
                        break;
                    //如果为2编辑升级规则信息
                    case 2:
                        var bllVipCardUpgradeRule = new VipCardUpgradeRuleBLL(loggingSessionInfo);
                        //升级类型不能为空 (1=购卡升级；2=充值升级；3=消费升级;)
                        if (para.UpGradeType != 0)
                        {
                            VipCardUpgradeRuleEntity vipCardUpgradeRuleEntity = new VipCardUpgradeRuleEntity();
                            if(para.OperateObjectID!=null)
                            {
                                var VipCardUpgradeRuleInfo = bllVipCardUpgradeRule.QueryByEntity(new VipCardUpgradeRuleEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardUpgradeRuleId = new Guid(para.OperateObjectID), VipCardTypeID = para.VipCardTypeID }, null).FirstOrDefault();
                                if (VipCardUpgradeRuleInfo != null)
                                {
                                    //先置为0 再进行更新
                                    SysVipCardTypeInfo.IsExtraMoney = 2;
                                    SysVipCardTypeInfo.Prices = 0;
                                    SysVipCardTypeInfo.ExchangeIntegral = 0;
                                    VipCardUpgradeRuleInfo.OnceRechargeAmount = 0;
                                    VipCardUpgradeRuleInfo.OnceBuyAmount = 0;
                                    VipCardUpgradeRuleInfo.BuyAmount = 0;
                                    switch (para.UpGradeType)
                                    {

                                        case 1:
                                            VipCardUpgradeRuleInfo.IsPurchaseUpgrade = 1;
                                            //金额和积分 与可补差价在卡等级表里面
                                            SysVipCardTypeInfo.VipCardTypeID = para.VipCardTypeID;
                                            SysVipCardTypeInfo.IsExtraMoney = para.IsExtraMoney;
                                            SysVipCardTypeInfo.Prices = para.Prices;
                                            SysVipCardTypeInfo.ExchangeIntegral = para.ExchangeIntegral;
                                            //充值升级归零
                                            VipCardUpgradeRuleInfo.IsRecharge = 0;
                                            //消费升级置零
                                            VipCardUpgradeRuleInfo.IsBuyUpgrade = 0;
                                            break;
                                        case 2:
                                            VipCardUpgradeRuleInfo.IsPurchaseUpgrade = 0;
                                            VipCardUpgradeRuleInfo.IsRecharge = 1;
                                            VipCardUpgradeRuleInfo.IsBuyUpgrade = 0;
                                            VipCardUpgradeRuleInfo.OnceRechargeAmount = para.OnceRechargeAmount;
                                            break;
                                        case 3:
                                            VipCardUpgradeRuleInfo.IsPurchaseUpgrade = 0;
                                            VipCardUpgradeRuleInfo.IsRecharge = 0;
                                            VipCardUpgradeRuleInfo.IsBuyUpgrade = 1;
                                            VipCardUpgradeRuleInfo.OnceBuyAmount = para.OnceBuyAmount;
                                            VipCardUpgradeRuleInfo.BuyAmount = para.BuyAmount;
                                            //如果该卡为不可充值 那么就默认删除
                                            var syscardentity = bllSysVipCardType.GetByID(para.VipCardTypeID);
                                            if (syscardentity != null && syscardentity.Isprepaid == 0) //消费升级 {不可充值} 默认删除该卡的激励规则
                                            {
                                                var Ids = bllVipCardProfitRule.QueryByEntity(new VipCardProfitRuleEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardTypeID = para.VipCardTypeID }, null).Select(m => m.CardBuyToProfitRuleId.Value.ToString()).ToArray();
                                                if (Ids.Length > 0)
                                                {
                                                    bllVipCardProfitRule.Delete(Ids);
                                                }
                                            }
                                            break;
                                    }
                                    //更新卡等级部分信息
                                    bllSysVipCardType.Update(SysVipCardTypeInfo);
                                    //修改虚拟商品
                                    ItemService _ItemServices = new ItemService(loggingSessionInfo);
                                    _ItemServices.SaveCardToOffenTItem(loggingSessionInfo, SysVipCardTypeInfo);
                                    bllVipCardUpgradeRule.Update(VipCardUpgradeRuleInfo);
                                }
                            }                            
                            else
                            {

                                //先置为0 再进行更新
                                SysVipCardTypeInfo.IsExtraMoney = 2;
                                SysVipCardTypeInfo.Prices = 0;
                                SysVipCardTypeInfo.ExchangeIntegral = 0;
                                vipCardUpgradeRuleEntity.OnceRechargeAmount = 0;
                                vipCardUpgradeRuleEntity.OnceBuyAmount = 0;
                                vipCardUpgradeRuleEntity.BuyAmount = 0;
                                switch (para.UpGradeType)
                                {

                                    case 1:
                                        vipCardUpgradeRuleEntity.IsPurchaseUpgrade = 1;
                                        vipCardUpgradeRuleEntity.VipCardTypeID = para.VipCardTypeID;
                                        //金额和积分 与可补差价在卡等级表里面
                                        SysVipCardTypeInfo.VipCardTypeID = para.VipCardTypeID;
                                        SysVipCardTypeInfo.IsExtraMoney = para.IsExtraMoney;
                                        SysVipCardTypeInfo.Prices = para.Prices;
                                        SysVipCardTypeInfo.ExchangeIntegral = para.ExchangeIntegral;
                                        //充值升级归零
                                        vipCardUpgradeRuleEntity.IsRecharge = 0;
                                        //消费升级置零
                                        vipCardUpgradeRuleEntity.IsBuyUpgrade = 0;
                                        break;
                                    case 2:
                                        vipCardUpgradeRuleEntity.IsPurchaseUpgrade = 0;
                                        vipCardUpgradeRuleEntity.IsRecharge = 1;
                                        vipCardUpgradeRuleEntity.IsBuyUpgrade = 0;
                                        vipCardUpgradeRuleEntity.OnceRechargeAmount = para.OnceRechargeAmount;
                                        break;
                                    case 3:  //消费升级
                                        vipCardUpgradeRuleEntity.IsPurchaseUpgrade = 0;
                                        vipCardUpgradeRuleEntity.IsRecharge = 0;
                                        vipCardUpgradeRuleEntity.IsBuyUpgrade = 1;
                                        vipCardUpgradeRuleEntity.OnceBuyAmount = para.OnceBuyAmount;
                                        vipCardUpgradeRuleEntity.BuyAmount = para.BuyAmount;
                                        break;
                                }
                                //更新卡等级部分信息
                                bllSysVipCardType.Update(SysVipCardTypeInfo);
                                //修改虚拟商品
                                ItemService _ItemServices = new ItemService(loggingSessionInfo);
                                _ItemServices.SaveCardToOffenTItem(loggingSessionInfo, SysVipCardTypeInfo);
                                //添加卡升级规则
                                vipCardUpgradeRuleEntity.VipCardUpgradeRuleId = Guid.NewGuid();
                                vipCardUpgradeRuleEntity.CustomerID = loggingSessionInfo.ClientID;
                                bllVipCardUpgradeRule.Create(vipCardUpgradeRuleEntity);
                            }

                        }
                        else
                        {
                            throw new APIException("升级类型不能为空！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }

                        break;
                    //如果为3编辑基本权益信息
                    case 3:
                        var bllVipCardRule = new VipCardRuleBLL(loggingSessionInfo);
                        var entityVipCardRule = new VipCardRuleEntity();
                        var VipCardRuleInfo = bllVipCardRule.QueryByEntity(new VipCardRuleEntity() { CustomerID = loggingSessionInfo.ClientID, RuleID = Convert.ToInt32(para.OperateObjectID), VipCardTypeID = para.VipCardTypeID }, null).FirstOrDefault();
                        if (VipCardRuleInfo != null)
                        {
                            entityVipCardRule.RuleID = Convert.ToInt32(para.OperateObjectID);
                            entityVipCardRule.VipCardTypeID = para.VipCardTypeID;
                            entityVipCardRule.CardDiscount = para.CardDiscount * 10;
                            entityVipCardRule.PaidGivePoints = para.PaidGivePoints;
                            entityVipCardRule.PaidGivePercetPoints = para.PaidGivePercetPoints;
                            entityVipCardRule.CustomerID = loggingSessionInfo.ClientID;
                            bllVipCardRule.Update(entityVipCardRule);
                        }
                        break;


                }
            }
            catch (APIException ex)
            {
                throw ex;
            }

            return rd;
        }
    }
}