using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetVipCardTypeSystemRD : IAPIResponseData
    {
        public List<VipCardTypeRelateInfo> VipCardRelateList { get; set; }
    }

    /// <summary>
    /// 卡体系相关信息
    /// </summary>
    public class VipCardTypeRelateInfo
    {       
        /// <summary>
        /// 卡类型相关信息
        /// </summary>
        public VipCardTypeInfo VipCardType { get; set; }
        /// <summary>
        /// 卡升级条件相关信息
        /// </summary>
        public VipCardUpgradeRuleInfo VipCardUpgradeRule { get; set; }
        /// <summary>
        /// 基本权益信息
        /// </summary>
        public VipCardRuleInfo VipCardRule { get; set; }
        /// <summary>
        /// 开卡礼信息
        /// </summary>
        public List<VipCardUpgradeRewardInfo> VipCardUpgradeRewardList { get; set; }
    }
    /// <summary>
    /// 卡类型相关信息
    /// </summary>
    public class VipCardTypeInfo
    {
        /// <summary>
        /// 卡类型ID
        /// </summary>
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
        /// <summary>
        /// 商品ID 在获取虚拟商品卡列表时使用
        /// </summary>
        public string ItemID { get; set; }
        /// <summary>
        /// 商品SkuID 在获取虚拟商品卡列表时使用
        /// </summary>
        public string SkuID { get; set; }
        /// <summary>
        /// 会员购买卡状态 0=未购买，1=已购买
        /// </summary>
        public int Status { get; set; }
    }
    /// <summary>
    /// 卡升级条件相关信息
    /// </summary>
    public class VipCardUpgradeRuleInfo
    {
        /// <summary>
        /// 卡升级条件信息ID
        /// </summary>
        public string VipCardUpgradeRuleId { get; set; }
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public int VipCardTypeID { get; set; }
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
    public class VipCardRuleInfo
    {
        /// <summary>
        /// 规则标识ID
        /// </summary>
        public int RuleID { get; set; }
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public int VipCardTypeID { get; set; }
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
    public class VipCardUpgradeRewardInfo
    {
        /// <summary>
        /// 开卡礼信息ID
        /// </summary>
        public string CardUpgradeRewardId { get; set; }
        /// <summary>
        /// 卡类型ID
        /// </summary>
        public int VipCardTypeID { get; set; }
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

        public string ValidityPeriod { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 年月日格式
        /// </summary>
        public string BeginTimeDate { get; set; }
        /// <summary>
        /// 年月日格式
        /// </summary>
        public string EndTimeDate { get; set; }
        public int? ServiceLife { get; set; }
        /// <summary>
        /// 优惠券描述
        /// </summary>
        public string CouponDesc { get; set; }
        /// <summary>
        /// 优惠券面值
        /// </summary>
        public decimal ParValue { get; set; }
    }
}
