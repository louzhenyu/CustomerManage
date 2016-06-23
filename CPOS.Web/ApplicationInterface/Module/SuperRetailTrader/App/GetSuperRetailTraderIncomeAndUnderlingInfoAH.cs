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
using System;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.App
{
    public class GetSuperRetailTraderIncomeAndUnderlingInfoAH : BaseActionHandler<GetSuperRetailTraderIncomeAndUnderlingInfoRP, GetSuperRetailTraderIncomeAndUnderlingInfoRD>
    {
        protected override GetSuperRetailTraderIncomeAndUnderlingInfoRD ProcessRequest(APIRequest<GetSuperRetailTraderIncomeAndUnderlingInfoRP> pRequest)
        {
            GetSuperRetailTraderIncomeAndUnderlingInfoRP para = pRequest.Parameters;
            GetSuperRetailTraderIncomeAndUnderlingInfoRD rd = new GetSuperRetailTraderIncomeAndUnderlingInfoRD();

            var bll = new T_SuperRetailTraderProfitDetailBLL(CurrentUserInfo);

            DataSet ds = bll.GetSuperRetailTraderIncomeAndUnderlingInfo(para.SuperRetailTraderId, para.Date == null ? DateTime.Now.ToShortDateString() : para.Date);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                IncomeAndUnderlingInfo dayInfo = new IncomeAndUnderlingInfo();
                IncomeAndUnderlingInfo monthInfo = new IncomeAndUnderlingInfo();
                IncomeAndUnderlingInfo allInfo = new IncomeAndUnderlingInfo();
                //当日信息
                if (ds.Tables[0].Rows.Count > 0)
                    dayInfo.UnderlingCount = int.Parse(ds.Tables[0].Rows[0]["UnderlingCount"].ToString());
                if (ds.Tables[4].Rows.Count > 0)
                    dayInfo.EarningMoney = Convert.ToDecimal(ds.Tables[4].Rows[0]["EarningMoney"].ToString());
                rd.DayInfo = dayInfo;
                //月信息
                if (ds.Tables[1].Rows.Count > 0)
                    monthInfo.UnderlingCount = int.Parse(ds.Tables[1].Rows[0]["UnderlingCount"].ToString());
                if (ds.Tables[5].Rows.Count > 0)
                    monthInfo.EarningMoney = Convert.ToDecimal(ds.Tables[5].Rows[0]["EarningMoney"].ToString());
                rd.MonthInfo = monthInfo;
                //总信息
                if (ds.Tables[2].Rows.Count > 0)
                    allInfo.UnderlingCount = int.Parse(ds.Tables[2].Rows[0]["UnderlingCount"].ToString());
                if (ds.Tables[3].Rows.Count > 0)
                    allInfo.ContributeUnderling = int.Parse(ds.Tables[3].Rows[0]["ContributeUnderling"].ToString());
                if (ds.Tables[6].Rows.Count > 0)
                    allInfo.EarningMoney = Convert.ToDecimal(ds.Tables[6].Rows[0]["EarningMoney"].ToString());
                rd.AllInfo = allInfo;
            }
            return rd;
        }
    }
}