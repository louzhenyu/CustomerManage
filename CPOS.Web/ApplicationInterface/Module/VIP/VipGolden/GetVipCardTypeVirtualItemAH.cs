using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 获取可升级虚拟卡列表信息
    /// </summary>
    public class GetVipCardTypeVirtualItemAH : BaseActionHandler<GetVipCardTypeVirtualItemRP, GetVipCardTypeVirtualItemRD>
    {
        protected override GetVipCardTypeVirtualItemRD ProcessRequest(DTO.Base.APIRequest<GetVipCardTypeVirtualItemRP> pRequest)
        {
            var rd = new GetVipCardTypeVirtualItemRD();
            var para = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            var vipBLL = new VipBLL(loggingSessionInfo);
            var vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
            var vipCardBLL = new VipCardBLL(loggingSessionInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(loggingSessionInfo);
            var vipT_InoutBLL = new T_InoutBLL(loggingSessionInfo);
            List<VipCardUpgradeRewardInfo> VipCardUpgradeRewardList = new List<VipCardUpgradeRewardInfo>();
            List<VipCardTypeRelateInfo> VipCardTypeRelateList = new List<VipCardTypeRelateInfo>();
            //获取当前会员卡等级
            VipEntity VipInfo = null;
            int? CurVipCardLevel = 0;
            //处理会员开卡礼信息
            var VipCardUpgradeRewardInfoList = sysVipCardTypeBLL.GetCardUpgradeRewardList(loggingSessionInfo.ClientID);
            //定义卡体系信息
            DataSet VipCardTypeSystemInfoList = null;
            string strVipID = string.Empty;
            switch (para.ApplicationType)
            {
                //为1是微信，为2时表示APP请求
                case "1"://微信
                    strVipID = pRequest.UserID;
                    break;
                case "2"://APP
                    strVipID = para.VipID;//获取会员信息                    
                    break;
            }

            VipInfo = vipBLL.GetByID(strVipID);//获取会员信息
            if (VipInfo != null)
            {
                rd.HeadImgUrl = VipInfo.HeadImgUrl == null ? "" : VipInfo.HeadImgUrl;
                var vipIntegralInfo = vipIntegralBLL.QueryByEntity(new VipIntegralEntity() { VipID = strVipID, VipCardCode = VipInfo.VipCode }, null).FirstOrDefault();
                if (vipIntegralInfo != null)//获取当前会员积分
                {
                    rd.Integration = vipIntegralInfo.ValidIntegral != null ? vipIntegralInfo.ValidIntegral.Value : 0;
                }
                //获取会员卡等级相关信息
                var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = strVipID, CustomerID = loggingSessionInfo.ClientID },
                    new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
                if (vipCardMappingInfo != null)
                {
                    var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID, VipCardStatusId = 1 }, null).FirstOrDefault();
                    if (vipCardInfo != null)
                    {
                        var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                        if (vipCardTypeInfo != null)//获取当前会员卡等级信息
                        {
                            rd.VipCardTypeName = vipCardTypeInfo.VipCardTypeName != null ? vipCardTypeInfo.VipCardTypeName : "";
                            rd.VipCardLevel = vipCardTypeInfo.VipCardLevel;
                            CurVipCardLevel = vipCardTypeInfo.VipCardLevel;
                        }
                    }
                }
                else
                {
                    var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardLevel = 1, CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                    if (vipCardTypeInfo != null)
                    {
                        rd.VipCardTypeName = vipCardTypeInfo.VipCardTypeName != null ? vipCardTypeInfo.VipCardTypeName : "";
                        rd.VipCardLevel = vipCardTypeInfo.VipCardLevel != null ? vipCardTypeInfo.VipCardLevel : 1;
                        CurVipCardLevel = vipCardTypeInfo.VipCardLevel != null ? vipCardTypeInfo.VipCardLevel : 1;
                    }
                }
                //获取会员消费金额
                decimal VipConsumptionInfo = vipT_InoutBLL.GetVipSumAmount(strVipID);
                if (VipConsumptionInfo > 0)
                {
                    rd.VipConsumptionAmount = Convert.ToDecimal(VipConsumptionInfo).ToString("0.00");
                }
                else
                {
                    rd.VipConsumptionAmount = "0";
                }
                //获取卡等级相关信息(会员卡等级信息、升级条件、基本权益关联虚拟商品)
                VipCardTypeSystemInfoList = sysVipCardTypeBLL.GetVipCardTypeVirtualItemList(loggingSessionInfo.ClientID, CurVipCardLevel, para.ApplicationType, VipInfo.VIPID);
            }


            if (VipCardTypeSystemInfoList != null && VipCardTypeSystemInfoList.Tables[0].Rows.Count > 0)
            {
                int flag = 0;//定义下面开卡礼能否进行展示(0=不进，1=进)
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
                    DataInfo.VipCardType.ItemID = dr["ItemID"].ToString();
                    DataInfo.VipCardType.SkuID = dr["SkuID"].ToString();
                    int cardStatus = 0;
                    //获取卡状态购买信息 0=没购买，1=已购买 pRequest.UserID
                    if (DataInfo.VipCardType.IsPrepaid != 1)//非可储值类型关联订单
                    {
                        cardStatus = vipT_InoutBLL.GetVirtualItemStatus(loggingSessionInfo.ClientID, strVipID, DataInfo.VipCardType.SkuID);
                    }
                    else//可储值类型关联充值订单
                    {
                        var RechargeOrderBll = new RechargeOrderBLL(loggingSessionInfo);
                        var RechargeOrderInfo = RechargeOrderBll.QueryByEntity(new RechargeOrderEntity() { OrderDesc = "Upgrade", VipID = strVipID, VipCardTypeId = DataInfo.VipCardType.VipCardTypeID, Status=1 }, null).FirstOrDefault();
                        if (RechargeOrderInfo != null)//如果为空需要在订单表里查找下记录
                        {
                            cardStatus = 1;
                        }
                        else
                        {
                            cardStatus = vipT_InoutBLL.GetVirtualItemStatus(loggingSessionInfo.ClientID, strVipID, DataInfo.VipCardType.SkuID);
                        }
                    }
                        
                    DataInfo.VipCardType.Status = cardStatus;
                    //var VipCardUpgradeRuleData = new VipCardUpgradeRuleInfo();
                    DataInfo.VipCardUpgradeRule.VipCardTypeID = Convert.ToInt32(dr["VipCardTypeID"]);
                    DataInfo.VipCardUpgradeRule.VipCardUpgradeRuleId = dr["VipCardUpgradeRuleId"].ToString();
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
                    DataInfo.VipCardRule.RuleID = Convert.ToInt32(dr["RuleID"]);
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
                                CouponDesc = t.CouponDesc,
                                ParValue = t.ParValue
                            }).ToList();
                    }
                    VipCardTypeRelateList.Add(DataInfo);
                }                
                rd.VipCardTypeItemList = VipCardTypeRelateList;
            }
            return rd;
        }
    }
}