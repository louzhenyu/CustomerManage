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
    /// 获取多级分销信息 等级利润 信息{最新一条信息}
    /// </summary>
    public class GetSuperRetailTraderProfitConfigAH : BaseActionHandler<GetSuperRetailTraderProfitConfigRP, GetSuperRetailTraderProfitConfigRD>
    {
        protected override GetSuperRetailTraderProfitConfigRD ProcessRequest(DTO.Base.APIRequest<GetSuperRetailTraderProfitConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetSuperRetailTraderProfitConfigRD();
            T_SuperRetailTraderProfitConfigBLL bll = new T_SuperRetailTraderProfitConfigBLL(loggingSessionInfo);
            T_SuperRetailTraderConfigBLL SuperRetailTraderConfigService = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
            List<T_SuperRetailTraderProfitConfigEntity> _model = bll.QueryByEntity(new T_SuperRetailTraderProfitConfigEntity() { IsDelete = 0, CustomerId = loggingSessionInfo.ClientID, Status = "10" }, null).ToList(); //获取最新

            if (_model.Count == 2)
            {
                foreach (var item in _model)
                {
                    if (item.Level == 1)
                    {
                        continue;
                    }
                    item.Level = 4;
                }
                _model.Add(new T_SuperRetailTraderProfitConfigEntity() { Level = 2, Status = "90", SuperRetailTraderProfitConfigId = null, Profit = 0 });
                _model.Add(new T_SuperRetailTraderProfitConfigEntity() { Level = 3, Status = "90", SuperRetailTraderProfitConfigId = null, Profit = 0 });
            }
            else if (_model.Count == 3)
            {

                foreach (var item in _model)
                {
                    if (item.Level == 1)
                    {
                        continue;
                    }
                    item.Level = item.Level + 1; ;
                }
                _model.Add(new T_SuperRetailTraderProfitConfigEntity() { Level = 2, Status = "90", SuperRetailTraderProfitConfigId = null, Profit = 0 });
            }

            //获取level==1 的数据

            var first = SuperRetailTraderConfigService.QueryByEntity(new T_SuperRetailTraderConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null).FirstOrDefault();
            if (first == null)
            {
                throw new APIException("系统繁忙,请稍候重试。") { ErrorCode = 135 };
            }
            if (_model.Where(m=>m.Level==1).Count()==0)
            {
                _model.Add(new T_SuperRetailTraderProfitConfigEntity() { Level = 1, Status = "10", SuperRetailTraderProfitConfigId = null, Profit = first.SkuCommission });
            }
            else
            {
                _model.Where(m => m.Level == 1).FirstOrDefault().Profit = first.SkuCommission;
            }
            rd.ProfitConfigList = _model.Select(m => new GetSuperRetailTraderProfitConfigInfoRD() { Level = m.Level, Profit = m.Profit, Status = m.Status, SuperRetailTraderProfitConfigId = m.SuperRetailTraderProfitConfigId }).ToList();
            return rd;
        }
    }
}