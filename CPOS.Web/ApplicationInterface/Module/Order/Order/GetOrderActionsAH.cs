/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/21 9:37:00
 * Description	:获取订单可执行操作
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
    public class GetOrderActionsAH:BaseActionHandler<GetOrderActionsRP,GetOrderActionsRD>
    {
        protected override GetOrderActionsRD ProcessRequest(APIRequest<GetOrderActionsRP> pRequest)
        {
            const int ERROR_NOEXISTS = 301;
            GetOrderActionsRD rd = new GetOrderActionsRD();
            TInOutStatusNodeBLL bll = new TInOutStatusNodeBLL(base.CurrentUserInfo);
            string pOrderID = pRequest.Parameters.OrderID;
            rd=bll.GetOrderActions(pOrderID);
            if (rd==null)
            {
                throw new APIException(string.Format("没有找到对应ID：{0}的订单", pOrderID)) {ErrorCode=ERROR_NOEXISTS };
            }
            return rd;
        }
    }
}