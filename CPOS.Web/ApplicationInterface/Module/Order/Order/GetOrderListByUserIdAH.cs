using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    public class GetOrderListByUserIdAH : BaseActionHandler<GetOrderListByUserIdRP, GetOrderListByUserIdRD>
    {
        protected override GetOrderListByUserIdRD ProcessRequest(DTO.Base.APIRequest<GetOrderListByUserIdRP> pRequest)
        {
            GetOrderListByUserIdRD rd = new GetOrderListByUserIdRD();

            string orderId = pRequest.Parameters.OrderID;
            string userId = pRequest.Parameters.UserID;
            string customerId = this.CurrentUserInfo.ClientID;
            int pageSize = pRequest.Parameters.PageSize;
            int pageIndex = pRequest.Parameters.PageIndex;

            string orderNo = pRequest.Parameters.OrderNo;

            var orderStatusInfo = pRequest.Parameters.OrderStatus;

            string orderStatusList = "";
            if (orderStatusInfo != null)
            {
                foreach (var item in orderStatusInfo)
                {
                    orderStatusList += item.Status + ",";
                }
            }
            orderStatusList = orderStatusList.Trim(',');


            T_InoutBLL bll = new T_InoutBLL(this.CurrentUserInfo);
            var ds = bll.GetOrdersList(orderId, userId,orderStatusList,orderNo, customerId, pageSize, pageIndex);

            var tmp = ds.Tables[0].AsEnumerable().Select(t => new OrdersInfo()
            {
                OrderID = Convert.ToString(t["order_id"]),
                OrderNO = Convert.ToString(t["order_no"]),
                DeliveryTypeID = Convert.ToString(t["DeliveryTypeId"]),
                OrderDate = Convert.ToString(t["OrderDate"]),
                VipName = Convert.ToString(t["vipName"]),
                OrderStatusDesc = Convert.ToString(t["OrderStatusDesc"]),
                OrderStatus = Convert.ToInt32(Utils.GetIntVal(FormatParamValue(t["OrderStatus"].ToString()))),
                TotalAmount = Convert.ToDecimal(t["TotalAmount"]),
                TotalQty = Convert.ToDecimal(t["TotalQty"])
                
            });

            rd.OrderList = tmp.ToArray();            

            return rd;
        }

        protected string FormatParamValue(string value)
        {
            if (value == null || value == "null" || value == "undefined")
            {
                return string.Empty;
            }
            return value.Trim();
        }
    }
}