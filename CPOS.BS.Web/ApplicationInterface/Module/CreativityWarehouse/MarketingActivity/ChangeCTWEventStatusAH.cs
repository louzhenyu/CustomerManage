using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingActivity
{
    public class ChangeCTWEventStatusAH: BaseActionHandler<ChangeCTWEventStatusRP, ChangeCTWEventStatusRD>
    {

        protected override ChangeCTWEventStatusRD ProcessRequest(APIRequest<ChangeCTWEventStatusRP> pRequest)
        {
            var rd = new ChangeCTWEventStatusRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventBLL bllCTWEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            if(!string.IsNullOrEmpty(para.CTWEventId) && para.Status>0)
            {
                var entityCTWEvent = bllCTWEvent.GetByID(para.CTWEventId);
                
                if(entityCTWEvent!=null)
                {
                    if(entityCTWEvent.EndDate<DateTime.Now.Date)
                    {
                        throw new APIException(999, "活动已经过期请调整时间后再发布！");
                    }
                    entityCTWEvent.Status = para.Status;
                    bllCTWEvent.Update(entityCTWEvent);
                    
                    rd.CTWEventId = para.CTWEventId;
                    rd.Status = para.Status;
                }
            }
            return rd;
        }
    }
}