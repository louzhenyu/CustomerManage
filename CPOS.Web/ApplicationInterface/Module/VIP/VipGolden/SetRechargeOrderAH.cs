using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 充值订单
    /// </summary>
    public class SetRechargeOrderAH : Base.BaseActionHandler<SetRechargeOrderRP, SetRechargeOrderRD>
    {
        protected override SetRechargeOrderRD ProcessRequest(APIRequest<SetRechargeOrderRP> pRequest)
        {
            SetRechargeOrderRP rp = pRequest.Parameters;
            SetRechargeOrderRD rd = new SetRechargeOrderRD();

            var rechargeOrderBll = new RechargeOrderBLL(CurrentUserInfo);
            TUnitExpandBLL serviceUnitExpandBll = new TUnitExpandBLL(CurrentUserInfo);
            
            var vipBll = new VipBLL(CurrentUserInfo);
            var vipCardVipMappingBll = new VipCardVipMappingBLL(CurrentUserInfo);
            var vipCardBll = new VipCardBLL(CurrentUserInfo);
            var sysVipCardTypeBll = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardUpgradeRuleBll = new VipCardUpgradeRuleBLL(CurrentUserInfo);

            //获取会员信息
            VipEntity vipInfo;
            string userId = "";
            //微信 
            if (pRequest.ChannelId == "2")
            {
                vipInfo = vipBll.GetByID(pRequest.UserID);
                UnitService unitServer = new UnitService(CurrentUserInfo);
                rp.UnitId = unitServer.GetUnitByUnitTypeForWX("OnlineShopping", null).Id; //获取在线商城的门店标识
            }
            //掌柜App 
            else
            {
                vipInfo = vipBll.GetByID(rp.VipId);
                userId = pRequest.UserID;
            }

            string OrderDesc = "ReRecharge"; //"升级","充值"
            int? VipCardTypeId = null; //会员卡类型
            decimal returnAmount = 0; //赠送金额

            //会员卡类型条件
            List<IWhereCondition> complexConditionOne = new List<IWhereCondition> { };
            //获取会员与会员卡关系表
            var vipCardVipMappingEntity = vipCardVipMappingBll.QueryByEntity(new VipCardVipMappingEntity(){CustomerID = CurrentUserInfo.ClientID, VIPID = vipInfo.VIPID},null).FirstOrDefault();
            //该会员有会员卡
            if (vipCardVipMappingEntity != null)
            {
                //获取该会员会员卡信息
                var vipCardEntity = vipCardBll.QueryByEntity(new VipCardEntity() { CustomerID = CurrentUserInfo.ClientID, VipCardID = vipCardVipMappingEntity.VipCardID }, null).FirstOrDefault();

                //获取该会员卡类型
                var sysVipCardTypeEntity = sysVipCardTypeBll.GetByID(vipCardEntity.VipCardTypeID);
                VipCardTypeId = vipCardEntity.VipCardTypeID;

                //获取会员卡的会员活动
                var rechargeStrategyBLL = new RechargeStrategyBLL(CurrentUserInfo);
                var RechargeActivityDS = rechargeStrategyBLL.GetRechargeActivityList(CurrentUserInfo.ClientID, VipCardTypeId.ToString(), 3);
                var RechargeStrategy = new RechargeStrategyInfo();

                if (RechargeActivityDS != null && RechargeActivityDS.Tables[0].Rows.Count > 0)
                {
                    var RechargeStrategyList = DataTableToObject.ConvertToList<RechargeStrategyInfo>(RechargeActivityDS.Tables[0]);
                    //获取与充值金额最接近的活动
                    RechargeStrategy = RechargeStrategyList.Where(n => n.RechargeAmount <= rp.ActuallyPaid).OrderByDescending(n => n.RechargeAmount).FirstOrDefault();
                    if (RechargeStrategy != null)
                    {
                        //梯度
                        if (RechargeStrategy.RuleType == "Step")
                        {
                            //赠送金额 = 会员活动梯度设置赠送金额
                            returnAmount = RechargeStrategy.GiftAmount;
                        }
                        //叠加
                        if (RechargeStrategy.RuleType == "Superposition")
                        {
                            //赠送金额与满赠条件金额比例
                            decimal discount = RechargeStrategy.GiftAmount / RechargeStrategy.RechargeAmount;
                            //赠送金额 = 充值金额 * （设置赠送金额 / 设置满赠条件金额）
                            returnAmount = rp.ActuallyPaid * RechargeStrategy.GiftAmount / RechargeStrategy.RechargeAmount;
                        }
                    }
                }

                //获取等级高的会员卡类型条件
                complexConditionOne.Add(new MoreThanCondition() { FieldName = "VipCardLevel", Value = sysVipCardTypeEntity.VipCardLevel, IncludeEquals = false });
                complexConditionOne.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
            }
            //该会员没有会员卡
            else
            {
                //获取所有的会员卡类型条件
                complexConditionOne.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
            }

            var sysVipCardTypeList = sysVipCardTypeBll.Query(complexConditionOne.ToArray(), null).ToList();

            if (sysVipCardTypeList == null)
            {
                throw new APIException("没有建立会员体系") { ErrorCode = 200 };
            }

            //获取会员卡类型升级规则
            List<IWhereCondition> complexConditionTwo = new List<IWhereCondition> { };
            complexConditionTwo.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
            complexConditionTwo.Add(new EqualsCondition() { FieldName = "IsRecharge", Value = 1 });
            var vipCardUpgradeRuleList = vipCardUpgradeRuleBll.Query(complexConditionTwo.ToArray(), null);

            //将升级卡规则和可升级的会员卡匹配
            var vipCardTypeDetailList = vipCardUpgradeRuleList.Join(sysVipCardTypeList, n => n.VipCardTypeID, m => m.VipCardTypeID, (n, m) => new { n, m }).OrderByDescending(t => t.m.VipCardLevel).ToList();
               
            //判断是否满充值条件
            bool isUpgrade = false;
            

            for (int i = 0; i < vipCardTypeDetailList.Count; i++)
            {
                //实付金额 >= 可升级金额
                if (vipCardTypeDetailList[i].n.OnceRechargeAmount <= rp.ActuallyPaid)
                {
                    isUpgrade = true;
                    VipCardTypeId = vipCardTypeDetailList[i].n.VipCardTypeID ?? 0;
                    break;
                }
            }
            if (isUpgrade)
            {
                OrderDesc = "Upgrade";  //升级
            }


            //充值订单
            var rechargeOrderEntity = new RechargeOrderEntity()
            {
                OrderID = Guid.NewGuid(),
                OrderNo = serviceUnitExpandBll.GetUnitOrderNo(),
                OrderDesc = OrderDesc,
                VipID = vipInfo.VIPID,
                VipCardNo = vipInfo.VipCode,
                UnitId = rp.UnitId,
                UserId = userId,
                TotalAmount = rp.ActuallyPaid,
                ActuallyPaid = rp.ActuallyPaid,
                ReturnAmount = returnAmount,
                PayerID = vipInfo.VIPID,
                Status = 0,
                CustomerID = CurrentUserInfo.ClientID,
                VipCardTypeId = VipCardTypeId
            };
            rechargeOrderBll.Create(rechargeOrderEntity);
            rd.orderId = rechargeOrderEntity.OrderID.ToString();
              
            return rd;
        }
    }
}