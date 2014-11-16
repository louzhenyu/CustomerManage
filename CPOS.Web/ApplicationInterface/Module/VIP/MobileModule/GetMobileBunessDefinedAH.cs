using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.MobileModule
{
    public class GetMobileBunessDefinedAH : BaseActionHandler<GetMobileBusinessDefinedRP, GetMobileBusinessDefinedRD>
    {
        protected override GetMobileBusinessDefinedRD ProcessRequest(DTO.Base.APIRequest<GetMobileBusinessDefinedRP> pRequest)
        {
            var pageResult = new MobileBussinessDefinedBLL(CurrentUserInfo).PagedQueryByEntity(
                new MobileBussinessDefinedEntity
                {
                    MobileModuleID =  Guid.Parse(pRequest.Parameters.MobileModuleID)
                },
                new[]
                {
                    new OrderBy {FieldName = "ListOrder", Direction = OrderByDirections.Asc}
                }, 
                pRequest.Parameters.Page, 
                pRequest.Parameters.PageSize);

            var result = new GetMobileBusinessDefinedRD();
            result.TotalRow = pageResult.RowCount;
            result.Items = pageResult.Entities.Select(it => new MobileBunessDefinedSubInfo
            {
                MobileBunessDefinedID = it.MobileBussinessDefinedID.ToString(),
                ColumnDesc = it.ColumnDesc,
                ColumnName = it.ColumnName,
                ControlType = it.ControlType.HasValue ? it.ControlType.Value : 0,
                CorrelationValue = it.CorrelationValue,
                ListOrder = it.EditOrder.HasValue ? it.EditOrder.Value : 0
            }).ToArray();
            return result;
        }
    }
}