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

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.App
{
    public class GetSuperRetailTraderOrderInfoAH : BaseActionHandler<GetSuperRetailTraderOrderInfoRP, GetSuperRetailTraderOrderInfoRD>
    {
        protected override GetSuperRetailTraderOrderInfoRD ProcessRequest(APIRequest<GetSuperRetailTraderOrderInfoRP> pRequest)
        {

            GetSuperRetailTraderOrderInfoRP para = pRequest.Parameters;
            GetSuperRetailTraderOrderInfoRD rd = new GetSuperRetailTraderOrderInfoRD();

            var bllOrderInfo = new T_SuperRetailTraderProfitDetailBLL(CurrentUserInfo);

            DataSet ds = bllOrderInfo.GetSuperRetailTraderOrderInfo(para.SuperRetailTraderId, CurrentUserInfo.ClientID,para.Page,para.Size);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.SuperRetailTraderId = para.SuperRetailTraderId;
                rd.OrderInfo = DataTableToObject.ConvertToList<SuperRetailTraderOrderInfo>(ds.Tables[0]);
                
            }
            return rd;
        }
    }
}