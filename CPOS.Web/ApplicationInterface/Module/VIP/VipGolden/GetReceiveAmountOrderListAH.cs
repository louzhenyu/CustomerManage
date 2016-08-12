using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 收款订单列表
    /// </summary>
    public class GetReceiveAmountOrderListAH : Base.BaseActionHandler<GetReceiveAmountOrderListRP,GetReceiveAmountOrderListRD>
    {
        protected override GetReceiveAmountOrderListRD ProcessRequest(APIRequest<GetReceiveAmountOrderListRP> pRequest)
        {
            GetReceiveAmountOrderListRP rp = pRequest.Parameters;
            GetReceiveAmountOrderListRD rd = new GetReceiveAmountOrderListRD();
            ReceiveAmountOrderBLL receiveAmountOrderBll = new ReceiveAmountOrderBLL(CurrentUserInfo);

            DateTime beginTime;
            DateTime endTime;
            //当天订单查询参数
            if (rp.Status == 0)
            {
                beginTime = DateTime.Today;
                endTime = DateTime.Now;
            }
            //一周内订单查询参数
            else
            {
                beginTime = DateTime.Today.AddDays(-7);
                endTime = DateTime.Today;
            }
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new MoreThanCondition() { FieldName = "PayDatetTime", Value = beginTime ,IncludeEquals = true});
            complexCondition.Add(new LessThanCondition() { FieldName = "PayDatetTime", Value = endTime ,IncludeEquals = true});
            complexCondition.Add(new EqualsCondition() { FieldName = "ServiceUserId",Value = pRequest.UserID });
            List<OrderBy> orderBy = new List<OrderBy> { new OrderBy() { FieldName = "PayDatetTime", Direction = OrderByDirections.Desc } };
            var receiveAmountOrder = receiveAmountOrderBll.PagedQuery(complexCondition.ToArray(),orderBy.ToArray(),rp.PageSize,rp.PageIndex + 1);
            //订单统计
            var OrderCount = receiveAmountOrderBll.Query(complexCondition.ToArray(), null);
            
           
            //var OrderCount = receiveAmountOrderBll.GetOrderCount(pRequest.UserID).FirstOrDefault();
            //订单表
            rd.OrderList = receiveAmountOrder.Entities.Select(n => new ReceiveAmountOrderDetail()
            {
                OrderId = n.OrderId.ToString(),
                VipId = n.VipId,
                OrderNo = n.OrderNo,
                PayStatus = n.PayStatus,
                PayDateTime = n.PayDatetTime.ToString(),
                TotalAmount = n.TotalAmount ?? 0,
                TransAmount = (n.TransAmount ?? 0) - (n.AmountAcctPay ?? 0)
            }).ToList();

            rd.OrderCount = receiveAmountOrder.RowCount; //订单数
            rd.SumTransAmount = OrderCount.Sum(n => (n.TransAmount - n.AmountAcctPay)) ?? 0; //订单实付总金额
            rd.SumTotalAmount = OrderCount.Sum(n => n.TotalAmount) ?? 0; //订单总金额
            rd.TotalCount = receiveAmountOrder.RowCount;
            rd.TotalPage = receiveAmountOrder.PageCount;
            return rd;

        }
    }
}