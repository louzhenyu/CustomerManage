using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using JIT.CPOS.BS.Entity;
using System.Data;
using JIT.CPOS.BS.DataAccess.Base;
using System.Data.SqlClient;
using System.Configuration;
using JIT.Utility.DataAccess;
using JIT.CPOS.Common;
namespace JIT.CPOS.BS.BLL.RedisOperationBLL.Order
{
    public class RedisCalculateVipConsumeForUpgrade
    {

        public void SetVipConsumeForUpgradeList(LoggingSessionInfo loggingSessionInfo, T_InoutEntity orderInfo)
        {
            var response = RedisOpenAPI.Instance.CCAllOrder().SetOrder(new CC_Order
            {
                CustomerID = loggingSessionInfo.ClientID,
                OrderId = orderInfo.order_id,
                OrderInfo = orderInfo.JsonSerialize()

            });
            if (response.Code == ResponseCode.Fail)
            {
				//直接计算
				CalculateVipConsumeForUpgrade(loggingSessionInfo, orderInfo);

			}
        }
		/// <summary>
		/// 销售升级job
		/// </summary>
        public void CalculateVipConsumeForUpgradeJob()
        {
            var numCount = 50;
            var customerIDs = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in customerIDs)
            {
                string connString = customer.Value;
                var count = RedisOpenAPI.Instance.CCAllOrder().GetOrderLength(new CC_Order
                {
                    CustomerID = customer.Key
                });
                if (count.Code != ResponseCode.Success)
                {
                    BaseService.WriteLog("从redis获取充值订单数据失败");
                    continue;
                }
                if (count.Result <= 0)
                {
                    continue;
                }
                if (count.Result < numCount)
                {
                    numCount = Convert.ToInt32(count.Result);
                }
                var loggingSessionInfo = CustomerBLL.Instance.GetBSLoggingSession(customer.Key, "RedisSystem");
                VipCardUpgradeRuleBLL bllVipCardUpgradeRule = new VipCardUpgradeRuleBLL(loggingSessionInfo);
                VipCardVipMappingBLL vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
                T_InoutBLL bllInout = new T_InoutBLL(loggingSessionInfo);
                for (var i = 0; i < numCount; i++)
                {
                    var response = RedisOpenAPI.Instance.CCAllOrder().GetOrder(new CC_Order
                    {
                        CustomerID = customer.Key
                    });
                    if (response.Code == ResponseCode.Success)
                    {
                        var orderInfo = response.Result.OrderInfo.JsonDeserialize<T_InoutEntity>();
                        var entityVipCardUpgradeRule = bllVipCardUpgradeRule.QueryByEntity(new VipCardUpgradeRuleEntity() { IsBuyUpgrade=1, CustomerID = loggingSessionInfo.ClientID },null);
                        if(entityVipCardUpgradeRule!=null)
                        {
							decimal vipSumAmount = bllInout.GetVipSumAmount(orderInfo.vip_no);
							bool isUpdate = false;
							foreach (var rule in entityVipCardUpgradeRule.OrderByDescending(a => a.VipCardTypeID))
                            {
                                if (rule.BuyAmount > 0)
                                {
									if (vipSumAmount >= rule.BuyAmount)
                                    {
										if (!isUpdate) {
											vipCardVipMappingBLL.BindVirtualItem(orderInfo.vip_no, orderInfo.VipCardCode, "", (int)rule.VipCardTypeID, "TotalSales", 2, orderInfo.order_id);
											isUpdate = true;
										}
                                    }
                                }
                                if (rule.OnceBuyAmount > 0)
                                {
                                    if (orderInfo.actual_amount >= rule.OnceBuyAmount)
                                    {
										if (!isUpdate) {

											vipCardVipMappingBLL.BindVirtualItem(orderInfo.vip_no, orderInfo.VipCardCode, "", (int)rule.VipCardTypeID, "OneSales", 2, orderInfo.order_id);
											isUpdate = true;
										}
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
		/// <summary>
		/// 在队列失效的情况下，直接计算
		/// </summary>
		/// <param name="loggingSessionInfo"></param>
		/// <param name="orderInfo"></param>
		public void CalculateVipConsumeForUpgrade(LoggingSessionInfo loggingSessionInfo, T_InoutEntity orderInfo) {
			VipCardUpgradeRuleBLL bllVipCardUpgradeRule = new VipCardUpgradeRuleBLL(loggingSessionInfo);
			VipCardVipMappingBLL vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
			T_InoutBLL bllInout = new T_InoutBLL(loggingSessionInfo);
			var entityVipCardUpgradeRule = bllVipCardUpgradeRule.QueryByEntity(new VipCardUpgradeRuleEntity() { IsBuyUpgrade = 1, CustomerID = loggingSessionInfo.ClientID }, null);
			if (entityVipCardUpgradeRule != null) {
				decimal vipSumAmount = bllInout.GetVipSumAmount(orderInfo.vip_no);
				bool isUpdate = false;
				foreach (var rule in entityVipCardUpgradeRule.OrderByDescending(a => a.VipCardTypeID)) {
					if (rule.BuyAmount > 0) {
						if (vipSumAmount >= rule.BuyAmount) {
							if (!isUpdate) {
								vipCardVipMappingBLL.BindVirtualItem(orderInfo.vip_no, orderInfo.VipCardCode, "", (int)rule.VipCardTypeID, "TotalSales", 2, orderInfo.order_id);
								isUpdate = true;
							}
						}
					}
					if (rule.OnceBuyAmount > 0) {
						if (orderInfo.actual_amount >= rule.OnceBuyAmount) {
							if (!isUpdate) {

								vipCardVipMappingBLL.BindVirtualItem(orderInfo.vip_no, orderInfo.VipCardCode, "", (int)rule.VipCardTypeID, "OneSales", 2, orderInfo.order_id);
								isUpdate = true;
							}
						}
					}
				}
			}
		}
    }
}
