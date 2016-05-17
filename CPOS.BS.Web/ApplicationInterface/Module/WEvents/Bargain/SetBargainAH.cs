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
    public class SetBargainAH : BaseActionHandler<SetBargainRP, SetBargainRD>
    {
        protected override SetBargainRD ProcessRequest(DTO.Base.APIRequest<SetBargainRP> pRequest)
        {
            var rd = new SetBargainRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllPanicbuyingEvent = new PanicbuyingEventBLL(loggingSessionInfo);
            string REventId = string.Empty;


            if (string.IsNullOrEmpty(para.EventId))
            {
                var entityPanicbuyingEvent = new PanicbuyingEventEntity();
                entityPanicbuyingEvent.EventId = System.Guid.NewGuid();
                entityPanicbuyingEvent.EventName = para.EventName;

                #region 名称重复处理
                var Result = bllPanicbuyingEvent.QueryByEntity(new PanicbuyingEventEntity() { EventName = para.EventName, EventTypeId = 4 }, null).ToList();
                if (Result.Count() > 0)
                    throw new APIException("已有相同的砍价活动名称,请重新命名！");
                #endregion

                entityPanicbuyingEvent.BeginTime = para.BeginTime;
                entityPanicbuyingEvent.EndTime = para.EndTime;
                //
                entityPanicbuyingEvent.EventTypeId = 4;
                entityPanicbuyingEvent.EventStatus = 20;
                entityPanicbuyingEvent.PromotePersonCount = 0;
                entityPanicbuyingEvent.BargainPersonCount = 0;
                entityPanicbuyingEvent.ItemQty = 0;
                entityPanicbuyingEvent.CustomerID = loggingSessionInfo.ClientID;
                bllPanicbuyingEvent.Create(entityPanicbuyingEvent);
                //
                REventId = entityPanicbuyingEvent.EventId.ToString();
            }
            else
            {
                var UpdateData = bllPanicbuyingEvent.GetByID(para.EventId);
                if (UpdateData == null)
                    throw new APIException("未找到砍价活动！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                #region 名称重复处理
                if (!UpdateData.EventName.Trim().Equals(para.EventName.Trim()))
                {
                    var Result = bllPanicbuyingEvent.QueryByEntity(new PanicbuyingEventEntity() { EventName = para.EventName, EventTypeId = 4 }, null).ToList();
                    if (Result.Count() > 0)
                        throw new APIException("已有相同的砍价活动名称,请重新命名！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
                #endregion

                if (UpdateData.EndTime < DateTime.Now)
                    throw new APIException("砍价活动已经结束了！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                UpdateData.EventName = para.EventName;
                UpdateData.BeginTime = para.BeginTime;
                UpdateData.EndTime = para.EndTime;
                bllPanicbuyingEvent.Update(UpdateData);
                //
                REventId = para.EventId;

            }
            rd.EventId = REventId;

            return rd;
        }
    }
}