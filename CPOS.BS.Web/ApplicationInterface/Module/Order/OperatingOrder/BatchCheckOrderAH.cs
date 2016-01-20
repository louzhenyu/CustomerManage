using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.OperatingOrder
{
    public class BatchCheckOrderAH : BaseActionHandler<BatchCheckOrderRP, OrderOperationRD>
    {
        protected override OrderOperationRD ProcessRequest(DTO.Base.APIRequest<BatchCheckOrderRP> pRequest)
        {
            var rd = new OrderOperationRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var T_Iount = new T_InoutBLL(loggingSessionInfo);
            var OrderIdList = new List<string>();
            foreach (var item in para.OrderList)
            {
                if (item.Status.Equals("100"))
                {
                    OrderIdList.Add(item.OrderID);
                }
            }
            int num = T_Iount.BatchCheckOrder(OrderIdList, para.Remark, loggingSessionInfo);
            rd.Message = "操作成功" + num + "条！";
            return rd;
        }
    }
}