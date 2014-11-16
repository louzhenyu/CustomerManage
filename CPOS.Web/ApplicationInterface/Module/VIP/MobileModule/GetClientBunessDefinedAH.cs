using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.Reflection;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.MobileModule
{
    public class GetClientBunessDefinedAH : BaseActionHandler<GetClientBusinessDefinedRP, GetClientBusinessDefinedRD>
    {
        protected override GetClientBusinessDefinedRD ProcessRequest(DTO.Base.APIRequest<GetClientBusinessDefinedRP> pRequest)
        {
            pRequest.Parameters.Validate();
            int totalRow;
            var dt = new MobileModuleBLL(CurrentUserInfo).GetClientBunessDefined(CurrentUserInfo.ClientID,
                pRequest.Parameters.Page, pRequest.Parameters.PageSize, out totalRow);
            if (dt == null || dt.Rows.Count ==0)
            {
                return new GetClientBusinessDefinedRD
                {
                    TotalRow = 0 ,
                    BasicItems = new ClientBunessDefinedSubInfo[0],
                    ExtendItems = new ClientBunessDefinedSubInfo[0],
                   SeniorItems = new ClientBunessDefinedSubInfo[0]
                };
            }

            var result = new GetClientBusinessDefinedRD();
            result.TotalRow = totalRow;
            var entity = DataLoader.LoadFrom<ClientBunessDefinedSubInfo>(dt);
            result.BasicItems = entity.Where(it => it.AttributeType == 1).ToArray();
            result.ExtendItems = entity.Where(it => it.AttributeType == 2).ToArray();
            result.SeniorItems = entity.Where(it => it.AttributeType == 3).ToArray();
            return result;
        }
    }
}