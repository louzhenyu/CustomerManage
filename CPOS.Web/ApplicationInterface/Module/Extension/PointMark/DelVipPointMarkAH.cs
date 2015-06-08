using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.PointMark
{
    public class DelVipPointMarkAH : BaseActionHandler<EmptyRequestParameter,EmptyResponseData>
    {

        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var pointMarkBLL = new X_VipPointMarkBLL(CurrentUserInfo);
            var pointMarkDetailBLL = new X_VipPointMarkDetailBLL(CurrentUserInfo);
            DateTime dtNow = DateTime.Now;  //当前时间
            DateTime startWeek = dtNow.AddDays(1 - Convert.ToInt32(dtNow.DayOfWeek.ToString("d"))).Date;  //本周周一
            DateTime endWeek = startWeek.AddDays(6);  //本周周日

            var pointMarkDeail = pointMarkDetailBLL.GetPointMarkByWeek(CurrentUserInfo.UserID, startWeek, endWeek);
            if (pointMarkDeail != null)
            {
                if (pointMarkDeail.Count > 0)//答题获得积分才处理
                {
                    pointMarkDetailBLL.Delete(pointMarkDeail);
                    var pointMarkInfo = pointMarkBLL.QueryByEntity(new X_VipPointMarkEntity() { VipID = CurrentUserInfo.UserID }, null).FirstOrDefault();
                    if (pointMarkInfo != null)
                    {
                        pointMarkInfo.Count = pointMarkInfo.Count - pointMarkDeail.Count;
                        pointMarkInfo.TotalCount = pointMarkInfo.TotalCount - pointMarkDeail.Count;
                        pointMarkBLL.Update(pointMarkInfo);
                    }
                }
            } 
            return rd;
        }
    }
}