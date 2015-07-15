using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Request;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    public class GetSalesReturnDetailAH : BaseActionHandler<GetSalesReturnDetailRP, GetSalesReturnDetailRD>
    {
        protected override GetSalesReturnDetailRD ProcessRequest(DTO.Base.APIRequest<GetSalesReturnDetailRP> pRequest)
        {
            var rd = new GetSalesReturnDetailRD();
            var para = pRequest.Parameters;
            var salesReturnBLL = new T_SalesReturnBLL(CurrentUserInfo);
            var historyBLL = new T_SalesReturnHistoryBLL(CurrentUserInfo);

            var salesReturnEntity = salesReturnBLL.GetByID(para.SalesReturnID);

            if (salesReturnEntity != null)
            {
                rd.SalesReturnID = salesReturnEntity.SalesReturnID.ToString();
                rd.SalesReturnNo = salesReturnEntity.SalesReturnNo;
                rd.Status = salesReturnEntity.Status;
                rd.DeliveryType = salesReturnEntity.DeliveryType;
                rd.Reason = salesReturnEntity.Reason;

                rd.UnitName = salesReturnEntity.UnitName;
                rd.Address = salesReturnEntity.Address;
                rd.UnitTel=salesReturnEntity.UnitTel;
                rd.ServicesType = salesReturnEntity.ServicesType;

                var history = historyBLL.QueryByEntity(new T_SalesReturnHistoryEntity() { SalesReturnID = salesReturnEntity.SalesReturnID }, new[] { new OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc } });
                rd.HistoryList = history.Select(t => new HistoryInfo() { HistoryID = t.HistoryID.ToString(), OperationDesc = t.OperationDesc, HisRemark = t.HisRemark, OperatorName = t.OperatorName, CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd HH:mm") }).ToArray();
            }
            return rd;
        }
    }
}