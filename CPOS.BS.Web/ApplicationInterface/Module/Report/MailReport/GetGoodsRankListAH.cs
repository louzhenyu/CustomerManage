using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Report.MailReport.Request;
using JIT.CPOS.DTO.Module.Report.MailReport.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.MailReport
{
    public class GetGoodsRankListAH : BaseActionHandler<GetGoodsRankListRP, GetGoodsRankListRD>
    {
        protected override GetGoodsRankListRD ProcessRequest(DTO.Base.APIRequest<GetGoodsRankListRP> pRequest)
        {
            R_WxO2OPanel_ItemTopTenBLL bll = new R_WxO2OPanel_ItemTopTenBLL(CurrentUserInfo);
            var dbEntity = bll.GetListByDate();
            return new GetGoodsRankListRD(dbEntity) { };
        }
    }
}