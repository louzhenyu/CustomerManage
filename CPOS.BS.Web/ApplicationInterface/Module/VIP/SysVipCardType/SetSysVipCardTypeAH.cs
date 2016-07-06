using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.SysVipCardType
{
    public class SetSysVipCardTypeAH : BaseActionHandler<SetSysVipCardTypeRP, SetSpecialDateRD>
    {
        protected override SetSpecialDateRD ProcessRequest(DTO.Base.APIRequest<SetSysVipCardTypeRP> pRequest)
        {
            var rd = new SetSpecialDateRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(loggingSessionInfo);
            SysVipCardTypeEntity sysVipCardTypeEntity = null;   //卡类型实体
            VipCardRuleEntity vipCardRuleEntity = null;         //卡类型规则实体
            SysVipCardTypeEntity[] vipcardList = null;          //获取同种类的所有卡类型集合
            SysVipCardTypeEntity typeCodeEntity =   null;         //相同vipCardTypeCode对象
            SysVipCardTypeEntity typeNameEntity = null;         //相同vipCardTypeName对象
            SysVipCardTypeEntity levelEntity = null;            //相同Level对象

            var pTran = sysVipCardTypeBLL.GetTran();            //事务  
            //获取同种类的所有卡类型
            vipcardList = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = loggingSessionInfo.ClientID, Category = para.Category }, new OrderBy[] { new OrderBy() { FieldName = "VipCardLevel", Direction = OrderByDirections.Desc } });

            //判断编码是否重复
            if (!string.IsNullOrEmpty(para.VipCardTypeCode))
                typeCodeEntity = vipcardList.Where(t => t.VipCardTypeCode == para.VipCardTypeCode).FirstOrDefault();

            //判断名称是否重复
            if (!string.IsNullOrEmpty(para.VipCardTypeName))
                typeNameEntity = vipcardList.Where(t => t.VipCardTypeName == para.VipCardTypeName).FirstOrDefault();

            //判断级别是否重复
            levelEntity = vipcardList.Where(t => t.VipCardLevel == para.VipCardLevel).FirstOrDefault();

            using (pTran.Connection)
            {
                try
                {
                    if (para.VipCardTypeID == 0 || para.VipCardTypeID == null)//创建
                    {
                        //判断编码是否重复
                        if (typeCodeEntity != null)
                            throw new APIException("同一种卡类型编码已存在") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        //判断编码是否重复
                        if (typeNameEntity != null)
                            throw new APIException("同一种卡类型名称已存在") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        //创建会员卡时判断等级是否重复
                        if (levelEntity != null && para.Category == 0)
                            throw new APIException("同一种卡类型等级已存在") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        //判断等级是否跨级创建
                        if (vipcardList != null && vipcardList.Count() > 0)
                        {
                            if (vipcardList[0].VipCardLevel + 1 < para.VipCardLevel)
                                throw new APIException("同一种卡类型不能跨等级创建，请创建等级" + (vipcardList[0].VipCardLevel + 1)) { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }
                        sysVipCardTypeEntity = new SysVipCardTypeEntity();   //卡类型实体
                        vipCardRuleEntity = new VipCardRuleEntity();         //卡类型规则实体
                    }
                    else//编辑
                    {
                        //获取卡信息
                        sysVipCardTypeEntity = sysVipCardTypeBLL.GetByID(para.VipCardTypeID);
                        //判断编码是否重复
                        if (sysVipCardTypeEntity.VipCardTypeCode != para.VipCardTypeCode)
                        {
                            if (typeCodeEntity != null)
                                throw new APIException("同一种卡类型编码已存在") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }
                        //判断编码是否重复
                        if (sysVipCardTypeEntity.VipCardTypeName != para.VipCardTypeName)
                        {
                            if (typeNameEntity != null)
                                throw new APIException("同一种卡类型名称已存在") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }
                        if (sysVipCardTypeEntity.VipCardLevel != para.VipCardLevel)
                        {
                            //修改会员卡判断等级是否重复
                            if (levelEntity != null && para.Category == 0)
                                throw new APIException("同一种卡类型等级已存在") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }
                        //判断等级是否跨级创建
                        if (vipcardList != null && vipcardList.Count() > 0)
                        {
                            if (sysVipCardTypeEntity.VipCardLevel != para.VipCardLevel)
                            {
                                if (vipcardList[0].VipCardLevel + 1 < para.VipCardLevel)
                                    throw new APIException("卡等级不能修改") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                            }
                        }
                        //获取卡规则信息
                        vipCardRuleEntity = vipCardRuleBLL.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = para.VipCardTypeID }, null).FirstOrDefault();
                    }

                    //卡类型
                    sysVipCardTypeEntity.VipCardTypeCode = para.VipCardTypeCode;
                    sysVipCardTypeEntity.VipCardTypeName = para.VipCardTypeName;
                    sysVipCardTypeEntity.PicUrl = para.PicUrl;
                    sysVipCardTypeEntity.IsDiscount = para.CardDiscount > 0 ? 1 : 0; //是否可打折扣（0=不可用；1=可用）
                    sysVipCardTypeEntity.IsPoints = para.PointsMultiple > 0 ? 1 : 0; //是否可积分（0=不可用；1=可用）
                    sysVipCardTypeEntity.IsOnlineRecharge = para.ChargeFull > 0 ? 1 : 0;//是否可线上充值（0=不可用；1=可用）
                    sysVipCardTypeEntity.IsPassword = para.IsPassword;
                    sysVipCardTypeEntity.Category = para.Category;
                    sysVipCardTypeEntity.VipCardLevel = para.VipCardLevel;
                    sysVipCardTypeEntity.Prices = para.Prices;
                    sysVipCardTypeEntity.IsExtraMoney = para.IsExtraMoney;
                    sysVipCardTypeEntity.ExchangeIntegral = para.ExchangeIntegral;
                    sysVipCardTypeEntity.UpgradeAmount = para.UpgradeAmount;
                    sysVipCardTypeEntity.UpgradeOnceAmount = para.UpgradeOnceAmount;
                    sysVipCardTypeEntity.UpgradePoint = para.UpgradePoint;
                    sysVipCardTypeEntity.CustomerID = loggingSessionInfo.ClientID;

                    //卡规则
                    vipCardRuleEntity.CardDiscount = para.CardDiscount;
                    vipCardRuleEntity.PointsMultiple = para.PointsMultiple;
                    vipCardRuleEntity.ChargeFull = para.ChargeFull;
                    vipCardRuleEntity.ChargeGive = para.ChargeGive;
                    vipCardRuleEntity.PaidGivePoints = para.PaidGivePoints;
                    vipCardRuleEntity.ReturnAmountPer = para.ReturnAmountPer;
                    vipCardRuleEntity.CustomerID = loggingSessionInfo.ClientID;

                    if (para.VipCardTypeID == 0 || para.VipCardTypeID == null) //创建
                    {
                        sysVipCardTypeBLL.Create(sysVipCardTypeEntity, pTran);

                        vipCardRuleEntity.VipCardTypeID = sysVipCardTypeEntity.VipCardTypeID;
                        vipCardRuleBLL.Create(vipCardRuleEntity, pTran);
                    }
                    else //修改
                    {
                        sysVipCardTypeBLL.Update(sysVipCardTypeEntity, pTran);
                        vipCardRuleBLL.Update(vipCardRuleEntity, pTran);
                    }
                    pTran.Commit();  //提交事物
                    rd.VipCardTypeID = sysVipCardTypeEntity.VipCardTypeID;
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
                catch (Exception ex)
                {
                    pTran.Rollback();//回滚事物
                    throw new Exception(ex.Message);
                }
                return rd;
            }
        }
    }
}