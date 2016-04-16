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
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Module.CreativityWarehouse.Log
{
    public class CTWEventShareLogAH : BaseActionHandler<CTWEventShareLogRP, CTWEventShareLogRD>
    {
        protected override CTWEventShareLogRD ProcessRequest(DTO.Base.APIRequest<CTWEventShareLogRP> pRequest)
        {
            var rd = new CTWEventShareLogRD();//返回值
            var para = pRequest.Parameters;
            if (!string.IsNullOrEmpty(para.CTWEventId) && !string.IsNullOrEmpty(para.Sender) && !string.IsNullOrEmpty(para.OpenId))
            {
                var bllLeventShareLog = new T_LEventsSharePersonLogBLL(this.CurrentUserInfo);
                var entityLeventShareLog = new T_LEventsSharePersonLogEntity();

                entityLeventShareLog.ShareVipID = para.Sender;
                entityLeventShareLog.ShareOpenID = para.OpenId;
                entityLeventShareLog.BeShareOpenID = para.BeSharedOpenId;
                entityLeventShareLog.BeShareVipID = para.BEsharedUserId;
                entityLeventShareLog.BusTypeCode = "CTW";
                entityLeventShareLog.ObjectId = para.CTWEventId;
                entityLeventShareLog.ShareURL = para.ShareURL;
                bllLeventShareLog.Create(entityLeventShareLog);

            }
            return rd;
        }
    }
}