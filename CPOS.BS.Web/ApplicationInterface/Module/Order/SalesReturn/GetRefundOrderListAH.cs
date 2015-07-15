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
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });

            if (!string.IsNullOrEmpty(para.RefundNo))
                complexCondition.Add(new LikeCondition() { FieldName = "RefundNo", Value = "%" + para.RefundNo + "%" });

            if (para.Status > 0)
                complexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = para.Status });
            
            //门店过滤处理

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            var tempList = refundOrderBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.RefundOrderList = tempList.Entities.Select(t => new RefundOrderInfo()
            {
                RefundID = t.RefundID,
                RefundNo = t.RefundNo,
                VipName = t.VipName,
                ActualRefundAmount = t.ActualRefundAmount,
                Status = t.Status,
                CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd hh:mm")
            }).ToArray();

            rd.TotalCount = tempList.RowCount;
            rd.TotalPageCount = tempList.PageCount;
            return rd;
        }
    }
}