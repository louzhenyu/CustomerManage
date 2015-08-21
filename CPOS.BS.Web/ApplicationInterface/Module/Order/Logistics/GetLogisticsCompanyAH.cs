using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Order.Logistics.Request;
using JIT.CPOS.DTO.Module.Order.Logistics.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.Logistics
{
    public class GetLogisticsCompanyAH : BaseActionHandler<GetLogisticsCompanyRP, GetLogisticsCompanyRD>
    {

        protected override GetLogisticsCompanyRD ProcessRequest(DTO.Base.APIRequest<GetLogisticsCompanyRP> pRequest)
        {

            var rd = new GetLogisticsCompanyRD();
            var para = pRequest.Parameters;

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var logisticsCompanyBLL = new T_LogisticsCompanyBLL(loggingSessionInfo);

            T_LogisticsCompanyEntity[] tempList = null;

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };

            if (!string.IsNullOrEmpty(para.LogisticsName))
                complexCondition.Add(new EqualsCondition() { FieldName = "lc.LogisticsName", Value = para.LogisticsName });

            if (para.IsSystem > 0)//读取系统预设信息
            {
                lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
                complexCondition.Add(new EqualsCondition() { FieldName = "IsSystem", Value = para.IsSystem });
                tempList = logisticsCompanyBLL.Query(complexCondition.ToArray(), lstOrder.ToArray());
            }
            else
            {
                lstOrder.Add(new OrderBy() { FieldName = "ls.CreateTime", Direction = OrderByDirections.Desc });
                complexCondition.Add(new EqualsCondition() { FieldName = "ls.CustomerID", Value = CurrentUserInfo.ClientID });
                complexCondition.Add(new EqualsCondition() { FieldName = "ls.isdelete", Value = 0 });
                tempList = logisticsCompanyBLL.QueryByCon(complexCondition.ToArray(), lstOrder.ToArray());
            }

            rd.LogisticsCompanyList = tempList.Select(t => new LogisticsCompanyInfo()
            {
                LogisticsID = t.LogisticsID,
                LogisticsName = t.LogisticsName,
                LogisticsShortName = t.LogisticsShortName
            }).ToList();

            return rd;
        }
    }
}