using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Request;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    public class GetRefundOrderListAH : BaseActionHandler<GetRefundOrderListRP, GetRefundOrderListRD>
    {
        protected override GetRefundOrderListRD ProcessRequest(DTO.Base.APIRequest<GetRefundOrderListRP> pRequest)
        {
            var rd = new GetRefundOrderListRD();
            var para = pRequest.Parameters;

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var refundOrderBLL = new T_RefundOrderBLL(loggingSessionInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "a.CustomerID", Value = loggingSessionInfo.ClientID });

            if (!string.IsNullOrEmpty(para.RefundNo))
                complexCondition.Add(new LikeCondition() { FieldName = "a.RefundNo", Value = "%" + para.RefundNo + "%" });

            if (para.Status > 0)
                complexCondition.Add(new EqualsCondition() { FieldName = "a.Status", Value = para.Status });
            if (!string.IsNullOrEmpty(para.paymentcenterId))
                complexCondition.Add(new EqualsCondition() { FieldName = "t.paymentcenterId", Value = para.paymentcenterId });
            if (!string.IsNullOrEmpty(para.payId))
                complexCondition.Add(new EqualsCondition() { FieldName = "t.pay_Id", Value = para.payId });
            //门店过滤处理

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "a.CreateTime", Direction = OrderByDirections.Desc });

            var tempList = refundOrderBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.RefundOrderList = tempList.Entities.Select(t => new RefundOrderInfo()
            {
                RefundID = t.RefundID,
                RefundNo = t.RefundNo,
                VipName = t.VipName,
                ActualRefundAmount = t.ActualRefundAmount,
                Status = t.Status,
                CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd HH:mm"),
                paymentcenterId=t.PayOrderID,
                paymentName=t.PayTypeName
            }).ToArray();

            rd.TotalCount = tempList.RowCount;
            rd.TotalPageCount = tempList.PageCount;
            return rd;
        }
    }
}