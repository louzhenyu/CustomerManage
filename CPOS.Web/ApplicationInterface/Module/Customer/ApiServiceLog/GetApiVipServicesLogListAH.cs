using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Request;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Customer.ApiServiceLog
{
    /// <summary>
    /// api客服记录列表
    /// </summary>
    public class GetApiVipServicesLogListAH : BaseActionHandler<GetVipServicesLogListRP, GetVipServicesLogListRD>
    {
        protected override GetVipServicesLogListRD ProcessRequest(DTO.Base.APIRequest<GetVipServicesLogListRP> pRequest)
        {
            var rd = new GetVipServicesLogListRD();
            var para = pRequest.Parameters;
            var vipServicesLogBLL = new VipServicesLogBLL(CurrentUserInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = para.VipID });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "ServicesTime", Direction = OrderByDirections.Desc });

            var tempList = vipServicesLogBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.VipServicesLogList = tempList.Entities.Select(t => new VipServicesLogInfo()
            {
                ServicesLogID = t.ServicesLogID.ToString(),
                ServicesTime = t.ServicesTime == null ? "" : t.ServicesTime.Value.ToString("yyyy-MM-dd hh:mm"),
                ServicesMode = t.ServicesMode,
                UnitName = t.UnitName,
                UserName = t.UserName,
                Content = t.Content
            }).ToArray();

            return rd;
        }
    }
}