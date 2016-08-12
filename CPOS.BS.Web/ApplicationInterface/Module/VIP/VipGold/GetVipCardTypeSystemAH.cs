using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    //获取会员体系信息
    public class GetVipCardTypeSystemAH : BaseActionHandler<EmptyRequestParameter, GetVipCardTypeSystemRD>
    {
        protected override GetVipCardTypeSystemRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetVipCardTypeSystemRD();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //声明卡等级相关逻辑
            var bllVipCardType = new SysVipCardTypeBLL(loggingSessionInfo);
            //获取卡等级相关信息(会员卡等级信息、升级条件、基本权益)
            var VipCardTypeSystemInfoList = bllVipCardType.GetVipCardTypeSystemList(loggingSessionInfo.ClientID);
            //处理会员开卡礼信息
            var VipCardUpgradeRewardInfoList = bllVipCardType.GetCardUpgradeRewardList(loggingSessionInfo.ClientID);
            List<VipCardUpgradeRewardInfo> VipCardUpgradeRewardList = new List<VipCardUpgradeRewardInfo>();
            List<VipCardTypeRelateInfo> VipCardTypeRelateList = new List<VipCardTypeRelateInfo>();

            if (VipCardTypeSystemInfoList != null && VipCardTypeSystemInfoList.Tables[0].Rows.Count > 0)
            {
                int flag = 0;//定义下面开卡礼能否进行循环(0=不进，1=进)
                if (VipCardUpgradeRewardInfoList != null && VipCardUpgradeRewardInfoList.Tables[0].Rows.Count > 0)
                {
                    flag = 1;
                    //获取开卡礼信息 为之后筛选数据使用
                    VipCardUpgradeRewardList = DataTableToObject.ConvertToList<VipCardUpgradeRewardInfo>(VipCardUpgradeRewardInfoList.Tables[0]);
                }

                var dt = VipCardTypeSystemInfoList.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    var DataInfo = new VipCardTypeRelateInfo();
                    //VipCardTypeRelateList = new List<VipCardTypeRelateInfo>();
                    DataInfo.VipCardType = new VipCardTypeInfo();//会员卡体系
                    DataInfo.VipCardUpgradeRule = new VipCardUpgradeRuleInfo();//会员卡升级规则
                    DataInfo.VipCardRule = new VipCardRuleInfo();//会员卡基本体系
                    DataInfo.VipCardUpgradeRewardList = new List<VipCardUpgradeRewardInfo>();
                    //var VipCardTypeData = new VipCardTypeInfo();
                    //给会员卡等级赋值
                    DataInfo.VipCardType.VipCardTypeID = Convert.ToInt32(dr["VipCardTypeID"]);
                    DataInfo.VipCardType.VipCardLevel = Convert.ToInt32(dr["VipCardLevel"]);
                    DataInfo.VipCardType.VipCardTypeName = dr["VipCardTypeName"].ToString();
                    DataInfo.VipCardType.PicUrl = dr["PicUrl"].ToString();
                    DataInfo.VipCardType.IsPrepaid = Convert.ToInt32(dr["IsPrepaid"]);
                    DataInfo.VipCardType.IsOnlineSales = Convert.ToInt32(dr["IsOnlineSales"]);

                    //var VipCardUpgradeRuleData = new VipCardUpgradeRuleInfo();
                    DataInfo.VipCardUpgradeRule.VipCardTypeID = Convert.ToInt32(dr["VipCardTypeID"]);
                    DataInfo.VipCardUpgradeRule.VipCardUpgradeRuleId = dr["VipCardUpgradeRuleId"] == null ? "" : dr["VipCardUpgradeRuleId"].ToString();
                    DataInfo.VipCardUpgradeRule.IsPurchaseUpgrade = Convert.ToInt32(dr["IsPurchaseUpgrade"]);
                    DataInfo.VipCardUpgradeRule.IsExtraMoney = Convert.ToInt32(dr["IsExtraMoney"]);
                    DataInfo.VipCardUpgradeRule.Prices = Convert.ToDecimal(dr["Prices"]);
                    DataInfo.VipCardUpgradeRule.ExchangeIntegral = Convert.ToInt32(dr["ExchangeIntegral"]);
                    DataInfo.VipCardUpgradeRule.IsRecharge = Convert.ToInt32(dr["IsRecharge"]);
                    DataInfo.VipCardUpgradeRule.OnceRechargeAmount = Convert.ToDecimal(dr["OnceRechargeAmount"]);
                    DataInfo.VipCardUpgradeRule.IsBuyUpgrade = Convert.ToInt32(dr["IsBuyUpgrade"]);
                    DataInfo.VipCardUpgradeRule.BuyAmount = Convert.ToDecimal(dr["BuyAmount"]);
                    DataInfo.VipCardUpgradeRule.OnceBuyAmount = Convert.ToDecimal(dr["OnceBuyAmount"]);

                    //var VipCardRuleData = new VipCardUpgradeRuleInfo();
                    DataInfo.VipCardRule.VipCardTypeID = Convert.ToInt32(dr["VipCardTypeID"]);
                    DataInfo.VipCardRule.RuleID = dr["RuleID"] == null ? 0 : Convert.ToInt32(dr["RuleID"]);
                    DataInfo.VipCardRule.CardDiscount = Convert.ToDecimal(dr["CardDiscount"]);
                    DataInfo.VipCardRule.PaidGivePercetPoints = Convert.ToDecimal(dr["PaidGivePercetPoints"]);
                    DataInfo.VipCardRule.PaidGivePoints = Convert.ToDecimal(dr["PaidGivePoints"]);
                    if (flag == 1)//当flag=1的时候进行开卡礼的展示
                    {
                        DataInfo.VipCardUpgradeRewardList = VipCardUpgradeRewardList.Where(m => m.VipCardTypeID == Convert.ToInt32(dr["VipCardTypeID"])).Select(
                            t => new VipCardUpgradeRewardInfo()
                            {
                                CardUpgradeRewardId = t.CardUpgradeRewardId,
                                VipCardTypeID = t.VipCardTypeID,
                                CouponTypeID = t.CouponTypeID,
                                CouponNum = t.CouponNum,
                                CouponName = t.CouponName,
                                ValidityPeriod = t.BeginTime == null ? ("领取后" + (t.ServiceLife == 0 ? "1天内有效" : t.ServiceLife.ToString() + "天内有效")) : (t.BeginTime.Value.ToString("yyyy-MM-dd") + "至" + t.EndTime.Value.ToString("yyyy-MM-dd")),
                                BeginTime = t.BeginTime,
                                EndTime = t.EndTime,
                                BeginTimeDate = t.BeginTime == null ? "" : t.BeginTime.Value.ToString("yyyy年MM月dd日"),
                                EndTimeDate = t.EndTime == null ? "" : t.EndTime.Value.ToString("yyyy年MM月dd日"),
                                ServiceLife = t.ServiceLife,
                            }).ToList();
                    }
                    VipCardTypeRelateList.Add(DataInfo);
                }
                rd.VipCardRelateList = VipCardTypeRelateList;
            }
            return rd;
        }
    }
}