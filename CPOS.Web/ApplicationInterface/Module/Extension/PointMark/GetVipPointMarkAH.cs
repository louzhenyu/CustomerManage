using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Extension.PointMark.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.PointMark
{
    public class GetVipPointMarkAH : BaseActionHandler<EmptyRequestParameter, GetVipPointMarkRD>
    {

        protected override GetVipPointMarkRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetVipPointMarkRD();
            var para = pRequest.Parameters;
            var pointMarkBLL = new X_VipPointMarkBLL(CurrentUserInfo);
            var pointMarkDetailBLL = new X_VipPointMarkDetailBLL(CurrentUserInfo);
            var pointMarkInfo = pointMarkBLL.QueryByEntity(new X_VipPointMarkEntity() { VipID = CurrentUserInfo.UserID }, null).FirstOrDefault();
            int count = 0;      //当前点数
            int totalCount = 0; //累计点数
            int weekCount = 0;  //本周获得点数

            DateTime dtNow = DateTime.Now;  //当前时间
            DateTime startWeek = dtNow.AddDays(1 - Convert.ToInt32(dtNow.DayOfWeek.ToString("d"))).Date;  //本周周一
            DateTime endWeek = startWeek.AddDays(7);  //本周周日


            string pointMarkId = string.Empty;
            if (pointMarkInfo != null)
            {
                pointMarkId = pointMarkInfo.PointMarkID.ToString();
                count = pointMarkInfo.Count.Value;
                totalCount = pointMarkInfo.TotalCount.Value;
                var pointMarkDetail= pointMarkDetailBLL.GetPointMarkByWeek(CurrentUserInfo.UserID, startWeek, endWeek);
                if (pointMarkDetail != null)
                    weekCount = pointMarkDetail.Count.Value;
            }
            rd.PointMarkID = pointMarkId;
            rd.Count = count;
            rd.TotalCount = totalCount;
            rd.WeekCount=weekCount;
            return rd;
        }
    }
}