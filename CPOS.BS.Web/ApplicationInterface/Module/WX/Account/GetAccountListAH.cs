using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.Account.Request;
using JIT.CPOS.DTO.Module.WeiXin.Account.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Account
{
    public class GetAccountListAH : BaseActionHandler<GetAccountListRP, GetAccountListRD>
    {
        protected override GetAccountListRD ProcessRequest(DTO.Base.APIRequest<GetAccountListRP> pRequest)
        {
            var rd = new GetAccountListRD();

            int? pageIndex = pRequest.Parameters.PageIndex;
            int? pageSize = pRequest.Parameters.PageSize;


            //string customerId = pRequest.CustomerID;

            //var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

            var bll = new WApplicationInterfaceBLL(CurrentUserInfo);

            var ds = bll.GetAccountList(CurrentUserInfo.ClientID, pageIndex ?? 0, pageSize ?? 15);

          

            if (ds.Tables[0].Rows.Count == 0)
            {
                rd.AccountList = null;
            }
            else
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new WXApplicationInfo
                {
                    ApplicationId = t["ApplicationId"].ToString(),
                    WeiXinName = t["WeiXinName"].ToString()
                });
                rd.AccountList = tmp.ToArray();
            }
            int totalCount = bll.GetTotalcount(CurrentUserInfo.ClientID);

            
            
            rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount*1.00 / (pageSize ?? 15) * 1.00)));

            return rd;
        }
    }
}