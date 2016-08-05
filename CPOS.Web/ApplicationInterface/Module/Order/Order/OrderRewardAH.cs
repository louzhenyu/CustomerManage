using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.CodeGeneration.Order;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    /// <summary>
    /// 订单奖励
    /// </summary>
    public class OrderRewardAH : BaseActionHandler<GetOrderDetailRP, OrderRewardRD>
    {
        protected override OrderRewardRD ProcessRequest(DTO.Base.APIRequest<GetOrderDetailRP> pRequest)
        {
            OrderRewardRD orderRewardRD = new OrderRewardRD() { IsSuccess = false };

            var loggingSessionInfo = CustomerBLL.Instance.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            OrderRewardBLL orderRewardBLL = new OrderRewardBLL(loggingSessionInfo);
            orderRewardBLL.RewardOrder(pRequest.Parameters.OrderId);
            orderRewardRD.IsSuccess = true;
            return orderRewardRD;
        }
    }
}