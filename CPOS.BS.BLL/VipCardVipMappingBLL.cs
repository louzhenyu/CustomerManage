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
        public void BindVipCard(string vipId, string vipCode)
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);

            UnitService unitServer = new UnitService(CurrentUserInfo);
            string onlineShoppingUnitId = unitServer.GetUnitByUnitTypeForWX("OnlineShopping", null).Id; //��ȡ�����̳ǵ��ŵ��ʶ

            //����vipid��ѯVipCardVipMapping���ж��Ƿ��а�
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipId }, null).FirstOrDefault();
            if (vipCardMappingInfo == null)
            {
                //��ѯ��͵ȼ��Ļ�Ա������
                var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
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
                        vipCardInfo.MembershipUnit = onlineShoppingUnitId;
                        vipCardInfo.MembershipTime = DateTime.Now;
                        vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
                        vipCardBLL.Create(vipCardInfo);
                    }
                    //������Ա������״̬��Ϣ
                    var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                    {
                        LogID = Guid.NewGuid().ToString().Replace("-", ""),
                        VipCardStatusID = vipCardInfo.VipCardStatusId,
                        VipCardID = vipCardInfo.VipCardID,
                        Action = "ע��",
                        UnitID = onlineShoppingUnitId,
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
                }
                else
                    throw new APIException("ϵͳδ������Ա������") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
        }
    }
}