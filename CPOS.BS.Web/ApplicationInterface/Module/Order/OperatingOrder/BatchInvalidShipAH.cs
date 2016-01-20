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
    public class BatchInvalidShipAH : BaseActionHandler<BatchInvalidShipRP, OrderOperationRD>
    {
        protected override OrderOperationRD ProcessRequest(DTO.Base.APIRequest<BatchInvalidShipRP> pRequest)
        {
            var rd = new OrderOperationRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var T_Iount = new T_InoutBLL(loggingSessionInfo);

            var OrderList = new List<InoutInfo>();
            foreach (var item in para.OrderList)
            {
                if (item.Status.Equals("500"))
                {
                    var Entity = new InoutInfo()
                    {
                        order_id = item.OrderID,
                        Field2 = item.DeliverOrder,
                        carrier_id = item.DeliverCompanyID,
                        Field9 = item.Field9
                    };
                    OrderList.Add(Entity);
                }
            }

            int num = T_Iount.BatchInvalidShip(OrderList, para.Remark, loggingSessionInfo);
            rd.Message = "操作成功" + num + "条！";
            return rd;
        }
    }
}