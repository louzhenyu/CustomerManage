using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
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
    /// 集客 内容报表 接口
    /// </summary>
    public class GetAggSetoffForToolListAH : BaseActionHandler<GetAggSetoffForToolListRP, GetAggSetoffForToolByConditionListRD>
    {
        protected override GetAggSetoffForToolByConditionListRD ProcessRequest(APIRequest<GetAggSetoffForToolListRP> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetAggSetoffForToolByConditionListRD();
            var parameter = pRequest.Parameters;
            pRequest.Parameters.PageSize = pRequest.Parameters.PageSize == 0 ? 10 : pRequest.Parameters.PageSize;
            Agg_SetoffForToolBLL bll = new Agg_SetoffForToolBLL(loggingSessionInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "a.CustomerID", Value = loggingSessionInfo.ClientID });  //默认条件

            if (!string.IsNullOrEmpty(parameter.SetoffRoleId))
                complexCondition.Add(new EqualsCondition() { FieldName = "a.SetoffToolType", Value = parameter.SetoffRoleId });

            if (!string.IsNullOrEmpty(parameter.starttime))
                complexCondition.Add(new DirectCondition("a.DateCode>= '" + parameter.starttime + "' "));

            if (!string.IsNullOrEmpty(parameter.endtime))
                complexCondition.Add(new DirectCondition("a.DateCode<= '" + parameter.endtime + "' "));


            if (String.IsNullOrEmpty(parameter.SortName)) { parameter.SortName = "OrderAmount"; }
            else { parameter.SortName = parameter.SortName; }
            if (String.IsNullOrEmpty(parameter.SortOrder)) { parameter.SortOrder = "0"; }
            OrderByDirections directionenum = (OrderByDirections)Enum.Parse(typeof(OrderByDirections), parameter.SortOrder);
            List<OrderBy> lstOrder = new List<OrderBy> { new OrderBy() { FieldName = parameter.SortName, Direction = directionenum } };

            var tempList = bll.FindAllByPage(complexCondition.ToArray(), lstOrder.ToArray(), parameter.PageSize, parameter.PageIndex);

            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;

            rd.aggsetoffforToollist = tempList.Entities.Select(m => new AggSetoffForToolByConditionList()
            {
                ID = m.ID.ToString(),
                ObjectName = m.ObjectName,
                SetoffRole = bll.GetSetoffToolTypeNameBySetoffToolType(m.SetoffToolType),
                ShareCount = m.ShareCount.ToString(),
                OrderAmount = m.OrderAmount.ToString(),
                SetoffCount = m.SetoffCount.ToString()
            }).ToList();

            return rd;
        }
    }
}