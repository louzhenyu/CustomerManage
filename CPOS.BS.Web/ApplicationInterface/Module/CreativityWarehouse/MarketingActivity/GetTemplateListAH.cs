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
    public class GetTemplateListAH : BaseActionHandler<TemplateListRP, TemplateListRD>
    {

        protected override TemplateListRD ProcessRequest(APIRequest<TemplateListRP> pRequest)
        {
            var rd = new TemplateListRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            T_CTW_LEventTemplateBLL bllTemplate = new T_CTW_LEventTemplateBLL(loggingSessionInfo);

            DataSet ds = bllTemplate.GetTemplateList(para.ActivityGroupCode);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Columns.Count > 0)
                {
                    rd.BannerList = DataTableToObject.ConvertToList<BannerInfo>(ds.Tables[0]);
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    rd.TemplateList = DataTableToObject.ConvertToList<TemplateInfo>(ds.Tables[1]);
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    rd.PlanList = DataTableToObject.ConvertToList<PlanInfo>(ds.Tables[2]);
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    rd.PlanImageUrl = ds.Tables[3].Rows[0]["PlanImageUrl"].ToString();
                }
            }
            return rd;

        }
    }
}