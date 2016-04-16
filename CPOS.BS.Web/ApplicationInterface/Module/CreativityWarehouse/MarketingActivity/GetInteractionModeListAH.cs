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
    public class GetInteractionModeListAH : BaseActionHandler<InteractionModeRP, InteractionModeRD>
    {

        protected override InteractionModeRD ProcessRequest(APIRequest<InteractionModeRP> pRequest)
        {
            var rd = new InteractionModeRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventInteractionBLL bllInteraction = new T_CTW_LEventInteractionBLL(loggingSessionInfo);

            var entityTheme = bllInteraction.QueryByEntity(new T_CTW_LEventInteractionEntity() { ThemeId = new Guid(para.ThemeId), IsDelete = 0 }, null).ToList();
            rd.InteractionModeList = entityTheme;
            return rd;

        }
    }
}