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
using JIT.CPOS.DTO.Module.Event.ContactEvent.Request;
using JIT.CPOS.DTO.Module.Event.ContactEvent.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.ContactEvent
{
    public class GetContactEventListAH : BaseActionHandler<GetContactEventListRP, GetContactEventListRD>
    {
        protected override GetContactEventListRD ProcessRequest(DTO.Base.APIRequest<GetContactEventListRP> pRequest)
        {
            var rd = new GetContactEventListRD();
            
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllContactEvent=new ContactEventBLL(loggingSessionInfo);

            DataSet ds = bllContactEvent.GetContactEventList(para.PageSize,para.PageIndex);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rd.ContactEventList = DataTableToObject.ConvertToList<JIT.CPOS.DTO.Module.Event.ContactEvent.Response.ContactEvent>(ds.Tables[0]);
                rd.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
                rd.TotalPage = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalPage"]);
            }
            return rd;
        }
    }
}