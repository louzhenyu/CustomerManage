using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    /// <summary>
    /// 添加/修改/删除 会员卡分润规则信息 使用事务处理验证关系，所有的验证都是 伪静态客户端验证的。全部在请求参数里面的Validate() 方法里面
    /// </summary>
    public class SetVipCardProfitRuleAH : BaseActionHandler<SetVipCardProfitRuleRP, SetVipCardProfitRuleRD>
    {
        protected override SetVipCardProfitRuleRD ProcessRequest(DTO.Base.APIRequest<SetVipCardProfitRuleRP> pRequest)
        {
            var rd = new SetVipCardProfitRuleRD();
            var parameter = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            VipCardProfitRuleBLL RuleService = new VipCardProfitRuleBLL(loggingSessionInfo);
            IDbTransaction tran = new TransactionHelper(loggingSessionInfo).CreateTransaction();

            #region 删除分润规则
            foreach (var item in parameter.CardBuyToProfitRuleInfoDeleteList)
            {
                RuleService.Delete(item.CardBuyToProfitRuleId, tran);
            }
            #endregion

            #region 新增分润规则
            foreach (var item in parameter.CardBuyToProfitRuleInfoAddList)
            {
                VipCardProfitRuleEntity model = RuleService.GetEntity(item);
                model.CustomerID = loggingSessionInfo.ClientID;
                RuleService.Create(model, tran);
                //处理门店和续费充值方式
                RuleService.SetUnitAndProfitRule(item, model.CardBuyToProfitRuleId, loggingSessionInfo, tran);
            }
            #endregion

            #region 修改分润规则  如果修改了 充值分润 就新入一条数据 FirstRechargeProfitPct
            foreach (var item in parameter.CardBuyToProfitRuleInfoUpdateList)
            {
                var entity = RuleService.GetByID(item.CardBuyToProfitRuleId);
                if (entity == null)
                {
                    tran.Rollback();
                    throw new APIException("分润规则为【" + item.CardBuyToProfitRuleId + "】 的信息已被删除！无法操作该数据") { ErrorCode = 135 };
                }

                //业务逻辑：如果修改了 FirstRechargeProfitPct 字段就重新入一条数据
                if (entity.FirstRechargeProfitPct == item.FirstRechargeProfitPct) //只需要修改字段信息
                {
                    VipCardProfitRuleEntity model = RuleService.GetEntity(item);
                    model.CardBuyToProfitRuleId = item.CardBuyToProfitRuleId;
                    model.CustomerID = loggingSessionInfo.ClientID;
                    model.IsDelete = item.IsDelete;
                    RuleService.Update(model, tran);
                }
                else //新增激励规则信息
                {
                    VipCardProfitRuleEntity model = RuleService.GetEntity(item);
                    model.CustomerID = loggingSessionInfo.ClientID;
                    RuleService.Delete(item.CardBuyToProfitRuleId, tran);
                    RuleService.Create(model, tran);
                    item.CardBuyToProfitRuleId = model.CardBuyToProfitRuleId;
                }
                //处理门店和续费充值方式
                RuleService.SetUnitAndProfitRule(item, item.CardBuyToProfitRuleId, loggingSessionInfo, tran);
            }
            #endregion
            tran.Commit();
            return rd;
        }
    }
}