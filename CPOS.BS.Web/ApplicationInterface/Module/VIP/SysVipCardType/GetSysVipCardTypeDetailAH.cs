using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.SysVipCardType
{
    public class GetSysVipCardTypeDetailAH : BaseActionHandler<DelSysVipCardTypeRP, GetSysVipCardTypeDetailRD>
    {
        protected override GetSysVipCardTypeDetailRD ProcessRequest(DTO.Base.APIRequest<DelSysVipCardTypeRP> pRequest)
        {
            var rd = new GetSysVipCardTypeDetailRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(loggingSessionInfo);
            var specialDateBLL = new SpecialDateBLL(loggingSessionInfo);

            //获取卡类型信息
            var vipCardTypeInfo = sysVipCardTypeBLL.GetByID(para.VipCardTypeID);
            if (vipCardTypeInfo != null)
            {
                rd.VipCardTypeID = vipCardTypeInfo.VipCardTypeID;
                rd.VipCardTypeCode = vipCardTypeInfo.VipCardTypeCode;
                rd.VipCardTypeName = vipCardTypeInfo.VipCardTypeName;
                rd.PicUrl = vipCardTypeInfo.PicUrl;
                rd.Category = vipCardTypeInfo.Category;
                rd.IsPassword = vipCardTypeInfo.IsPassword;
                rd.VipCardLevel = vipCardTypeInfo.VipCardLevel;
                rd.Prices = vipCardTypeInfo.Prices;
                rd.IsExtraMoney = vipCardTypeInfo.IsExtraMoney;
                rd.ExchangeIntegral = vipCardTypeInfo.ExchangeIntegral;
                rd.UpgradeAmount = vipCardTypeInfo.UpgradeAmount;
                rd.UpgradeOnceAmount = vipCardTypeInfo.UpgradeOnceAmount;
                rd.UpgradePoint = vipCardTypeInfo.UpgradePoint;

                //获取卡规则信息
                var vipCardRuleInfo = vipCardRuleBLL.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = para.VipCardTypeID }, null).FirstOrDefault();
                if (vipCardRuleInfo != null)
                {
                    rd.CardDiscount = vipCardRuleInfo.CardDiscount;
                    rd.PointsMultiple = vipCardRuleInfo.PointsMultiple;
                    rd.ChargeFull = vipCardRuleInfo.ChargeFull;
                    rd.ChargeGive = vipCardRuleInfo.ChargeGive;
                    rd.PaidGivePoints=vipCardRuleInfo.PaidGivePoints;
                    rd.ReturnAmountPer = vipCardRuleInfo.ReturnAmountPer;
                }
                //获取特殊日期设置信息
                //查询参数
                List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                complexCondition.Add(new EqualsCondition() { FieldName = "sd.VipCardTypeID", Value = para.VipCardTypeID });
                var orderBy = new OrderBy[] { new OrderBy() { FieldName = "sd.CreateTime", Direction = OrderByDirections.Asc } };//排序字段
                var specialDateList = specialDateBLL.GetSpecialDateList(complexCondition.ToArray(), orderBy.ToArray());
                rd.SpecialDateList = specialDateList.Select(t => new SpecialDateInfo()
                {
                    SpecialID = t.SpecialId,
                    HolidayName = t.HolidayName,
                    BeginDate = t.BeginDate == null ? "" : t.BeginDate.Value.ToString("yyyy-MM-dd"),
                    EndDate = t.EndDate == null ? "" : t.EndDate.Value.ToString("yyyy-MM-dd"),
                    NoAvailablePoints = t.NoAvailablePoints,
                    NoAvailableDiscount = t.NoAvailableDiscount,
                    NoRewardPoints = t.NoRewardPoints
                }).ToList();
            }
            return rd;
        }
    }
}