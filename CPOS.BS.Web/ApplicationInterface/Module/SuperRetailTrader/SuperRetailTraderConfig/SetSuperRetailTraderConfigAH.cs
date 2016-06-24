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

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.SuperRetailTraderConfig
{
    /// <summary>
    /// 设置分润 佣金、协议信息 
    /// </summary>
    public class SetSuperRetailTraderConfigAH : BaseActionHandler<SetTSuperRetailTraderConfigRP, SetTSuperRetailTraderConfigRD>
    {
        protected override SetTSuperRetailTraderConfigRD ProcessRequest(DTO.Base.APIRequest<SetTSuperRetailTraderConfigRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new SetTSuperRetailTraderConfigRD();
            T_SuperRetailTraderConfigBLL bll = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);

            #region 修改协议
            if (parameter.IsFlag == true)
            {
                var oldmodel = bll.GetByID(parameter.Id);
                oldmodel.Agreement = parameter.Agreement;
                oldmodel.AgreementName = parameter.AgreementName;
                bll.Update(oldmodel);
                rd = GetRd(oldmodel);
                return rd;
            }
            #endregion

            #region 修改可分配利润
            if (String.IsNullOrEmpty(parameter.Id)) //头一次添加规则信息
            {
                T_SuperRetailTraderConfigEntity _model = GetModel(loggingSessionInfo.ClientID, parameter);
                _model.Id = Guid.NewGuid();
                //改动了数据 要重新插入信息  
                bll.UpdateByCondition(loggingSessionInfo.ClientID); //修改以前信息为失效状态
                bll.Create(_model);
                rd = GetRd(_model);

                //修改 佣金分润信息
                T_SuperRetailTraderProfitConfigBLL SuperRetailTraderProfitConfigService = new T_SuperRetailTraderProfitConfigBLL(loggingSessionInfo);
                SuperRetailTraderProfitConfigService.UpdateByCustomerId(loggingSessionInfo.ClientID);


            }
            else   //有可能是修改有可能是添加
            {
                //判断总和是否超过了100%
                var currentmodel = bll.GetByID(parameter.Id);
                if (currentmodel == null)
                {
                    throw new APIException("系统繁忙,请稍后重试！");
                }

                if (!(currentmodel.Cost == parameter.Cost && currentmodel.CustomerProfit == parameter.CustomerProfit && currentmodel.DistributionProfit == parameter.DistributionProfit && currentmodel.SkuCommission == parameter.SkuCommission))
                {

                    //改动了数据 要重新插入信息  
                    bll.UpdateByCondition(loggingSessionInfo.ClientID); //修改以前信息为失效状态
                    T_SuperRetailTraderConfigEntity _model = GetModel(loggingSessionInfo.ClientID, parameter);
                    _model.Id = Guid.NewGuid();
                    _model.AgreementName = currentmodel.AgreementName;
                    _model.Agreement = currentmodel.Agreement;
                    _model.RefId = currentmodel.Id; //相关编号
                    bll.Create(_model);
                    rd = GetRd(_model);
                }
                else
                {
                    rd = GetRd(currentmodel);
                }
            }
            #endregion

            #region 默认设置分润体系 1：2：1 的体系
            //将该商户下面所有的分润设置设为失效状态
            T_SuperRetailTraderProfitConfigBLL superretailtraderprofitconfigService = new T_SuperRetailTraderProfitConfigBLL(loggingSessionInfo);
            superretailtraderprofitconfigService.UpdateByCustomerId(loggingSessionInfo.ClientID);
            int[] precent = new int[] { 1, 2, 1 };
            int level = 2;
            double DistributionProfit = (Convert.ToDouble(parameter.DistributionProfit) / 4.0);
            decimal specialProfit = 0.0m;
            foreach (var item in precent)
            {
                decimal Profit = Math.Round(Convert.ToDecimal(DistributionProfit * item), 1);

                if (level == 4)
                {
                    Profit = Convert.ToDecimal(parameter.DistributionProfit - specialProfit);
                }
                else
                {
                    specialProfit += Profit;
                }
                superretailtraderprofitconfigService.Create(new T_SuperRetailTraderProfitConfigEntity()
                {
                    CustomerId = loggingSessionInfo.ClientID,
                    Level = level,
                    Profit = Profit,   //小数点有问题
                    IsDelete = 0,
                    Status = "10",
                    ProfitType = "Percent",
                    RefSuperRetailTraderProfitConfigId = null,
                    SuperRetailTraderProfitConfigId = Guid.NewGuid()

                });

                if (level == 2)
                {
                    superretailtraderprofitconfigService.Create(new T_SuperRetailTraderProfitConfigEntity()
                    {
                        CustomerId = loggingSessionInfo.ClientID,
                        Level = 1,
                        Profit = parameter.SkuCommission,
                        IsDelete = 0,
                        Status = "10",
                        ProfitType = "Percent",
                        RefSuperRetailTraderProfitConfigId = null,
                        SuperRetailTraderProfitConfigId = Guid.NewGuid()
                    });
                }
                level = level + 1;
            }
            #endregion
            return rd;
        }

        /// <summary>
        /// 转换返回值
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public SetTSuperRetailTraderConfigRD GetRd(T_SuperRetailTraderConfigEntity _model)
        {
            SetTSuperRetailTraderConfigRD rd = new SetTSuperRetailTraderConfigRD();
            rd.Cost = _model.Cost.ToString();
            rd.CustomerProfit = _model.CustomerProfit.ToString();
            rd.DistributionProfit = _model.DistributionProfit.ToString();
            rd.SkuCommission = _model.SkuCommission.ToString();
            rd.Agreement = _model.Agreement;
            rd.AgreementName = _model.AgreementName;
            rd.Id = _model.Id.ToString();
            return rd;
        }
        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T_SuperRetailTraderConfigEntity GetModel(string CustomerId, SetTSuperRetailTraderConfigRP parameter)
        {
            T_SuperRetailTraderConfigEntity _model = new T_SuperRetailTraderConfigEntity()
            {
                Agreement = parameter.Agreement,
                AgreementName = parameter.AgreementName,
                SkuCommission = parameter.SkuCommission,
                DistributionProfit = parameter.DistributionProfit,
                CustomerId = CustomerId,
                IsDelete = 0,
                Cost = parameter.Cost,
                CustomerProfit = parameter.CustomerProfit,
                RefId = parameter.RefId
            };
            return _model;
        }
    }
}