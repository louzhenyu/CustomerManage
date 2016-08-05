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
    /// 设置卡体系相关信息
    /// </summary>
    public class SetVipCardTypeSystemAH : BaseActionHandler<SetVipCardTypeSystemRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetVipCardTypeSystemRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //卡相关逻辑
            var bllVipCardType = new SysVipCardTypeBLL(loggingSessionInfo);
            
            //升级规则逻辑
            var bllVipCardUpgradeRule = new VipCardUpgradeRuleBLL(loggingSessionInfo);
            
            //基本权益逻辑
            var bllVipCardRule = new VipCardRuleBLL(loggingSessionInfo);
            
            //开卡礼逻辑
            var bllVipCardUpgradeReward = new VipCardUpgradeRewardBLL(loggingSessionInfo);
            //卡分润规则
            var bllVipCardProfitRule = new VipCardProfitRuleBLL(loggingSessionInfo);
            //续费充值方式
            var VipCardReRechargeProfitRuleService = new VipCardReRechargeProfitRuleBLL(loggingSessionInfo);
            
            string _CustomerId = loggingSessionInfo.ClientID;
            if (para.VipCardRelateList.Count > 0)
            {
                try 
                {
                    foreach (var VipCardSystem in para.VipCardRelateList)
                    {
                        //获取当前会员信息
                        //List<IWhereCondition> wheres = new List<IWhereCondition>();
                        //wheres.Add(new EqualsCondition() { FieldName = "Category", Value = 0 });
                        //wheres.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
                        //wheres.Add(new EqualsCondition() { FieldName = "VipCardTypeName", Value = VipCardSystem.VipCardType.VipCardTypeName });
                        //wheres.Add(new DirectCondition("VipCardLevel!='" + VipCardSystem.VipCardType.VipCardLevel + "'"));
                        //var ExistVipCardTypeResult = bllVipCardType.Query(wheres.ToArray(), null).FirstOrDefault();
                        //if (ExistVipCardTypeResult != null)
                        //{
                        //    throw new APIException("会员卡名称不能重复！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        //}
                        //获取当前待添加的卡等级数据
                        var VipCardTypeResult = bllVipCardType.QueryByEntity(new SysVipCardTypeEntity() { Category = 0, VipCardLevel = VipCardSystem.VipCardType.VipCardLevel, CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                        
                        if (VipCardTypeResult != null)
                        {
                            #region 编辑会员卡等级信息
                            //throw new APIException("卡等级不能重复！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                            //如果存在数据需要进行修改
                            VipCardTypeResult.VipCardTypeName = VipCardSystem.VipCardType.VipCardTypeName;
                            VipCardTypeResult.VipCardLevel = VipCardSystem.VipCardType.VipCardLevel;
                            if (VipCardTypeResult.VipCardLevel == 1)//如果等级为1默认可充值
                            {
                                VipCardTypeResult.Isprepaid = 1;
                            }
                            else 
                            {
                                VipCardTypeResult.Isprepaid = VipCardSystem.VipCardType.IsPrepaid;
                            }                            
                            VipCardTypeResult.IsOnlineSales = VipCardSystem.VipCardType.IsOnlineSales;
                            //如果IsOnlineSales为0则需要逻辑删除分润规则数据
                            if (VipCardTypeResult.IsOnlineSales == 0)
                            {
                                var VipCardProfitRuleInfo = bllVipCardProfitRule.QueryByEntity(new VipCardProfitRuleEntity() { VipCardTypeID = VipCardTypeResult.VipCardTypeID,CustomerID=loggingSessionInfo.ClientID},null);
                                if (VipCardProfitRuleInfo.Length > 0)
                                {
                                    bllVipCardProfitRule.Delete(VipCardProfitRuleInfo);
                                }
                            }
                            VipCardTypeResult.PicUrl = VipCardSystem.VipCardType.PicUrl;
                            if (VipCardSystem.VipCardUpgradeRule != null)
                            {
                                if (VipCardSystem.VipCardUpgradeRule.IsPurchaseUpgrade == 1)
                                {
                                    VipCardTypeResult.Prices = VipCardSystem.VipCardUpgradeRule.Prices;
                                    VipCardTypeResult.ExchangeIntegral = VipCardSystem.VipCardUpgradeRule.ExchangeIntegral;
                                    VipCardTypeResult.IsExtraMoney = VipCardSystem.VipCardUpgradeRule.IsExtraMoney;
                                }
                                else
                                {
                                    VipCardTypeResult.Prices = 0;
                                    VipCardTypeResult.ExchangeIntegral = 0;
                                    VipCardTypeResult.IsExtraMoney = 2;
                                }
                            }
                            else
                            {
                                VipCardTypeResult.Prices = 0;
                                VipCardTypeResult.ExchangeIntegral = 0;
                                VipCardTypeResult.IsExtraMoney = 2;
                            }
                            //要先生成卡等级数据  不然取不到VipCardTypeID
                            if (VipCardSystem.VipCardType.VipCardLevel != 0)
                            {
                                bllVipCardType.Update(VipCardTypeResult);
                                //修改虚拟商品
                                ItemService _ItemService = new ItemService(loggingSessionInfo);
                                _ItemService.SaveCardToOffenTItem(loggingSessionInfo, VipCardTypeResult);                                                               
                            }

                            #endregion
                            #region 编辑卡升级规则信息
                            //编辑卡升级规则
                            if (VipCardSystem.VipCardType.VipCardLevel != 1)
                            {
                                VipCardUpgradeRuleEntity vipCardUpgradeRuleEntity = new VipCardUpgradeRuleEntity();
                                var entityVipCardUpgradeRule = bllVipCardUpgradeRule.QueryByEntity(new VipCardUpgradeRuleEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardTypeID = VipCardTypeResult.VipCardTypeID }, null).FirstOrDefault();
                                //entityVipCardUpgradeRule.VipCardUpgradeRuleId = System.Guid.NewGuid();
                                if (entityVipCardUpgradeRule != null)
                                {

                                    if (VipCardTypeResult.Isprepaid == 0) //不可充值
                                    {
                                        //将所有梯度信息 逻辑删除 
                                        string[] ProfitRuleIds = bllVipCardProfitRule.GetRechargeProfitRuleByIsPrepaid(loggingSessionInfo.ClientID, VipCardTypeResult.VipCardTypeID);
                                        if (ProfitRuleIds.Length > 0)
                                        {
                                            VipCardReRechargeProfitRuleService.Delete(ProfitRuleIds);
                                        }
                                    }

                                    if (VipCardTypeResult != null && VipCardTypeResult.VipCardTypeID != null)
                                    {
                                        entityVipCardUpgradeRule.VipCardTypeID = VipCardTypeResult.VipCardTypeID;
                                    }
                                    //是否购卡升级
                                    if (VipCardSystem.VipCardUpgradeRule != null)
                                    {
                                        if (VipCardSystem.VipCardUpgradeRule.IsPurchaseUpgrade == 1)
                                        {
                                            entityVipCardUpgradeRule.IsPurchaseUpgrade = 1;
                                        }
                                        else
                                        {
                                            entityVipCardUpgradeRule.IsPurchaseUpgrade = 0;
                                        }
                                        //是否充值升级
                                        if (VipCardSystem.VipCardUpgradeRule.IsRecharge == 1)
                                        {
                                            entityVipCardUpgradeRule.IsRecharge = 1;
                                            entityVipCardUpgradeRule.OnceRechargeAmount = VipCardSystem.VipCardUpgradeRule.OnceRechargeAmount;
                                        }
                                        else
                                        {
                                            entityVipCardUpgradeRule.IsRecharge = 0;
                                            entityVipCardUpgradeRule.OnceRechargeAmount = 0;
                                        }
                                        //是否消费升级
                                        if (VipCardSystem.VipCardUpgradeRule.IsBuyUpgrade == 1)
                                        {
                                            entityVipCardUpgradeRule.IsBuyUpgrade = 1;
                                            entityVipCardUpgradeRule.BuyAmount = VipCardSystem.VipCardUpgradeRule.BuyAmount;
                                            entityVipCardUpgradeRule.OnceBuyAmount = VipCardSystem.VipCardUpgradeRule.OnceBuyAmount;
                                        }
                                        else
                                        {
                                            entityVipCardUpgradeRule.IsBuyUpgrade = 0;
                                            entityVipCardUpgradeRule.BuyAmount = 0;
                                            entityVipCardUpgradeRule.OnceBuyAmount = 0;
                                        }
                                    }
                                    else
                                    {
                                        throw new APIException("升级条件不能为空！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                    }
                                    entityVipCardUpgradeRule.CustomerID = loggingSessionInfo.ClientID;
                                }
                                else
                                {
                                    if (VipCardTypeResult != null && VipCardTypeResult.VipCardTypeID != null)
                                    {
                                        vipCardUpgradeRuleEntity.VipCardTypeID = VipCardTypeResult.VipCardTypeID;
                                    }
                                    //是否购卡升级
                                    if (VipCardSystem.VipCardUpgradeRule != null)
                                    {
                                        if (VipCardSystem.VipCardUpgradeRule.IsPurchaseUpgrade == 1)
                                        {
                                            vipCardUpgradeRuleEntity.IsPurchaseUpgrade = 1;
                                        }
                                        else
                                        {
                                            vipCardUpgradeRuleEntity.IsPurchaseUpgrade = 0;
                                        }
                                        //是否充值升级
                                        if (VipCardSystem.VipCardUpgradeRule.IsRecharge == 1)
                                        {
                                            vipCardUpgradeRuleEntity.IsRecharge = 1;
                                            vipCardUpgradeRuleEntity.OnceRechargeAmount = VipCardSystem.VipCardUpgradeRule.OnceRechargeAmount;
                                        }
                                        else
                                        {
                                            vipCardUpgradeRuleEntity.IsRecharge = 0;
                                            vipCardUpgradeRuleEntity.OnceRechargeAmount = 0;
                                        }
                                        //是否消费升级
                                        if (VipCardSystem.VipCardUpgradeRule.IsBuyUpgrade == 1)
                                        {
                                            vipCardUpgradeRuleEntity.IsBuyUpgrade = 1;
                                            vipCardUpgradeRuleEntity.BuyAmount = VipCardSystem.VipCardUpgradeRule.BuyAmount;
                                            vipCardUpgradeRuleEntity.OnceBuyAmount = VipCardSystem.VipCardUpgradeRule.OnceBuyAmount;
                                        }
                                        else
                                        {
                                            vipCardUpgradeRuleEntity.IsBuyUpgrade = 0;
                                            vipCardUpgradeRuleEntity.BuyAmount = 0;
                                            vipCardUpgradeRuleEntity.OnceBuyAmount = 0;
                                        }
                                    }
                                    else
                                    {
                                        throw new APIException("升级条件不能为空！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                    }
                                    vipCardUpgradeRuleEntity.CustomerID = loggingSessionInfo.ClientID;
                                }

                                //将卡升级规则进行入库
                                if (entityVipCardUpgradeRule != null)
                                {
                                    bllVipCardUpgradeRule.Update(entityVipCardUpgradeRule);
                                }
                                else
                                {
                                    vipCardUpgradeRuleEntity.VipCardUpgradeRuleId = Guid.NewGuid();
                                    bllVipCardUpgradeRule.Create(vipCardUpgradeRuleEntity);
                                }

                            }
                            #endregion
                            #region 编辑基本权益信息
                            //基本权益实体
                            var VipCardRuleInfo = bllVipCardRule.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = VipCardTypeResult.VipCardTypeID, CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                            if (VipCardRuleInfo != null)
                            {
                                if (VipCardSystem.VipCardRule != null)
                                {
                                    VipCardRuleInfo.CardDiscount = VipCardSystem.VipCardRule.CardDiscount * 10;//因为给的是整数 目前折扣这里乘10
                                    VipCardRuleInfo.PaidGivePoints = VipCardSystem.VipCardRule.PaidGivePoints;
                                    VipCardRuleInfo.PaidGivePercetPoints = VipCardSystem.VipCardRule.PaidGivePercetPoints;
                                }
                                else
                                {

                                    VipCardRuleInfo.CardDiscount = 0;//因为给的是整数 目前折扣这里乘10
                                    VipCardRuleInfo.PaidGivePoints = 0;
                                    VipCardRuleInfo.PaidGivePercetPoints = 0;
                                }
                                //将基本权益数据进行入库
                                bllVipCardRule.Update(VipCardRuleInfo);
                            }
                            else
                            {
                                var vipCardRuleEntity = new VipCardRuleEntity();
                                vipCardRuleEntity.VipCardTypeID = VipCardTypeResult.VipCardTypeID;
                                vipCardRuleEntity.CardDiscount = VipCardSystem.VipCardRule.CardDiscount * 10;//因为给的是整数 目前折扣这里乘10
                                vipCardRuleEntity.PaidGivePoints = VipCardSystem.VipCardRule.PaidGivePoints;
                                vipCardRuleEntity.PaidGivePercetPoints = VipCardSystem.VipCardRule.PaidGivePercetPoints;
                                vipCardRuleEntity.CustomerID = loggingSessionInfo.ClientID;
                                bllVipCardRule.Create(vipCardRuleEntity);
                            }
                            
                                            
                            
                            #endregion
                            #region 编辑开卡礼信息
                            if (VipCardSystem.VipCardUpgradeRewardList != null)
                            {
                                //开卡礼处理
                                foreach (var Rewards in VipCardSystem.VipCardUpgradeRewardList)
                                {
                                    var entityVipCardUpgradeReward = new VipCardUpgradeRewardEntity();
                                    if (VipCardTypeResult != null && VipCardTypeResult.VipCardTypeID != null)
                                    {
                                        entityVipCardUpgradeReward.VipCardTypeID = VipCardTypeResult.VipCardTypeID;
                                    }
                                    entityVipCardUpgradeReward.CouponTypeId = new Guid(Rewards.CouponTypeID);
                                    entityVipCardUpgradeReward.CouponNum = Rewards.CouponNum;
                                    entityVipCardUpgradeReward.CustomerID = loggingSessionInfo.ClientID;                                   
                                    if (!string.IsNullOrEmpty(Rewards.CardUpgradeRewardId))
                                    {
                                        entityVipCardUpgradeReward.CardUpgradeRewardId = new Guid(Rewards.CardUpgradeRewardId);
                                    }                                     
                                    switch (Rewards.OperateType)//判断此处为何种操作(0=删除券;1=新增;2=修改;)
                                    {
                                        case 0://删除券
                                            bllVipCardUpgradeReward.Delete(entityVipCardUpgradeReward);
                                            break;
                                        case 1://新增券
                                            var existVipCardUpgradeReward = bllVipCardUpgradeReward.QueryByEntity(new VipCardUpgradeRewardEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardTypeID = VipCardTypeResult.VipCardTypeID, CouponTypeId = new Guid(Rewards.CouponTypeID) }, null).FirstOrDefault();
                                            if (existVipCardUpgradeReward != null)
                                            {
                                                existVipCardUpgradeReward.CouponTypeId = new Guid(Rewards.CouponTypeID);
                                                existVipCardUpgradeReward.CouponNum = Rewards.CouponNum;
                                                existVipCardUpgradeReward.CustomerID = loggingSessionInfo.ClientID;
                                                bllVipCardUpgradeReward.Update(existVipCardUpgradeReward);
                                            }
                                            else
                                            {
                                                entityVipCardUpgradeReward.CardUpgradeRewardId = Guid.NewGuid();
                                                bllVipCardUpgradeReward.Create(entityVipCardUpgradeReward);
                                            }                                            
                                            break;
                                        case 2://修改
                                            bllVipCardUpgradeReward.Update(entityVipCardUpgradeReward);
                                            break;
                                    }
                                    
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 添加会员卡等级信息
                            //如果卡等级不重复则添加卡等级数据
                            var entityVipCardType = new SysVipCardTypeEntity();
                            entityVipCardType.Category = 0;
                            entityVipCardType.VipCardTypeName = VipCardSystem.VipCardType.VipCardTypeName;
                            entityVipCardType.VipCardLevel = VipCardSystem.VipCardType.VipCardLevel;
                            entityVipCardType.IsPassword = 0;
                            if (entityVipCardType.VipCardLevel == 1)//如果等级为1默认可充值
                            {
                                entityVipCardType.Isprepaid = 1;
                            }
                            else
                            {
                                entityVipCardType.Isprepaid = VipCardSystem.VipCardType.IsPrepaid;
                            }       
                            entityVipCardType.IsOnlineSales = VipCardSystem.VipCardType.IsOnlineSales;
                            entityVipCardType.PicUrl = VipCardSystem.VipCardType.PicUrl;
                            entityVipCardType.CustomerID = loggingSessionInfo.ClientID;
                            if (VipCardSystem.VipCardUpgradeRule != null)
                            {
                                if (VipCardSystem.VipCardUpgradeRule.IsPurchaseUpgrade == 1)
                                {
                                    entityVipCardType.Prices = VipCardSystem.VipCardUpgradeRule.Prices;
                                    entityVipCardType.ExchangeIntegral = VipCardSystem.VipCardUpgradeRule.ExchangeIntegral;
                                    entityVipCardType.IsExtraMoney = VipCardSystem.VipCardUpgradeRule.IsExtraMoney;
                                }
                                else
                                {
                                    entityVipCardType.Prices = 0;
                                    entityVipCardType.ExchangeIntegral = 0;
                                    entityVipCardType.IsExtraMoney = 2;
                                }
                            }
                            else
                            {
                                entityVipCardType.Prices = 0;
                                entityVipCardType.ExchangeIntegral = 0;
                                entityVipCardType.IsExtraMoney = 2;
                            }
                            var existVipCardType = bllVipCardType.QueryByEntity(new SysVipCardTypeEntity() { Category = 0, VipCardLevel = VipCardSystem.VipCardType.VipCardLevel, CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                            if (existVipCardType==null)
                            {
                                //要先生成卡等级数据  不然取不到VipCardTypeID
                                if (VipCardSystem.VipCardType.VipCardLevel != 0 && VipCardSystem.VipCardType.VipCardLevel < 8)
                                {
                                    bllVipCardType.Create(entityVipCardType);
                                    //添加虚拟商品
                                    ItemService _ItemService = new ItemService(loggingSessionInfo);
                                    _ItemService.SaveCardToOffenTItem(loggingSessionInfo, entityVipCardType);
                                }
                                else
                                {
                                    throw new APIException("等级不能超过七级！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                }
                            }
                            //else
                            //{
                            //    throw new APIException("会员卡名称不能重复！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                            //}
                            
                            #endregion                            
                            //因为卡等级不能重复  所以这里根据卡等级获取VipCardTypeID的信息
                            var vipCardTypeInfo = bllVipCardType.QueryByEntity(new SysVipCardTypeEntity() { Category = 0, VipCardLevel = VipCardSystem.VipCardType.VipCardLevel, CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                            //因为注册表单暂时没用上 如果卡等级不等于1时 就有升级条件数据存进去
                            if (VipCardSystem.VipCardType.VipCardLevel != 0)
                            {
                                #region 添加卡升级规则信息
                                if (VipCardSystem.VipCardType.VipCardLevel != 1)
                                {
                                    var entityVipCardUpgradeRule = new VipCardUpgradeRuleEntity();
                                    entityVipCardUpgradeRule.VipCardUpgradeRuleId = System.Guid.NewGuid();
                                    if (vipCardTypeInfo != null && vipCardTypeInfo.VipCardTypeID != null)
                                    {
                                        entityVipCardUpgradeRule.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                                    }
                                    //是否购卡升级
                                    if (VipCardSystem.VipCardUpgradeRule != null)
                                    {
                                        if (VipCardSystem.VipCardUpgradeRule.IsPurchaseUpgrade == 1)
                                        {
                                            entityVipCardUpgradeRule.IsPurchaseUpgrade = 1;                                            
                                        }
                                        else
                                        {
                                            entityVipCardUpgradeRule.IsPurchaseUpgrade = 0;
                                            entityVipCardUpgradeRule.OnceRechargeAmount = 0;
                                        }
                                        //是否充值升级
                                        if (VipCardSystem.VipCardUpgradeRule.IsRecharge == 1)
                                        {
                                            entityVipCardUpgradeRule.IsRecharge = 1;
                                            entityVipCardUpgradeRule.OnceRechargeAmount = VipCardSystem.VipCardUpgradeRule.OnceRechargeAmount;
                                        }
                                        else
                                        {
                                            entityVipCardUpgradeRule.IsRecharge = 0;
                                            entityVipCardUpgradeRule.OnceRechargeAmount = 0;
                                        }
                                        //是否消费升级
                                        if (VipCardSystem.VipCardUpgradeRule.IsBuyUpgrade == 1)
                                        {
                                            entityVipCardUpgradeRule.IsBuyUpgrade = 1;
                                            entityVipCardUpgradeRule.BuyAmount = VipCardSystem.VipCardUpgradeRule.BuyAmount;
                                            entityVipCardUpgradeRule.OnceBuyAmount = VipCardSystem.VipCardUpgradeRule.OnceBuyAmount;
                                        }
                                        else
                                        {
                                            entityVipCardUpgradeRule.IsBuyUpgrade = 0;
                                            entityVipCardUpgradeRule.BuyAmount = 0;
                                            entityVipCardUpgradeRule.OnceBuyAmount = 0;
                                        }
                                    }
                                    else
                                    {
                                        throw new APIException("升级条件不能为空！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                    }
                                    entityVipCardUpgradeRule.CustomerID = loggingSessionInfo.ClientID;
                                    //将卡升级规则进行入库
                                    bllVipCardUpgradeRule.Create(entityVipCardUpgradeRule);
                                }
                           #endregion
                                #region 添加基本权益实体
                                //基本权益实体
                                var entityVipCardRule = new VipCardRuleEntity();
                                if (vipCardTypeInfo != null && vipCardTypeInfo.VipCardTypeID != null)
                                {
                                    entityVipCardRule.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                                }
                                if (VipCardSystem.VipCardRule != null)
                                {
                                    entityVipCardRule.CardDiscount = VipCardSystem.VipCardRule.CardDiscount * 10;//因为给的是整数 目前折扣这里乘10
                                    entityVipCardRule.PaidGivePoints = VipCardSystem.VipCardRule.PaidGivePoints;
                                    entityVipCardRule.PaidGivePercetPoints = VipCardSystem.VipCardRule.PaidGivePercetPoints;
                                }
                                else
                                {
                                    entityVipCardRule.CardDiscount = 0;//因为给的是整数 目前折扣这里乘10
                                    entityVipCardRule.PaidGivePoints = 0;
                                    entityVipCardRule.PaidGivePercetPoints = 0;
                                }
                                entityVipCardRule.CustomerID = loggingSessionInfo.ClientID;
                                //将基本权益数据进行入库
                                bllVipCardRule.Create(entityVipCardRule);
                                #endregion
                                #region 添加开卡礼信息
                                if (VipCardSystem.VipCardUpgradeRewardList != null)
                                {
                                    //开卡礼处理
                                    foreach (var Rewards in VipCardSystem.VipCardUpgradeRewardList)
                                    {
                                        var entityVipCardUpgradeReward = new VipCardUpgradeRewardEntity();
                                        entityVipCardUpgradeReward.CardUpgradeRewardId = Guid.NewGuid();
                                        if (vipCardTypeInfo != null && vipCardTypeInfo.VipCardTypeID != null)
                                        {
                                            entityVipCardUpgradeReward.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                                        }
                                        entityVipCardUpgradeReward.CouponTypeId = new Guid(Rewards.CouponTypeID);
                                        entityVipCardUpgradeReward.CouponNum = Rewards.CouponNum;
                                        entityVipCardUpgradeReward.CustomerID = loggingSessionInfo.ClientID;
                                        var existVipCardUpgradeReward = bllVipCardUpgradeReward.QueryByEntity(new VipCardUpgradeRewardEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardTypeID = vipCardTypeInfo.VipCardTypeID, CouponTypeId = new Guid(Rewards.CouponTypeID) }, null).FirstOrDefault();
                                        if (existVipCardUpgradeReward != null)
                                        {
                                            existVipCardUpgradeReward.CouponTypeId = new Guid(Rewards.CouponTypeID);
                                            existVipCardUpgradeReward.CouponNum = Rewards.CouponNum;
                                            existVipCardUpgradeReward.CustomerID = loggingSessionInfo.ClientID;
                                            bllVipCardUpgradeReward.Update(existVipCardUpgradeReward);
                                        }
                                        else
                                        {
                                            bllVipCardUpgradeReward.Create(entityVipCardUpgradeReward);
                                        }  
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                catch (APIException ex)
                {
                    //pTran.Rollback();
                    throw ex;
                }
            }
            return rd;
        }
    }
}