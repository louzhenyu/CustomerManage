﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.Holiday.Request;
using JIT.CPOS.DTO.Module.Basic.Holiday.Response;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.BasicSet
{
    public class GetHolidayListAH : BaseActionHandler<GetHolidayListRP, GetHolidayListRD>
    {
        protected override GetHolidayListRD ProcessRequest(DTO.Base.APIRequest<GetHolidayListRP> pRequest)
        {
            var rd = new GetHolidayListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBLL = new HolidayBLL(loggingSessionInfo);
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
            //调用会员卡管理列表查询
            var tempList = VipCardBLL.PagedQuery(null, lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.HolidayList = tempList.Entities.Select(t => new HolidayInfo()
            {
                HolidayId = t.HolidayId.ToString(),
                HolidayName = t.HolidayName,
                BeginDate = t.BeginDate.Value == null ? "" : t.BeginDate.Value.ToString("yyyy-MM-dd"),
                EndDate = t.EndDate.Value == null ? "" : t.EndDate.Value.ToString("yyyy-MM-dd"),
                Desciption = t.Desciption == null ? "" : t.Desciption,
                CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd")
            }).ToList();
            return rd;
        }
    } 
}