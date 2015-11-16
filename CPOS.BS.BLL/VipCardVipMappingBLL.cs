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
    /// 业务处理：  
    /// </summary>
    public partial class VipCardVipMappingBLL
    {
        /// <summary>
        /// 会员微信注册绑卡
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
            string onlineShoppingUnitId = unitServer.GetUnitByUnitTypeForWX("OnlineShopping", null).Id; //获取在线商城的门店标识

            //根据vipid查询VipCardVipMapping，判断是否有绑卡
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipId }, null).FirstOrDefault();
            if (vipCardMappingInfo == null)
            {
                //查询最低等级的会员卡类型
                var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = CurrentUserInfo.ClientID, Category = 0 }, new OrderBy[] { new OrderBy() { FieldName = "vipcardlevel", Direction = OrderByDirections.Asc } }).FirstOrDefault();
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
                        vipCardInfo.MembershipUnit = onlineShoppingUnitId;
                        vipCardInfo.MembershipTime = DateTime.Now;
                        vipCardInfo.CustomerID = CurrentUserInfo.ClientID;
                        vipCardBLL.Create(vipCardInfo);
                    }
                    //新增会员卡操作状态信息
                    var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                    {
                        LogID = Guid.NewGuid().ToString().Replace("-", ""),
                        VipCardStatusID = vipCardInfo.VipCardStatusId,
                        VipCardID = vipCardInfo.VipCardID,
                        Action = "注册",
                        UnitID = onlineShoppingUnitId,
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
                }
                else
                    throw new APIException("系统未创建会员卡类型") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
        }
    }
}