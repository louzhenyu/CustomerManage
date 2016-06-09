using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    /// <summary>
    /// 集客来源列表
    /// </summary>
    public class GetAggSetoffForSourceListAH : BaseActionHandler<GetAggSetoffForSourceListRP, GetAggSetoffForSourceByConditionListRD>
    {
        protected override GetAggSetoffForSourceByConditionListRD ProcessRequest(DTO.Base.APIRequest<GetAggSetoffForSourceListRP> pRequest)
        {

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetAggSetoffForSourceByConditionListRD();
            var parameter = pRequest.Parameters;
            Agg_SetoffForSourceBLL bll = new Agg_SetoffForSourceBLL(loggingSessionInfo);

            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "h.CustomerID", Value = loggingSessionInfo.ClientID });

            if (!string.IsNullOrEmpty(parameter.SetoffRoleId))
                complexCondition.Add(new EqualsCondition() { FieldName = "h.SetoffRole", Value = parameter.SetoffRoleId });

            if (!string.IsNullOrEmpty(parameter.unitid))
                complexCondition.Add(new EqualsCondition() { FieldName = "h.UnitId", Value = parameter.unitid });

            if (!string.IsNullOrEmpty(parameter.starttime))
                complexCondition.Add(new DirectCondition("h.DateCode>= '" + parameter.starttime + "' "));

            if (!string.IsNullOrEmpty(parameter.endtime))
                complexCondition.Add(new DirectCondition("h.DateCode<= '" + parameter.endtime + "' "));

            if (String.IsNullOrEmpty(parameter.SortName)) { parameter.SortName = "OrderAmount"; }
            else { parameter.SortName = "" + parameter.SortName; }

            if (String.IsNullOrEmpty(parameter.SortOrder)) { parameter.SortOrder = "2"; }


            OrderBy order = null;

            if (parameter.SortOrder == "1")
            {
                order = new OrderBy() { FieldName = parameter.SortName, Direction = OrderByDirections.Asc };
            }
            else
            {
               order= new OrderBy() { FieldName = parameter.SortName, Direction = OrderByDirections.Desc };
            }

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(order);
            var tempList = bll.FindAllByPage(complexCondition.ToArray(), lstOrder.ToArray(), parameter.PageSize, parameter.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.aggsetoffforSourcelist = tempList.Entities.Select(m => new AggSetoffForSourceByConditionList()
            {
                ID = m.ID.ToString(),
                PushMessageCount = m.PushMessageCount.ToString(),
                SetoffRole = bll.GetSetoffRoleNameBySetoffRoleId(Convert.ToInt32(m.SetoffRole.ToString())),
                ShareCount = m.ShareCount.ToString(),
                UnitName = m.UnitName,
                UserName = m.UserName,
                SetoffCount = m.SetoffCount.ToString(),
                OrderAmount = m.OrderAmount.ToString()

            }).ToList();

            return rd;
        }

    }
}