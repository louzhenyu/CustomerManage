using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Event.Lottery.Request;
using JIT.CPOS.DTO.Module.Event.Lottery.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Event.Lottery
{
    public class GetPrizeWinnerListAH : BaseActionHandler<GetPrizeLocationListRP, PrizeWinnerListRD>
    {
        protected override PrizeWinnerListRD ProcessRequest(DTO.Base.APIRequest<GetPrizeLocationListRP> pRequest)
        {
            var rd = new PrizeWinnerListRD();//返回值

            LPrizeWinnerBLL bllWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
            DataTable dtWinner=bllWinner.GetTop10PizewWinnerListByEventId(pRequest.Parameters.EventID).Tables[0];
            rd.WinnerList = DataTableToObject.ConvertToList<WinnerInfo>(dtWinner);
            rd.TotalCount = dtWinner.Rows.Count;
            return rd;

        }
    }
}