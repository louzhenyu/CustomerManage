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

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingActivity
{
    public class GetMyActivityListAH : BaseActionHandler<ActivityListRP, ActivityListRD>
    {

        protected override ActivityListRD ProcessRequest(APIRequest<ActivityListRP> pRequest)
        {
            var rd = new ActivityListRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventBLL bllCTWEvent = new T_CTW_LEventBLL(loggingSessionInfo);
            WQRCodeManagerBLL bllWQRCode = new WQRCodeManagerBLL(loggingSessionInfo);
            DataSet ds = bllCTWEvent.GetLeventInfo(para.Status, para.ActivityGroupCode, para.EventName);
            if (ds != null && ds.Tables.Count > 0)
            {
                var activity = new MyActivity();
                List<MyActivity> ActiviyList = new List<MyActivity>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {  
                    activity = DataTableToObject.ConvertToObject<MyActivity>(dr);
                    if (dr["DrawMethodCode"].ToString() == "HB" || dr["DrawMethodCode"].ToString() == "DZP" || dr["DrawMethodCode"].ToString() == "QN")
                    {
                        activity.EventInfo = DataTableToObject.ConvertToObject<EventInfo>(bllCTWEvent.GetEventInfoByLEventId(1, dr["LeventId"].ToString()).Tables[0].Rows[0]);
                    }
                    if (dr["DrawMethodCode"].ToString() == "QG" || dr["DrawMethodCode"].ToString() == "TG" || dr["DrawMethodCode"].ToString() == "RX")
                    {
                        activity.EventInfo = DataTableToObject.ConvertToObject<EventInfo>(bllCTWEvent.GetEventInfoByLEventId(2, dr["LeventId"].ToString()).Tables[0].Rows[0]);
                    }
                    //WQRCodeManagerEntity entityQRCode = bllWQRCode.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = dr["CTWEventId"].ToString() }, null).FirstOrDefault();
                    //activity.QRCodeImageUrlForOnline = entityQRCode == null ? "" : entityQRCode.ImageUrl;

                    //entityQRCode = bllWQRCode.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = dr["LeventId"].ToString() }, null).FirstOrDefault();
                    //activity.QRCodeImageUrlForUnit = entityQRCode == null ? "" : entityQRCode.ImageUrl;
                    ActiviyList.Add(activity);
                }
                rd.MyActivityList = ActiviyList;
            }
            DataSet dsEvent=new DataSet();
            dsEvent= bllCTWEvent.GetEventStatusCount(loggingSessionInfo.CurrentUser.customer_id);
            if(dsEvent!=null && dsEvent.Tables.Count>0)
            {
                rd.EventStatusCoountList = DataTableToObject.ConvertToList<EventStatusCoount>(dsEvent.Tables[0]);

            }
            return rd;

        }
    }
}