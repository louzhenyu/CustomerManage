using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    public class GetVipCardProfitRuleListRD : IAPIResponseData
    {
        /// <summary>
        /// 列表数组
        /// </summary>
        public List<VipCardProfitRuleInfo> VipCardProfitRuleList { get; set; }
        public GetVipCardProfitRuleListRD()
        {
            VipCardProfitRuleList = new List<VipCardProfitRuleInfo>();
        }
    }
    /// <summary>
    /// 会员卡 销售激励 实体对象
    /// </summary>
    public class VipCardProfitRuleInfo
    {
        /// <summary>
        /// 会员卡分润规则Id
        /// </summary>
        public Guid? CardBuyToProfitRuleId { get; set; }

        /// <summary>
        /// 卡类型标识
        /// </summary>
        public int? VipCardTypeID { get; set; }
        /// <summary>
        /// 分润拥有者（Employee =员工、Unit =门店）
        /// </summary>
        public string ProfitOwner { get; set; }
        /// <summary>
        /// 首次购卡分润
        /// </summary>
        public decimal? FirstCardSalesProfitPct { get; set; }
        /// <summary>
        /// 首次充值分润
        /// </summary>
        public decimal? FirstRechargeProfitPct { get; set; }
        /// <summary>
        /// 是否应用全部门店(0=否；1=是；)
        /// </summary>
        public int? IsApplyAllUnits { get; set; }
        /// <summary>
        /// 购卡分润
        /// </summary>
        public decimal? CardSalesProfitPct { get; set; }
        /// <summary>
        /// 充值分润
        /// </summary>
        public decimal? RechargeProfitPct { get; set; }
        /// <summary>
        /// 会员卡续费充值列表
        /// </summary>
        public List<VipCardReRechargeProfitRuleInfo> VipCardReRechargeProfitRuleList { get; set; }

        /// <summary>
        /// 门店信息列表
        /// </summary>
        public List<RuleUnitInfoRD> RuleUnitInfoList { get; set; }
        public VipCardProfitRuleInfo()
        {
            RuleUnitInfoList = new List<RuleUnitInfoRD>();
            VipCardReRechargeProfitRuleList = new List<VipCardReRechargeProfitRuleInfo>();
        }

    }

    /// <summary>
    /// 返回组织信息
    /// </summary>
    public class RuleUnitInfoRD
    {
        /// <summary>
        /// 上级组织编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 上级组织名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 门店集合
        /// </summary>
        public List<MappingUnitInfo> children { get; set; }

        public RuleUnitInfoRD()
        {
            children = new List<MappingUnitInfo>();
        }
    }
    /// <summary>
    /// 门店集合数组
    /// </summary>
    public class MappingUnitInfo
    {
        /// <summary>
        /// 映射主标识
        /// </summary>
        public string MappingUnitId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string text { get; set; }
    }
    /// <summary>
    /// 会员卡续费充值方式实体对象
    /// </summary>
    public class VipCardReRechargeProfitRuleInfo
    {
        /// <summary>
        /// 会员卡分润规则
        /// </summary>
        public Guid CardBuyToProfitRuleId { get; set; }
        public Guid? ReRechargeProfitRuleId { get; set; }
        /// <summary>
        /// 充值分润方式（Step 阶梯、Superposition 叠加） 
        /// </summary>
        public string ProfitType { get; set; }
        /// <summary>
        /// 满/每满充值金额
        /// </summary>
        public decimal? LimitAmount { get; set; }
        /// <summary>
        /// 充值分润
        /// </summary>
        public decimal? ProfitPct { get; set; }
    }

    /// <summary>
    /// 组织信息
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// 组织结构编号
        /// </summary>
        public string src_unit_id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string unit_name { get; set; }
    }
    /// <summary>
    /// 门店信息
    /// </summary>
    public class Stores
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 规则分润编号
        /// </summary>
        public Guid CardBuyToProfitRuleId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 组织结构编号
        /// </summary>
        public string src_unit_id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string unit_name { get; set; }
    }
}