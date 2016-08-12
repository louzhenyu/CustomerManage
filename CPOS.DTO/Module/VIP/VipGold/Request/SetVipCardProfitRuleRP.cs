using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO
{
    public class SetVipCardProfitRuleRP : IAPIRequestParameter
    {
        /// <summary>
        /// 前段传递信息
        /// </summary>
        public List<CardBuyToProfitRuleInfoInfo> CardBuyToProfitRuleInfoList { get; set; }
        /// <summary>
        /// 存删除 数组
        /// </summary>
        public List<CardBuyToProfitRuleInfoInfo> CardBuyToProfitRuleInfoDeleteList { get; set; }
        /// <summary>
        /// 存添加 数组
        /// </summary>
        public List<CardBuyToProfitRuleInfoInfo> CardBuyToProfitRuleInfoAddList { get; set; }
        /// <summary>
        /// 修改分润规则 信息 的 数组
        /// </summary>
        public List<CardBuyToProfitRuleInfoInfo> CardBuyToProfitRuleInfoUpdateList { get; set; }
        public void Validate()
        {
            List<CardBuyToProfitRuleInfoUnitInfo> lst = new List<CardBuyToProfitRuleInfoUnitInfo>();

            #region 检查门店和会员卡和分润方式是否有重复
            foreach (var item in CardBuyToProfitRuleInfoList)
            {
                if (item.IsApplyAllUnits == 1)   //全部门店
                {
                    //查看该卡有没有部分门店
                    var count = lst.Where(m => m.ProfitOwner == item.ProfitOwner && m.CardTypeId == item.VipCardTypeID && m.IsApplyAllUnits == 0).Count();
                    if (count > 0)
                    {
                        throw new APIException("门店和卡【" + item.VipCardTypeName + "】有重复,请重新添加门店和卡") { ErrorCode = 135 };
                    }
                    CardBuyToProfitRuleInfoUnitInfo model = new CardBuyToProfitRuleInfoUnitInfo()
                    {
                        CardTypeId = item.VipCardTypeID,
                        UnitId = "-1",
                        ProfitOwner = item.ProfitOwner,
                        IsApplyAllUnits = 1
                    };
                    lst.Add(model);
                }
                else   //部分门店
                {

                    var count = lst.Where(m => m.ProfitOwner == item.ProfitOwner && m.CardTypeId == item.VipCardTypeID && m.IsApplyAllUnits == 1).Count();
                    if (count > 0)
                    {
                        throw new APIException("门店和卡【" + item.VipCardTypeName + "】有重复,请重新添加门店和卡") { ErrorCode = 135 };
                    }
                    if (item.RuleUnitInfoList==null)
                    {
                        continue;
                    }
                    foreach (var unititem in item.RuleUnitInfoList)
                    {
                        if (unititem.IsDelete == 1)
                        {
                            continue;
                        }
                        CardBuyToProfitRuleInfoUnitInfo model = new CardBuyToProfitRuleInfoUnitInfo()
                        {
                            CardTypeId = item.VipCardTypeID,
                            UnitId = unititem.UnitID,
                            ProfitOwner = item.ProfitOwner,
                            IsApplyAllUnits = 0
                        };
                        lst.Add(model);

                    }
                }
            }
            //验证完完全全相同数据
            if (lst.Select(t => new { t.CardTypeId, t.UnitId, t.ProfitOwner }).GroupBy(m => new { m.CardTypeId, m.UnitId, m.ProfitOwner }).Distinct().ToList().Count() != lst.Count())
            {
                throw new APIException("门店和卡有重复,请重新添加门店和卡") { ErrorCode = 135 };
            }
            #endregion

            #region 梯度叠加集合 最多为5 个
            foreach (var item in CardBuyToProfitRuleInfoList)
            {
                if (item.ProfitTypeInfoList == null)
                {
                    continue;
                }
                if (item.IsDelete == 1)
                {
                    continue;
                }
                //分润拥有者
                if (String.IsNullOrEmpty(item.ProfitOwner))
                {
                    throw new APIException("请选择分润拥有者") { ErrorCode = 135 };
                }
                //验证续费充值的信息
                if (item.ProfitTypeInfoList.Where(m => m.IsDelete == 0).Count() > 5)
                {
                    throw new APIException("续费充值分润不能大于5个") { ErrorCode = 135 };
                }
            }
            #endregion

            CardBuyToProfitRuleInfoDeleteList = new List<CardBuyToProfitRuleInfoInfo>();
            CardBuyToProfitRuleInfoAddList = new List<CardBuyToProfitRuleInfoInfo>();
            CardBuyToProfitRuleInfoUpdateList = new List<CardBuyToProfitRuleInfoInfo>();
            CardBuyToProfitRuleInfoDeleteList = CardBuyToProfitRuleInfoList.Where(m => m.IsDelete == 1).ToList();
            CardBuyToProfitRuleInfoAddList = CardBuyToProfitRuleInfoList.Where(m => m.CardBuyToProfitRuleId == null).ToList();
            CardBuyToProfitRuleInfoUpdateList = CardBuyToProfitRuleInfoList.Where(m => m.IsDelete == 0 && m.CardBuyToProfitRuleId != null).ToList();
        }
    }

    public class CardBuyToProfitRuleInfoInfo
    {
        /// <summary>
        /// 会员卡分润规则Id
        /// </summary>
        public Guid? CardBuyToProfitRuleId { get; set; }
        /// <summary>
        /// 卡类型标识
        /// </summary>
        public int VipCardTypeID { get; set; }
        /// <summary>
        /// 卡类型名称 {用于做验证的显示}
        /// </summary>
        public string VipCardTypeName { get; set; }
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
        /// 是否应用全部门店 （1= 是、0= 否）
        /// </summary>
        public int IsApplyAllUnits { get; set; }

        /// <summary>
        ///购卡分润
        /// </summary>
        public decimal CardSalesProfitPct { get; set; }
        /// <summary>
        ///充值分润
        /// </summary>
        public decimal RechargeProfitPct { get; set; }

        /// <summary>
        ///（1=是、0=否）
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 客户标识 用于后台
        /// </summary>
        public string ClientId { get; set; }


        /// <summary>
        ///门店集合数组
        /// </summary>
        public List<RuleUnitInfo> RuleUnitInfoList { get; set; }

        /// <summary>
        ///叠加/梯度数组
        /// </summary>
        public List<ProfitTypeInfo> ProfitTypeInfoList { get; set; }

    }

    public class RuleUnitInfo
    {
        /// <summary>
        /// 映射主标识
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 是否删除	（0=未删除、1=已删除）
        /// </summary>
        public int IsDelete { get; set; }

    }

    public class ProfitTypeInfo
    {
        /// <summary>
        /// 分润规则Id
        /// </summary>
        public Guid? ReRechargeProfitRuleId { get; set; }
        /// <summary>
        /// 满/每满充值金额	
        /// </summary>
        public decimal LimitAmount { get; set; }
        /// <summary>
        /// 分充值分润方式	（Step=阶梯、Superposition 叠加）
        /// </summary>
        public string ProfitType { get; set; }
        /// <summary>
        /// 是否删除	（0=未删除、1=已删除）
        /// </summary>
        public int IsDelete { get; set; }
        /// <summary>
        /// 充值分润
        /// </summary>
        public decimal ProfitPct { get; set; }
    }

    /// <summary>
    /// 该类用于做静态的验证
    /// </summary>
    public class CardBuyToProfitRuleInfoUnitInfo
    {
        /// <summary>
        /// 卡类型编号
        /// </summary>
        public int CardTypeId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 分润拥有者
        /// </summary>
        public string ProfitOwner { get; set; }
        /// <summary>
        /// 是否应用于全部门店
        /// </summary>
        public int IsApplyAllUnits { get; set; }
    }
}
