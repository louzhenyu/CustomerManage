using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.Customer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Customer
{
    public class SetSalesSettingAH : BaseActionHandler<SetSalesSettingRP, EmptyResponseData>
    {


        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetSalesSettingRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);

            var setoffEventBLL = new SetoffEventBLL(loggingSessionInfo);    //集客行动业务层
            var iincentiveRuleBLL = new IincentiveRuleBLL(loggingSessionInfo); //集客奖励业务层

            ResponseData res = new ResponseData();
            List<CustomerBasicSettingEntity> list = new List<CustomerBasicSettingEntity>();

            //获取集客行动信息
            var setoffEventList = setoffEventBLL.QueryByEntity(new SetoffEventEntity() { CustomerId = loggingSessionInfo.ClientID, Status = "10" }, null);
            SetoffEventEntity setoffEventUser = setoffEventList.Where(t => t.SetoffType == 2).FirstOrDefault(); //员工集客活动设置
            SetoffEventEntity setoffEventVip = setoffEventList.Where(t => t.SetoffType == 1).FirstOrDefault();  //会员集客活动设置

            var iincentiveRuleList = iincentiveRuleBLL.QueryByEntity(new IincentiveRuleEntity() { CustomerId = loggingSessionInfo.ClientID, Status = "10" }, null);
            IincentiveRuleEntity iincentiveRuleUser = null; //员工奖励
            IincentiveRuleEntity iincentiveRuleVip = null;  //会员奖励

            //获取员工奖励规则
            if (setoffEventUser != null)
                iincentiveRuleUser = iincentiveRuleList.Where(t => t.SetoffEventID==setoffEventUser.SetoffEventID && t.SetoffType == 2 ).FirstOrDefault();
            //获取会员奖励规则
            if (setoffEventVip != null)
                iincentiveRuleVip = iincentiveRuleList.Where(t => t.SetoffEventID == setoffEventVip.SetoffEventID && t.SetoffType == 1).FirstOrDefault();

            if (!string.IsNullOrEmpty(para.SocialSalesType.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
              {
                  SettingCode = "SocialSalesType",
                  SettingValue = para.SocialSalesType.ToString()
              });
            }
            if (!string.IsNullOrEmpty(para.EnableEmployeeSales.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "EnableEmployeeSales",
                    SettingValue = para.EnableEmployeeSales.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.EDistributionPricePer.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "EDistributionPricePer",
                    SettingValue = para.EDistributionPricePer.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.EOrderCommissionPer.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "EOrderCommissionPer",
                    SettingValue = para.EOrderCommissionPer.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.EnableVipSales.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "EnableVipSales",
                    SettingValue = para.EnableVipSales.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.VDistributionPricePer.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "VDistributionPricePer",
                    SettingValue = para.VDistributionPricePer.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.VOrderCommissionPer.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "VOrderCommissionPer",
                    SettingValue = para.VOrderCommissionPer.ToString()
                });
            }
            if (!string.IsNullOrEmpty(para.GetVipUserOrderPer.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "GetVipUserOrderPer",
                    SettingValue = para.GetVipUserOrderPer.ToString()
                });
                //同步到集客行动奖励设置(集客销售提成） 
                if (iincentiveRuleUser != null)
                {
                    iincentiveRuleUser.SetoffOrderPer =Convert.ToDecimal(para.GetVipUserOrderPer);
                    iincentiveRuleBLL.Update(iincentiveRuleUser);
                }

            }
            if (!string.IsNullOrEmpty(para.InvitePartnersPoints.ToString()))
            {
                list.Add(new CustomerBasicSettingEntity()
               {
                   SettingCode = "InvitePartnersPoints",
                   SettingValue = para.InvitePartnersPoints.ToString()

               });
                //同步到集客行动奖励设置（分享获得积分）   
                if (iincentiveRuleVip != null)
                {
                    iincentiveRuleVip.SetoffRegPrize = Convert.ToDecimal(para.InvitePartnersPoints);
                    iincentiveRuleBLL.Update(iincentiveRuleVip);
                }
            }
            int i = customerBasicSettingBLL.SaveCustomerBasicInfo(loggingSessionInfo.ClientID, list);

            return rd;
        }
    }
}