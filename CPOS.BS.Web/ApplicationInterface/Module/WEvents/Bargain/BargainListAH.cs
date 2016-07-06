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
    public class BargainListAH : BaseActionHandler<BargainListRP, BargainListRD>
    {
        protected override BargainListRD ProcessRequest(DTO.Base.APIRequest<BargainListRP> pRequest)
        {
            var rd = new BargainListRD();

            var para = pRequest.Parameters; 
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            PanicbuyingEventBLL bll = new PanicbuyingEventBLL(loggingSessionInfo);
            DataSet ds = bll.GetKJEventList(para.PageIndex, para.PageSize, para.EventName, para.EventStatus, para.BeginTime, para.EndTime);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.BargainList = DataTableToObject.ConvertToList<BargainEvent>(ds.Tables[0]).ToArray();//直接根据所需要的字段反序列化
                rd.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString());
                rd.PageCount = Convert.ToInt32(ds.Tables[1].Rows[0]["PageCount"].ToString());
            }
            
            return rd;
        }
    }
}