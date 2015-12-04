using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Marketing.Request;
using JIT.CPOS.DTO.Module.Marketing.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class GetActivityListAH : BaseActionHandler<GetActivityListRP, GetActivityListRD>
    {
        protected override GetActivityListRD ProcessRequest(DTO.Base.APIRequest<GetActivityListRP> pRequest)
        {
            var rd = new GetActivityListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);

            //条件参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            if (!string.IsNullOrEmpty(para.ActivityType))
                complexCondition.Add(new EqualsCondition() { FieldName = "ActivityType", Value = para.ActivityType });
            if (!string.IsNullOrEmpty(para.ActivityName))
                complexCondition.Add(new LikeCondition() { FieldName = "ActivityName", Value = para.ActivityName + "%" });
            if (!string.IsNullOrEmpty(para.Status))
            {
                if (para.Status.Equals("1"))
                {
                    //暂停
                    complexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = para.Status });
                }
                else
                {
                    DateTime m_Date = DateTime.Now.Date;
                    //正常状态
                    complexCondition.Add(new DirectCondition("Status=0 "));
                    switch (para.Status)
                    {
                        case "2":
                            //未开始
                            complexCondition.Add(new DirectCondition("StartTime>'" + m_Date + "' "));
                            break;
                        case "3":
                            //运行中
                            complexCondition.Add(new DirectCondition("((StartTime<='" + m_Date + "' and EndTime>='" + m_Date + "') or (IsLongTime=0 and StartTime<='" + m_Date + "')) "));
                            break;
                        case "4":
                            //结束
                            complexCondition.Add(new DirectCondition("EndTime<'" + m_Date + "' "));
                            break;
                        default:
                            break;
                    }
                }

            }

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
            //获取数据集
            var tempList = ActivityBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;

            rd.ActivityList = tempList.Entities.Select(t => new ActivityInfo()
            {
                ActivityID = t.ActivityID.ToString(),
                ActivityName = t.ActivityName,
                TargetGroups = ActivityBLL.GetTargetGroups(t.ActivityID.ToString()),
                BeginEndData = (t.StartTime == null ? "" : t.StartTime.Value.ToString("yyyy-MM-dd至")) + (t.EndTime == null ? "" : t.EndTime.Value.ToString("yyyy-MM-dd")),
                Status = t.Status.Value,
                SendCouponQty = t.SendCouponQty == null ? 0 : t.SendCouponQty.Value,
                IsLongTime = t.IsLongTime == null ? 0 : t.IsLongTime.Value,
                StartTime = t.StartTime == null ? "" : t.StartTime.Value.ToString("yyyy-MM-dd"),
                EndTime = t.EndTime == null ? "" : t.StartTime.Value.ToString("yyyy-MM-dd"),
            }).ToList();

            foreach (var item in rd.ActivityList)
            {
                if (item.Status == 0)
                {
                    //当前活动不是长期
                    DateTime NowData = DateTime.Now.Date;
                    if (item.IsLongTime == 0)
                    {
                        //长期
                        if (!string.IsNullOrWhiteSpace(item.StartTime))
                        {
                            if (NowData < Convert.ToDateTime(item.StartTime).Date)
                                item.Status = 2;
                            else
                                item.Status = 3;
                        }
                    }
                    else
                    {
                        //不是长期
                        if (!string.IsNullOrWhiteSpace(item.StartTime) && !string.IsNullOrWhiteSpace(item.EndTime))
                        {
                            if (NowData < Convert.ToDateTime(item.StartTime).Date)
                                item.Status = 2;
                            else if (NowData <= Convert.ToDateTime(item.EndTime).Date)
                                item.Status = 3;
                            else
                                item.Status = 4;
                        }
                    }
                }

                if (item.IsLongTime == 0)
                {
                    int i = item.BeginEndData.IndexOf("至");
                    if (i > 0)
                    {
                        item.BeginEndData = item.BeginEndData.Substring(0, (i + 1)) + "长期";
                    }
                }

            }

            return rd;
        }

    }
}