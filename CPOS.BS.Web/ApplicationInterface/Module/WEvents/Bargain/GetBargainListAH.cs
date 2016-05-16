using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class GetBargainListAH : BaseActionHandler<GetBargainListRP, GetBargainListRD>
    {
        protected override GetBargainListRD ProcessRequest(DTO.Base.APIRequest<GetBargainListRP> pRequest)
        {
            var rd = new GetBargainListRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new PanicbuyingEventBLL(loggingSessionInfo);

            var complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrWhiteSpace(para.EventName))
            {
                complexCondition.Add(new LikeCondition() { FieldName = "EventName ", Value = "%" + para.EventName + "%" });
            }
            if (para.EventStatus > 0)
            {
                #region MyRegion
                DateTime m_Date = DateTime.Now;
                //正常状态
                //complexCondition.Add(new DirectCondition("Status=0 "));
                switch (para.EventStatus)
                {
                    case 1:
                        //未开始
                        complexCondition.Add(new DirectCondition("BeginTime>'" + m_Date + "' "));
                        break;
                    case 2:
                        //运行中
                        complexCondition.Add(new DirectCondition("(BeginTime<='" + m_Date + "' and EndTime>='" + m_Date + "') "));
                        break;
                    case 3:
                        //结束
                        complexCondition.Add(new DirectCondition("EndTime<'" + m_Date + "' "));
                        break;
                    case 4:
                        //提前结束 
                        complexCondition.Add(new EqualsCondition() { FieldName = "EventStatus ", Value = 10 });
                        break;
                    default:
                        break;
                }
                #endregion
            }
            if (!string.IsNullOrWhiteSpace(para.BeginTime))
            {
                complexCondition.Add(new DirectCondition("BeginTime>='" + para.BeginTime + "' "));
            }
            if (!string.IsNullOrWhiteSpace(para.EndTime))
            {
                complexCondition.Add(new DirectCondition("EndTime<='" + para.EndTime + "' "));
            }
            //商户ID
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID ", Value = loggingSessionInfo.ClientID });
            complexCondition.Add(new EqualsCondition() { FieldName = "EventTypeId ", Value = 4 });
            
            //排序参数
            var lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "LastUpdateTime", Direction = OrderByDirections.Desc });
            //
            var tempList = bll.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.BargainList = tempList.Entities.Select(t => new BargainInfo()
            {
                EventId = t.EventId.ToString(),
                EventName = t.EventName,
                ItemCount = t.ItemQty.Value,
                PromotePersonCount = t.PromotePersonCount.Value,
                BargainPersonCount = t.BargainPersonCount.Value,
                BeginTime = t.BeginTime.ToString("yyyy-MM-dd HH:mm"),
                EndTime = t.EndTime.ToString("yyyy-MM-dd HH:mm"),
                Status = t.EventStatus.Value
            }).ToList();

            var Time = DateTime.Now;
            //处理状态
            foreach (var item in rd.BargainList)
            {
                if (item.Status == 10)
                    item.Status=3;
                if (item.Status == 20)
                {
                    if (Convert.ToDateTime(item.BeginTime) > Time)
                    {
                        item.Status = 1;//未开始
                        continue;
                    }
                    if (Convert.ToDateTime(item.BeginTime) <= Time && Convert.ToDateTime(item.EndTime) >= Time)
                    {
                        item.Status = 2;//进行中
                        continue;
                    }
                    if (Convert.ToDateTime(item.EndTime) < Time)
                    {
                        item.Status = 3;//未开始
                        continue;
                    }

                }
            }
            return rd;
        }
    }
}