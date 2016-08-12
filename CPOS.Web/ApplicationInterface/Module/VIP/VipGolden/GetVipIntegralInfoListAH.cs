using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 微信端 会员中心 积分说明
    /// </summary>
    public class GetVipIntegralInfoListAH : BaseActionHandler<GetVipIntegralInfoListRP, GetVipIntegralInfoListRD>
    {
        protected override GetVipIntegralInfoListRD ProcessRequest(DTO.Base.APIRequest<GetVipIntegralInfoListRP> pRequest)
        {
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            SysVipCardTypeBLL bll = new SysVipCardTypeBLL(loggingSessionInfo);
            VipCardRuleBLL VipCardRuleService = new VipCardRuleBLL(loggingSessionInfo);
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
            var vipCardBLL = new VipCardBLL(loggingSessionInfo);
            var vipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var rd = new GetVipIntegralInfoListRD();

            string[] CustomerBaseSettingCode = new string[] { "IntegralAmountPer", "PointsRedeemLowestLimit", "PointsRedeemUpLimit", "PointsValidPeriod" };
            string[] CustomerBaseSettingDesc = new string[] { "积分抵扣比例", "账户积分累计满", "每单可使用积分上限", "积分有效期" };

            CustomerBasicSettingBLL CustomerBasicSettingService = new CustomerBasicSettingBLL(loggingSessionInfo);
            //循环获取积分信息
            for (int i = 0; i < CustomerBaseSettingCode.Length; i++)
            {
                var entity = CustomerBasicSettingService.QueryByEntity(new CustomerBasicSettingEntity() { CustomerID = loggingSessionInfo.ClientID, IsDelete = 0, SettingCode = CustomerBaseSettingCode[i] }, null).FirstOrDefault();
                if (entity != null)
                {
                    rd.VipIntegralInfoList.Add(new VipIntegralInfo() { CustomerBaseSettingDesc = CustomerBaseSettingDesc[i], CustomerBaseSettingValue = entity.SettingValue });
                }
                else
                {
                    rd.VipIntegralInfoList.Add(new VipIntegralInfo() { CustomerBaseSettingDesc = CustomerBaseSettingDesc[i], CustomerBaseSettingValue = "0" });
                }
            }
            //获取会员 卡映射关系
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = pRequest.UserID, CustomerID = CurrentUserInfo.ClientID },
                new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
            if (vipCardMappingInfo != null)
            {
                //获取卡信息
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID, VipCardStatusId = 1 }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    //获取会员卡信息
                    var vipCardTypeInfo = vipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                    if (vipCardTypeInfo != null)
                    {
                        //获取卡升级规则信息
                        var cardrulemodel = VipCardRuleService.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = vipCardTypeInfo.VipCardTypeID }, null).FirstOrDefault();
                        if (cardrulemodel != null)
                        {
                            if (cardrulemodel.PaidGivePoints > 0) //积分
                            {
                                rd.VipIntegralInfoList.Add(new VipIntegralInfo() { CustomerBaseSettingDesc = "每消费" + Convert.ToInt32(cardrulemodel.PaidGivePoints) + "元返 1积分", CustomerBaseSettingValue = Convert.ToInt32(cardrulemodel.PaidGivePoints) + "" });
                            }
                            if (cardrulemodel.PaidGivePercetPoints > 0) //积分比例
                            {
                                rd.VipIntegralInfoList.Add(new VipIntegralInfo() { CustomerBaseSettingDesc = "消费返积分比例" + Convert.ToInt32(cardrulemodel.PaidGivePercetPoints) + "%", CustomerBaseSettingValue = Convert.ToInt32(cardrulemodel.PaidGivePercetPoints) + "" });
                            }
                        }
                    }
                }
            }
            return rd;
        }
    }
}