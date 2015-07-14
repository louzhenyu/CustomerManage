using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.VIP.Order.Request;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.Order.Response;


namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Order
{
    /// <summary>
    /// 会员订单处理
    /// </summary>
    public class GetOrdersAH : BaseActionHandler<GetOrdersRP, GetOrdersRD>
    {
        protected override GetOrdersRD ProcessRequest(DTO.Base.APIRequest<GetOrdersRP> pRequest)
        {
            GetOrdersRD rd = new GetOrdersRD();
            var detailBll = new T_Inout_DetailBLL(CurrentUserInfo);
            var orderBll = new T_InoutBLL(CurrentUserInfo);

            string vipno = pRequest.UserID;
            string customer_id = pRequest.CustomerID;
            int pageindex = pRequest.Parameters.PageIndex;
            int PageSize = pRequest.Parameters.PageSize;
            string ChannelId = pRequest.ChannelId; // add by donal 2014-9-26 13:34:55
            string UserId = pRequest.UserID; // add by donal 2014-9-26 17:46:46
            //switch (pRequest.Parameters.GroupingType)
            //{
            //    case 1:
            //    //待付款 
            //        rd = orderBll.GetOrder(vipno, pageindex, PageSize, customer_id, pRequest.Parameters.GroupingType);
            //        break;
            //    case 2:
            //    //待收货，提货
            //        rd = orderBll.GetOrder(vipno, pageindex, PageSize, customer_id, pRequest.Parameters.GroupingType);
            //        break;
            //    case 3:
            //        //已完成
            //        rd = orderBll.GetOrder(vipno, pageindex, PageSize, customer_id, pRequest.Parameters.GroupingType);
            //        break;
            //    default:
            //        break;
            //}
            rd = orderBll.GetOrder(vipno, pageindex, PageSize, customer_id, pRequest.Parameters.GroupingType, ChannelId, UserId);
            if (!string.IsNullOrEmpty(pRequest.Parameters.UnitID))
            {
                rd.Orders = rd.Orders.Where(p => p.purchase_unit_id == pRequest.Parameters.UnitID).ToArray();
            }
            return rd;
        }
    }
}