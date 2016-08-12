/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:29
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Base;
using RedisOpenAPIClient.Models.CC;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipCardVipMappingBLL
    {
        /// <summary>
        /// 会员微信注册绑卡
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="vipCode"></param>
        /// <param name="unitId">会籍店</param>
        public void BindVipCard(string vipId, string vipCode, string unitId)
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);

            UnitService unitServer = new UnitService(CurrentUserInfo);
            if (string.IsNullOrEmpty(unitId))
                unitId = unitServer.GetUnitByUnitTypeForWX("总部", null).Id; //获取总部门店标识

            //根据vipid查询VipCardVipMapping，判断是否有绑卡,没绑卡默认给等级1的卡
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipId }, null).FirstOrDefault();
            if (vipCardMappingInfo == null)
            {
                //判断商户是否有付费的会员卡，有付费会员卡时，不自动绑卡
                List<IWhereCondition> freeCardCon = new List<IWhereCondition> { };
                freeCardCon.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
                freeCardCon.Add(new DirectCondition("VipCardLevel=1"));
                var orderBys = new OrderBy[1];
                orderBys[0] = new OrderBy() { FieldName = "VipCardLevel", Direction = OrderByDirections.Asc };
                var freeCardTypeInfo = sysVipCardTypeBLL.Query(freeCardCon.ToArray(), orderBys).FirstOrDefault();
                if (freeCardTypeInfo != null)
                {
                    //查询最低等级的会员卡类型
                    var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0,VipCardLevel=1 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                    if (vipCardTypeInfo != null)
                    {
                        //查询此类型会员卡是否存在
                        var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardTypeID = vipCardTypeInfo.VipCardTypeID, VipCardStatusId = 0, MembershipUnit = "" }, null).FirstOrDefault();
                        //不存在,制卡
                        if (vipCardInfo == null)
                        {
                            vipCardInfo = new VipCardEntity();
                            vipCardInfo.VipCardID = Guid.NewGuid().ToString();
                            vipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                            vipCardInfo.VipCardTypeName = vipCardTypeInfo.VipCardTypeName;
                            vipCardInfo.VipCardCode = vipCode;
                            vipCardInfo.VipCardStatusId = 1;//正常
                            vipCardInfo.MembershipUnit = unitId;
                            vipCardInfo.MembershipTime = DateTime.Now;
                            vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
                            vipCardBLL.Create(vipCardInfo);
                        }
                        else//存在更新成现在的
                        {
                            vipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                            vipCardBLL.Update(vipCardInfo);
                        }
                        //新增会员卡操作状态信息
                        var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                        {
                            LogID = Guid.NewGuid().ToString().Replace("-", ""),
                            VipCardStatusID = vipCardInfo.VipCardStatusId,
                            VipCardID = vipCardInfo.VipCardID,
                            Action = "注册",
                            UnitID = unitId,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        vipCardStatusChangeLogBLL.Create(vipCardStatusChangeLogEntity);

                        //绑定会员卡和会员
                        var vipCardVipMappingEntity = new VipCardVipMappingEntity()
                        {
                            MappingID = Guid.NewGuid().ToString().Replace("-", ""),
                            VIPID = vipId,
                            VipCardID = vipCardInfo.VipCardID,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        vipCardVipMappingBLL.Create(vipCardVipMappingEntity);
                        //开卡礼
                        var vipCardUpgradeRewardBll = new VipCardUpgradeRewardBLL(CurrentUserInfo);
                        var vipCardUpgradeRewardEntityList = vipCardUpgradeRewardBll.QueryByEntity(new VipCardUpgradeRewardEntity() { VipCardTypeID = vipCardTypeInfo.VipCardTypeID }, null);
                        var redisVipMappingCouponBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon.RedisVipMappingCouponBLL();
                        if (vipCardUpgradeRewardEntityList.Length > 0)
                        {
                            foreach (var vipCardUpgradeRewardEntity in vipCardUpgradeRewardEntityList)
                            {
                                for (int i = 0; i < vipCardUpgradeRewardEntity.CouponNum; i++)
                                {
                                    redisVipMappingCouponBLL.SetVipMappingCoupon(new CC_Coupon()
                                    {
                                        CustomerId = CurrentUserInfo.ClientID,
                                        CouponTypeId = vipCardUpgradeRewardEntity.CouponTypeId.ToString()
                                    }, vipCardUpgradeRewardEntity.CardUpgradeRewardId.ToString(), vipId, "OpenVipCard");
                                }
                            }
                        }
                    }
                    else
                        throw new APIException("系统未创建会员卡类型") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }                
            }
            else //更新旧卡 需要确认旧卡是否为卡体系内的卡
            {

                var oldVipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();
                int oldVipCardTypeID = oldVipCardInfo.VipCardTypeID ?? 0;
                //查询最低等级的会员卡类型
                var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0, VipCardLevel = 1 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                if (vipCardTypeInfo == null)
                {
                    throw new APIException("系统未创建会员卡类型体系") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
                if (oldVipCardInfo != null)
                {
                    //旧卡不为空时,确认当前VipCardTypeID是否属于卡体系
                    var curvipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0, VipCardTypeID = oldVipCardInfo.VipCardTypeID }, new OrderBy[] { new OrderBy() { FieldName = "VipCardTypeID", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                    if (curvipCardTypeInfo == null)//若为空  则更新为当前信息
                    {
                        oldVipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                        vipCardBLL.Update(oldVipCardInfo);
                        //开卡礼
                        var vipCardUpgradeRewardBll = new VipCardUpgradeRewardBLL(CurrentUserInfo);
                        var vipCardUpgradeRewardEntityList = vipCardUpgradeRewardBll.QueryByEntity(new VipCardUpgradeRewardEntity() { VipCardTypeID = vipCardTypeInfo.VipCardTypeID }, null);
                        var redisVipMappingCouponBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon.RedisVipMappingCouponBLL();
                        if (vipCardUpgradeRewardEntityList.Length > 0)
                        {
                            foreach (var vipCardUpgradeRewardEntity in vipCardUpgradeRewardEntityList)
                            {
                                for (int i = 0; i < vipCardUpgradeRewardEntity.CouponNum; i++)
                                {
                                    redisVipMappingCouponBLL.SetVipMappingCoupon(new CC_Coupon()
                                    {
                                        CustomerId = CurrentUserInfo.ClientID,
                                        CouponTypeId = vipCardUpgradeRewardEntity.CouponTypeId.ToString()
                                    }, vipCardUpgradeRewardEntity.CardUpgradeRewardId.ToString(), vipId, "OpenVipCard");
                                }
                            }
                        }
                    }                    
                }
                //老卡操作状态信息
                var oldVipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                {
                    LogID = Guid.NewGuid().ToString().Replace("-", ""),
                    VipCardStatusID = oldVipCardInfo.VipCardStatusId,
                    VipCardID = oldVipCardInfo.VipCardID,
                    Action = "注册",
                    UnitID = unitId,
                    CustomerID = CurrentUserInfo.ClientID
                };
                vipCardStatusChangeLogBLL.Create(oldVipCardStatusChangeLogEntity);                
            }
        }

        /// <summary>
        /// 支付完成时注册绑卡   (目前可用于绑卡列表处的绑卡升级处理)
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="vipCode"></param>
        /// <param name="unitId"></param>
        /// <param name="ObjecetTypeId">卡类型Id</param>
        /// <param name="OrderType">订单类型 SalesCard=销售 Recharge=充值</param>
        /// <param name="OperationType">操作类型 1-手动 2-自动</param>
        /// <returns></returns>
        public string BindVirtualItem(string vipId, string vipCode, string unitId, int ObjecetTypeId, string OrderType = "SalesCard", int OperationType = 1,string orderId = "")
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);

            string ObjectNo = string.Empty;//卡号或券号
            try
            {
                UnitService unitServer = new UnitService(CurrentUserInfo);
                if (string.IsNullOrEmpty(unitId))
                    unitId = unitServer.GetUnitByUnitTypeForWX("总部", null).Id; //获取总部门店标识

                //根据vipid查询VipCardVipMapping，判断是否有绑卡
                var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipId }, null).FirstOrDefault();

                //判断商户是否有付费的会员卡，有付费会员卡时，不自动绑卡
                //List<IWhereCondition> freeCardCon = new List<IWhereCondition> { };
                //freeCardCon.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
                //freeCardCon.Add(new DirectCondition("Prices>0"));
                //var freeCardTypeInfo = sysVipCardTypeBLL.Query(freeCardCon.ToArray(), null).FirstOrDefault();
                //if (freeCardTypeInfo == null)
                //{
                ////查询最低等级的会员卡类型
                //var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                //if (vipCardTypeInfo != null)
                //{


                if (vipCardMappingInfo == null)//绑卡
                {
                    //最低等级卡类型
                    var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0, VipCardLevel = 1 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                    if (vipCardTypeInfo == null)
                    {
                        throw new APIException("系统未创建会员卡类型") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                    //查询此类型会员卡是否存在
                    var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardTypeID = vipCardTypeInfo.VipCardTypeID, VipCardStatusId = 0, MembershipUnit = "" }, null).FirstOrDefault();
                    //不存在,制卡
                    if (vipCardInfo == null)
                    {
                        vipCardInfo = new VipCardEntity();
                        vipCardInfo.VipCardID = Guid.NewGuid().ToString();
                        vipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                        //vipCardInfo.VipCardTypeName = vipCardTypeInfo.VipCardTypeName;
                        vipCardInfo.VipCardCode = vipCode;
                        vipCardInfo.VipCardStatusId = 1;//正常
                        vipCardInfo.MembershipUnit = unitId;
                        vipCardInfo.MembershipTime = DateTime.Now;
                        vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
                        vipCardBLL.Create(vipCardInfo);
                    }
                    ObjectNo = vipCardInfo.VipCardCode;
                    //绑定会员卡和会员
                    var vipCardVipMappingEntity = new VipCardVipMappingEntity()
                    {
                        MappingID = Guid.NewGuid().ToString().Replace("-", ""),
                        VIPID = vipId,
                        VipCardID = vipCardInfo.VipCardID,
                        CustomerID = CurrentUserInfo.ClientID
                    };
                    vipCardVipMappingBLL.Create(vipCardVipMappingEntity);
                    vipCardMappingInfo = vipCardVipMappingEntity;
                    //新增会员卡操作状态信息
                    var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                    {
                        LogID = Guid.NewGuid().ToString().Replace("-", ""),
                        VipCardStatusID = vipCardInfo.VipCardStatusId,
                        VipCardID = vipCardInfo.VipCardID,
                        Action = "注册",
                        UnitID = unitId,
                        CustomerID = CurrentUserInfo.ClientID
                    };
                    vipCardStatusChangeLogBLL.Create(vipCardStatusChangeLogEntity);
                }
 
                #region 老卡业务处理

                ObjectNo = vipCode;
                //老卡信息获取
                var oldVipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();

                //老卡卡类型ID
                int oldVipCardTypeID = 0;
                if (oldVipCardInfo != null)
                {
                    //获取老卡的类型
                    var oldVipCardType = sysVipCardTypeBLL.GetByID(oldVipCardInfo.VipCardTypeID);
                    //获取即将升级卡的类型
                    var newVipCardType = sysVipCardTypeBLL.GetByID(ObjecetTypeId);
                    if (newVipCardType.VipCardLevel > oldVipCardType.VipCardLevel)
                    {
                        //老卡卡类型ID
                        oldVipCardTypeID = oldVipCardInfo.VipCardTypeID ?? 0;
                        //更新卡信息
                        oldVipCardInfo.VipCardTypeID = ObjecetTypeId;
                        vipCardBLL.Update(oldVipCardInfo);
                        //老卡操作状态信息
                        var oldVipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                        {
                            LogID = Guid.NewGuid().ToString().Replace("-", ""),
                            VipCardStatusID = oldVipCardInfo.VipCardStatusId,
                            VipCardID = oldVipCardInfo.VipCardID,
                            Action = "升级处理",
                            UnitID = unitId,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        vipCardStatusChangeLogBLL.Create(oldVipCardStatusChangeLogEntity);

                        //入升级日志
                        var vipCardGradeChangeLogBll = new VipCardGradeChangeLogBLL(CurrentUserInfo);
                        var vipCardUpgradeRuleBll = new VipCardUpgradeRuleBLL(CurrentUserInfo);
                        var vipCardUpgradeRuleEntity = vipCardUpgradeRuleBll.QueryByEntity(new VipCardUpgradeRuleEntity() { CustomerID = CurrentUserInfo.ClientID, VipCardTypeID = ObjecetTypeId }, null).FirstOrDefault();
                        var VipCardGradeChangeLogEntity = new VipCardGradeChangeLogEntity()
                        {
                            ChangeLogID = Guid.NewGuid().ToString().Replace("-", ""),
                            VipCardUpgradeRuleId = vipCardUpgradeRuleEntity.VipCardUpgradeRuleId,
                            OrderType = OrderType,
                            VipCardID = oldVipCardInfo.VipCardID,
                            ChangeBeforeVipCardID = oldVipCardInfo.VipCardID,
                            ChangeBeforeGradeID = oldVipCardTypeID,
                            NowGradeID = oldVipCardInfo.VipCardTypeID,
                            ChangeReason = "upgrade",
                            OperationType = OperationType,
                            ChangeTime = DateTime.Now,
                            UnitID = unitId,
                            OrderId = orderId,
                            CustomerID = CurrentUserInfo.ClientID,
                        };
                        vipCardGradeChangeLogBll.Create(VipCardGradeChangeLogEntity);

                        //开卡礼
                        var vipCardUpgradeRewardBll = new VipCardUpgradeRewardBLL(CurrentUserInfo);
                        var vipCardUpgradeRewardEntityList = vipCardUpgradeRewardBll.QueryByEntity(new VipCardUpgradeRewardEntity() { VipCardTypeID = ObjecetTypeId }, null);
                        var redisVipMappingCouponBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon.RedisVipMappingCouponBLL();
                        if (vipCardUpgradeRewardEntityList.Length > 0)
                        {
                            foreach (var vipCardUpgradeRewardEntity in vipCardUpgradeRewardEntityList)
                            {
                                for (int i = 0; i < vipCardUpgradeRewardEntity.CouponNum; i++)
                                {
                                    redisVipMappingCouponBLL.SetVipMappingCoupon(new CC_Coupon()
                                    {
                                        CustomerId = CurrentUserInfo.ClientID,
                                        CouponTypeId = vipCardUpgradeRewardEntity.CouponTypeId.ToString()
                                    }, vipCardUpgradeRewardEntity.CardUpgradeRewardId.ToString(), vipId, "OpenVipCard");
                                }
                            }
                        }
                        //消费升级
                        if (OperationType == 2)
                        {
                            //群发消息表
                            var InnerGroupNewsBll = new InnerGroupNewsBLL(CurrentUserInfo);
                            var innerGroupNewsEntity = new InnerGroupNewsEntity()
                            {
                                GroupNewsId = Guid.NewGuid().ToString().Replace("-", ""),
                                SentType = 2,
                                NoticePlatformType = 1,
                                BusType = 3,
                                Title = "升级提醒",
                                Text = "升级成功",
                                CustomerID = CurrentUserInfo.ClientID
                            };
                            InnerGroupNewsBll.Create(innerGroupNewsEntity);
                            //消息和用户关系表
                            var newsUserMappingBll = new NewsUserMappingBLL(CurrentUserInfo);
                            var newsUserMappingEntity = new NewsUserMappingEntity()
                            {
                                MappingID = Guid.NewGuid().ToString().Replace("-", ""),
                                UserID = vipId,
                                GroupNewsID = innerGroupNewsEntity.GroupNewsId,
                                CustomerId = CurrentUserInfo.ClientID,
                                HasRead = 0
                            };
                            newsUserMappingBll.Create(newsUserMappingEntity);
                        }
                    }
                }
                #endregion
                
                  
            
            }
            catch { throw new APIException("升级失败！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS }; };
            return ObjectNo;
        }

        public void updateVipCardByType(string vipId, int vipCardTypeId,string changeReason,string remark, string vipCode, IDbTransaction pTran)
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);

            UnitService unitServer = new UnitService(CurrentUserInfo);
            string unitId = unitServer.GetUnitByUnitTypeForWX("总部", null).Id; //获取总部门店标识

            VipCardEntity vipCardInfo = vipCardBLL.GetVipCardByVipMapping(vipId);

            //更新卡信息
            vipCardInfo.VipCardTypeID = vipCardTypeId;
            vipCardBLL.Update(vipCardInfo,pTran);

            //var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardTypeID = vipCardTypeId, VipCardStatusId = 0, MembershipUnit = "" }, null).FirstOrDefault();
            
            //不存在，制卡
            //if (vipCardInfo == null)
            //{
            //    vipCardInfo = new VipCardEntity();
            //    vipCardInfo.VipCardID = Guid.NewGuid().ToString();
            //    vipCardInfo.VipCardTypeID = vipCardTypeId;
            //    vipCardInfo.VipCardTypeName = vipCardTypeName;
            //    vipCardInfo.VipCardCode = vipCode;
            //    vipCardInfo.VipCardStatusId = 1;//正常
            //    vipCardInfo.MembershipUnit = unitId;
            //    vipCardInfo.MembershipTime = DateTime.Now;
            //    vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
            //    vipCardBLL.Create(vipCardInfo, pTran);
            //}

            //新增会员卡操作状态信息
            var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
            {
                LogID = Guid.NewGuid().ToString().Replace("-", ""),
                VipCardStatusID = vipCardInfo.VipCardStatusId,
                VipCardID = vipCardInfo.VipCardID,
                Action = "升级",
                Reason = changeReason,
                Remark = remark,
                UnitID = unitId,
                CustomerID = CurrentUserInfo.ClientID
            };
            vipCardStatusChangeLogBLL.Create(vipCardStatusChangeLogEntity, pTran);

            
            var vipCardVipMappingEntity = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity(){ VipCardID = vipCardInfo.VipCardID },null).FirstOrDefault();
            vipCardVipMappingEntity.LastUpdateTime = DateTime.Now;

            vipCardVipMappingBLL.Update(vipCardVipMappingEntity, pTran);
            //绑定会员卡和会员
            //var vipCardVipMappingEntity = new VipCardVipMappingEntity()
            //{
            //    MappingID = Guid.NewGuid().ToString().Replace("-", ""),
            //    VIPID = vipId,
            //    VipCardID = vipCardInfo.VipCardID,
            //    CustomerID = CurrentUserInfo.ClientID
            //};
            //vipCardVipMappingBLL.Create(vipCardVipMappingEntity, pTran);
            
            
        }
    }
}