using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingData
{
         
    public class GetMarketingGroupTypeAH : BaseActionHandler<EmptyRequestParameter, GetMarketingGroupTypeRD>
    {

        protected override GetMarketingGroupTypeRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetMarketingGroupTypeRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;//后台管理系统，从session里取数据

            SysMarketingGroupTypeBLL _SysMarketingGroupTypeBLL = new SysMarketingGroupTypeBLL(loggingSessionInfo);

            DataSet ds = _SysMarketingGroupTypeBLL.GetMarketingGroupType(loggingSessionInfo);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Columns.Count > 0)
                {
                    rd.MarketingGroupTypeList = DataTableToObject.ConvertToList<SysMarketingGroupTypeInfo>(ds.Tables[0]);
                }             
            }
            return rd;

        }
    }
}