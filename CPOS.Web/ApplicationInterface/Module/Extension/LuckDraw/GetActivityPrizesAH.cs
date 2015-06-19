using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Extension.LuckDraw.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Extension.LuckDraw
{
    public class GetActivityPrizesAH : BaseActionHandler<EmptyRequestParameter, GetActivityPrizesRD>
    {

        protected override GetActivityPrizesRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetActivityPrizesRD();
            var para = pRequest.Parameters;
            var activityBLL = new X_ActivityBLL(CurrentUserInfo);
            var activityInfo = activityBLL.QueryByEntity(new X_ActivityEntity() { CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
            if (activityInfo != null)
            {
                rd.ActivityID = activityInfo.ActivityID;
                rd.ActivityName = activityInfo.ActivityName;
                rd.BeginTime = activityInfo.BeginTime;
                rd.EndTime = activityInfo.EndTime;
                rd.JoinLimit = activityInfo.JoinLimit;
                rd.LowestPointLimit = activityInfo.LowestPointLimit;
            }
            return rd;
        }
    }
}