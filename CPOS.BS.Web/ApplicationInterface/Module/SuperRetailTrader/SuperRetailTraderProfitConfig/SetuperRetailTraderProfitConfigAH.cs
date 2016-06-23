using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.SuperRetailTraderProfitConfig
{
    /// <summary>
    /// 设置多级分销信息 前端传递数组类型数据 {Id profit status level}
    /// 如果 Id==null 则代表添加信息 否则 代表修改信息
    /// </summary>
    public class SetuperRetailTraderProfitConfigAH : BaseActionHandler<SetuperRetailTraderProfitConfigRP, SetuperRetailTraderProfitConfigRD>
    {
        protected override SetuperRetailTraderProfitConfigRD ProcessRequest(DTO.Base.APIRequest<SetuperRetailTraderProfitConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            if (loggingSessionInfo == null)
            {
                throw new APIException("登录超时,请重试") { ErrorCode = 135 };
            }
            var rd = new SetuperRetailTraderProfitConfigRD();
            T_SuperRetailTraderProfitConfigBLL bll = new T_SuperRetailTraderProfitConfigBLL(loggingSessionInfo);
            int level = 0;
            var _models = parameter.ProfitConfigList.OrderBy(m => m.Level);

            #region 服务端验证 将未分配完的利润合并到商家利润里面去

            T_SuperRetailTraderConfigBLL SuperRetailTraderConfigService = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>()
                {
                    new EqualsCondition(){FieldName = "CustomerId", Value =loggingSessionInfo.ClientID},
                    new EqualsCondition(){ FieldName="IsDelete", Value=0},
                };

            List<OrderBy> orderCondition = new List<OrderBy>() { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } };
            var model = SuperRetailTraderConfigService.Query(lstWhereCondition.ToArray(), orderCondition.ToArray());

            if (model == null || model.Length == 0)
            {
                throw new APIException("请先设置商家佣金信息") { ErrorCode = 135 };
            }

            decimal? ProfitMoney = parameter.ProfitConfigList.Where(m => m.Level != 1 && m.Status == "10").Sum(m => m.Profit); //分销体系百分比

            if (ProfitMoney > model[0].DistributionProfit) //设置分润比例大于商家比例
            {
                throw new APIException("分润体系不能大于佣金体系,请重新设置") { ErrorCode = 135 };
            }

            if (ProfitMoney != model[0].DistributionProfit)  //新入一条规则信息 如果设置比例不等于商家比例
            {
                SuperRetailTraderConfigService.UpdateByCondition(loggingSessionInfo.ClientID);
                decimal? undistributedprofit = model[0].DistributionProfit - ProfitMoney;
                T_SuperRetailTraderConfigEntity entity = new T_SuperRetailTraderConfigEntity()
                {
                    AgreementName = model[0].AgreementName,
                    Agreement = model[0].Agreement,
                    Cost = model[0].Cost,
                    DistributionProfit = model[0].DistributionProfit - undistributedprofit,
                    CustomerProfit = model[0].CustomerProfit + undistributedprofit,
                    IsDelete = 0,
                    CustomerId = loggingSessionInfo.ClientID,
                    Id = Guid.NewGuid(),
                    RefId = model[0].Id,
                    SkuCommission = model[0].SkuCommission
                };
                SuperRetailTraderConfigService.Create(entity);
            }
            #endregion
            bll.UpdateByCustomerId(loggingSessionInfo.ClientID);

            foreach (var item in _models)
            {
                Guid id = Guid.NewGuid();

                if (item.Status.Trim() == "10")
                    level = level + 1;    //如果是启用状态那么 level就自增



                var oldmodel = bll.GetByID(item.SuperRetailTraderProfitConfigId);

                string refid = null;
                if (oldmodel != null)
                {
                    refid = oldmodel.SuperRetailTraderProfitConfigId.ToString();
                    oldmodel.Status = "90";  //将本条信息置为失效状态。
                    bll.Update(oldmodel);
                }


                T_SuperRetailTraderProfitConfigEntity _model = new T_SuperRetailTraderProfitConfigEntity()
                {
                    SuperRetailTraderProfitConfigId = id,
                    Status = item.Status,
                    Profit = item.Profit,
                    Level = level,
                    CustomerId = loggingSessionInfo.ClientID,
                    IsDelete = 0,
                    ProfitType = "Percent",
                    RefSuperRetailTraderProfitConfigId = refid,
                };
                bll.Create(_model); //添加一条新的信息。

                SetSuperRetailTraderProfitConfigInfo info = new SetSuperRetailTraderProfitConfigInfo()
                {
                    Level = item.Level == null ? 1 : int.Parse(item.Level + ""),
                    Profit = item.Profit,
                    Status = item.Status,
                    SuperRetailTraderProfitConfigId = id
                };
                rd.lst.Add(info); //返回值
            }
            return rd;
        }
    }
}