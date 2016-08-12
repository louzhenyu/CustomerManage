using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    /// <summary>
    ///获取会员卡分润规则 信息列表
    /// </summary>
    public class GetVipCardProfitRuleListAH : BaseActionHandler<GetVipCardProfitRuleListRP, GetVipCardProfitRuleListRD>
    {
        protected override GetVipCardProfitRuleListRD ProcessRequest(DTO.Base.APIRequest<GetVipCardProfitRuleListRP> pRequest)
        {
            var rd = new GetVipCardProfitRuleListRD();
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            VipCardProfitRuleBLL bll = new VipCardProfitRuleBLL(loggingSessionInfo);
            VipCardProfitRuleUnitMappingBLL UnitService = new VipCardProfitRuleUnitMappingBLL(loggingSessionInfo);
            //获取会员卡销售激励规则
            rd.VipCardProfitRuleList = bll.PagedQueryByEntity(new VipCardProfitRuleEntity() { CustomerID = loggingSessionInfo.ClientID }, null, 100, 1)
            .Entities.Select(m => new VipCardProfitRuleInfo()
            {
                ProfitOwner = m.ProfitOwner,
                CardBuyToProfitRuleId = m.CardBuyToProfitRuleId,
                VipCardTypeID = m.VipCardTypeID,
                RechargeProfitPct = m.RechargeProfitPct,
                IsApplyAllUnits = m.IsApplyAllUnits,
                FirstRechargeProfitPct = m.FirstRechargeProfitPct,
                FirstCardSalesProfitPct = m.FirstCardSalesProfitPct,
                CardSalesProfitPct = m.CardSalesProfitPct
            }
            ).ToList();

            // 按照商户获取 会员卡 续费充值规则
            var VipCardReRechargeProfitRuleDs = bll.GetVipCardReRechargeProfitRuleList(loggingSessionInfo.ClientID);
            var ruleinfolst = DataTableToObject.ConvertToList<VipCardReRechargeProfitRuleInfo>(VipCardReRechargeProfitRuleDs.Tables[0]);


            foreach (var item in rd.VipCardProfitRuleList)
            {
                //会员卡规则 续费充值 方式
                if (ruleinfolst!=null)
                {
                    item.VipCardReRechargeProfitRuleList = ruleinfolst.Where(m => m.CardBuyToProfitRuleId == item.CardBuyToProfitRuleId).ToList(); 
                }
                
                //获取门店信息 table[0]=上级门店 table[1]=门店信息
                DataSet ds = UnitService.GetAllUnitTypeList(loggingSessionInfo.ClientID, item.CardBuyToProfitRuleId);

                if (ds != null && ds.Tables.Count == 2)
                {
                    //获取会员卡上级 门店 数组
                    List<Organization> parentunitList = DataTableToObject.ConvertToList<Organization>(ds.Tables[0]);
                    //获取会员卡门店信息
                    List<Stores> StoresList = DataTableToObject.ConvertToList<Stores>(ds.Tables[1]);
                    //分组匹配门店信息
                    foreach (var parentunitInfo in parentunitList)
                    {
                        RuleUnitInfoRD unit = new RuleUnitInfoRD() { text = parentunitInfo.unit_name, id = parentunitInfo.src_unit_id };
                        unit.children = StoresList.Where(m => m.src_unit_id == parentunitInfo.src_unit_id).Select(m => new MappingUnitInfo() { text = m.unit_name, id = m.UnitId, MappingUnitId = m.Id }).ToList();
                        item.RuleUnitInfoList.Add(unit);
                    }
                }
            }
            return rd;
        }
    }
}