/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/16 14:20:00
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    public class GetOrderListAH :BaseActionHandler<GetOrderListRP, GetOrderListRD>
    {
        protected override GetOrderListRD ProcessRequest(APIRequest<GetOrderListRP> pRequest)
        {
            GetOrderListRD rd = new GetOrderListRD();
            var orderBLL = new T_InoutBLL(this.CurrentUserInfo);
            bool pIsIncludeOrderDetails = pRequest.Parameters.IsIncludeOrderDetails;
            int[] pOrderStatuses = pRequest.Parameters.OrderStatuses;
            string pOrderID = pRequest.Parameters.OrderID;
            int pPageSize = pRequest.Parameters.PageSize;
            int pPageIndex = pRequest.Parameters.PageIndex;
            //返回订单列表
            rd = orderBLL.GetOrderList(pIsIncludeOrderDetails, pOrderStatuses, pOrderID,pPageSize, pPageIndex);
            return rd;
        }
    }
}