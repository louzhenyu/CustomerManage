using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class SetVipCardTypeSystemRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡体系相关信息
        /// </summary>
        public List<VipCardTypeRelateRPInfo> VipCardRelateList { get; set; }
        public void Validate()
        {
            int Count = VipCardRelateList.Select(m => m.VipCardType.VipCardTypeName).Distinct().Count();
            int Count1 = VipCardRelateList.Select(m => m.VipCardType.VipCardTypeName).Count();
            if (Count != Count1)
            {
                throw new APIException("会员卡名称不能重复！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
        }
    }

    /// <summary>
    /// 卡体系相关信息
    /// </summary>
    public class VipCardTypeRelateRPInfo
    {
        /// <summary>
        /// 卡类型相关信息
        /// </summary>
        public VipCardTypeRPInfo VipCardType { get; set; }
        /// <summary>
        /// 卡升级条件相关信息
        /// </summary>
        public VipCardUpgradeRuleRPInfo VipCardUpgradeRule { get; set; }
        /// <summary>
        /// 基本权益信息
        /// </summary>
        public VipCardRuleRPInfo VipCardRule { get; set; }
        /// <summary>
        /// 开卡礼信息
        /// </summary>
        public List<VipCardUpgradeRewardRPInfo> VipCardUpgradeRewardList { get; set; }
    }
    /// <summary>
    /// 卡类型相关信息
    /// </summary>
    public class VipCardTypeRPInfo
    {
        public int VipCardTypeID { get; set; }
        /// <summary>
        /// 卡等级
        /// </summary>
        public int VipCardLevel { get; set; }
        /// <summary>
        /// 卡等级名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 卡类型图片
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 是否可储值 （0=否；1=是；）
        /// </summary>
        public int IsPrepaid { get; set; }
        /// <summary>
        /// 是否在线销售 （0=否；1=是；）
        /// </summary>
        public int IsOnlineSales { get; set; }
    }
    /// <summary>
    /// 卡升级条件相关信息
    /// </summary>
    public class VipCardUpgradeRuleRPInfo
    {
        public string VipCardUpgradeRuleId { get; set; }
        /// <summary>
        /// 是否购买升级 (1=是;0=否;)
        /// </summary>
        public int IsPurchaseUpgrade { get; set; }
        /// <summary>
        /// 是否补差(1=可补;2=不可补;)
        /// </summary>
        public int IsExtraMoney { get; set; }
        /// <summary>
        /// 卡售价
        /// </summary>
        public decimal Prices { get; set; }
        /// <summary>
        /// 使用多少积分兑换
        /// </summary>
        public int ExchangeIntegral { get; set; }
        /// <summary>
        /// 是否充值升级
        /// </summary>
        public int IsRecharge { get; set; }
        /// <summary>
        /// 单次充值金额
        /// </summary>
        public decimal OnceRechargeAmount { get; set; }
        /// <summary>
        /// 是否消费升级
        /// </summary>
        public int IsBuyUpgrade { get; set; }
        /// <summary>
        /// 累积消费满多少金额升级
        /// </summary>
        public decimal BuyAmount { get; set; }
        /// <summary>
        /// 单次消费多少金额升级
        /// </summary>
        public decimal OnceBuyAmount { get; set; }
    }
    /// <summary>
    /// 基本权益信息
    /// </summary>
    public class VipCardRuleRPInfo
    {
        public int RuleID { get; set; }
        /// <summary>
        /// 卡折扣
        /// </summary>
        public decimal CardDiscount { get; set; }
        /// <summary>
        /// 消费n元赠送比例积分
        /// </summary>
        public decimal PaidGivePercetPoints { get; set; }
        /// <summary>
        /// 消费n元赠送1积分
        /// </summary>
        public decimal PaidGivePoints { get; set; }
    }
    /// <summary>
    /// 开卡礼信息
    /// </summary>
    public class VipCardUpgradeRewardRPInfo
    {
        /// <summary>
        /// 开卡礼主标识 (更新数据时使用)
        /// </summary>
        public string CardUpgradeRewardId { get; set; }
        /// <summary>
        /// 卡类型ID (更新数据时使用)
        /// </summary>
        public int VipCardTypeID { get; set; }
        /// <summary>
        /// 操作类型(0=删除券；1=新增；2=修改；) (更新数据时使用)
        /// </summary>
        public int OperateType { get; set; }

        /// <summary>
        /// 券类型Id
        /// </summary>
        public string CouponTypeID { get; set; }
        /// <summary>
        /// 券张数
        /// </summary>
        public int CouponNum { get; set; }
        /// <summary>
        /// 券种名称
        /// </summary>
        public string CouponName { get; set; }

    }
}
