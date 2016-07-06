using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.OperatingOrder
{
    public class BatchInvalidOrderAH : BaseActionHandler<BatchInvalidOrderRP, OrderOperationRD>
    {
        protected override OrderOperationRD ProcessRequest(DTO.Base.APIRequest<BatchInvalidOrderRP> pRequest)
        {
            var rd = new OrderOperationRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var T_Iount = new T_InoutBLL(loggingSessionInfo);

            //审核不通过:900
            //未发货：500
            //待提货：510
            //备货中：410
            //未审核：100
            var OrderList = new List<InoutInfo>();
            foreach (var item in para.OrderList)
            {
                if (item.Status.Equals("900") || item.Status.Equals("500") || item.Status.Equals("510") || item.Status.Equals("410") || item.Status.Equals("100"))
                {
                    var Entity = new InoutInfo()
                    {
                        order_id = item.OrderID,
                        OldStatusDesc = item.StatusDesc,
                        status = item.Status
                    };
                    OrderList.Add(Entity);
                }
            }

            int num = T_Iount.BatchInvalidOrder(OrderList, para.Remark, loggingSessionInfo);
            rd.Message = "操作成功" + num + "条！";
            return rd;
        }
    }
}