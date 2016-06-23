using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using System.Data;
using System.Configuration;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.App
{
    public class GetSuperRetailTraderUnderlingInfoAH : BaseActionHandler<GetSuperRetailTraderUnderlingInfoRP, GetSuperRetailTraderUnderlingInfoRD>
    {
        protected override GetSuperRetailTraderUnderlingInfoRD ProcessRequest(APIRequest<GetSuperRetailTraderUnderlingInfoRP> pRequest)
        {

            GetSuperRetailTraderUnderlingInfoRP para = pRequest.Parameters;
            GetSuperRetailTraderUnderlingInfoRD rd = new GetSuperRetailTraderUnderlingInfoRD();
            string strDomin = ConfigurationManager.AppSettings["original_url"];
            var bllUnderlingInfo = new T_SuperRetailTraderProfitDetailBLL(CurrentUserInfo);

            DataSet ds = bllUnderlingInfo.GetSuperRetailTraderUnderlingInfo(para.SuperRetailTraderId, CurrentUserInfo.ClientID, para.Page, para.Size, strDomin);
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                rd.SuperRetailTraderId = para.SuperRetailTraderId;
                rd.UnderlingInfo = DataTableToObject.ConvertToList<UnderlingInfo>(ds.Tables[0]);
            }
            return rd;
        }
    }
}