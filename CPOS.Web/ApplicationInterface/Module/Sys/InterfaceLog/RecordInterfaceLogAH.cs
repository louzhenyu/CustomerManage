using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Sys.InterfaceLog.Request;
using JIT.CPOS.DTO.Module.Sys.InterfaceLog.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;


namespace JIT.CPOS.Web.ApplicationInterface.Module.Sys.InterfaceLog
{
    public class RecordInterfaceLogAH:BaseActionHandler<RecordInterfaceLogRP,RecordInterfaceLogRD>
    {
        protected override RecordInterfaceLogRD ProcessRequest(APIRequest<RecordInterfaceLogRP> pRequest)
        {
            RecordInterfaceLogRD rd = new RecordInterfaceLogRD();
            InterfaceWebLogBLL InterfaceBll = new InterfaceWebLogBLL(this.CurrentUserInfo);
            
            var entity = new InterfaceWebLogEntity() 
            {
                LogId=Guid.NewGuid(),
                InterfaceName=pRequest.Parameters.PageName,
                WebPage=pRequest.Parameters.Action,
                CustomerId=CurrentUserInfo.ClientID,
                UserId=CurrentUserInfo.UserID,
                OpenId=CurrentUserInfo.ClientID,
                RequestIP=HttpContext.Current.GetClientIP()
            };
            InterfaceBll.Create(entity);//插入数据
            return rd;
        }
    }
}