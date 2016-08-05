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
    /// ҵ����  
    /// </summary>
    public partial class VipCardVipMappingBLL
    {
        /// <summary>
        /// ��Ա΢��ע���
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="vipCode"></param>
        /// <param name="unitId">�Ἦ��</param>
        public void BindVipCard(string vipId, string vipCode, string unitId)
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);

            UnitService unitServer = new UnitService(CurrentUserInfo);
            if (string.IsNullOrEmpty(unitId))
                unitId = unitServer.GetUnitByUnitTypeForWX("�ܲ�", null).Id; //��ȡ�ܲ��ŵ��ʶ

            //����vipid��ѯVipCardVipMapping���ж��Ƿ��а�,û��Ĭ�ϸ��ȼ�1�Ŀ�
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipId }, null).FirstOrDefault();
            if (vipCardMappingInfo == null)
            {
                //�ж��̻��Ƿ��и��ѵĻ�Ա�����и��ѻ�Ա��ʱ�����Զ���
                List<IWhereCondition> freeCardCon = new List<IWhereCondition> { };
                freeCardCon.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
                freeCardCon.Add(new DirectCondition("VipCardLevel=1"));
                var orderBys = new OrderBy[1];
                orderBys[0] = new OrderBy() { FieldName = "VipCardLevel", Direction = OrderByDirections.Asc };
                var freeCardTypeInfo = sysVipCardTypeBLL.Query(freeCardCon.ToArray(), orderBys).FirstOrDefault();
                if (freeCardTypeInfo != null)
                {
                    //��ѯ��͵ȼ��Ļ�Ա������
                    var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0,VipCardLevel=1 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                    if (vipCardTypeInfo != null)
                    {
                        //��ѯ�����ͻ�Ա���Ƿ����
                        var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardTypeID = vipCardTypeInfo.VipCardTypeID, VipCardStatusId = 0, MembershipUnit = "" }, null).FirstOrDefault();
                        //������,�ƿ�
                        if (vipCardInfo == null)
                        {
                            vipCardInfo = new VipCardEntity();
                            vipCardInfo.VipCardID = Guid.NewGuid().ToString();
                            vipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                            vipCardInfo.VipCardTypeName = vipCardTypeInfo.VipCardTypeName;
                            vipCardInfo.VipCardCode = vipCode;
                            vipCardInfo.VipCardStatusId = 1;//����
                            vipCardInfo.MembershipUnit = unitId;
                            vipCardInfo.MembershipTime = DateTime.Now;
                            vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
                            vipCardBLL.Create(vipCardInfo);
                        }
                        else//���ڸ��³����ڵ�
                        {
                            vipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                            vipCardBLL.Update(vipCardInfo);
                        }
                        //������Ա������״̬��Ϣ
                        var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                        {
                            LogID = Guid.NewGuid().ToString().Replace("-", ""),
                            VipCardStatusID = vipCardInfo.VipCardStatusId,
                            VipCardID = vipCardInfo.VipCardID,
                            Action = "ע��",
                            UnitID = unitId,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        vipCardStatusChangeLogBLL.Create(vipCardStatusChangeLogEntity);

                        //�󶨻�Ա���ͻ�Ա
                        var vipCardVipMappingEntity = new VipCardVipMappingEntity()
                        {
                            MappingID = Guid.NewGuid().ToString().Replace("-", ""),
                            VIPID = vipId,
                            VipCardID = vipCardInfo.VipCardID,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        vipCardVipMappingBLL.Create(vipCardVipMappingEntity);
                        //������
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
                        throw new APIException("ϵͳδ������Ա������") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }                
            }
            else //���¾ɿ� ��Ҫȷ�Ͼɿ��Ƿ�Ϊ����ϵ�ڵĿ�
            {

                var oldVipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();
                int oldVipCardTypeID = oldVipCardInfo.VipCardTypeID ?? 0;
                //��ѯ��͵ȼ��Ļ�Ա������
                var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0, VipCardLevel = 1 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                if (vipCardTypeInfo == null)
                {
                    throw new APIException("ϵͳδ������Ա��������ϵ") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
                if (oldVipCardInfo != null)
                {
                    //�ɿ���Ϊ��ʱ,ȷ�ϵ�ǰVipCardTypeID�Ƿ����ڿ���ϵ
                    var curvipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0, VipCardTypeID = oldVipCardInfo.VipCardTypeID }, new OrderBy[] { new OrderBy() { FieldName = "VipCardTypeID", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                    if (curvipCardTypeInfo == null)//��Ϊ��  �����Ϊ��ǰ��Ϣ
                    {
                        oldVipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                        vipCardBLL.Update(oldVipCardInfo);
                        //������
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
                //�Ͽ�����״̬��Ϣ
                var oldVipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                {
                    LogID = Guid.NewGuid().ToString().Replace("-", ""),
                    VipCardStatusID = oldVipCardInfo.VipCardStatusId,
                    VipCardID = oldVipCardInfo.VipCardID,
                    Action = "ע��",
                    UnitID = unitId,
                    CustomerID = CurrentUserInfo.ClientID
                };
                vipCardStatusChangeLogBLL.Create(oldVipCardStatusChangeLogEntity);                
            }
        }

        /// <summary>
        /// ֧�����ʱע���   (Ŀǰ�����ڰ��б��İ���������)
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="vipCode"></param>
        /// <param name="unitId"></param>
        /// <param name="ObjecetTypeId">������Id</param>
        /// <param name="OrderType">�������� SalesCard=���� Recharge=��ֵ</param>
        /// <param name="OperationType">�������� 1-�ֶ� 2-�Զ�</param>
        /// <returns></returns>
        public string BindVirtualItem(string vipId, string vipCode, string unitId, int ObjecetTypeId, string OrderType = "SalesCard", int OperationType = 1,string orderId = "")
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);

            string ObjectNo = string.Empty;//���Ż�ȯ��
            try
            {
                UnitService unitServer = new UnitService(CurrentUserInfo);
                if (string.IsNullOrEmpty(unitId))
                    unitId = unitServer.GetUnitByUnitTypeForWX("�ܲ�", null).Id; //��ȡ�ܲ��ŵ��ʶ

                //����vipid��ѯVipCardVipMapping���ж��Ƿ��а�
                var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipId }, null).FirstOrDefault();

                //�ж��̻��Ƿ��и��ѵĻ�Ա�����и��ѻ�Ա��ʱ�����Զ���
                //List<IWhereCondition> freeCardCon = new List<IWhereCondition> { };
                //freeCardCon.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
                //freeCardCon.Add(new DirectCondition("Prices>0"));
                //var freeCardTypeInfo = sysVipCardTypeBLL.Query(freeCardCon.ToArray(), null).FirstOrDefault();
                //if (freeCardTypeInfo == null)
                //{
                ////��ѯ��͵ȼ��Ļ�Ա������
                //var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                //if (vipCardTypeInfo != null)
                //{


                if (vipCardMappingInfo == null)//��
                {
                    //��͵ȼ�������
                    var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0, VipCardLevel = 1 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
                    if (vipCardTypeInfo == null)
                    {
                        throw new APIException("ϵͳδ������Ա������") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                    //��ѯ�����ͻ�Ա���Ƿ����
                    var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardTypeID = vipCardTypeInfo.VipCardTypeID, VipCardStatusId = 0, MembershipUnit = "" }, null).FirstOrDefault();
                    //������,�ƿ�
                    if (vipCardInfo == null)
                    {
                        vipCardInfo = new VipCardEntity();
                        vipCardInfo.VipCardID = Guid.NewGuid().ToString();
                        vipCardInfo.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                        //vipCardInfo.VipCardTypeName = vipCardTypeInfo.VipCardTypeName;
                        vipCardInfo.VipCardCode = vipCode;
                        vipCardInfo.VipCardStatusId = 1;//����
                        vipCardInfo.MembershipUnit = unitId;
                        vipCardInfo.MembershipTime = DateTime.Now;
                        vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
                        vipCardBLL.Create(vipCardInfo);
                    }
                    ObjectNo = vipCardInfo.VipCardCode;
                    //�󶨻�Ա���ͻ�Ա
                    var vipCardVipMappingEntity = new VipCardVipMappingEntity()
                    {
                        MappingID = Guid.NewGuid().ToString().Replace("-", ""),
                        VIPID = vipId,
                        VipCardID = vipCardInfo.VipCardID,
                        CustomerID = CurrentUserInfo.ClientID
                    };
                    vipCardVipMappingBLL.Create(vipCardVipMappingEntity);
                    vipCardMappingInfo = vipCardVipMappingEntity;
                    //������Ա������״̬��Ϣ
                    var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                    {
                        LogID = Guid.NewGuid().ToString().Replace("-", ""),
                        VipCardStatusID = vipCardInfo.VipCardStatusId,
                        VipCardID = vipCardInfo.VipCardID,
                        Action = "ע��",
                        UnitID = unitId,
                        CustomerID = CurrentUserInfo.ClientID
                    };
                    vipCardStatusChangeLogBLL.Create(vipCardStatusChangeLogEntity);
                }
 
                #region �Ͽ�ҵ����

                ObjectNo = vipCode;
                //�Ͽ���Ϣ��ȡ
                var oldVipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();

                //�Ͽ�������ID
                int oldVipCardTypeID = 0;
                if (oldVipCardInfo != null)
                {
                    //��ȡ�Ͽ�������
                    var oldVipCardType = sysVipCardTypeBLL.GetByID(oldVipCardInfo.VipCardTypeID);
                    //��ȡ����������������
                    var newVipCardType = sysVipCardTypeBLL.GetByID(ObjecetTypeId);
                    if (newVipCardType.VipCardLevel > oldVipCardType.VipCardLevel)
                    {
                        //�Ͽ�������ID
                        oldVipCardTypeID = oldVipCardInfo.VipCardTypeID ?? 0;
                        //���¿���Ϣ
                        oldVipCardInfo.VipCardTypeID = ObjecetTypeId;
                        vipCardBLL.Update(oldVipCardInfo);
                        //�Ͽ�����״̬��Ϣ
                        var oldVipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                        {
                            LogID = Guid.NewGuid().ToString().Replace("-", ""),
                            VipCardStatusID = oldVipCardInfo.VipCardStatusId,
                            VipCardID = oldVipCardInfo.VipCardID,
                            Action = "��������",
                            UnitID = unitId,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        vipCardStatusChangeLogBLL.Create(oldVipCardStatusChangeLogEntity);

                        //��������־
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

                        //������
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
                        //��������
                        if (OperationType == 2)
                        {
                            //Ⱥ����Ϣ��
                            var InnerGroupNewsBll = new InnerGroupNewsBLL(CurrentUserInfo);
                            var innerGroupNewsEntity = new InnerGroupNewsEntity()
                            {
                                GroupNewsId = Guid.NewGuid().ToString().Replace("-", ""),
                                SentType = 2,
                                NoticePlatformType = 1,
                                BusType = 3,
                                Title = "��������",
                                Text = "�����ɹ�",
                                CustomerID = CurrentUserInfo.ClientID
                            };
                            InnerGroupNewsBll.Create(innerGroupNewsEntity);
                            //��Ϣ���û���ϵ��
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
            catch { throw new APIException("����ʧ�ܣ�") { ErrorCode = ERROR_CODES.INVALID_BUSINESS }; };
            return ObjectNo;
        }

        public void updateVipCardByType(string vipId, int vipCardTypeId,string changeReason,string remark, string vipCode, IDbTransaction pTran)
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);

            UnitService unitServer = new UnitService(CurrentUserInfo);
            string unitId = unitServer.GetUnitByUnitTypeForWX("�ܲ�", null).Id; //��ȡ�ܲ��ŵ��ʶ

            VipCardEntity vipCardInfo = vipCardBLL.GetVipCardByVipMapping(vipId);

            //���¿���Ϣ
            vipCardInfo.VipCardTypeID = vipCardTypeId;
            vipCardBLL.Update(vipCardInfo,pTran);

            //var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardTypeID = vipCardTypeId, VipCardStatusId = 0, MembershipUnit = "" }, null).FirstOrDefault();
            
            //�����ڣ��ƿ�
            //if (vipCardInfo == null)
            //{
            //    vipCardInfo = new VipCardEntity();
            //    vipCardInfo.VipCardID = Guid.NewGuid().ToString();
            //    vipCardInfo.VipCardTypeID = vipCardTypeId;
            //    vipCardInfo.VipCardTypeName = vipCardTypeName;
            //    vipCardInfo.VipCardCode = vipCode;
            //    vipCardInfo.VipCardStatusId = 1;//����
            //    vipCardInfo.MembershipUnit = unitId;
            //    vipCardInfo.MembershipTime = DateTime.Now;
            //    vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
            //    vipCardBLL.Create(vipCardInfo, pTran);
            //}

            //������Ա������״̬��Ϣ
            var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
            {
                LogID = Guid.NewGuid().ToString().Replace("-", ""),
                VipCardStatusID = vipCardInfo.VipCardStatusId,
                VipCardID = vipCardInfo.VipCardID,
                Action = "����",
                Reason = changeReason,
                Remark = remark,
                UnitID = unitId,
                CustomerID = CurrentUserInfo.ClientID
            };
            vipCardStatusChangeLogBLL.Create(vipCardStatusChangeLogEntity, pTran);

            
            var vipCardVipMappingEntity = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity(){ VipCardID = vipCardInfo.VipCardID },null).FirstOrDefault();
            vipCardVipMappingEntity.LastUpdateTime = DateTime.Now;

            vipCardVipMappingBLL.Update(vipCardVipMappingEntity, pTran);
            //�󶨻�Ա���ͻ�Ա
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